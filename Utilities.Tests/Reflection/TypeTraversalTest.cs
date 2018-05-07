using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utilities.Tests
{
    [TestClass()]
    public class TypeTraversalTest
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

        #region TestObjects

        public class SimpleType
        {
            public void AMethod()
            {
            }

            public string StringProperty
            {
                get;
                set;
            }

            public int integerField;

            public enum AnEnumeration
            {
                EnumerationItem1,
                EnumerationItem2
            }

            public Func<int, int> DelegateProperty { get; set; }
        } 

        #endregion

        [TestMethod()]
        public void TypeTraversalTraverseTest()
        {
            string fullName = string.Empty;
            Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
            Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo>();
            Dictionary<string, PropertyInfo> delegateProperties = new Dictionary<string, PropertyInfo>();
            Dictionary<string, FieldInfo> fields = new Dictionary<string, FieldInfo>();
            Dictionary<string, Type> nestedTypes = new Dictionary<string, Type>();

            Type type = typeof(SimpleType);

            TypeTraversal typeTraversal = new TypeTraversal
            {
                // Excluded types
                IsExcludedType = t => { return false; }, // No types are excluded
                // OnType handler
                OnBeginType = (trav, t) =>
                {
                    fullName = t.FullName;
                    return true;
                },
                // OnMethod handler
                OnMethod = (trav, t, methodInfo) =>
                {
                    methods.Add(methodInfo.Name, methodInfo);
                },
                // OnProperty handler
                OnProperty = (trav, t, propertyInfo) =>
                {
                    properties.Add(propertyInfo.Name, propertyInfo);
                },
                // OnProperty delegate handler
                OnPropertyDelegate = (trav, t, propertyInfo) =>
                {
                    delegateProperties.Add(propertyInfo.Name, propertyInfo);
                },
                // OnField handler
                OnField = (trav, t, fieldInfo) =>
                {
                    fields.Add(fieldInfo.Name, fieldInfo);
                },
                // Nested type handler
                OnNestedType = (trav, nestedType) =>
                {
                    nestedTypes.Add(nestedType.Name, nestedType);
                }
            };
            
            typeTraversal.Traverse(type);

            Assert.AreEqual(type.FullName, fullName);

            Assert.IsNotNull(methods["AMethod"]);
            Assert.IsNotNull(properties["StringProperty"]);
            Assert.IsNotNull(delegateProperties["DelegateProperty"]);
            Assert.IsNotNull(fields["integerField"]);
            Assert.IsNotNull(nestedTypes["AnEnumeration"]);
        }
    }
}
