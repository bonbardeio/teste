import { startGame } from '../../game/engine.js';

const apiBase = '';
const authMessage = document.querySelector('#authMessage');
const coinsEl = document.querySelector('#coins');
const inventoryEl = document.querySelector('#inventory');
const storeList = document.querySelector('#storeList');
const playersList = document.querySelector('#playersList');

const state = { token: '', username: '', saveBuffer: {} };
const socket = io();

socket.on('lobby:update', (players) => {
  playersList.innerHTML = '';
  players.forEach((player) => {
    const li = document.createElement('li');
    li.textContent = `${player.username} entrou no lobby`;
    playersList.appendChild(li);
  });
});

async function api(path, options = {}) {
  const headers = { 'Content-Type': 'application/json', ...(options.headers || {}) };
  if (state.token) headers.Authorization = `Bearer ${state.token}`;

  const response = await fetch(`${apiBase}${path}`, { ...options, headers });
  return response.json();
}

function refreshHud(coins, inventory) {
  coinsEl.textContent = coins;
  inventoryEl.textContent = inventory.length ? inventory.join(', ') : '(vazio)';
}

async function loadStore() {
  const items = await api('/api/store');
  storeList.innerHTML = '';

  items.forEach((item) => {
    const li = document.createElement('li');
    li.innerHTML = `<span>${item.name} - ${item.price} moedas</span>`;
    const button = document.createElement('button');
    button.textContent = 'Comprar';
    button.onclick = async () => {
      const result = await api('/api/store/buy', {
        method: 'POST',
        body: JSON.stringify({ itemId: item.id })
      });
      if (result.error) return (authMessage.textContent = result.error);
      refreshHud(result.coins, result.inventory);
    };
    li.appendChild(button);
    storeList.appendChild(li);
  });
}

document.querySelector('#registerBtn').onclick = async () => {
  const username = document.querySelector('#username').value;
  const password = document.querySelector('#password').value;
  const result = await api('/api/register', { method: 'POST', body: JSON.stringify({ username, password }) });
  authMessage.textContent = result.message || result.error;
};

document.querySelector('#loginBtn').onclick = async () => {
  const username = document.querySelector('#username').value;
  const password = document.querySelector('#password').value;

  const result = await api('/api/login', { method: 'POST', body: JSON.stringify({ username, password }) });
  if (result.error) return (authMessage.textContent = result.error);

  state.token = result.token;
  state.username = result.username;
  refreshHud(result.coins, result.inventory);
  authMessage.textContent = `Bem-vindo, ${result.username}`;
  socket.emit('player:join', { username: state.username });
  await loadStore();
};

document.querySelector('#saveBtn').onclick = async () => {
  const result = await api('/api/save', { method: 'POST', body: JSON.stringify(state.saveBuffer) });
  authMessage.textContent = result.message || result.error;
};

document.querySelector('#loadBtn').onclick = async () => {
  const result = await api('/api/load');
  if (result.error) return (authMessage.textContent = result.error);
  refreshHud(result.coins, result.inventory);
  authMessage.textContent = 'Save carregado com sucesso.';
};

startGame(document.querySelector('#gameCanvas'), (saveState) => {
  state.saveBuffer = saveState;
});
