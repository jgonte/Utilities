using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Utilities.Tests
{
    [TestClass()]
    public class EmitterTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        public interface IAccessed
        {
            string TextData { get; set; }
            int IntegerData { get; set; }
        }

        public class AccessedClass
        {
            public string TextData { get; set; }
            public int IntegerData { get; set; }
        }

        public struct AccessedStruct
            : IAccessed
        {
            public string TextData { get; set; }
            public int IntegerData { get; set; }
        }

        public struct AccessedStructNoInterface
        {
            public string TextData { get; set; }
            public int IntegerData { get; set; }
        }

        #region Setter tests

        [TestMethod()]
        public void EmitterEmitPropertySetterStringClassTest()
        {
            Type type = typeof(AccessedClass);

            PropertyInfo propertyInfo = type.GetProperty("TextData");

            Action<object, object> setter = Emitter.EmitPropertySetter(propertyInfo);

            string value = "Some text";

            AccessedClass obj = new AccessedClass();

            setter(obj, value);

            Assert.AreEqual(value, obj.TextData);
        }

        [TestMethod()]
        public void EmitterEmitPropertySetterIntegerClassTest()
        {
            Type type = typeof(AccessedClass);

            PropertyInfo propertyInfo = type.GetProperty("IntegerData");

            Action<object, object> setter = Emitter.EmitPropertySetter(propertyInfo);

            int value = 427;

            AccessedClass obj = new AccessedClass();

            setter(obj, value);

            Assert.AreEqual(value, obj.IntegerData);
        }

        [TestMethod()]
        public void EmitterEmitPropertySetterStringStructTest()
        {
            Type type = typeof(AccessedStruct);

            PropertyInfo propertyInfo = type.GetProperty("TextData");

            Action<object, object> setter = Emitter.EmitPropertySetter(propertyInfo);

            string value = "Some text";

            IAccessed obj = new AccessedStruct();

            setter(obj, value);

            Assert.AreEqual(value, obj.TextData);
        }

        [TestMethod()]
        public void EmitterEmitPropertySetterIntegerStructTest()
        {
            Type type = typeof(AccessedStruct);

            PropertyInfo propertyInfo = type.GetProperty("IntegerData");

            Action<object, object> setter = Emitter.EmitPropertySetter(propertyInfo);

            int value = 427;

            IAccessed obj = new AccessedStruct();

            setter(obj, value);

            Assert.AreEqual(value, obj.IntegerData);
        } 

        #endregion

        #region Getter tests

        [TestMethod()]
        public void EmitterEmitPropertyGetterStringClassTest()
        {
            Type type = typeof(AccessedClass);

            PropertyInfo propertyInfo = type.GetProperty("TextData");

            Func<object, object> getter = Emitter.EmitPropertyGetter(propertyInfo);

            string value = "Some text";

            AccessedClass obj = new AccessedClass();

            obj.TextData = value;

            Assert.AreEqual(value, getter(obj));
        }

        [TestMethod()]
        public void EmitterEmitPropertyGetterIntegerClassTest()
        {
            Type type = typeof(AccessedClass);

            PropertyInfo propertyInfo = type.GetProperty("IntegerData");

            Func<object, object> getter = Emitter.EmitPropertyGetter(propertyInfo);

            int value = 427;

            AccessedClass obj = new AccessedClass();

            obj.IntegerData = value;

            Assert.AreEqual(value, getter(obj));
        }

        [TestMethod()]
        public void EmitterEmitPropertyGetterStringStructTest()
        {
            Type type = typeof(AccessedStruct);

            PropertyInfo propertyInfo = type.GetProperty("TextData");

            Func<object, object> getter = Emitter.EmitPropertyGetter(propertyInfo);

            string value = "Some text";

            AccessedStruct obj = new AccessedStruct();

            obj.TextData = value;

            Assert.AreEqual(value, getter(obj));
        }

        [TestMethod()]
        public void EmitterEmitPropertyGetterIntegerStructTest()
        {
            Type type = typeof(AccessedStruct);

            PropertyInfo propertyInfo = type.GetProperty("IntegerData");

            Func<object, object> getter = Emitter.EmitPropertyGetter(propertyInfo);

            int value = 427;

            AccessedStruct obj = new AccessedStruct();

            obj.IntegerData = value;

            Assert.AreEqual(value, getter(obj));
        } 

        #endregion

        /// <summary>
        /// Tests that the emitted object comparer works as expected
        /// </summary>
        [TestMethod()]
        public void EmitterEmitObjectComparerTest()
        {
            AccessedStruct obj1 = new AccessedStruct
            {
                IntegerData = 10,
                TextData = "Text1"               
            };

            AccessedStruct obj2 = new AccessedStruct
            {
                IntegerData = 20,
                TextData = "Text2"
            };

            Type type = typeof(AccessedStruct);

            Func<object, object, List<string>> comparer = Emitter.EmitObjectComparer(type);

            List<string> diffProperties = comparer(obj1, obj2);

            Assert.AreEqual(2, diffProperties.Count);

            Assert.AreEqual("TextData", diffProperties[0]);

            Assert.AreEqual("IntegerData", diffProperties[1]);
        }

        /// <summary>
        /// Tests that the emitted object cloner works as expected for a class
        /// </summary>
        [TestMethod()]
        public void EmitterEmitObjectClonerClassTest()
        {
            AccessedClass objToClone = new AccessedClass
            {
                IntegerData = 10,
                TextData = "Text1"
            };

            Type type = typeof(AccessedClass);

            Func<object, object> cloner = Emitter.EmitObjectCloner(type);

            AccessedClass clone = (AccessedClass)cloner(objToClone);

            Assert.AreEqual(clone.IntegerData, objToClone.IntegerData);

            Assert.AreEqual(clone.TextData, objToClone.TextData);
        }

        /// <summary>
        /// Tests that the emitted object cloner works as expected for a class
        /// </summary>
        [TestMethod()]
        public void EmitterEmitObjectClonerStructTest()
        {
            AccessedStruct objToClone = new AccessedStruct
            {
                IntegerData = 10,
                TextData = "Text1"
            };

            Type type = typeof(AccessedStruct);

            Func<object, object> cloner = Emitter.EmitObjectCloner(type);

            AccessedStruct clone = (AccessedStruct)cloner(objToClone);

            Assert.AreEqual(clone.IntegerData, objToClone.IntegerData);

            Assert.AreEqual(clone.TextData, objToClone.TextData);
        }

        /// <summary>
        /// Tests that the emitted object cloner returns a null delegate for a value type that does
        /// not inherits from an interface
        /// </summary>
        [TestMethod()]
        public void EmitterEmitObjectClonerStructWithNoInterfaceTest()
        {
            AccessedStructNoInterface objToClone = new AccessedStructNoInterface
            {
                IntegerData = 10,
                TextData = "Text1"
            };

            Type type = typeof(AccessedStructNoInterface);

            Assert.IsNull(Emitter.EmitObjectCloner(type));
        }

        public class TestObject
        {
            public int MethodWithOneParameterAndResult(int n)
            {
                return n + 1;
            }

            public int MethodWithTwoParametersAndResult(int n1, int n2)
            {
                return n1 + n2;
            }

            public void MethodWithOneParameterNoResult(string s)
            {
                Debug.WriteLine(s);
            }

            public int MethodWithNoParameterAndResult()
            {
                return 26;
            }
        }

        [TestMethod()]
        public void EmitterGetMethodFromMethodInfoMethodWithOneParameterAndOneResultTest()
        {
            Type type = typeof(TestObject);

            MethodInfo methodInfo = type.GetMethod("MethodWithOneParameterAndResult");

            // Instance
            ParameterExpression instance = Expression.Parameter(type, "instance");

            // Parameters
            IList<ParameterExpression> parameters = Emitter.GetMethodParameters(methodInfo);

            MethodCallExpression call = Expression.Call(instance, methodInfo, parameters);

            List<ParameterExpression> lambdaParameters = new List<ParameterExpression>
            {
                instance
            };

            lambdaParameters.AddRange(parameters); // Add the parameters

            LambdaExpression lambda = Expression.Lambda(call, lambdaParameters);

            Func<TestObject, int, int> d = (Func<TestObject, int, int>)lambda.Compile();

            TestObject obj = new TestObject();

            Assert.AreEqual(2, d(obj, 1));
        }

        [TestMethod()]
        public void EmitterGetMethodFromMethodInfoMethodWithTwoParametersAndResultTest()
        {
            Type type = typeof(TestObject);
            MethodInfo methodInfo = type.GetMethod("MethodWithTwoParametersAndResult");

            // Instance
            ParameterExpression instance = Expression.Parameter(type, "instance");

            // Parameters
            IList<ParameterExpression> parameters = Emitter.GetMethodParameters(methodInfo);

            MethodCallExpression call = Expression.Call(instance, methodInfo, parameters);

            List<ParameterExpression> lambdaParameters = new List<ParameterExpression>
            {
                instance
            };

            lambdaParameters.AddRange(parameters); // Add the parameters

            LambdaExpression lambda = Expression.Lambda(call, lambdaParameters);

            Func<TestObject, int, int, int> d = (Func<TestObject, int, int, int>)lambda.Compile();

            TestObject obj = new TestObject();

            Assert.AreEqual(26, d(obj, 12, 14));
        }

        [TestMethod()]
        public void EmitterGetMethodFromMethodInfoMethodWithOneParameterNoResultTest()
        {
            Type type = typeof(TestObject);

            MethodInfo methodInfo = type.GetMethod("MethodWithOneParameterNoResult");

            // Instance
            ParameterExpression instance = Expression.Parameter(type, "instance");

            // Parameters
            IList<ParameterExpression> parameters = Emitter.GetMethodParameters(methodInfo);

            MethodCallExpression call = Expression.Call(instance, methodInfo, parameters);

            List<ParameterExpression> lambdaParameters = new List<ParameterExpression>
            {
                instance
            };

            lambdaParameters.AddRange(parameters); // Add the parameters

            LambdaExpression lambda = Expression.Lambda(call, lambdaParameters);

            Action<TestObject, string> d = (Action<TestObject, string>)lambda.Compile();

            TestObject obj = new TestObject();

            d(obj, "Hola");
        }

        [TestMethod()]
        public void EmitterGetMethodFromMethodInfoMethodWithNoParameterAndResultTest()
        {
            Type type = typeof(TestObject);

            MethodInfo methodInfo = type.GetMethod("MethodWithNoParameterAndResult");

            // Instance
            ParameterExpression instance = Expression.Parameter(type, "instance");

            // Parameters
            IList<ParameterExpression> parameters = Emitter.GetMethodParameters(methodInfo);

            MethodCallExpression call = Expression.Call(instance, methodInfo, parameters);

            List<ParameterExpression> lambdaParameters = new List<ParameterExpression>
            {
                instance
            };

            lambdaParameters.AddRange(parameters); // Add the parameters

            LambdaExpression lambda = Expression.Lambda(call, lambdaParameters);

            Func<TestObject, int> d = (Func<TestObject, int>)lambda.Compile();

            TestObject obj = new TestObject();

            Assert.AreEqual(26, d(obj));
        }

        [TestMethod()]
        public void EmitterTestDelegateCallTest()
        {
            Action<int> func = i => Console.WriteLine(i * i);

            var callExpr = Expression.Call(func.Method, Expression.Constant(5));

            var lambdaExpr = Expression.Lambda<Action>(callExpr);

            var fn = lambdaExpr.Compile();

            fn();    //  Prints 25
        }
    }
}
