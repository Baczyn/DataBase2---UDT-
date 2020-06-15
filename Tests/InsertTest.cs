using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace ProjectTestFIRMA
{
    
    
    /// <summary>
    ///This is a test class for InsertTest and is intended
    ///to contain all InsertTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InsertTest
    {

        private TestContext testContextInstance;
        private const string ConnOption = "DATA SOURCE=MSSQLServer;Database=FIRMA; INTEGRATED SECURITY=SSPI;";
        static SqlConnection connection;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        
        [ClassInitialize()]
        public static void ClassInit(TestContext context){
            connection = new SqlConnection(ConnOption);
            String sqlcommand = "CREATE TABLE pracownik(id INT PRIMARY KEY IDENTITY (1, 1),osoba dbo.Person NOT NULL,adres dbo.Location NOT NULL,nrKonta dbo.AccountNr NOT NULL);"

                               + "INSERT INTO pracownik (osoba,adres,nrKonta) values ('EMILA,Fill,kobieta,98092809154','Nowosielce,Prosta,12,32005','47103014581000093916676008');"
                               + "CREATE TABLE telefon(id INT PRIMARY KEY IDENTITY (1, 1),telefon dbo.Phone NOT NULL);"
                               + "INSERT INTO telefon (telefon) values('Iphone,6s,726617679');"
                               + "CREATE TABLE prac_sprzet(id_pracownik int,id_laptop int,id_telefon int ,id_samochod int);"
                               + "INSERT INTO prac_sprzet(id_pracownik,id_laptop,id_telefon,id_samochod) values(1,1,1,1);";
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
            
            String sqlcommand = "DROP TABLE pracownik;Drop Table telefon;DROP TABLE prac_sprzet";
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




        /// <summary>
        ///A test for WorkerExistsInEquipment
        ///</summary>
        [TestMethod()]
        public void TestPeselExists()
        {
            Assert.IsTrue(Insert.PeselExists("imie,nazwisko,kobieta,98092809154"));
        }

        [TestMethod()]
        public void TestPeselNotExists()
        {
            Assert.IsFalse(Insert.PeselExists("imie,nazwisko,kobieta,98022809154"));
        }

        /// <summary>
        ///A test for PhoneNrExists
        ///</summary>
        [TestMethod()]
        public void TestPhoneNrExistsTest()
        {
            Assert.IsTrue(Insert.PhoneNrExists("tele,sss,726617679"));
        }

        [TestMethod()]
        public void TestPhoneNrNotExistsTest()
        {
            Assert.IsFalse(Insert.PhoneNrExists("tele,sss,726612679"));
        }

        [TestMethod()]
        public void TestWorkerExistsInEquipment()
        {
            Assert.IsTrue(Insert.WorkerExistsInEquipment("1"));
        }
        [TestMethod()]
        public void TestWorkerExistsNotInEquipment()
        {
            Assert.IsFalse(Insert.WorkerExistsInEquipment("10"));
        }
       


        /// <summary>
        ///A test for ExecuteUpadte
        ///</summary>
        [TestMethod()]
        public void ExecuteUpadteTest()
        {

        }

        /// <summary>
        ///A test for ExecuteInsert
        ///</summary>
        [TestMethod()]
        public void ExecuteInsertTest()
        {

        }

    }
}
