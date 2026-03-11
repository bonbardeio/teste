# Tarefas Inteligente (Android)

Aplicativo Android em Kotlin + XML para gerenciamento de tarefas com persistência local em SQLite.

## Funcionalidades
- Criar tarefas
- Listar tarefas
- Marcar tarefa como concluída
- Excluir tarefa
- Tela de configurações (notificações locais via preferência)

## Arquitetura
- `MainActivity`: camada de apresentação principal e eventos da UI
- `TaskDbHelper`: camada de dados em SQLite (`SQLiteOpenHelper`)
- `TaskAdapter`: renderização da lista com RecyclerView
- `SettingsActivity`: gerenciamento de preferências com `SharedPreferences`

## Build do APK
1. Abra o projeto no Android Studio.
2. Aguarde a sincronização do Gradle.
3. Vá em **Build > Build Bundle(s) / APK(s) > Build APK(s)**.
4. O APK será gerado em `app/build/outputs/apk/debug/app-debug.apk`.

## Otimizações aplicadas
- `minifyEnabled` + `shrinkResources` no build de release
- uso de `ViewBinding` para reduzir `findViewById` e melhorar segurança de tipos


## Compatibilidade de ambiente (importante)
- O Android Gradle Plugin usado no projeto exige JDK compatível (17+).
- Se houver erro como `Unsupported class file major version 69`, execute o Gradle com JDK 17.
- Exemplo Linux/macOS:
  - `export JAVA_HOME=/root/.local/share/mise/installs/java/17.0.2`
  - `./gradlew :app:assembleDebug`
