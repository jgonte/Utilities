using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Utilities.Tests
{
    [TestClass()]
    public class NullableTypeTest
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

        public struct StructType
        {
        }

        public class ClassType
        {
        }

        public class NullableTestObject
        {
            public int? NullableIntProperty
            {
                get;
                set;
            }

            public StructType? NullableStructProperty
            {
                get;
                set;
            }

            // The following code does not compile
            // Must be a non-nullable value type in order to use it as parameter 'T' in the generic type or method 'System.Nullable<T>
            //public ClassType? NullableClassProperty
            //{
            //    get;
            //    set;
            //}
        }

        [TestMethod()]
        public void NullableTypeGetNullableTypeTest()
        {
            Type ownerObject = typeof(NullableTestObject);
            PropertyInfo propertyInfo = ownerObject.GetProperty("NullableIntProperty");
            Type propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsNullable());
            Type innerType = propertyType.GetNullableType();
            Assert.AreEqual(typeof(int), innerType);

            propertyInfo = ownerObject.GetProperty("NullableStructProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsNullable());
            innerType = propertyType.GetNullableType();
            Assert.AreEqual(typeof(StructType), innerType);
        }
    }
}
