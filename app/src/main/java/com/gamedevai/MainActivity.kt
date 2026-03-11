package com.gamedevai

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.ProgressBar
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import kotlin.concurrent.thread

class MainActivity : AppCompatActivity() {

    private lateinit var commandInput: EditText
    private lateinit var createButton: Button
    private lateinit var progressBar: ProgressBar
    private lateinit var progressText: TextView
    private lateinit var consoleText: TextView

    private lateinit var learningEngine: OfflineLearningEngine
    private val orchestrator = GameStudioOrchestrator()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        commandInput = findViewById(R.id.commandInput)
        createButton = findViewById(R.id.createButton)
        progressBar = findViewById(R.id.progressBar)
        progressText = findViewById(R.id.progressText)
        consoleText = findViewById(R.id.consoleText)

        learningEngine = OfflineLearningEngine(this)
        val modelLoaded = learningEngine.loadModelIfAvailable()
        appendConsole(if (modelLoaded) "> Modelo TFLite carregado." else "> Modelo TFLite não encontrado, fallback heurístico ativo.")

        createButton.setOnClickListener {
            val command = commandInput.text.toString().ifBlank { "criar jogo survival" }
            runPipeline(command)
        }
    }

    private fun runPipeline(command: String) {
        createButton.isEnabled = false
        progressBar.progress = 0
        appendConsole("\n> comando recebido: $command")

        thread {
            val profile = learningEngine.analyzeCommand(command)
            runOnUiThread { appendConsole("> perfil detectado: $profile") }

            val project = orchestrator.generate(command) { progress, message ->
                runOnUiThread {
                    progressBar.progress = progress
                    progressText.text = message
                    appendConsole("> $message")
                }
                Thread.sleep(250)
            }

            runOnUiThread {
                appendConsole("\n=== PROJETO GERADO ===")
                appendConsole(project.concept)
                appendConsole(project.designDoc)
                appendConsole(project.map)
                appendConsole(project.characters)
                appendConsole(project.gameplayScript)
                appendConsole(project.uiHud)
                appendConsole(project.testReport)
                appendConsole(project.buildArtifact)
                createButton.isEnabled = true
            }
        }
    }

    private fun appendConsole(message: String) {
        consoleText.text = consoleText.text.toString() + "\n" + message
    }
}
