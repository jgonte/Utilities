using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class CollectionExtensionsTest
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
        public void ForTest()
        {
            IList<int> list = new List<int>
            {
                1, 2, 3, 4
            };

            int total = 0;
            int totalIndex = 0;
            list.For((item, index) =>
            {
                total += item;
                totalIndex += index;
            });

            Assert.AreEqual(10, total);
            Assert.AreEqual(6, totalIndex);
        }

        [TestMethod()]
        public void JoinTest()
        {
            IList<int> list = new List<int>
            {
                1, 2, 3, 4
            };

            StringBuilder builder = new StringBuilder();
            string separator = ";+";
            list.Join(builder, separator, (b, o, i) =>
            {
                b.Append(o);
                b.AppendLine();
                b.Append(i);
                b.AppendLine();
            });

            string s = builder.ToString();
            Assert.AreEqual(
@"1
0
;+2
1
;+3
2
;+4
3
"
, s);
        }
    }
}
