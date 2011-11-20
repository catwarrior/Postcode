using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace IanFNelson.Postcode.Tests
{
    [TestFixture]
    public class OverseasTerritoryFormatTests
    {
        [TestCase("ASCN 1ZZ")]
        [TestCase("BBND 1ZZ")]
        [TestCase("BIQQ 1ZZ")]
        [TestCase("FIQQ 1ZZ")]
        [TestCase("PCRN 1ZZ")]
        [TestCase("SIQQ 1ZZ")]
        [TestCase("STHL 1ZZ")]
        [TestCase("TDCU 1ZZ")]
        [TestCase("TKCA 1ZZ")]
        public void TryParse_ValidOverseasTerritoryPostcode_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, PostcodeParseOptions.MatchOverseasTerritories);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }
    }
}
