using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class StringExtensionsTest
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

        // GetRight Tests
        [TestMethod()]
        public void StringExtensionsGetRightWithSeparatorFoundTest()
        {
            string @string = "LeftPart.-.RightPart";
            string separator = ".-.";
            Assert.AreEqual("RightPart", @string.Right(separator));
        }

        [TestMethod()]
        public void StringExtensionsGetRightWithSeparatorOneTest()
        {
            string @string = "LeftPart.RightPart";
            string separator = ".";
            Assert.AreEqual("RightPart", @string.Right(separator));
        }

        [TestMethod()]
        public void StringExtensionsGetRightWithSeparatorWordAtBeginTest()
        {
            string @string = "LeftPartRightPart";
            string separator = "LeftPart";
            Assert.AreEqual("RightPart", @string.Right(separator));
        }

        [TestMethod()]
        public void StringExtensionsGetRightWithSeparatorNotFoundTest()
        {
            string @string = "RightPart";
            string separator = ".";
            Assert.AreEqual("RightPart", @string.Right(separator));
        }

        // GetLeft Tests
        [TestMethod()]
        public void StringExtensionsGetLeftWithSeparatorFoundTest()
        {
            string @string = "LeftPart.-.RightPart";
            string separator = ".-.";
            Assert.AreEqual("LeftPart", @string.Left(separator));
        }

        [TestMethod()]
        public void StringExtensionsGetLeftWithSeparatorOneTest()
        {
            string @string = "LeftPart.RightPart";
            string separator = ".";
            Assert.AreEqual("LeftPart", @string.Left(separator));
        }

        [TestMethod()]
        public void StringExtensionsGetLeftWithSeparatorWordAtEndTest()
        {
            string @string = "LeftPartRightPart";
            string separator = "RightPart";
            Assert.AreEqual("LeftPart", @string.Left(separator));
        }

        [TestMethod()]
        public void StringExtensionsGetLeftWithSeparatorNotFoundTest()
        {
            string @string = "LeftPart";
            string separator = ".";
            Assert.AreEqual("LeftPart", @string.Left(separator));
        }

        [TestMethod()]
        public void StringExtensionRemoveEndTest()
        {
            string @string = @"LeftPart\";
            string toRemove = @"\";
            Assert.AreEqual("LeftPart", @string.RemoveEnd(toRemove));
        }

        [TestMethod()]
        public void StringExtensionRemoveEndMultipleTest()
        {
            string @string = @"LeftPart\\\";
            string toRemove = @"\\";
            Assert.AreEqual(@"LeftPart\", @string.RemoveEnd(toRemove));
        }

        [TestMethod()]
        public void StringExtensionSingularizeTest()
        {
            Assert.AreEqual("Entity", "Entities".Singularize());
        }

        [TestMethod()]
        public void StringExtensionPluralizeTest()
        {
            Assert.AreEqual("Entities", "Entity".Pluralize());
        }

        [TestMethod()]
        public void StringExtensionsEmptyIfNullWithNullTest()
        {
            string @string = null;
            Assert.AreEqual(string.Empty, @string.EmptyIfNull());
        }

        [TestMethod()]
        public void StringExtensionsEmptyIfNullWithValueTest()
        {
            string @string = "some string";
            Assert.AreEqual(@string, @string.EmptyIfNull());
        }
    }
}
