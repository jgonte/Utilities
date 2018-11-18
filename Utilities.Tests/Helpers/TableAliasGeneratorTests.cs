using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilities.Tests
{
    [TestClass]
    public class TableAliasGeneratorTests
    {
        [TestMethod]
        public void Should_Return_The_Same_Alias_For_The_Same_Table_Name()
        {
            var generator = new TableAliasGenerator();

            Assert.AreEqual("e", generator.GenerateAlias("Entity"));

            Assert.AreEqual("e", generator.GenerateAlias("Entity"));
        }

        [TestMethod]
        public void Should_Return_The_Different_Aliases_For_Different_Table_Names()
        {
            var generator = new TableAliasGenerator();

            Assert.AreEqual("e", generator.GenerateAlias("Entity"));

            Assert.AreEqual("a", generator.GenerateAlias("AnotherEntity"));

            Assert.AreEqual("o", generator.GenerateAlias("OtherEntity"));
        }

        [TestMethod]
        public void Should_Return_The_Different_Aliases_For_Different_Table_Names_Starting_The_Same()
        {
            var generator = new TableAliasGenerator();

            Assert.AreEqual("e", generator.GenerateAlias("Entity"));

            Assert.AreEqual("e1", generator.GenerateAlias("Entity_Another"));

            Assert.AreEqual("e2", generator.GenerateAlias("Entity_Another_More"));
        }
    }
}
