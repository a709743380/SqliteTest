using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public class KeyIVServices
    {
        private readonly SqliteConnection _connection = new SqliteConnection("Data Source=DB/test.db");
        public List<SelectListItem> SelectDdl()
        {
            List<SelectListItem> option = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value ="",
                    Text = "選擇解密鑰匙",
                    Selected =true
                }
            };
            // 打開連接
            _connection.Open();

            // 定義查詢命令
            string createTableQuery = @" SELECT ID,Name FROM EncryptionKeys ;";

            // 執行查詢
            using (var command = new SqliteCommand(createTableQuery, _connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        option.Add(new SelectListItem
                        {
                            Value = reader["ID"].ToString(),
                            Text = reader["Name"].ToString()
                        });
                    }
                }
            }
            return option;

        }
        public KeyIv SelectKeyIv(int id)
        {
            KeyIv result = new KeyIv();
            // 打開連接
            _connection.Open();
            // 定義查詢命令
            string createTableQuery = @" 
                                        SELECT 
                                            EncryptionKey,IV 
                                        FROM 
                                            EncryptionKeys 
                                        WHERE ID = @ID;";

            // 執行查詢
            using (var command = new SqliteCommand(createTableQuery, _connection))
            {
                SqliteParameter parameter = new SqliteParameter("@ID", id);
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Key = reader["EncryptionKey"].ToString();
                        result.Iv = reader["IV"].ToString();
                    }
                }
            }

            return result;
        }

        public List<SetKeyIv> GetKeyIv()
        {
            List<SetKeyIv> setKeyIvs = new List<SetKeyIv>();
            // 打開連接
            _connection.Open();
            // 定義查詢命令
            string query = @" SELECT ID, Name, EncryptionKey,Iv FROM EncryptionKeys ;";

            // 執行查詢
            using (var command = new SqliteCommand(query, _connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SetKeyIv
                        {
                            Id = (long)reader["ID"],
                            Name = (string)reader["Name"],
                            Key = (string)reader["EncryptionKey"],
                            Iv = (string)reader["Iv"],
                        };
                        setKeyIvs.Add(item);
                    }
                }
            }
            return setKeyIvs;

        }
        public void AddKeyIv(SetKeyIv model)
        {
            // 打開連接
            _connection.Open();
            // 定義查詢命令
            string insertQuery = @" 
INSERT INTO EncryptionKeys
(Name, EncryptionKey, IV, CreatedAt) VALUES
( @Name, @Key, @Iv, getdate);";

            string editQuery = @" 
UPDATE EncryptionKeys 
SET
    EncryptionKey = @Key,
    IV = @Iv,
    Name = @Name,

WHERE ID =@Id";

            // 執行查詢
            using (var command = new SqliteCommand(model.IsExist ? editQuery : insertQuery, _connection))
            {
                command.Parameters.AddWithValue($"@{nameof(model.Iv)}", model.Iv);
                command.Parameters.AddWithValue($"@{nameof(model.Key)}", model.Key);
                command.Parameters.AddWithValue($"@{nameof(model.Name)}", model.Name);
                command.Parameters.AddWithValue($"@{nameof(model.Id)}", model.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
