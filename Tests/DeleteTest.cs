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
    public class DeleteTest
    {
        [TestMethod()]
        public void TestExecuteDeleteInvalidID()
        {
           
            Assert.IsFalse(Delete.ExecuteDelete(1, "d"));
        }
        [TestMethod()]
        public void TestExecuteDeleteValidID()
        {

            Assert.IsTrue(Delete.ExecuteDelete(1, "1"));
        }
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void TestExecuteDeleteInValidCommand()
        {

            Delete.ExecuteDelete(10, "1");
        }


    }
}
