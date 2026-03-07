# MobileFPS Android (Unity)

Projeto base completo de um FPS para Android com foco em desempenho mobile, controles touch e expansão rápida.

## Funcionalidades implementadas

- Jogador em primeira pessoa com:
  - joystick virtual para movimentação
  - look touch por arrasto
  - botões de atirar, mirar, recarregar e pular
- Sistema de vida e dano (jogador e inimigos).
- HUD com vida, munição e minimapa.
- Sistema de armas:
  - Pistola (munição de reserva infinita, cadência lenta)
  - Rifle automático
  - Escopeta de curto alcance (pellets)
- Recarga, dispersão e recuo básico.
- IA de inimigos (soldados) com estados:
  - patrulhar
  - perseguir
  - atirar em alcance
- Modos de jogo:
  - Sobrevivência (ondas infinitas)
  - Missão (eliminação + captura de área)
- Progressão com XP, níveis, desbloqueio de armas e upgrades.
- Menu inicial com: Jogar, Arsenal, Configurações e Sair.
- Estrutura para sons de tiros e efeitos de impacto/partículas.

## Estrutura de pastas

```text
Assets/
  Scripts/
    AI/
    Core/
    GameModes/
    Maps/
    Mobile/
    Player/
    Progression/
    UI/
    Weapons/
  Scenes/
Docs/
```

## Cenas recomendadas

Crie as cenas abaixo no Unity Build Settings:

1. `MainMenu`
2. `CityDestroyed` (cidade destruída)
3. `MilitaryBase` (base militar)
4. `WarForest` (floresta de guerra)
5. `Arsenal`
6. `Settings`

## Como configurar rapidamente no Unity

1. Crie um projeto Unity 3D (URP opcional para leveza).
2. Copie a pasta `Assets/Scripts` para o projeto.
3. Na cena de jogo:
   - Player com `CharacterController` + `PlayerMotor` + `PlayerHealth`.
   - Câmera FPS com `TouchLook`.
   - Canvas com joystick e botões (Fire/Aim/Reload/Jump) ligados ao `MobileInputBridge`.
   - HUD com `HUDController` (Slider vida, Text munição, RawImage minimapa).
4. Crie 3 `WeaponData` assets:
   - `Pistol`: `magazineSize=12`, `infiniteReserveAmmo=true`, `fireRate=2`
   - `Rifle`: `magazineSize=30`, `fireRate=10`
   - `Shotgun`: `magazineSize=8`, `fireRate=1`, `pellets=8`, `range=20`
5. Configure prefabs de inimigos com `EnemyAIController` + `EnemyHealth` + `NavMeshAgent`.
6. Para sobrevivência, adicione `SurvivalModeManager` e pontos de spawn.
7. Para missão, adicione `MissionModeManager` e zonas de captura.
8. Em Android:
   - API level compatível com seu target
   - IL2CPP
   - Texture Compression ASTC
   - Quality com sombras simples ou desativadas

## Otimização Android sugerida

- Use malhas low-poly e texturas 512/1024.
- Limite partículas simultâneas.
- Use Occlusion Culling e Batching.
- Evite luzes dinâmicas excessivas.
- Mantenha `Application.targetFrameRate = 60`.

## Observações

Este projeto entrega uma base funcional e modular para expansão. Você pode adicionar:
- matchmaking multiplayer,
- recoil procedural avançado,
- animações de armas,
- sistema de missões com checkpoints,
- save/load de progressão.
