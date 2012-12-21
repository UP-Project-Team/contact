using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Security.Cryptography;
namespace Contact.Server
{
    // Class for interactions with DB (Access)
    class DBAccess
    {
        //Connect to DB with query              
        public static User CheckUser(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE [Имя]='"+username+"'"; //SQL-query
            
            //connection options
            string connectionString = "Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source= /../Users.accdb"; 
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader dataReader = command.ExecuteReader();           
            
            //check results            
            if (dataReader.Read())
            {
                if (dataReader["Пароль"].ToString() == SHA512Hash(password))
                {
                    return new User(Convert.ToInt32(dataReader["Код"]), username);
                }
            }            
          
            // If autorization failed 
            return new User(-1, "Error");
        }
        public static string SHA512Hash(string input)
        {
            SHA512 sha512 = System.Security.Cryptography.SHA512.Create();
            
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = sha512.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));                
            }            
            return sb.ToString();
        }
        
        static string CheckWord(string word) // correctly username and password (sql-injections possible) 
        {
            //Dopilit
            return word;
        }

        //Registration. If username already busy - return -1;
        public static int UserReg(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE [Имя]='" + CheckWord(username) + "'"; //SQL-query
            string connectionString = "Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source= /GameService/Users.accdb"; 
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                return -1;
            }
            sql = "INSERT INTO [Users] ([Имя],[Пароль]) VALUES ('" + CheckWord(username) + "','" + CheckWord(SHA512Hash(password)) + "')";        
            command = new OleDbCommand(sql, connection);
            command.ExecuteNonQuery();
            
            return 0;
        }

    }
}
