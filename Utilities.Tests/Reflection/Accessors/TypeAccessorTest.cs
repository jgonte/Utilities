using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Utilities.Tests
{
    [TestClass()]
    public class TypeAccessorTest
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

        public class AccessedClass
        {
            public string TextData { get; set; }
            public int IntegerData { get; set; }
            public NestedDataClass NestedData { get; set; }
        }

        public class NestedDataClass
        {
            public string NestedTextData { get; set; }
            public DateTime NestedDateTimeData { get; set; }
        }

        public interface IAccessed
        {
            string TextData { get; set; }
            int IntegerData { get; set; }
        }

        public struct AccessedStruct
            : IAccessed
        {
            public string TextData { get; set; }
            public int IntegerData { get; set; }
        }

        [TestMethod()]
        public void TypeAccessorClassSetValueTest()
        {
            AccessedClass obj = new AccessedClass();
            TypeAccessor accessor = obj.GetTypeAccessor();

            accessor.SetValue(obj, "TextData", "Some text");
            accessor.SetValue(obj, "IntegerData", 427);
            accessor.SetValue(obj, "NestedData.NestedTextData", "Some nested text");
            accessor.SetValue(obj, "NestedData.NestedDateTimeData", new DateTime(1928, 5, 24));

            Assert.AreEqual("Some text", obj.TextData);
            Assert.AreEqual(427, obj.IntegerData);
            Assert.AreEqual("Some nested text", obj.NestedData.NestedTextData);
            Assert.AreEqual(new DateTime(1928, 5, 24), obj.NestedData.NestedDateTimeData);
        }

        [TestMethod()]
        public void TypeAccessorClassGetValueTest()
        {
            AccessedClass obj = new AccessedClass();
            TypeAccessor accessor = obj.GetTypeAccessor();
            obj.TextData = "Some text";
            obj.IntegerData = 427;

            Assert.AreEqual("Some text", accessor.GetValue(obj, "TextData"));
            Assert.AreEqual(427, accessor.GetValue(obj, "IntegerData"));
        }

        [TestMethod()]
        public void TypeAccessorStructSetValueTest()
        {
            AccessedStruct obj = new AccessedStruct();
            TypeAccessor accessor = obj.GetTypeAccessor();
            accessor.SetValue(obj, "TextData", "Some text");
            accessor.SetValue(obj, "IntegerData", 427);

            Assert.IsNull(obj.TextData);
            Assert.AreEqual(0, obj.IntegerData);
        }

        [TestMethod()]
        public void TypeAccessorStructSetValueUsingInterfaceTest()
        {
            IAccessed obj = new AccessedStruct();
            TypeAccessor accessor = obj.GetTypeAccessor();
            accessor.SetValue(obj, "TextData", "Some text");
            accessor.SetValue(obj, "IntegerData", 427);

            Assert.AreEqual("Some text", obj.TextData);
            Assert.AreEqual(427, obj.IntegerData);
        }

        [TestMethod()]
        public void TypeAccessorStructGetValueTest()
        {
            AccessedStruct obj = new AccessedStruct();
            TypeAccessor accessor = obj.GetTypeAccessor();
            obj.TextData = "Some text";
            obj.IntegerData = 427;

            Assert.AreEqual("Some text", accessor.GetValue(obj, "TextData"));
            Assert.AreEqual(427, accessor.GetValue(obj, "IntegerData"));
        }

        public class PropertyChangedSubscriber
            : IPropertyChangedSuscriber
        {
            public PropertyChangedSubscriber()
            {
                ChangedProperties = new List<string>();
            }

            public IList<string> ChangedProperties { get; set; }

            #region IPropertyChangedWatcher Members

            public void OnPropertyChanged(object o, string propertyName, object oldValue, object newValue)
            {
                ChangedProperties.Add(propertyName);
                Debug.WriteLine(string.Format("{0}, {1}, {2}, {3}", o, propertyName, oldValue, newValue));
            }

            #endregion
        }

        [TestMethod()]
        public void TypeAccessorClassSetValueAndNotifyChangedTest()
        {
            PropertyChangedSubscriber subscriber = new PropertyChangedSubscriber();
            AccessedClass obj = new AccessedClass();

            TypeAccessor accessor = obj.GetTypeAccessor();
            
            accessor.SetValueAndNotifyChange(obj, "TextData", "Some text", subscriber);
            accessor.SetValueAndNotifyChange(obj, "IntegerData", 427, subscriber);

            Assert.AreEqual("Some text", obj.TextData);
            Assert.AreEqual(427, obj.IntegerData);

            Assert.AreEqual(2, subscriber.ChangedProperties.Count);
        }
    }
}
