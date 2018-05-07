using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class NumericExtensionsTest
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
        public void NumericExtensionsIsBetweenIntTest()
        {
            Assert.IsTrue(4.IsBetween(3, 5));
            Assert.IsTrue(3.IsBetween(3, 5));
            Assert.IsTrue(5.IsBetween(3, 5));
            Assert.IsFalse(2.IsBetween(3, 5));
            Assert.IsFalse(6.IsBetween(3, 5));

            Assert.IsTrue(4u.IsBetween(3u, 5u));
            Assert.IsTrue(3u.IsBetween(3u, 5u));
            Assert.IsTrue(5u.IsBetween(3u, 5u));
            Assert.IsFalse(2u.IsBetween(3u, 5u));
            Assert.IsFalse(6u.IsBetween(3u, 5u));

            Assert.IsTrue(4L.IsBetween(3L, 5L));
            Assert.IsTrue(3L.IsBetween(3L, 5L));
            Assert.IsTrue(5L.IsBetween(3L, 5L));
            Assert.IsFalse(2L.IsBetween(3L, 5L));
            Assert.IsFalse(6L.IsBetween(3L, 5L));
        }
    }
}
