using System;
using NUnit.Framework;

namespace IanFNelson.Postcode.Tests
{
    [TestFixture]
    public class BehaviouralTests
    {
        [TestCase("TDCU 1ZZ", "TDCU", "1ZZ")]
        [TestCase("BFPO 101", "BFPO", "101")]
        [TestCase("SAN TA1", "SAN", "TA1")]
        [TestCase("GIR 0AA", "GIR", "0AA")]
        [TestCase("LS25", "LS25", null)]
        [TestCase("GIR", "GIR", null)]
        [TestCase("SAN", "SAN", null)]
        [TestCase("BFPO", "BFPO", null)]
        [TestCase("TDCU", "TDCU", null)]
        public void Parse_CanCombineOptions(string input, string expectedOutcode, string expectedIncode)
        {
            // Arrange
            const PostcodeParseOptions options = PostcodeParseOptions.IncodeOptional ^
                                                 PostcodeParseOptions.MatchBfpo ^
                                                 PostcodeParseOptions.MatchGirobank ^
                                                 PostcodeParseOptions.MatchOverseasTerritories ^
                                                 PostcodeParseOptions.MatchSanta;

            // Act
            Postcode output = Postcode.Parse(input, options);

            // Assert
            Assert.That(output.InCode, Is.EqualTo(expectedIncode));
            Assert.That(output.OutCode, Is.EqualTo(expectedOutcode));
        }

        [TestCase("LS256LG", "LS25", "6LG")]
        [TestCase("LS25 6LG", "LS25", "6LG")]
        [TestCase("S1  1AA", "S1", "1AA")]
        [TestCase("LS25-6LG", "LS25", "6LG")]
        [TestCase("S1--1AA", "S1", "1AA")]
        [TestCase("LS25_6LG", "LS25", "6LG")]
        [TestCase("LS25*6LG", "LS25", "6LG")]
        [TestCase("LS25%6LG", "LS25", "6LG")]
        public void Parse_InputContainsVariousSeparatorsBetweenIncodeAndOutcode_AllAreValid(string input,
                                                                                            string expectedOutcode,
                                                                                            string expectedIncode)
        {
            // Act
            Postcode output = Postcode.Parse(input);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo(expectedOutcode));
            Assert.That(output.InCode, Is.EqualTo(expectedIncode));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Parse_NullOrEmptyOrWhitespaceInput_NotValid(string input)
        {
            Assert.Throws<FormatException>(() => Postcode.Parse(input));
        }

        [Test]
        public void Parse_BfpoAllowed_IsValid()
        {
            // Arrange
            const string bfpoPostcode = "BFPO 805";

            // Act
            Postcode output = Postcode.Parse(bfpoPostcode, PostcodeParseOptions.MatchBfpo);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("BFPO"));
            Assert.That(output.InCode, Is.EqualTo("805"));
        }

        [Test]
        public void Parse_BfpoNotAllowed_IsInvalid()
        {
            // Arrange
            const string bfpoPostcode = "BFPO 805";

            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(bfpoPostcode));
        }

        [Test]
        public void Parse_GirobankAllowed_IsValid()
        {
            // Arrange
            const string girobankPostcode = "GIR 0AA";

            // Act
            Postcode output = Postcode.Parse(girobankPostcode, PostcodeParseOptions.MatchGirobank);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("GIR"));
            Assert.That(output.InCode, Is.EqualTo("0AA"));
        }

        [Test]
        public void Parse_GirobankNotAllowed_IsInvalid()
        {
            // Arrange
            const string girobankPostcode = "GIR 0AA";

            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(girobankPostcode));
        }

        [Test]
        public void Parse_InvalidPostcodeContainsValidPostcodeAsSubstring_IsInvalid()
        {
            // Arrange
            const string input = "QS81 8SH";
            // "S81 8SH" is valid. "QS81 8SH" looks valid at first glance, 
            //  but isn't as postcodes can't start with Q.

            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(input));
        }

        [Test]
        public void Parse_MixedCaseInput_OutputIsUppercase()
        {
            // Arrange
            const string input = "ls25 6Lg";

            // Act
            Postcode output = Postcode.Parse(input);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("LS25"));
            Assert.That(output.InCode, Is.EqualTo("6LG"));
        }

        [Test]
        public void Parse_NonWhitespaceNonAlphanumericSurroundsInput_IsStripped()
        {
            // Arrange
            const string input = "%LS25 6LG\"";

            // Act
            Postcode output = Postcode.Parse(input);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("LS25"));
            Assert.That(output.InCode, Is.EqualTo("6LG"));
        }

        [Test]
        public void Parse_OutcodeOnlyPassedIncodeOptionalSet_IsValid()
        {
            // Arrange
            const string input = "LS25";

            // Act
            Postcode output = Postcode.Parse(input, PostcodeParseOptions.IncodeOptional);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo(input));
            Assert.That(output.InCode, Is.Null);
        }

        [Test]
        public void Parse_OutcodeOnlyPassed_NotValidByDefault()
        {
            // Arrange
            const string input = "LS25";

            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(input));
        }

        [Test]
        public void Parse_OverseasTerritoriesAllowed_IsValid()
        {
            // Arrange
            const string overseasTerritoryPostcode = "TDCU 1ZZ";

            // Act
            Postcode output = Postcode.Parse(overseasTerritoryPostcode, PostcodeParseOptions.MatchOverseasTerritories);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("TDCU"));
            Assert.That(output.InCode, Is.EqualTo("1ZZ"));
        }

        [Test]
        public void Parse_OverseasTerritoriesNotAllowed_IsInvalid()
        {
            // Arrange
            const string overseasTerritoryPostcode = "TDCU 1ZZ";

            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(overseasTerritoryPostcode));
        }

        [Test]
        public void Parse_SantaAllowed_IsValid()
        {
            // Arrange
            const string santaPostcode = "SAN TA1";

            // Act
            Postcode output = Postcode.Parse(santaPostcode, PostcodeParseOptions.MatchSanta);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("SAN"));
            Assert.That(output.InCode, Is.EqualTo("TA1"));
        }

        [Test]
        public void Parse_SantaNotAllowed_IsInvalid()
        {
            // Arrange
            const string santaPostcode = "SAN TA1";

            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(santaPostcode));
        }

        [TestCase("M1 1AAA", PostcodeParseOptions.None)]
        [TestCase("BFPO 1234A", PostcodeParseOptions.MatchBfpo)]
        [TestCase("TDCU 1ZZA", PostcodeParseOptions.MatchOverseasTerritories)]
        [TestCase("S81A", PostcodeParseOptions.IncodeOptional)]
        public void Parse_ValidPostcodeAtStartOfLongerString_IsInvalid(string invalidPostcode, PostcodeParseOptions options)
        {
            // Act
            Assert.Throws<FormatException>(() => Postcode.Parse(invalidPostcode, options), string.Format("No exception thrown when parsing {0}", invalidPostcode));
        }

        [Test]
        public void Parse_ValidPostcode_ReturnedObjectHasOutcodeAndIncodeSet()
        {
            // Arrange
            const string input = "LS25 6LG";

            // Act
            Postcode output = Postcode.Parse(input);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("LS25"));
            Assert.That(output.InCode, Is.EqualTo("6LG"));
        }

        [Test]
        public void Parse_WhitespaceSurroundsValidInput_WhitespaceIsStripped()
        {
            // Arrange
            const string input = "  LS25 6LG ";

            // Act
            Postcode output = Postcode.Parse(input);

            // Assert
            Assert.That(output.OutCode, Is.EqualTo("LS25"));
            Assert.That(output.InCode, Is.EqualTo("6LG"));
        }
    }
}