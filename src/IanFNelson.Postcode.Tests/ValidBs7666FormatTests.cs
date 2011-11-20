using NUnit.Framework;

namespace IanFNelson.Postcode.Tests
{
    /// <summary>
    /// This test fixture contains ten test cases for each of the six standard postcode formats.
    /// 
    /// Test cases have been drawn at random from the data in the August 2011 release of Code-Point Open
    /// https://www.ordnancesurvey.co.uk/oswebsite/products/code-point-open/index.html
    /// </summary>
    [TestFixture]
    public class ValidBs7666FormatTests
    {
        /// <summary>
        /// Test that the struct can correctly parse a sample of postcodes in the A9 9AA format, e.g M1 1AA.
        /// These cover the B, E, G, L, M, N, S and W postcode areas.
        /// 
        /// The August 2011 release of Code-Point Open contained 44,731 postcodes in this format (2.6% of the total).
        /// </summary>
        /// <param name="input"></param>
        [TestCase("G2 4JB")]
        [TestCase("G4 9RH")]
        [TestCase("W2 6NG")]
        [TestCase("W3 7YG")]
        [TestCase("E9 5RT")]
        [TestCase("E8 2HL")]
        [TestCase("M5 3GX")]
        [TestCase("W4 5DX")]
        [TestCase("S6 5AN")]
        [TestCase("E6 1BT")]
        public void Parse_A9_9AA_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, true);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }

        /// <summary>
        /// Test that the struct can correctly parse a sample of postcodes in the A99 9AA format, e.g B33 8TH.
        /// These cover the B, E, G, L, M, N, S and W postcode areas.
        /// 
        /// The August 2011 release of Code-Point Open contained 157186 postcodes in this format (9.3% of the total).
        /// </summary>
        /// <param name="input"></param>
        [TestCase("S41 8QT")]
        [TestCase("G83 9BU")]
        [TestCase("M13 9SQ")]
        [TestCase("B93 8TH")]
        [TestCase("G72 9UN")]
        [TestCase("G83 3AB")]
        [TestCase("B75 6LS")]
        [TestCase("G52 3QB")]
        [TestCase("M40 7BW")]
        [TestCase("M24 2BW")]
        public void Parse_A99_9AA_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, true);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }

        /// <summary>
        /// Test that the struct can correctly parse a sample of postcodes in the AA9 9AA format, e.g CR2 6XH.
        /// These cover all postcode areas except B, E, G, L, M, N, S, W and WC.
        /// 
        /// The August 2011 release of Code-Point Open contained 683372 postcodes in this format (40.3% of the total).
        /// </summary>
        /// <param name="input"></param>
        [TestCase("CM3 1JT")]
        [TestCase("TQ3 2NX")]
        [TestCase("CR5 3AF")]
        [TestCase("SW2 1QE")]
        [TestCase("KY7 9UR")]
        [TestCase("HA1 1SP")]
        [TestCase("EN1 2WA")]
        [TestCase("SL4 5QE")]
        [TestCase("SR4 8RN")]
        [TestCase("LE3 3ZB")]
        public void Parse_AA9_9AA_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, true);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }

        /// <summary>
        /// Test that the struct can correctly parse a sample of postcodes in the AAA9 9AA format, e.g DN55 1PT.
        /// These cover all postcode areas except B, E, G, L, M, N, S, W and WC.
        /// 
        /// The August 2011 release of Code-Point Open contained 786572 postcodes in this format (46.4% of the total).
        /// </summary>
        /// <param name="input"></param>
        [TestCase("IP12 2QY")]
        [TestCase("HP14 3UT")]
        [TestCase("IV26 2YF")]
        [TestCase("MK14 6BW")]
        [TestCase("GL54 5ET")]
        [TestCase("EH49 7TA")]
        [TestCase("BN10 8RB")]
        [TestCase("LL11 2NE")]
        [TestCase("PO20 7PR")]
        [TestCase("BS21 7SS")]
        public void Parse_AA99_9AA_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, true);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }

        /// <summary>
        /// Test that the struct can correctly parse a sample of postcodes in the A9A 9AA format, e.g W1A 1HQ.
        /// These cover E1W, N1C, N1P, W1 postcode districts (high density areas where codes ran out).
        /// 
        /// The August 2011 release of Code-Point Open contained 10205 postcodes in this format (0.6% of the total).
        /// </summary>
        /// <param name="input"></param>
        [TestCase("W1T 2NQ")]
        [TestCase("W1H 1WB")]
        [TestCase("W1T 6BH")]
        [TestCase("W1F 9TW")]
        [TestCase("W1W 6RU")]
        [TestCase("N1P 2AB")]
        [TestCase("W1D 4TT")]
        [TestCase("E1W 3TF")]
        [TestCase("W1K 5SA")]
        [TestCase("N1C 4AX")]
        public void Parse_A9A_9AA_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, true);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }

        /// <summary>
        /// Test that the struct can correctly parse a sample of postcodes in the AA9A 9AA format, e.g EC1A 1BB.
        /// These cover WC postcode area; EC1–EC4, NW1W, SE1P, SW1 postcode districts (high density areas where codes ran out).
        /// 
        /// The August 2011 release of Code-Point Open contained 11736 postcodes in this format (0.7% of the total).
        /// </summary>
        /// <param name="input"></param>
        [TestCase("SE1P 5GH")]
        [TestCase("SW1P 3PF")]
        [TestCase("EC2N 1AR")]
        [TestCase("WC1R 4AY")]
        [TestCase("SW1X 9PD")]
        [TestCase("SW1Y 6NY")]
        [TestCase("SW1P 9DF")]
        [TestCase("EC3V 3BT")]
        [TestCase("NW1W 9AZ")]
        [TestCase("EC1V 0AN")]
        public void Parse_AA9A_9AA_ParsesSuccessfully(string input)
        {
            // Arrange
            Postcode output;

            // Act
            bool result = Postcode.TryParse(input, out output, true);

            // Assert
            Assert.That(result, Is.True, string.Format("Unable to parse {0} as valid postcode", input));
        }
    }
}
