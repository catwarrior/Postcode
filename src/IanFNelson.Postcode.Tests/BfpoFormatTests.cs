using NUnit.Framework;

namespace IanFNelson.Postcode.Tests
{
    [TestFixture]
    public class BfpoFormatTests
    {
        [TestCase("BFPO 2")]
        [TestCase("BFPO 11")]
        [TestCase("BFPO 109")]
        [TestCase("BFPO 1001")]
        public void TryParse_ValidBfpoPostcode_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, PostcodeParseOptions.MatchBfpo);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }
    }
}
