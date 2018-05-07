using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace Utilities.Tests
{
    [TestClass()]
    public class ExpressionHelperTest
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
        public void ExpressionHelperCastTest()
        {
            Expression from = Expression.Constant(34, typeof(int));
            Type to = typeof(int?);
            UnaryExpression cast = ExpressionHelper.Cast(from, to);

            Assert.AreEqual("Convert(34)", cast.ToString());
        }

        [TestMethod()]
        public void ExpressionHelperCastToValueTest()
        {
            Expression from = Expression.Constant(34, typeof(int));
            Type to = typeof(string);
            UnaryExpression cast = ExpressionHelper.Cast(from, to);

            Assert.AreEqual("(34 As String)", cast.ToString());
        }
    }
}
