using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


   public class Insert
    {
        //variable for Sql Connection preferences
        private const string ConnOption = "DATA SOURCE=MSSQLServer;Database=FIRMA; INTEGRATED SECURITY=SSPI;";
        //List of command
        static private List<string> command = new List<string>() {
        "INSERT INTO pracownik (osoba,adres,nrKonta) values ('",
        "INSERT INTO telefon (telefon) values('",
        "INSERT INTO laptop (laptop) values('",
        "INSERT INTO samochod (samochod) values('",
        "UPDATE prac_sprzet SET id_laptop='",
        "UPDATE prac_sprzet SET id_telefon='",
        "UPDATE prac_sprzet SET id_samochod='",
        "INSERT INTO prac_sprzet(id_pracownik,id_laptop,id_telefon,id_samochod) values('",

        
};
        //method checks if pesel exists in table Pracownik
        //argument: s - strig inserted from client
        public static bool PeselExists(string s)
        {
            string[] data = s.Split(",".ToCharArray());
            string pesel = data[3].Substring(0, 11);
            Console.WriteLine(pesel + "\n");
            SqlConnection connection = new SqlConnection(ConnOption);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT 1 from pracownik where pracownik.osoba.GetPesel()='" + pesel + "';", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                string flag = "";
                while (reader.Read())
                    flag = reader[0].ToString();
                if (flag.Equals("1")) return true;
                return false;

            }
            catch (SqlException ex)
            {
                Console.WriteLine("\nBłąd!");
 
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return false;
        }
        //method checks if phoneNumber exists in table telefon
        //argument: s - strig inserted from client
        public static bool PhoneNrExists(string s)
        {
            string[] data = s.Split(",".ToCharArray());

            SqlConnection connection = new SqlConnection(ConnOption);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT 1 from telefon where telefon.GetPhoneNumber()='" + data[2] + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                string flag = "";
                while (reader.Read())
                    flag = reader[0].ToString();
                if (flag.Equals("1")) return true;
                return false;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("\nBłąd!");
                
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return false;
        }
        //method checks if worker exists in table prac-sprzet
        //argument: WorkerID
        public static bool WorkerExistsInEquipment(string WorkerID)
        {

            SqlConnection connection = new SqlConnection(ConnOption);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT 1 from prac_sprzet WHERE id_pracownik='" + WorkerID + "'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                string flag = "";
                while (reader.Read())
                    flag = reader[0].ToString();
                if (flag.Equals("1")) return true;
                return false;

            }
            catch (SqlException)
            {
                Console.WriteLine("\nBłąd!");
                Console.WriteLine("Podaj poprawne id");
            }
            finally { connection.Close(); }
            return false;
        }
        //Method executes Insert operations
        //arguments: nr - which command will be executed
        //           id - id of element to Insert
        public static void ExecuteInsert(int nr, string date)
        {
            if (nr == 1 && PeselExists(date)) { Console.WriteLine("\nTaki Pesel istnieje w bazie!"); Console.Read(); return; }
            if (nr == 2 && PhoneNrExists(date)) { Console.WriteLine("\nTaki Numer istnieje w bazie!"); Console.Read(); return; }
            SqlConnection connection = new SqlConnection(ConnOption);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(command[nr - 1] + date + "');", connection);
                cmd.ExecuteNonQuery();

                Console.ReadLine();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("\nBłąd!");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally { connection.Close(); }
        }
        //Method executes Update operations if Pracownik exists in prac_sprzet.If not executes Insert
        //arguments: nr - which command will be executed
        //           WorkerId - id of Worker to whom EquipmentID will be assigned
        //           EquipmentID - id of equipment(laptop,phone,car)
        public static void ExecuteUpadte(int nr, string WorkerID, string EquipmentID)
        {
            string values = "";
            bool flag = false;
            if (WorkerExistsInEquipment(WorkerID)) flag = true;
            SqlConnection connection = new SqlConnection(ConnOption);
            try
            {
                connection.Open();
                SqlCommand cmd = null;
                if (flag)
                    cmd = new SqlCommand(command[nr - 1] + EquipmentID + "' WHERE id_pracownik='" + WorkerID + "'", connection);
                else
                {
                    if (nr == 5) values = "','" + EquipmentID + "',NULL,NULL)";
                    else if (nr == 6) values = "', NULL ,'" + EquipmentID + "', NULL)";
                    else if (nr == 7) values = "',NULL,NULL,'" + EquipmentID + "')";
                    cmd = new SqlCommand(command[7] + WorkerID + values, connection);

                }
                cmd.ExecuteNonQuery();

                Console.ReadLine();
            }

            catch (SqlException )
            {
                Console.WriteLine("\nBłąd!"); ;
                Console.WriteLine("Sprzet jest zajety lub nie istnieje");
                Console.ReadLine();
            }
            finally { connection.Close(); }
        }


    }

