using Microsoft.Data.Sqlite;
using TwitchBot.PcClient.Models;

namespace TwitchBot.PcClient.DataAccessLayerSQlite
{
    public partial class SQLiteDal
    {
        /// <summary>
        /// Add user if not exists
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddNewUser(User user)
        {
            string query = "INSERT INTO User (UserName,IdTwitch,LastConnectionDate,LastMessageDate) VALUES (@username,@IdTwitch,CURRENT_TIMESTAMP,@LastMessageDate); select last_insert_rowid();";
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqliteParameter("@username", user.UserName));
                        cmd.Parameters.Add(user.IdTwitch == null
                            ? new SqliteParameter("@IdTwitch", DBNull.Value)
                            : new SqliteParameter("@IdTwitch", user.IdTwitch));

                        cmd.Parameters.Add(user.LastMessageDate == null
                            ? new SqliteParameter("@LastMessageDate", DBNull.Value)
                            : new SqliteParameter("@LastMessageDate", user.LastMessageDate));

                        user.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        user.FirstConnectionDate = DateTime.Now;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - AddNewUser");
                throw;
            }
        }

        /// <summary>
        /// Update last connection date 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(User user)
        {
            string query = "UPDATE USER SET LastConnectionDate = @LastConnectionDate, IdTwitch = @IdTwitch, LastMessageDate = @LastMessageDate  WHERE Id = @Id;";
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.Add(user.LastConnectionDate == null
                            ? new SqliteParameter("@LastConnectionDate", DBNull.Value)
                            : new SqliteParameter("@LastConnectionDate", user.LastConnectionDate));

                        cmd.Parameters.Add(user.IdTwitch == null
                            ? new SqliteParameter("@IdTwitch", DBNull.Value)
                            : new SqliteParameter("@IdTwitch", user.IdTwitch));

                        cmd.Parameters.Add(user.LastMessageDate == null
                            ? new SqliteParameter("@LastMessageDate", DBNull.Value)
                            : new SqliteParameter("@LastMessageDate", user.LastMessageDate));

                        cmd.Parameters.Add(new SqliteParameter("@Id", user.Id));
                        var result = cmd.ExecuteNonQuery();
                        return result == 1;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - UpdateUserLastConnection");
                throw;
            }
        }

        /// <summary>
        /// Retrieve all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers()
        {
            string query = $"SELECT Id,IdTwitch,UserName,FirstConnectionDate,LastConnectionDate,LastMessageDate FROM USER;";
            List<User> users = new List<User>();
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var userId = reader.GetInt32("Id");
                            users.Add(new User
                            {
                                Id = userId,
                                IdTwitch = reader.GetNullableInt64("IdTwitch"),
                                UserName = reader.GetString("UserName"),
                                FirstConnectionDate = reader.GetDateTime("FirstConnectionDate"),
                                LastConnectionDate = reader.GetNullableDateTime("LastConnectionDate"),
                                LastMessageDate = reader.GetNullableDateTime("LastMessageDate"),
                                Messages = GetUserMessages(userId).ToList()
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - GetAllUsers");
                throw;
            }
            return users;
        }

        /// <summary>
        /// Retrieve user by this id twitch
        /// </summary>
        /// <param name="twitchId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public User? GetUserByTwitchIdOrUserName(long twitchId, string username)
        {
            string query = $"SELECT Id,IdTwitch,UserName,FirstConnectionDate,LastConnectionDate,LastMessageDate FROM User WHERE IdTwitch = @idTwitch OR (IdTwitch IS NULL AND UserName = @username);";
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqliteParameter("@idTwitch", twitchId));
                        cmd.Parameters.Add(new SqliteParameter("@username", username));
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            var userId = reader.GetInt32("Id");
                            return new User
                            {
                                Id = userId,
                                IdTwitch = reader.GetNullableInt64("IdTwitch"),
                                UserName = reader.GetString("UserName"),
                                FirstConnectionDate = reader.GetDateTime("FirstConnectionDate"),
                                LastConnectionDate = reader.GetNullableDateTime("LastConnectionDate"),
                                LastMessageDate = reader.GetNullableDateTime("LastMessageDate"),
                                Messages = GetUserMessages(userId).ToList()
                            };
                        }
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - GetAllUsers");
                throw;
            }
        }

        /// <summary>
        /// Retrieve user by this username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User? GetUserByUsername(string username)
        {
            string query = $"SELECT Id,IdTwitch,UserName,FirstConnectionDate,LastConnectionDate,LastMessageDate FROM User WHERE UserName = @userName;";
            try
            {
                using (SqliteConnection conn = new SqliteConnection(_connectionString))
                {
                    conn.Open();
                    using (SqliteCommand cmd = new SqliteCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqliteParameter("@userName", username));
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            var userId = reader.GetInt32("Id");
                            return new User
                            {
                                Id = userId,
                                IdTwitch = reader.GetNullableInt64("IdTwitch"),
                                UserName = reader.GetString("UserName"),
                                FirstConnectionDate = reader.GetDateTime("FirstConnectionDate"),
                                LastConnectionDate = reader.GetNullableDateTime("LastConnectionDate"),
                                LastMessageDate = reader.GetNullableDateTime("LastMessageDate"),
                                Messages = GetUserMessages(userId).ToList()
                            };
                        }
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "SQLiteService - GetAllUsers");
                throw;
            }
        }

    }
}
