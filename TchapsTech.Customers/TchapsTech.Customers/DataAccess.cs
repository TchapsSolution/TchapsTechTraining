using System.Data;
using Microsoft.Data.SqlClient;

namespace TchapsTech.Customers
{
    public static class DataAccess
    {
        public static string GetSqlConnectionString()
        {
            var sqlConnection = new SqlConnectionStringBuilder
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "TchapsTech",
                UserID = "Tchaps",
                Password = "Password123",
                IntegratedSecurity = true,
                Encrypt = true,
                ConnectTimeout = 30
            };
            return sqlConnection.ToString();
        }

        public static List<UserModel> GetUser(int userId)
        {
            var connectionStr = new SqlConnection(GetSqlConnectionString());

            using (SqlCommand command = connectionStr.CreateCommand())
            {
                connectionStr.Open();

                command.Connection = connectionStr;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                SELECT UserId, FirstName, LastName, Email, CreationDate, PhoneNumber
                FROM Users
                WHERE UserId = @UserId;                
                ";

                SqlParameter parameter = new SqlParameter("@UserId", SqlDbType.Int);
                parameter.Value = userId;
                command.Parameters.Add(parameter);

                SqlDataReader reader = command.ExecuteReader();

                List<UserModel> users = new();

                while (reader.Read())
                {
                    var user = new UserModel
                    {
                        UserId = reader.GetInt32("UserId"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        Email = reader.GetString("Email"),
                        CreationDate = reader.GetDateTime("CreationDate"),
                        PhoneNumber = reader.GetString("PhoneNumber")
                    };
                    users.Add(user);
                }

                connectionStr.Close();

                return users;
            }
        }

        public static List<UserModel> GetUsers()
        {
            var connectionStr = new SqlConnection(GetSqlConnectionString());

            using (SqlCommand command = connectionStr.CreateCommand())
            {
                connectionStr.Open();

                command.Connection = connectionStr;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                SELECT UserId, FirstName, LastName, Email, CreationDate, PhoneNumber
                FROM Users;                
                ";

                SqlDataReader reader = command.ExecuteReader();

                List<UserModel> users = new();

                while (reader.Read())
                {
                    var user = new UserModel
                    {
                        UserId = reader.GetInt32("UserId"),
                        FirstName = reader.GetString("FirstName"),
                        LastName = reader.GetString("LastName"),
                        Email = reader.GetString("Email"),
                        CreationDate = reader.GetDateTime("CreationDate"),
                        PhoneNumber = reader.GetString("PhoneNumber")
                    };
                    users.Add(user);
                }

                connectionStr.Close();

                return users;
            }
        }

        public static void Insert(UserModel user)
        {
            var connectionStr = new SqlConnection(GetSqlConnectionString());

            using (SqlCommand command = connectionStr.CreateCommand())
            {
                connectionStr.Open();

                command.Connection = connectionStr;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                INSERT INTO Users
                    (FirstName, LastName, Email, CreationDate, PhoneNumber)
                OUTPUT 
                    INSERTED.UserId
                VALUES
                    (@FirstName, @LastName, @Email, @CreationDate, @PhoneNumber);
                ";

                SqlParameter parameter = new SqlParameter("@FirstName", SqlDbType.NVarChar, 100);
                parameter.Value = user.FirstName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                parameter.Value = user.LastName;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter("@Email", SqlDbType.VarChar, 255);
                parameter.Value = user.Email;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter("@CreationDate", SqlDbType.DateTime);
                parameter.Value = DateTime.Now;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 20);
                parameter.Value = user.PhoneNumber;
                command.Parameters.Add(parameter);

                var userId = (int)command.ExecuteScalar();

                connectionStr.Close();

                Console.WriteLine("\nThe user was added with the UserID = {0}.\n", userId);
            }

        }

        public static void Delete(int userId)
        {
            var connectionStr = new SqlConnection(GetSqlConnectionString());

            using (SqlCommand command = connectionStr.CreateCommand())
            {
                connectionStr.Open();

                command.Connection = connectionStr;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                DELETE FROM Users
                WHERE UserId = @UserId;
                ";

                SqlParameter parameter = new SqlParameter("@UserId", SqlDbType.Int);
                parameter.Value = userId;
                command.Parameters.Add(parameter);

                command.ExecuteScalar();

                connectionStr.Close();

                Console.WriteLine("\n The user with the UserID = {0} was deleted\n", userId);
            }
        }
    }
}
