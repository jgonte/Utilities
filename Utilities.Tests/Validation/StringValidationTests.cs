using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Utilities.Validation;

namespace Utilities.Tests.Validation
{
    [TestClass]
    public class StringValidationTests
    {
        [TestMethod]
        public void When_String_Is_Empty_It_Should_Emit_Message()
        {
            var result = new ValidationResult();

            var str = string.Empty;

            str.ValidateRequired(result, nameof(str));

            Assert.IsFalse(result.IsValid);

            var validationError = result.Errors.Single();

            Assert.AreEqual("str", validationError.PropertyName);

            Assert.AreEqual("Value of 'str' cannot be empty", validationError.Message);
        }

        [TestMethod]
        public void When_String_Is_Null_It_Should_Emit_Message()
        {
            var result = new ValidationResult();

            string str = null;

            str.ValidateRequired(result, nameof(str));

            Assert.IsFalse(result.IsValid);

            var validationError = result.Errors.Single();

            Assert.AreEqual("str", validationError.PropertyName);

            Assert.AreEqual("Value of 'str' cannot be empty", validationError.Message);
        }

        [TestMethod]
        public void When_String_Is_Empty_Default_Message_Can_Be_Overriden()
        {
            var result = new ValidationResult();

            var str = string.Empty;

            str.ValidateRequired(result, nameof(str), "Please provide a value for str");

            Assert.IsFalse(result.IsValid);

            var validationError = result.Errors.Single();

            Assert.AreEqual("str", validationError.PropertyName);

            Assert.AreEqual("Please provide a value for str", validationError.Message);
        }

        [TestMethod]
        public void When_String_Is_Not_Empty_It_Should_Not_Emit_Message()
        {
            var result = new ValidationResult();

            string str = "some text";

            str.ValidateRequired(result, nameof(str));

            Assert.IsTrue(result.IsValid);

            Assert.IsTrue(!result.Errors.Any());
        }

        [TestMethod]
        public void When_String_Is_Greater_Than_Max_Length_It_Should_Emit_Message()
        {
            var result = new ValidationResult();

            var str = "abcd";

            var maxLength = (uint)3;

            str.ValidateMaxLength(result, nameof(str), maxLength);

            Assert.IsFalse(result.IsValid);

            var validationError = result.Errors.Single();

            Assert.AreEqual("str", validationError.PropertyName);

            Assert.AreEqual("Value of 'str' cannot exceed '3' characters. Length: '4'", validationError.Message);
        }

        [TestMethod]
        public void When_String_Is_Less_Or_Equal_Than_Max_Length_It_Should_Not_Emit_Message()
        {
            var result = new ValidationResult();

            var str = "abc";

            var maxLength = (uint)3;

            str.ValidateMaxLength(result, nameof(str), maxLength);

            Assert.IsTrue(result.IsValid);

            Assert.IsTrue(!result.Errors.Any());
        }
    }
}
