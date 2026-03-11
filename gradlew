#!/bin/sh

DIR="$(cd "$(dirname "$0")" && pwd)"
WRAPPER_JAR="$DIR/gradle/wrapper/gradle-wrapper.jar"

# Força JDK compatível com Android Gradle Plugin (evita erro com Java 25 no ambiente).
if [ -z "$JAVA_HOME" ]; then
  for CANDIDATE in \
    /root/.local/share/mise/installs/java/17.0.2 \
    /root/.local/share/mise/installs/java/21.0.2
  do
    if [ -x "$CANDIDATE/bin/java" ]; then
      JAVA_HOME="$CANDIDATE"
      export JAVA_HOME
      PATH="$JAVA_HOME/bin:$PATH"
      export PATH
      break
    fi
  done
fi

if [ -f "$WRAPPER_JAR" ]; then
  exec "$JAVA_HOME/bin/java" -classpath "$WRAPPER_JAR" org.gradle.wrapper.GradleWrapperMain "$@"
else
  echo "gradle-wrapper.jar não encontrado; usando Gradle instalado no sistema." >&2
  exec gradle "$@"
fi
