using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class TaskDatabase
    {
        private string connectionString = "Data Source=tasks.db;Version=3;";

        public TaskDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Description TEXT,
                        DueDate TEXT,
                        Priority TEXT,
                        IsCompleted INTEGER
                    )";
                var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public List<TaskModel> GetAllTasks()
        {
            var tasks = new List<TaskModel>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Tasks";
                var command = new SQLiteCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var task = new TaskModel
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        DueDate = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        Priority = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        IsCompleted = reader.GetInt32(5) == 1
                    };
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        public void AddTask(TaskModel task)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                INSERT INTO Tasks (Title, Description, DueDate, Priority, IsCompleted) 
                VALUES (@Title, @Description, @DueDate, @Priority, @IsCompleted)";

                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description ?? string.Empty);
                command.Parameters.AddWithValue("@DueDate", task.DueDate ?? string.Empty);
                command.Parameters.AddWithValue("@Priority", task.Priority ?? string.Empty);
                command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted ? 1 : 0);
                command.ExecuteNonQuery();
            }
        }


        public void DeleteTask(int taskId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Tasks WHERE Id = @Id";
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", taskId);
                command.ExecuteNonQuery();
            }
        }
    }
}


