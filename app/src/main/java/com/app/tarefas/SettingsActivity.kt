package com.app.tarefas

import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.app.tarefas.databinding.ActivitySettingsBinding

class SettingsActivity : AppCompatActivity() {

    private lateinit var binding: ActivitySettingsBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivitySettingsBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val prefs = getSharedPreferences("app_settings", MODE_PRIVATE)
        binding.switchNotifications.isChecked = prefs.getBoolean("notifications_enabled", true)

        binding.switchNotifications.setOnCheckedChangeListener { _, isChecked ->
            prefs.edit().putBoolean("notifications_enabled", isChecked).apply()
            Toast.makeText(this, R.string.settings_saved, Toast.LENGTH_SHORT).show()
        }

        binding.buttonBack.setOnClickListener { finish() }
    }
}
