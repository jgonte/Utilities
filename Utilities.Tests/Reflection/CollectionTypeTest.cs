using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.Tests
{
    [TestClass()]
    public class CollectionTypeTest
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

        public class CollectionTestObject
        {
            public int[] ArrayIntProperty { get; set; }

            public StructType[] ArrayStructProperty { get; set; }

            public ClassType[] ArrayClassProperty { get; set; }

            public List<int> ListIntProperty { get; set; }

            public List<StructType> ListStructProperty { get; set; }

            public IList<StructType> IListStructProperty { get; set; } // Interface

            public List<ClassType> ListClassProperty { get; set; }

            public IList<ClassType> IListClassProperty { get; set; } // Interface

            public Dictionary<int, int> DictionaryIntProperty { get; set; }

            public Dictionary<int, StructType> DictionaryStructProperty { get; set; }

            public IDictionary<int, StructType> IDictionaryStructProperty { get; set; } // Interface

            public Dictionary<int, ClassType> DictionaryClassProperty { get; set; }

            public Dictionary<int, ClassType> IDictionaryClassProperty { get; set; } // Interface

            public ArrayList ArrayListProperty { get; set; }
        }

        [TestMethod()]
        public void CollectionTypeGetCollectionItemTypeTest()
        {
            Type collectionType = typeof(CollectionTestObject);

            // Array of primitives
            PropertyInfo propertyInfo = collectionType.GetProperty("ArrayIntProperty");
            Type propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Array, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(int), propertyType.GetItemType());

            // Array of structures
            propertyInfo = collectionType.GetProperty("ArrayStructProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Array, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(StructType), propertyType.GetItemType());

            // Array of classes
            propertyInfo = collectionType.GetProperty("ArrayClassProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Array, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(ClassType), propertyType.GetItemType());

            // List of primitives
            propertyInfo = collectionType.GetProperty("ListIntProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.List, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(int), propertyType.GetItemType());

            // List of structures
            propertyInfo = collectionType.GetProperty("ListStructProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.List, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(StructType), propertyType.GetItemType());

            // Interface list of structures
            propertyInfo = collectionType.GetProperty("IListStructProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.List, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(StructType), propertyType.GetItemType());

            // List of classes
            propertyInfo = collectionType.GetProperty("ListClassProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.List, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(ClassType), propertyType.GetItemType());

            // Interface list of classes
            propertyInfo = collectionType.GetProperty("IListClassProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.List, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(ClassType), propertyType.GetItemType());

            // Dictionary of primitives
            propertyInfo = collectionType.GetProperty("DictionaryIntProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Dictionary, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(int), propertyType.GetItemType());

            // Dictionary of structures
            propertyInfo = collectionType.GetProperty("DictionaryStructProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Dictionary, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(StructType), propertyType.GetItemType());

            // Interface dictionary of structures
            propertyInfo = collectionType.GetProperty("IDictionaryStructProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Dictionary, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(StructType), propertyType.GetItemType());

            // Dictionary of classes
            propertyInfo = collectionType.GetProperty("DictionaryClassProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Dictionary, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(ClassType), propertyType.GetItemType());

            // Interface dictionary of classes
            propertyInfo = collectionType.GetProperty("IDictionaryClassProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.Dictionary, propertyType.GetCollectionType());
            Assert.AreEqual(typeof(ClassType), propertyType.GetItemType());

            // ArrayList property
            propertyInfo = collectionType.GetProperty("ArrayListProperty");
            propertyType = propertyInfo.PropertyType;

            Assert.IsTrue(propertyType.IsCollection());
            Assert.AreEqual(CollectionTypes.List, propertyType.GetCollectionType());

            // This one throws an exception since we can not determine the type of an array list
            //Assert.AreEqual(typeof(ClassType), propertyType.GetItemType());
        }
    }
}
