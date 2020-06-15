using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;


namespace ProjectTestFIRMA
{
    /// <summary>
    /// Summary description for LcoationTest
    /// </summary>
    [TestClass]
    public class CarTest
    {
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
            String sqlcommand = "CREATE TABLE car (car dbo.Car); "
                              + "INSERT INTO car (car) VALUES ('Skoda,Fabia,RKR2345');";
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
            String sqlcommand = "DROP TABLE car;";
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
        public void TestAccountNrToString()
        {
            String sqlcommand = "SELECT car.ToString() from car;";
            String expected = "Car: Skoda, Fabia, RKR2345";
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

        [TestMethod]
        public void TestGetNr()
        {
            String sqlcommand = "SELECT car.GetNr() from car;";
            String expected = "RKR2345";
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

        [TestMethod]
        public void ValidateFirstTwoChars() {
            Car car = new Car();
            Assert.IsFalse(car.Validate("R11234"));
        }
        [TestMethod]
        public void Validate()
        {
            Car car = new Car();
            Assert.IsTrue(car.Validate("RK12345"));
        }
    }
}

