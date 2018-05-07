using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.Tests
{
    [TestClass()]
    public class TypeExtensionsTest
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

        public struct ComplexStruct
        {
        }

        public class TypeExtensionsTestObject
        {
            public int?[] NullableIntArray
            {
                get;
                set;
            }

            public ComplexStruct?[] ComplexTypeArray
            {
                get;
                set;
            }

            public List<Guid?> NullableGuidList
            {
                get;
                set;
            }

            public Dictionary<string, DateTime?> NullableDateTimeDictionary
            {
                get;
                set;
            }
        }

        [TestMethod()]
        public void TypeExtensionsUnwrapTypeTest()
        {
            Type type = typeof(TypeExtensionsTestObject);

            PropertyInfo propertyInfo = type.GetProperty("NullableIntArray");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), propertyType.UnwrapType());

            propertyInfo = type.GetProperty("ComplexTypeArray");
            propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(ComplexStruct), propertyType.UnwrapType());

            propertyInfo = type.GetProperty("NullableGuidList");
            propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(Guid), propertyType.UnwrapType());

            propertyInfo = type.GetProperty("NullableDateTimeDictionary");
            propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(DateTime), propertyType.UnwrapType());
        }
    }
}
