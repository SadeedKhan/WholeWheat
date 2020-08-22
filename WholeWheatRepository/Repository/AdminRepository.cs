using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholeWheatRepository.Models;

namespace WholeWheatRepository.Repository
{
    public class AdminRepository
    {
        private static string _connString { get; set; }
        private static object _dbLock = new object();
        static AdminRepository()
        {
            _connString = Connection.LiveConnectionString();
        }
        #region Login
        public static AdminUser AuthenticateUser(string userName, string userPassword)
        {
            AdminUser user = null;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPassword))
                return user;
            try
            {
                lock (_dbLock)
                {
                    using (var conn = new SqlConnection(_connString))
                    {
                        SqlCommand cmd = new SqlCommand("proc_admin_authenticate_user", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@UserPassword", userPassword);
                        SqlDataReader reader = null;
                        conn.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                user = new AdminUser();
                                user.Id = Convert.ToInt32((reader["AdminID"]));
                                user.UserName = reader["AdminName"] as string;
                                //user.UserPassword = reader["user_password"] as string;
                                reader.Close();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                user = null;
            }

            return user;
        }
    }
}
#endregion