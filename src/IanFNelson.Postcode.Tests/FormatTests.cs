using System;
using NUnit.Framework;

namespace IanFNelson.Postcode.Tests
{
    [TestFixture]
    public class FormatTests
    {
        #region Test Invalid First letters Q,V,X

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidLetterQInFirstPosition_ThrowFormatException()
        {
            // Arrange
            const string postcodeString = "QO7 5PQ";

            // Act
            var actual = Postcode.Parse(postcodeString);
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidLetterVInFirstPosition_ThrowFormatException()
        {
            // Arrange
            const string postcodeString = "VO7 5PQ";

            // Act
            var actual = Postcode.Parse(postcodeString);
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidLetterXInFirstPosition_ThrowFormatException()
        {
            // Arrange
            const string postcodeString = "XO7 5PQ";

            // Act
            var actual = Postcode.Parse(postcodeString);
        }

        #endregion

        #region Test Invalid Second letters I, J and Z

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidSecondLetterI_ThrowFormatException()
        {
            // Arrange
            const string postcodeString = "PI7 5PQ";

            // Act
            var actual = Postcode.Parse(postcodeString);
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidSecondLetterJ_ThrowFormatException()
        {
            // Arrange
            const string postcodeString = "PJ7 5PQ";

            // Act
            var actual = Postcode.Parse(postcodeString);
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Parse_InvalidSecondLetterZ_ThrowFormatException()
        {
            // Arrange
            const string postcodeString = "PZ7 5PQ";

            // Act
            var actual = Postcode.Parse(postcodeString);
        }

        #endregion


    }
}
