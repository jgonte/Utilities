using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Utilities.Tests
{
    [TestClass()]
    public class PropertyExtensionsTest
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

        // Test objects
        class Derived2
            : Derived
        {
            public new int P3 { get; set; } // Hidden in derived2
        }

        class Derived
            : Base
        {
            public new int P2 { get; set; } // Hidden in derived
        }

        class Base
        {
            public int P1 { get; set; } // Not hidden
            public int P2 { get; set; } // Hidden in derived
            public int P3 { get; set; } // Hidden in derived2
        }

        /// <summary>
        /// Tests that the is hiding extension method works for hidden properties
        ///</summary>
        [TestMethod()]
        public void PropertyExtensionsIsHidingPropertyTest()
        {
            // Test the base type
            Type baseType = typeof(Base);

            PropertyInfo baseP1 = baseType.GetProperty("P1");
            Assert.IsTrue(baseP1.GetGetMethod().IsHideBySig); // Not true
            Assert.IsFalse(baseP1.IsHidingProperty());
            Assert.IsFalse(baseP1.IsVirtual());

            PropertyInfo baseP2 = baseType.GetProperty("P2");
            Assert.IsFalse(baseP2.IsHidingProperty());
            Assert.IsFalse(baseP2.IsVirtual());

            PropertyInfo baseP3 = baseType.GetProperty("P3");
            Assert.IsFalse(baseP3.IsHidingProperty());
            Assert.IsFalse(baseP3.IsVirtual());

            // Test the derived type
            Type derivedType = typeof(Derived);

            PropertyInfo derivedP2 = derivedType.GetProperty("P2");
            Assert.IsTrue(derivedP2.IsHidingProperty());
            Assert.IsFalse(derivedP2.IsVirtual());

            // Test the derived 2 type
            Type derived2Type = typeof(Derived2);

            PropertyInfo derived2P3 = derived2Type.GetProperty("P3");
            Assert.IsTrue(derived2P3.IsHidingProperty());
            Assert.IsFalse(derived2P3.IsVirtual());
        }

        // Test objects
        class DerivedV2
            : DerivedV
        {
            public override int P3 { get; set; } // Hidden in derived2
        }

        class DerivedV
            : BaseV
        {
            public override int P2 { get; set; } // Hidden in derived
        }

        class BaseV
        {
            public int P1 { get; set; } // Not hidden
            public virtual int P2 { get; set; } // Hidden in derived
            public virtual int P3 { get; set; } // Hidden in derived2
        }

        /// <summary>
        /// Tests that the is hiding extension method works for hidden properties
        ///</summary>
        [TestMethod()]
        public void PropertyExtensionsIsHidingOverridenPropertyTest()
        {
            // Test the base type
            Type baseType = typeof(BaseV);

            PropertyInfo baseP1 = baseType.GetProperty("P1");
            Assert.IsTrue(baseP1.GetGetMethod().IsHideBySig); // Not what we want
            Assert.IsFalse(baseP1.IsHidingProperty());
            Assert.IsFalse(baseP1.IsVirtual());

            PropertyInfo baseP2 = baseType.GetProperty("P2");
            Assert.IsFalse(baseP2.IsHidingProperty());
            Assert.IsTrue(baseP2.IsVirtual());

            PropertyInfo baseP3 = baseType.GetProperty("P3");
            Assert.IsFalse(baseP3.IsHidingProperty());
            Assert.IsTrue(baseP3.IsVirtual());

            // Test the derived type
            Type derivedType = typeof(DerivedV);

            PropertyInfo derivedP2 = derivedType.GetProperty("P2");
            Assert.IsTrue(derivedP2.IsHidingProperty());
            Assert.IsTrue(derivedP2.IsVirtual());

            // Test the derived 2 type
            Type derived2Type = typeof(DerivedV2);

            PropertyInfo derived2P3 = derived2Type.GetProperty("P3");
            Assert.IsTrue(derived2P3.IsHidingProperty());
            Assert.IsTrue(derived2P3.IsVirtual());
        }
    }
}

