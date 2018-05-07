using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class ObjectTraversalTest
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

        /// <summary>
        /// Tests that the OnNUll method gets called when the object is null
        /// </summary>
        [TestMethod()]
        public void ObjectTraversalTraverseNullObjectTest()
        {
            bool isNull = false;

            ObjectTraversal traversal = new ObjectTraversal
            {
                OnNullObject = (propertyName) =>
                {
                    isNull = true;
                }
            };

            traversal.Traverse(null);

            Assert.IsTrue(isNull);
        }

        // Tests whether the object was visited
        public class Parent
        {
            public Child Child { get; set; }
        }

        public class Child
        {
            public Parent Parent { get; set; }
        }

        [TestMethod]
        public void ObjectTraversalTraverseVisitedObjectTest()
        {
            bool isVisited = false;

            ObjectTraversal traversal = new ObjectTraversal
            {
                OnVisitedObject = (o, p) =>
                {
                    isVisited = true;
                }
            };

            Parent parent = new Parent();
            Child child = new Child();
            parent.Child = child;
            child.Parent = parent;

            traversal.Traverse(parent);

            Assert.IsTrue(isVisited);

            isVisited = false;

            traversal.Traverse(child);

            Assert.IsTrue(isVisited);
        }
    }
}
