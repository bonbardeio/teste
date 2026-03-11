package com.app.tarefas

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.app.tarefas.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding
    private lateinit var dbHelper: TaskDbHelper
    private lateinit var adapter: TaskAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        dbHelper = TaskDbHelper(this)
        setupRecycler()
        setupActions()
        loadTasks()
    }

    private fun setupRecycler() {
        adapter = TaskAdapter(
            tasks = emptyList(),
            onToggleDone = { task, checked ->
                dbHelper.updateTaskStatus(task.id, checked)
                loadTasks()
            },
            onDelete = { task ->
                dbHelper.deleteTask(task.id)
                loadTasks()
                Toast.makeText(this, R.string.task_deleted, Toast.LENGTH_SHORT).show()
            }
        )

        binding.recyclerTasks.layoutManager = LinearLayoutManager(this)
        binding.recyclerTasks.adapter = adapter
    }

    private fun setupActions() {
        binding.fabAddTask.setOnClickListener {
            val title = binding.inputTitle.text.toString().trim()
            val description = binding.inputDescription.text.toString().trim()

            if (title.isBlank() || description.isBlank()) {
                Toast.makeText(this, R.string.fill_all_fields, Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            dbHelper.insertTask(title, description)
            binding.inputTitle.text?.clear()
            binding.inputDescription.text?.clear()
            loadTasks()
            Toast.makeText(this, R.string.task_created, Toast.LENGTH_SHORT).show()
        }

        binding.openSettings.setOnClickListener {
            startActivity(Intent(this, SettingsActivity::class.java))
        }
    }

    private fun loadTasks() {
        val tasks = dbHelper.getTasks()
        adapter.updateData(tasks)
        binding.emptyState.text = if (tasks.isEmpty()) getString(R.string.no_tasks) else ""
    }
}
