using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Tests
{
    [TestClass()]
    public class DataRecordExtensionsTests
    {
        private TestContext testContextInstance;

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

        [TestMethod()]
        public void DataRecordExtensions_Populate_Data_Record_From_Boolean_Oprions()
        {
            var records = new List<object>
            {
                new
                {
                    text = "Yes",
                    value = true
                },
                new
                {
                    text = "No",
                    value = false
                }
            };

            var dataRecords = records.ToDataRecords();

            Assert.AreEqual(2, dataRecords.Count());

            var dataRecord = dataRecords.First();

            Assert.AreEqual(2, dataRecord.Fields.Count());

            var dataField = dataRecord.Fields.First();

            Assert.AreEqual("text", dataField.Name);

            Assert.AreEqual("Yes", dataField.Value);

            dataField = dataRecord.Fields.Last();

            Assert.AreEqual("value", dataField.Name);

            Assert.AreEqual(true, dataField.Value);

            dataRecord = dataRecords.Last();

            Assert.AreEqual(2, dataRecord.Fields.Count());

            dataField = dataRecord.Fields.First();

            Assert.AreEqual("text", dataField.Name);

            Assert.AreEqual("No", dataField.Value);

            dataField = dataRecord.Fields.Last();

            Assert.AreEqual("value", dataField.Name);

            Assert.AreEqual(false, dataField.Value);
        }
    }
}
