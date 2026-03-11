import express from 'express';
import cors from 'cors';
import bcrypt from 'bcryptjs';
import http from 'http';
import { Server } from 'socket.io';
import { initDb } from '../database/init.js';
import { authMiddleware, signUserToken } from './auth.js';
import { applyPurchase } from '../src/economy.js';

const app = express();
const server = http.createServer(app);
const io = new Server(server, { cors: { origin: '*' } });

const PORT = process.env.PORT || 3000;
const STORE_ITEMS = [
  { id: 'potion', name: 'Poção', price: 20 },
  { id: 'sword', name: 'Espada de Treino', price: 50 },
  { id: 'shield', name: 'Escudo de Madeira', price: 40 }
];

app.use(cors());
app.use(express.json());
app.use(express.static('ui/public'));

const db = await initDb();

app.post('/api/register', async (req, res) => {
  const { username, password } = req.body;
  if (!username || !password) return res.status(400).json({ error: 'Usuário e senha são obrigatórios.' });

  try {
    const hash = await bcrypt.hash(password, 10);
    await db.run('INSERT INTO users (username, password_hash) VALUES (?, ?)', [username, hash]);
    res.status(201).json({ message: 'Usuário criado com sucesso.' });
  } catch (error) {
    res.status(409).json({ error: 'Usuário já existe.', detail: error.message });
  }
});

app.post('/api/login', async (req, res) => {
  const { username, password } = req.body;
  const user = await db.get('SELECT * FROM users WHERE username = ?', [username]);
  if (!user) return res.status(401).json({ error: 'Credenciais inválidas.' });

  const ok = await bcrypt.compare(password, user.password_hash);
  if (!ok) return res.status(401).json({ error: 'Credenciais inválidas.' });

  const token = signUserToken(user);
  res.json({ token, username: user.username, coins: user.coins, inventory: JSON.parse(user.inventory) });
});

app.get('/api/store', (_req, res) => res.json(STORE_ITEMS));

app.post('/api/store/buy', authMiddleware, async (req, res) => {
  const { itemId } = req.body;
  const item = STORE_ITEMS.find((entry) => entry.id === itemId);
  if (!item) return res.status(404).json({ error: 'Item não encontrado.' });

  const user = await db.get('SELECT * FROM users WHERE id = ?', [req.user.id]);
  try {
    const newBalance = applyPurchase(user.coins, item.price);
    const inventory = JSON.parse(user.inventory);
    inventory.push(item.id);

    await db.run('UPDATE users SET coins = ?, inventory = ? WHERE id = ?', [newBalance, JSON.stringify(inventory), req.user.id]);
    res.json({ coins: newBalance, inventory });
  } catch (error) {
    res.status(400).json({ error: error.message });
  }
});

app.post('/api/save', authMiddleware, async (req, res) => {
  const saveData = req.body;
  await db.run('UPDATE users SET save_data = ? WHERE id = ?', [JSON.stringify(saveData), req.user.id]);
  res.json({ message: 'Progresso salvo.' });
});

app.get('/api/load', authMiddleware, async (req, res) => {
  const user = await db.get('SELECT save_data, coins, inventory FROM users WHERE id = ?', [req.user.id]);
  res.json({
    save: JSON.parse(user.save_data || '{}'),
    coins: user.coins,
    inventory: JSON.parse(user.inventory || '[]')
  });
});

const onlinePlayers = new Map();
io.on('connection', (socket) => {
  socket.on('player:join', (payload) => {
    onlinePlayers.set(socket.id, payload);
    io.emit('lobby:update', Array.from(onlinePlayers.values()));
  });

  socket.on('disconnect', () => {
    onlinePlayers.delete(socket.id);
    io.emit('lobby:update', Array.from(onlinePlayers.values()));
  });
});

server.listen(PORT, () => {
  console.log(`Servidor online em http://localhost:${PORT}`);
});
