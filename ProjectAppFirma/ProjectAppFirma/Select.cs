using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


public class Select
{
    //variable for Sql Connection preferences
    private const string ConnOption = "DATA SOURCE=MSSQLServer;Database=FIRMA; INTEGRATED SECURITY=SSPI;";
    //List of command
    static private List<string> command = new List<string>(){
        "SELECT id, osoba.ToString(), adres.ToString() from pracownik where osoba.GetSurname()",

        "SELECT osoba.GetSurname(),laptop.ToString(),telefon.ToString(),samochod.ToString()"
        +" from pracownik"
        +" JOIN prac_sprzet ON pracownik.id=prac_sprzet.id_pracownik"
        +" LEFT JOIN telefon ON telefon.id=prac_sprzet.id_telefon"
        +" LEFT JOIN laptop ON laptop.id=prac_sprzet.id_laptop"
        +" LEFT JOIN samochod ON samochod.id=prac_sprzet.id_samochod"
        +" WHERE osoba.GetSurname()",

        "SELECT osoba.ToString(),telefon.GetPhoneNumber() from pracownik"
        +" JOIN prac_sprzet ON pracownik.id=prac_sprzet.id_pracownik"
        +" JOIN telefon ON telefon.id=prac_sprzet.id_telefon"
        +" WHERE osoba.GetSurname()",

        "SELECT osoba.GetSurname(),nrKonta.ToString() from pracownik"
        +" WHERE osoba.GetSurname()",


        "SELECT id, samochod.ToString() from samochod;",

        "SELECT id, laptop.ToString() from laptop;",

        "SELECT id, telefon.ToString() from telefon;",

        "SELECT id, samochod.ToString() FROM samochod WHERE id NOT IN (SELECT id_samochod from prac_sprzet WHERE id_samochod IS NOT NULL);",

        "SELECT id, laptop.ToString() FROM laptop WHERE id NOT IN (SELECT id_laptop from prac_sprzet WHERE id_laptop IS NOT NULL);",

        "SELECT id, telefon.ToString() FROM telefon WHERE id NOT IN (SELECT id_telefon from prac_sprzet WHERE id_telefon IS NOT NULL);"
    };
    //Method executes Select operations
    //arguments: nr - which command will be executed
    //           Surname - Surname of Worker 
    public static bool ExecuteSelect(int nr, string Surname)
    {
        if (nr >= 5) Surname = "";
        else if (Surname == "") Surname = "IS NOT NULL";
        else Surname = "='" + Surname + "'";
        SqlConnection connection = new SqlConnection(ConnOption);
        try
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(command[nr - 1] + Surname, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write(reader[i].ToString() + " ");
                Console.Write("\n");

            }
            Console.ReadLine();
            return true;
        }
        catch (SqlException ex)
        {
            Console.WriteLine("\nBłąd!");
            Console.WriteLine(ex.Message);
            return false;
        }
        finally { connection.Close(); }
    }

}
