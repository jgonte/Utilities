using System;
using System.Reflection;
using Utilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class PropertyAccessorTest
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

            public char CharacterData { get; set; }
        }

        public interface IAccessed
        {
            string TextData { get; set; }

            int IntegerData { get; set; }

            char CharacterData { get; set; }
        }

        public struct AccessedStruct
            : IAccessed
        {
            public string TextData { get; set; }

            public int IntegerData { get; set; }

            public char CharacterData { get; set; }
        }

        [TestMethod()]
        public void PropertyAccessorClassGetValueStringTest()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("TextData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = "Some text";

            AccessedClass obj = new AccessedClass();
            obj.TextData = value;

            Assert.AreEqual(value, accessor.GetValue(obj));
        }

        [TestMethod()]
        public void PropertyAccessorClassGetValueIntTest()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("IntegerData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = 427;

            AccessedClass obj = new AccessedClass();
            obj.IntegerData = value;

            Assert.AreEqual(value, accessor.GetValue(obj));
        }

        [TestMethod()]
        public void PropertyAccessorClassGetValueCharTest()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("CharacterData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = 'M';

            AccessedClass obj = new AccessedClass();
            obj.CharacterData = value;

            Assert.AreEqual(value, accessor.GetValue(obj));
        }


        [TestMethod()]
        public void PropertyAccessorClassBindValueIntLambdaExpressionTest()
        {
            AccessedClass obj = new AccessedClass();
            PropertyAccessor accessor = PropertyAccessor.GetAccessor(() => obj.IntegerData);

            obj.IntegerData = 1907;

            Assert.AreEqual(obj.IntegerData, accessor.GetValue(obj));
        }

        [TestMethod()]
        public void PropertyAccessorClassSetValueStringTest()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("TextData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            string value = "Some text";

            AccessedClass obj = new AccessedClass();
            Assert.IsNull(obj.TextData);

            accessor.SetValue(obj, value);
            Assert.AreEqual(value, obj.TextData);
        }

        [TestMethod()]
        public void PropertyAccessorClassSetValueIntTest()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("IntegerData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = 427;

            AccessedClass obj = new AccessedClass();
            accessor.SetValue(obj, value);
            Assert.AreEqual(value, obj.IntegerData);
        }

        [TestMethod()]
        public void PropertyAccessorClassSetValueInt_Convert_From_String_Test()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("IntegerData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = "427";

            AccessedClass obj = new AccessedClass();
            accessor.SetValue(obj, value);
            Assert.AreEqual(427, obj.IntegerData);
        }

        [TestMethod()]
        public void PropertyAccessorClassSetValueCharTest()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("CharacterData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = 'F';

            AccessedClass obj = new AccessedClass();
            accessor.SetValue(obj, value);
            Assert.AreEqual(value, obj.CharacterData);
        }

        [TestMethod()]
        public void PropertyAccessorClassSetValueChar_Convert_From_String_Test()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("CharacterData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = "F";

            AccessedClass obj = new AccessedClass();
            accessor.SetValue(obj, value);
            Assert.AreEqual('F', obj.CharacterData);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PropertyAccessorClassSetValueChar_Convert_From_String_Too_Long_Value_Test()
        {
            Type type = typeof(AccessedClass);
            PropertyInfo propertyInfo = type.GetProperty("CharacterData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            var value = "MF";

            AccessedClass obj = new AccessedClass();
            accessor.SetValue(obj, value);
            Assert.IsNull(obj.CharacterData);
        }

        [TestMethod()]
        public void PropertyAccessorStructGetValueStringTest()
        {
            Type type = typeof(AccessedStruct);
            PropertyInfo propertyInfo = type.GetProperty("TextData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            string value = "Some text";

            AccessedStruct obj = new AccessedStruct();
            obj.TextData = value;

            Assert.AreEqual(value, accessor.GetValue(obj));
        }

        [TestMethod()]
        public void PropertyAccessorStructGetValueIntTest()
        {
            Type type = typeof(AccessedStruct);
            PropertyInfo propertyInfo = type.GetProperty("IntegerData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            int value = 427;

            AccessedStruct obj = new AccessedStruct();
            obj.IntegerData = value;

            Assert.AreEqual(value, accessor.GetValue(obj));
        }

        [TestMethod()]
        public void PropertyAccessorStructSetValueStringTest()
        {
            Type type = typeof(AccessedStruct);
            PropertyInfo propertyInfo = type.GetProperty("TextData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            string value = "Some text";

            AccessedStruct obj = new AccessedStruct();
            Assert.IsNull(obj.TextData);

            accessor.SetValue(obj, value);
            Assert.IsNull(obj.TextData); // It does not set the value
        }

        [TestMethod()]
        public void PropertyAccessorStructSetValueIntTest()
        {
            Type type = typeof(AccessedStruct);
            PropertyInfo propertyInfo = type.GetProperty("IntegerData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            int value = 427;

            AccessedStruct obj = new AccessedStruct();
            accessor.SetValue(obj, value);
            Assert.AreEqual(0, obj.IntegerData); // It does not set the value
        }

        [TestMethod()]
        public void PropertyAccessorStructSetValueStringUsingInterfaceTest()
        {
            Type type = typeof(AccessedStruct);
            PropertyInfo propertyInfo = type.GetProperty("TextData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            string value = "Some text";

            IAccessed obj = new AccessedStruct();
            Assert.IsNull(obj.TextData);

            accessor.SetValue(obj, value);
            Assert.AreEqual(value, obj.TextData);
        }

        [TestMethod()]
        public void PropertyAccessorStructSetValueIntUsingInterfaceTest()
        {
            Type type = typeof(AccessedStruct);
            PropertyInfo propertyInfo = type.GetProperty("IntegerData");
            PropertyAccessor accessor = new PropertyAccessor(propertyInfo);

            Assert.IsTrue(accessor.CanGet);
            Assert.IsTrue(accessor.CanSet);
            Assert.AreEqual(propertyInfo.PropertyType, accessor.PropertyType);

            int value = 427;

            IAccessed obj = new AccessedStruct();
            accessor.SetValue(obj, value);
            Assert.AreEqual(value, obj.IntegerData);
        }
    }
}
