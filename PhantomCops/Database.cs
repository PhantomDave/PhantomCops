using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace PhantomCops
{
    public class Database
    {
        public static string connString = $"server={Config.DBHost};user={Config.DBUser};database={Config.DBName};port={Config.DBPort};password={Config.DBPass}";
        public static MySqlConnection conn = new MySqlConnection(connString);
        public static MySqlCommand Command;
        public static MySqlDataReader Reader;
        public static bool Init()
        {
            try
            {
                Main.Log("Connecting to MySQL...");
                conn.Open();
            }
            catch (Exception ex)
            {
                Main.Log("===============================");
                Main.Log("Failed connection to MySQL");
                Main.Log(ex.ToString());
                Main.Log("===============================");
                return false;
            }

            conn.Close();
            Main.Log($"Connected to MySQL Database {Config.DBName}");
            return true;
        }

        public static void Query(string sql)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Main.Log(sql);
            }
            catch(Exception ex)
            {
                Main.Log("===============================");
                Main.Log("Error during the SQL Query");
                Main.Log(ex.ToString());
                Main.Log("===============================");
            }
            conn.Close();
        }

        public static DataTable QueryResult(string query)
        {
            using (conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    Command = conn.CreateCommand();
                    Command.CommandText = query;
                    Reader = Command.ExecuteReader();
                    if (!Reader.HasRows)
                    {
                        Reader.Close();
                        conn.Close();
                        return null;
                    }
                    var reslut = new DataTable();
                    reslut.Load(Reader);
                    Reader.Close();

                    conn.Close();

                    return reslut;
                }
                catch (MySqlException ex)
                {
                    Main.Log("===============================");
                    Main.Log("Error during the SQL Query");
                    Main.Log(ex.ToString());
                    Main.Log("===============================");
                    return null;
                }
            }
        }
        public static bool IsRegistered(string mail)
        {
            if (QueryResult($"SELECT email FROM accounts WHERE email = '{mail}'") != null)
            {
                return true;
            }
            return false;
        }

        public static void RegisterPlayer(Client player, string username, string email, string password)
        {
            var Hashpass = BCrypt.Net.BCrypt.HashPassword(password);

            Query($"INSERT INTO accounts (username, password, email, socialclubname, regip) VALUES ('{username}', '{Hashpass}', '{email}', '{player.SocialClubName}', '{player.Address}')");
        }

        public static bool CheckLogin(Client player, string email, string password)
        {
            var Result = QueryResult($"SELECT * FROM accounts WHERE email = '{email}'");

            Accounts.Controller.LoadPlayerAccount(player, Result);

            var pData = Accounts.Controller.GetData(player);
            
            if(BCrypt.Net.BCrypt.Verify(password, pData.Account.password))
            {
                Accounts.Controller.UpdateAccountData(player);
                return true;
            }
            pData = null; 
            return false;
        }
    }
}
