export function startGame(canvas, onTickSave) {
  const ctx = canvas.getContext('2d');
  const player = { x: 40, y: 120, speed: 3 };
  const npc = { x: 280, y: 140, dir: 1 };
  const keys = new Set();

  addEventListener('keydown', (event) => keys.add(event.key));
  addEventListener('keyup', (event) => keys.delete(event.key));

  function update() {
    if (keys.has('ArrowUp')) player.y -= player.speed;
    if (keys.has('ArrowDown')) player.y += player.speed;
    if (keys.has('ArrowLeft')) player.x -= player.speed;
    if (keys.has('ArrowRight')) player.x += player.speed;

    npc.x += npc.dir * 1.5;
    if (npc.x > 460 || npc.x < 20) npc.dir *= -1;

    player.x = Math.max(10, Math.min(490, player.x));
    player.y = Math.max(10, Math.min(250, player.y));

    onTickSave?.({ player, npc, updatedAt: Date.now() });
  }

  function draw() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    ctx.fillStyle = '#065f46';
    ctx.fillRect(0, 220, canvas.width, 40);

    ctx.fillStyle = '#1d4ed8';
    ctx.fillRect(player.x - 8, player.y - 8, 16, 16);

    ctx.fillStyle = '#b91c1c';
    ctx.fillRect(npc.x - 8, npc.y - 8, 16, 16);

    ctx.fillStyle = '#111';
    ctx.fillText('Player', player.x - 12, player.y - 12);
    ctx.fillText('NPC IA', npc.x - 12, npc.y - 12);
  }

  setInterval(() => {
    update();
    draw();
  }, 16);
}
