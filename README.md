# GameDevAI (Android)

Protótipo de estúdio automático de criação de jogos rodando offline no Android.

## O que está implementado

- Arquitetura multiagente inspirada em Auto-GPT:
  - Game Designer Agent
  - Programmer Agent
  - Level Designer Agent
  - Artist Agent
  - Tester Agent
  - Build Agent
- Pipeline de 9 etapas para geração automática de jogo.
- Motor de aprendizado offline com TensorFlow Lite (com fallback heurístico quando o modelo real não está disponível).
- Interface com:
  - Tela principal
  - Botão **Criar Jogo**
  - Console de comandos
  - Painel de progresso

## Comandos exemplo

- `criar jogo fps`
- `criar jogo survival`
- `criar mapa aberto`
- `criar personagem anime`
- `compilar jogo`

## Estrutura

- `OfflineLearningEngine`: camada de análise de perfil de jogo local.
- `GameStudioOrchestrator`: coordena os agentes na sequência de geração.
- `MainActivity`: UI e execução do pipeline.

## Observações importantes

Este repositório entrega um **MVP funcional de arquitetura e fluxo** dentro de um APK Android. Para atingir produção completa (geração real de projetos Unity exportáveis, assets avançados, compilação real de APKs gerados e aprendizagem robusta dos títulos citados), é necessário evoluir:

1. Dataset e treinamento legalmente licenciados.
2. Modelo TFLite treinado para design/procedural generation.
3. Integração com backend de build toolchain local ou híbrida.
4. Geração de assets com modelos específicos (2D/3D) embarcados.


## Build local (APK)

1. Instale Android SDK (API 34 + build-tools).
2. Use Java 17 (`JAVA_HOME` apontando para JDK 17).
3. Execute: `./gradlew :app:assembleDebug` (ou `gradle :app:assembleDebug`).

### Problema comum em ambientes com proxy

O Android Gradle Plugin é publicado no repositório Google Maven (`https://dl.google.com/dl/android/maven2/`).
Se esse domínio estiver bloqueado (ex.: `CONNECT tunnel failed, response 403`), a resolução de dependências vai falhar.
Nesse caso, libere o domínio no proxy/firewall ou configure um espelho interno do Google Maven.
