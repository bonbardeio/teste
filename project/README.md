# CoinQuest Starter (App + Jogo Web)

## 1. Arquitetura do projeto
- **Frontend/UI**: HTML/CSS/JS para interface, HUD, loja, menu e canvas do jogo.
- **Game engine**: loop de jogo com mapa simples, movimentação, NPC com IA básica e sistema de save em buffer.
- **Backend/API**: Node.js + Express para autenticação, loja, inventário e persistência de save.
- **Database**: SQLite com tabela `users` para login, moedas, inventário e progresso.
- **Multiplayer básico**: Socket.IO para presença de jogadores no lobby em tempo real.

## 2. Estrutura de pastas

```text
project/
├── src/            # regras de negócio compartilhadas
├── assets/         # sprites, áudio e recursos visuais (placeholder)
├── scripts/        # automações (dev/test/build)
├── ui/             # frontend web
├── game/           # lógica do jogo
├── backend/        # servidor e APIs
├── database/       # banco SQLite e init
├── build/          # saída de build
└── tests/          # testes automatizados
```

## 3. Código completo
Todos os arquivos essenciais já foram gerados neste template:
- Login e registro (`/api/register`, `/api/login`)
- Loja virtual com compra de itens (`/api/store`, `/api/store/buy`)
- Moedas e inventário persistidos no SQLite
- Multiplayer de presença com Socket.IO
- Canvas com HUD, mapa e NPC
- Sistema de save/load (`/api/save`, `/api/load`)
- Scripts e teste automatizado inicial

## 4. Instruções de execução
```bash
cd project
npm install
npm run dev
```
Abra: `http://localhost:3000`

Rodar testes:
```bash
npm test
```

Gerar build web:
```bash
npm run build
```

## 5. Melhorias futuras
1. Sistema de combate por turnos ou ação em tempo real.
2. Matchmaking multiplayer com salas e sincronização de posição.
3. IA de NPC com máquina de estados e objetivos dinâmicos.
4. Loja com catálogo no banco e promoções.
5. Sistema robusto de mapa com tiles e colisão.
6. Cloud save e autenticação OAuth2.
