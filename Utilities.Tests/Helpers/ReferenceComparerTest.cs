using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class ReferenceComparerTest
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

        public class SimpleObject
        {
            // Some property
            public int A { get; set; }
        }

        /// <summary>
        /// Tests that if the object does not override the GetHashCode method,
        /// modifying the object allows to find the instance in the hash set
        /// </summary>
        [TestMethod]
        public void HashSetOfObjectsSimpleObjectTest()
        {
            // Create an object and add it to the hash set
            SimpleObject obj = new SimpleObject
            {
                A = 2
            };


            HashSet<object> hs = new HashSet<object>();
            hs.Add(obj);

            Assert.IsTrue(hs.Contains(obj));

            // Modify the object
            obj.A = 33;

            Assert.IsTrue(hs.Contains(obj)); // We can still find the instance of the object
        }

        public class SimpleObjectWithOverridenEquals
        {
            // Some property
            public int A { get; set; }

            public override bool Equals(object obj)
            {
                SimpleObjectWithOverridenEquals o = obj as SimpleObjectWithOverridenEquals;

                if (o == null)
                {
                    return false;
                }

                return A == o.A;
            }

            public override int GetHashCode()
            {
                return A;
            }
        }

        /// <summary>
        /// Tests that if the object does override the GetHashCode method,
        /// modifying the object will not allow us to find the instance in the hash set
        /// </summary>
        [TestMethod]
        public void HashSetOfObjectsSimpleObjectWithOverridenEqualsTest()
        {
            // Create an object and add it to the hash set
            SimpleObjectWithOverridenEquals obj = new SimpleObjectWithOverridenEquals
            {
                A = 2
            };

            HashSet<object> hs = new HashSet<object>();
            hs.Add(obj);

            Assert.IsTrue(hs.Contains(obj));

            // Modify the object
            obj.A = 33;

            Assert.IsFalse(hs.Contains(obj)); // Modified objects will have a different hash code
        }

        /// <summary>
        /// Tests that by using the ReferenceComparer object in the hash set, even if the object overrides the GetHashCode method
        /// it will be found in the hash set after modifications
        /// </summary>
        [TestMethod]
        public void HashSetOfObjectsSimpleObjectWithOverridenEqualsButWithAReferenceEqualityComparerTest()
        {
            // Create an object and add it to the hash set
            SimpleObjectWithOverridenEquals obj = new SimpleObjectWithOverridenEquals
            {
                A = 2
            };

            HashSet<object> hs = new HashSet<object>(ReferenceComparer.Instance);
            hs.Add(obj);

            Assert.IsTrue(hs.Contains(obj));

            // Modify the object
            obj.A = 33;

            Assert.IsTrue(hs.Contains(obj)); // Modified objects will have a different hash code, but they are still found
        }
    }
}
