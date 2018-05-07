using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass()]
    public class PluralizerTest
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
        public void PluralizeTest()
        {
            // Unpluralizables
            Assert.AreEqual("Equipment", Pluralizer.Pluralize("Equipment"));
            Assert.AreEqual("equipment", Pluralizer.Pluralize("equipment"));

            Assert.AreEqual("Information", Pluralizer.Pluralize("Information"));
            Assert.AreEqual("information", Pluralizer.Pluralize("information"));

            Assert.AreEqual("Rice", Pluralizer.Pluralize("Rice"));
            Assert.AreEqual("rice", Pluralizer.Pluralize("rice"));

            Assert.AreEqual("Money", Pluralizer.Pluralize("Money"));
            Assert.AreEqual("money", Pluralizer.Pluralize("money"));

            Assert.AreEqual("Species", Pluralizer.Pluralize("Species"));
            Assert.AreEqual("species", Pluralizer.Pluralize("species"));

            Assert.AreEqual("Series", Pluralizer.Pluralize("Series"));
            Assert.AreEqual("series", Pluralizer.Pluralize("series"));

            Assert.AreEqual("Fish", Pluralizer.Pluralize("Fish"));
            Assert.AreEqual("fish", Pluralizer.Pluralize("fish"));

            Assert.AreEqual("Sheep", Pluralizer.Pluralize("Sheep"));
            Assert.AreEqual("sheep", Pluralizer.Pluralize("sheep"));

            Assert.AreEqual("Deer", Pluralizer.Pluralize("Deer"));
            Assert.AreEqual("deer", Pluralizer.Pluralize("deer"));

            // Adjectives are also unpluralizables
            Assert.AreEqual("Derived", Pluralizer.Pluralize("Derived"));
            Assert.AreEqual("Broken", Pluralizer.Pluralize("Broken"));
            
            // Specific rules
            Assert.AreEqual("People", Pluralizer.Pluralize("Person"));
            Assert.AreEqual("people", Pluralizer.Pluralize("person"));
            Assert.AreEqual("Salespeople", Pluralizer.Pluralize("Salesperson"));
            Assert.AreEqual("SalesPeople", Pluralizer.Pluralize("SalesPerson"));

            Assert.AreEqual("Oxen", Pluralizer.Pluralize("Ox"));
            Assert.AreEqual("oxen", Pluralizer.Pluralize("ox"));

            Assert.AreEqual("Children", Pluralizer.Pluralize("Child"));
            Assert.AreEqual("children", Pluralizer.Pluralize("child"));
            Assert.AreEqual("Stepchildren", Pluralizer.Pluralize("Stepchild"));
            Assert.AreEqual("StepChildren", Pluralizer.Pluralize("StepChild"));

            Assert.AreEqual("Teeth", Pluralizer.Pluralize("Tooth"));
            Assert.AreEqual("teeth", Pluralizer.Pluralize("tooth"));

            Assert.AreEqual("Geese", Pluralizer.Pluralize("Goose"));
            Assert.AreEqual("geese", Pluralizer.Pluralize("goose"));

            Assert.AreEqual("Octopi", Pluralizer.Pluralize("Octopus"));
            Assert.AreEqual("octopi", Pluralizer.Pluralize("octopus"));

            Assert.AreEqual("Viri", Pluralizer.Pluralize("Virus"));
            Assert.AreEqual("viri", Pluralizer.Pluralize("virus"));

            Assert.AreEqual("Mice", Pluralizer.Pluralize("Mouse"));
            Assert.AreEqual("mice", Pluralizer.Pluralize("mouse"));

            // General rules
            Assert.AreEqual("sausages", Pluralizer.Pluralize("sausage"));
            Assert.AreEqual("Sausages", Pluralizer.Pluralize("Sausage"));

            Assert.AreEqual("Statuses", Pluralizer.Pluralize("Status"));
            Assert.AreEqual("statuses", Pluralizer.Pluralize("status"));

            Assert.AreEqual("Axes", Pluralizer.Pluralize("Ax"));
            Assert.AreEqual("axes", Pluralizer.Pluralize("ax"));

            Assert.AreEqual("Infos", Pluralizer.Pluralize("Info"));
            Assert.AreEqual("infos", Pluralizer.Pluralize("info"));

            Assert.AreEqual("Wives", Pluralizer.Pluralize("Wife"));
            Assert.AreEqual("wives", Pluralizer.Pluralize("wife"));

            Assert.AreEqual("Wolves", Pluralizer.Pluralize("Wolf"));
            Assert.AreEqual("wolves", Pluralizer.Pluralize("wolf"));

            Assert.AreEqual("Quizzes", Pluralizer.Pluralize("Quiz"));
            Assert.AreEqual("quizzes", Pluralizer.Pluralize("quiz"));

            Assert.AreEqual("Days", Pluralizer.Pluralize("Day"));
            Assert.AreEqual("days", Pluralizer.Pluralize("day"));

            Assert.AreEqual("Matrices", Pluralizer.Pluralize("Matrix"));
            Assert.AreEqual("matrices", Pluralizer.Pluralize("matrix"));

            Assert.AreEqual("Skies", Pluralizer.Pluralize("Sky"));
            Assert.AreEqual("skies", Pluralizer.Pluralize("sky"));
        }
    }
}
