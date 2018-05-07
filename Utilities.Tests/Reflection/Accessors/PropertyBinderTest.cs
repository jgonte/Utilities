using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace Utilities.Tests
{
    [TestClass()]
    public class PropertyBinderTest
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

        public class Source
        {
            public int SourceProperty1 { get; set; }

            public string SourceProperty2 { get; set; }
        }

        public class Target
        {
            public int TargetProperty1 { get; set; }

            public string TargetProperty2 { get; set; }
        }

        /// <summary>
        /// Verifies that the properties get copied from one object to the other
        ///</summary>
        [TestMethod()]
        public void PropertyBinderBindTest()
        {
            Source source = new Source
            {
                SourceProperty1 = 12345,
                SourceProperty2 = "Some string data"
            };

            PropertyBinder binder = new PropertyBinder
            {
                Source = source,
                PropertyLinks = new PropertyLink[]
                {
                    new PropertyLink
                    {
                        Source = "SourceProperty1",
                        Target = "TargetProperty1"
                    },
                    new PropertyLink
                    {
                        Source = "SourceProperty2",
                        Target = "TargetProperty2"
                    }
                }
            };

            Target target = new Target();

            binder.Bind(target);

            Assert.AreEqual(source.SourceProperty1, target.TargetProperty1);
            Assert.AreEqual(source.SourceProperty2, target.TargetProperty2);
        }
    }
}
