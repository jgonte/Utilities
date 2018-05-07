using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Utilities.Tests
{
    [TestClass()]
    public class ProxyFactoryTest
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

        public class TestObject
        {
            public string GetString (string s)
            {
                return s;
            }
        }

        [TestMethod()]
        public void ProxyFactoryCreateProxyTest()
        {
            ProxyFactory factory = new ProxyFactory
            {
                Type = typeof(TestObject),
                Interceptors = new ProxyFactory.Interceptor[]
                {
                    new ProxyFactory.Interceptor
                    {
                        MethodInfo = typeof(TestObject).GetMethod("GetString"),
                        BeforeInvoke = (parameters) =>
                        {
                            Debug.WriteLine(string.Format("Parameter: {0}", parameters[0]));

                            return true;
                        },
                        AfterInvoke = (parameters, result) =>
                        {
                            Debug.WriteLine(string.Format("Result: {0}", result));
                        }
                    }
                }
            };

            TestObject proxy = (TestObject)factory.CreateProxy();

            Assert.AreEqual("hola", proxy.GetString("hola"));
        }
    }
}
