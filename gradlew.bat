@ECHO OFF
SET DIR=%~dp0

IF "%JAVA_HOME%"=="" (
  IF EXIST "%USERPROFILE%\.local\share\mise\installs\java\17.0.2\bin\java.exe" (
    SET JAVA_HOME=%USERPROFILE%\.local\share\mise\installs\java\17.0.2
  )
)

IF EXIST "%DIR%\gradle\wrapper\gradle-wrapper.jar" (
  "%JAVA_HOME%\bin\java.exe" -classpath "%DIR%\gradle\wrapper\gradle-wrapper.jar" org.gradle.wrapper.GradleWrapperMain %*
) ELSE (
  ECHO gradle-wrapper.jar nao encontrado; usando Gradle do sistema.
  gradle %*
)
