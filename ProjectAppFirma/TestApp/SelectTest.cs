using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ProjectTestFIRMA
{
    /// <summary>
    /// Summary description for DeleteTest
    /// </summary>
    [TestClass]
    public class SelectTest
    {
        private const string ConnOption = "DATA SOURCE=MSSQLServer;Database=FIRMA; INTEGRATED SECURITY=SSPI;";
        static SqlConnection connection;
        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            connection = new SqlConnection(ConnOption);
            String sqlcommand = "CREATE TABLE pracownik(id INT PRIMARY KEY IDENTITY (1, 1),osoba dbo.Person NOT NULL,adres dbo.Location NOT NULL,nrKonta dbo.AccountNr NOT NULL);"

                               + "INSERT INTO pracownik (osoba,adres,nrKonta) values ('EMILA,Fill,kobieta,98092809154','Nowosielce,Prosta,12,32005','47103014581000093916676008');"
                               + "CREATE TABLE telefon(id INT PRIMARY KEY IDENTITY (1, 1),telefon dbo.Phone NOT NULL);"
                               + "INSERT INTO telefon (telefon) values('Iphone,6s,726617679');"
                               + "CREATE TABLE prac_sprzet(id_pracownik int,id_laptop int,id_telefon int ,id_samochod int);"
                               + "INSERT INTO prac_sprzet(id_pracownik,id_laptop,id_telefon,id_samochod) values(1,1,1,1);"
                                + "CREATE TABLE laptop(id INT PRIMARY KEY IDENTITY (1, 1),laptop dbo.Laptop NOT NULL);"
                                + "CREATE TABLE samochod(id INT PRIMARY KEY IDENTITY (1, 1),samochod dbo.Car NOT NULL);";
                                
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlcommand, connection);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {

            String sqlcommand = "DROP TABLE pracownik;Drop Table telefon;DROP TABLE prac_sprzet;DROP TABLE samochod;DROP table laptop;";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlcommand, connection);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod()]
        public void TestExecuteSelect()
        {

            Assert.IsTrue(Select.ExecuteSelect(7, ""));
        }

        [TestMethod()]
        public void Test2ExecuteSelect()
        {

            Assert.IsTrue(Select.ExecuteSelect(1, "898"));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestExecuteSelectInvalidCommand2()
        {

            Assert.IsTrue(Select.ExecuteSelect(0, "898"));
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestExecuteSelectInvalidCommand()
        {

            Select.ExecuteSelect(11, "1");
        }




 

    }
}
