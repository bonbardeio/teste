package com.app.tarefas

import android.content.ContentValues
import android.content.Context
import android.database.sqlite.SQLiteDatabase
import android.database.sqlite.SQLiteOpenHelper

class TaskDbHelper(context: Context) : SQLiteOpenHelper(context, DATABASE_NAME, null, DATABASE_VERSION) {

    override fun onCreate(db: SQLiteDatabase) {
        db.execSQL(
            """
            CREATE TABLE $TABLE_TASKS (
                $COLUMN_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                $COLUMN_TITLE TEXT NOT NULL,
                $COLUMN_DESCRIPTION TEXT NOT NULL,
                $COLUMN_DONE INTEGER NOT NULL DEFAULT 0
            )
            """.trimIndent()
        )
    }

    override fun onUpgrade(db: SQLiteDatabase, oldVersion: Int, newVersion: Int) {
        db.execSQL("DROP TABLE IF EXISTS $TABLE_TASKS")
        onCreate(db)
    }

    fun insertTask(title: String, description: String): Long {
        val values = ContentValues().apply {
            put(COLUMN_TITLE, title)
            put(COLUMN_DESCRIPTION, description)
            put(COLUMN_DONE, 0)
        }
        return writableDatabase.insert(TABLE_TASKS, null, values)
    }

    fun getTasks(): List<Task> {
        val tasks = mutableListOf<Task>()
        val cursor = readableDatabase.query(
            TABLE_TASKS,
            arrayOf(COLUMN_ID, COLUMN_TITLE, COLUMN_DESCRIPTION, COLUMN_DONE),
            null,
            null,
            null,
            null,
            "$COLUMN_ID DESC"
        )

        cursor.use {
            while (it.moveToNext()) {
                tasks.add(
                    Task(
                        id = it.getLong(0),
                        title = it.getString(1),
                        description = it.getString(2),
                        isDone = it.getInt(3) == 1
                    )
                )
            }
        }
        return tasks
    }

    fun updateTaskStatus(taskId: Long, isDone: Boolean): Int {
        val values = ContentValues().apply {
            put(COLUMN_DONE, if (isDone) 1 else 0)
        }
        return writableDatabase.update(
            TABLE_TASKS,
            values,
            "$COLUMN_ID = ?",
            arrayOf(taskId.toString())
        )
    }

    fun deleteTask(taskId: Long): Int {
        return writableDatabase.delete(
            TABLE_TASKS,
            "$COLUMN_ID = ?",
            arrayOf(taskId.toString())
        )
    }

    companion object {
        private const val DATABASE_NAME = "tasks.db"
        private const val DATABASE_VERSION = 1

        const val TABLE_TASKS = "tasks"
        const val COLUMN_ID = "id"
        const val COLUMN_TITLE = "title"
        const val COLUMN_DESCRIPTION = "description"
        const val COLUMN_DONE = "is_done"
    }
}
