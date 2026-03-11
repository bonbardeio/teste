# JARVIS Unity Starter (PT-BR)

Este repositório inicializa uma estrutura de projeto Unity organizada para acelerar a criação de jogos por comandos simples.

## Estrutura padrão

```text
Assets/
  Scripts/
    Player/
    Enemies/
    UI/
    Systems/
  Models/
  Materials/
  Animations/
  Prefabs/
  UI/
```

## Scripts incluídos

- `PlayerShooting.cs`: sistema básico de tiro com `Fire1`.
- `Projectile.cs`: controle de vida útil e dano do projétil.
- `EnemyHealth.cs`: vida simples para inimigos.

## Como testar rápido no Unity

1. Crie um objeto `Player` e adicione `PlayerShooting`.
2. Crie um `Empty` filho chamado `ShootPoint` na ponta da arma.
3. Crie um prefab de projétil com `Rigidbody` e `Collider` (`isTrigger = true`) + script `Projectile`.
4. Arraste o prefab e o `ShootPoint` no Inspector do `PlayerShooting`.
5. Crie um cubo como inimigo, adicione `Collider` e `EnemyHealth`.
6. Execute a cena e atire com botão esquerdo do mouse.

## Observação

Este template serve como base para expandir sistemas como inimigos, menus, mapas e loja de skins.
