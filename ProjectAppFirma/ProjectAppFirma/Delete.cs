using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


    public class Delete
    {   //variable for Sql Connection preferences
        private const string ConnOption = "DATA SOURCE=MSSQLServer;Database=FIRMA; INTEGRATED SECURITY=SSPI;";
        //List of command
        static private List<string> command = new List<string>() {
             "DELETE  from pracownik where id='",
             "DELETE  from telefon where id='",
             "DELETE  from laptop where id='",
             "DELETE  from samochod where id='",
        };
        //Method executes Delete Operation
        //arguments: nr - which command will be executed
        //           id - id of element to delete
        public static bool ExecuteDelete(int nr,string id) {
            SqlConnection connection = new SqlConnection(ConnOption);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(command[nr - 1] + id + "';", connection);
                cmd.ExecuteNonQuery();
                Console.ReadLine();
                return true;
            }
            catch (SqlException)
            {
                Console.WriteLine("\nBłąd!");
                Console.WriteLine("Podaj poprawne id");
                Console.ReadLine();
                return false;
            }
            finally { connection.Close(); }
        }
    }


