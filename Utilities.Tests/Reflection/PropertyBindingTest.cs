using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class PropertyBindingTest
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

        public class TestClass
        {
            public int IntProperty { get; set; }
        }

        [TestMethod()]
        public void PropertyBindingWithStringTest()
        {
            TestClass o = new TestClass();
            PropertyBinding binding = new PropertyBinding(o, "IntProperty");

            o.IntProperty = 1907;
            Assert.AreEqual(o.IntProperty, binding.GetValue());

            binding.SetValue(2012);
            Assert.AreEqual(o.IntProperty, binding.GetValue());
        }

        [TestMethod()]
        public void PropertyBindingWithLambdaExpressionTest()
        {
            TestClass o = new TestClass();
            PropertyBinding binding = new PropertyBinding(o, () => o.IntProperty);

            o.IntProperty = 1907;
            Assert.AreEqual(o.IntProperty, binding.GetValue());

            binding.SetValue(2012);
            Assert.AreEqual(o.IntProperty, binding.GetValue());
        }
    }
}
