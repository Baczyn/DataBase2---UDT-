using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;


namespace ProjectTestFIRMA
{
    /// <summary>
    /// Summary description for LaptopTest
    /// </summary>
    [TestClass]
    public class LaptopTest{
        private TestContext testContextInstance;
        private const string ConnOption = "DATA SOURCE=MSSQLServer;Database=FIRMA; INTEGRATED SECURITY=SSPI;";
        static SqlConnection connection;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        ///
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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            connection = new SqlConnection(ConnOption);
            String sqlcommand = "CREATE TABLE laptop (laptop dbo.Laptop); "
                              + "INSERT INTO laptop (laptop) VALUES ('Lenovo,G50-70,Nr242');";
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
            String sqlcommand = "DROP TABLE laptop;";
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

        [TestMethod]
        public void TestLaptopToString()
        {
            String sqlcommand = "SELECT laptop.ToString() from laptop;";
            String expected = "Laptop: Lenovo, G50-70, Nr242";
            try
            {
                SqlCommand cmd = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = cmd.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader[0].ToString());
                }
                datareader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

