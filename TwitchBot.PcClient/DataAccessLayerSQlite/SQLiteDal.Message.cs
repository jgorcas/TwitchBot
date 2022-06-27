using Microsoft.Data.Sqlite;
using TwitchBot.PcClient.Models;

namespace TwitchBot.PcClient.DataAccessLayerSQlite
{
    public partial class SQLiteDal
    {
        public IEnumerable<UserMessage> GetUserMessages(int idUser)
        {
            List<UserMessage> messages = new List<UserMessage>();
            string query = $"SELECT Id,Text,CreatedDate FROM Message where IdUser = @IdUser;";
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqliteParameter("@IdUser", idUser));
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            messages.Add(new UserMessage
                            {
                                Id = reader.GetInt32("Id"),
                                Text = reader.GetString("Text"),
                                CreatedDate = reader.GetDateTime("CreatedDate")
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - GetUserMessages");
            }
            return messages;
        }

        public bool AddUserMessage(int userId, string message)
        {
            string query = $"INSERT INTO Message (IdUser,Text) Values (@id,@text);";
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqliteParameter("@id", userId));
                        cmd.Parameters.Add(new SqliteParameter("@text", message));
                        var result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - AddUserMessage");
            }

            return false;
        }
    }
}
