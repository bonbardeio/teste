package com.gamedevai

import android.content.Context
import org.tensorflow.lite.Interpreter
import java.nio.MappedByteBuffer

class OfflineLearningEngine(private val context: Context) {

    private var interpreter: Interpreter? = null

    fun loadModelIfAvailable(): Boolean {
        return try {
            val model = loadModelFile("game_pattern_model.tflite")
            interpreter = Interpreter(model)
            true
        } catch (_: Exception) {
            false
        }
    }

    fun analyzeCommand(command: String): String {
        val normalized = command.lowercase()
        return when {
            "fps" in normalized -> "perfil_fps"
            "survival" in normalized -> "perfil_survival"
            "mapa aberto" in normalized -> "perfil_open_world"
            else -> "perfil_hibrido"
        }
    }

    private fun loadModelFile(assetName: String): MappedByteBuffer {
        val fileDescriptor = context.assets.openFd(assetName)
        val inputStream = fileDescriptor.createInputStream()
        val channel = inputStream.channel
        return channel.map(
            java.nio.channels.FileChannel.MapMode.READ_ONLY,
            fileDescriptor.startOffset,
            fileDescriptor.declaredLength
        )
    }
}
