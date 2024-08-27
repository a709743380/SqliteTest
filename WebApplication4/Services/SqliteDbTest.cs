using Microsoft.Data.Sqlite;

namespace WebApplication4.Services
{
    public class SqliteDbTest
    {
        public string  SelectData()
        {
            var connectionString = "Data Source=DB/test.db";
            string show = "";
            // 建立連接
            using (var connection = new SqliteConnection(connectionString))
            {
                // 打開連接
                connection.Open();

                // 定義查詢命令
                string createTableQuery = @"
                SELECT name
                FROM sqlite_master
                WHERE type = 'table';";

                // 執行查詢
                using (var command = new SqliteCommand(createTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            show += $"name: {reader["name"]};";
                        }
                    }
                }
                return show;
            }
        }
    }
}
