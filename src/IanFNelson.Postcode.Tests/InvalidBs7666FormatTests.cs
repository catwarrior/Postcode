using NUnit.Framework;

namespace IanFNelson.Postcode.Tests
{
    [TestFixture]
    public class InvalidBs7666FormatTests
    {
        [TestCase("QS25 6LG")]
        [TestCase("VS25 6LG")]
        [TestCase("XS25 6LG")]
        public void TryParse_OutcodeInvalidFirstCharacter_Unsuccessful(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output);

            // Assert
            Assert.That(result, Is.False, string.Format("Incorrectly parsed {0} as valid postcode", input));
        }

        [TestCase("AI25 6LG")]
        [TestCase("AJ25 6LG")]
        [TestCase("AZ25 6LG")]
        public void TryParse_OutcodeInvalidSecondCharacter_Unsuccessful(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output);

            // Assert
            Assert.That(result, Is.False, string.Format("Incorrectly parsed {0} as valid postcode", input));
        }

        [TestCase("W1I 6LG")]
        [TestCase("W1L 6LG")]
        [TestCase("W1M 6LG")]
        [TestCase("W1N 6LG")]
        [TestCase("W1O 6LG")]
        [TestCase("W1Q 6LG")]
        [TestCase("W1R 6LG")]
        [TestCase("W1V 6LG")]
        [TestCase("W1X 6LG")]
        [TestCase("W1Y 6LG")]
        [TestCase("W1Z 6LG")]
        public void TryParse_OutcodeInvalidThirdCharacter_Unsuccessful(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output);

            // Assert
            Assert.That(result, Is.False, string.Format("Incorrectly parsed {0} as valid postcode", input));
        }

        [TestCase("EC1C 6LG")]
        [TestCase("EC1D 6LG")]
        [TestCase("EC1F 6LG")]
        [TestCase("EC1G 6LG")]
        [TestCase("EC1I 6LG")]
        [TestCase("EC1J 6LG")]
        [TestCase("EC1K 6LG")]
        [TestCase("EC1L 6LG")]
        [TestCase("EC1O 6LG")]
        [TestCase("EC1Q 6LG")]
        [TestCase("EC1S 6LG")]
        [TestCase("EC1T 6LG")]
        [TestCase("EC1U 6LG")]
        [TestCase("EC1Z 6LG")]
        public void TryParse_OutcodeInvalidFourthCharacter_Unsuccessful(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output);

            // Assert
            Assert.That(result, Is.False, string.Format("Incorrectly parsed {0} as valid postcode", input));
        }

        [TestCase("LS25 6CG")]
        [TestCase("LS25 6IG")]
        [TestCase("LS25 6KG")]
        [TestCase("LS25 6MG")]
        [TestCase("LS25 6OG")]
        [TestCase("LS25 6VG")]
        public void TryParse_IncodeInvalidSecondCharacter_Unsuccessful(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output);

            // Assert
            Assert.That(result, Is.False, string.Format("Incorrectly parsed {0} as valid postcode", input));
        }

        [TestCase("LS25 6GC")]
        [TestCase("LS25 6GI")]
        [TestCase("LS25 6GK")]
        [TestCase("LS25 6GM")]
        [TestCase("LS25 6GO")]
        [TestCase("LS25 6GV")]
        public void TryParse_IncodeInvalidThirdCharacter_Unsuccessful(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output);

            // Assert
            Assert.That(result, Is.False, string.Format("Incorrectly parsed {0} as valid postcode", input));
        }
    }
}
