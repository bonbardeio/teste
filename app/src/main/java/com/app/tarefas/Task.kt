package com.app.tarefas

data class Task(
    val id: Long = 0,
    val title: String,
    val description: String,
    val isDone: Boolean
)
