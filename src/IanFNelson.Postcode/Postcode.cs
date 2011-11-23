using System;
using System.Text.RegularExpressions;

namespace IanFNelson.Postcode
{
    /// <summary>
    /// Represents a United Kingdom postcode.
    /// </summary>
    /// <remarks>
    /// For more information, see http://ianfnelson.com/blog/postcodestruct
    /// </remarks>
    [Serializable]
    public struct Postcode
    {
        private const string RegexBs7666Outer =
            "(?<outCode>^[A-PR-UWYZ]([0-9][0-9A-HJKPS-UW]?|[A-HK-Y][0-9][0-9ABEHMNPRV-Y]?))";

        private const string RegexBs7666Inner = "(?<inCode>[0-9][ABD-HJLNP-UW-Z]{2})";
        private const string RegexBs7666Full = RegexBs7666Outer + RegexBs7666Inner + "$";
        private const string RegexBs7666OuterStandAlone = RegexBs7666Outer + "$";

        private const string RegexBfpoOuter = "(?<outCode>BFPO)";
        private const string RegexBfpoInner = "(?<inCode>[0-9]{1,4})";
        private const string RegexBfpoFull = RegexBfpoOuter + RegexBfpoInner + "$";
        private const string RegexBfpoOuterStandalone = RegexBfpoOuter + "$";

        private static readonly string[,] OverseasTerritories =
            {
                {"ASCN", "1ZZ"}, // Ascension Island
                {"BIQQ", "1ZZ"}, // British Antarctic Territory
                {"BBND", "1ZZ"}, // British Indian Ocean Territory
                {"FIQQ", "1ZZ"}, // Falkland Islands
                {"PCRN", "1ZZ"}, // Pitcairn Islands
                {"STHL", "1ZZ"}, // Saint Helena
                {"SIQQ", "1ZZ"}, // South Georgia and the Sandwich Islands
                {"TDCU", "1ZZ"}, // Tristan da Cunha
                {"TKCA", "1ZZ"} // Turks and Caicos Islands
            };

        /// <summary>
        /// Outer portion of the Postcode
        /// </summary>
        public string OutCode { get; private set; }

        /// <summary>
        /// Inner portion of the Postcode
        /// </summary>
        public string InCode { get; private set; }

        /// <summary>
        /// Parses a string as a Postcode.
        /// </summary>
        /// <param name="value">String to be parsed</param>
        /// <returns>Postcode object</returns>
        /// <exception cref="FormatException">
        /// If the passed string cannot be parsed as a UK postcode
        /// </exception>
        /// <remarks>Using this overload, the inner code is not mandatory.</remarks>
        public static Postcode Parse(string value)
        {
            return Parse(value, PostcodeParseOptions.None);
        }

        /// <summary>
        /// Parses a string as a Postcode.
        /// </summary>
        /// <param name="value">String to be parsed</param>
        /// <param name="options"></param>
        /// <returns>Postcode object</returns>
        /// <exception cref="FormatException">
        /// If the passed string cannot be parsed as a UK postcode
        /// </exception>
        public static Postcode Parse(string value, PostcodeParseOptions options)
        {
            Postcode p;
            if (TryParse(value, out p, options))
                return p;

            throw new FormatException();
        }

        /// <summary>
        /// Attempts to parse a string as a Postcode.
        /// </summary>
        /// <param name="value">String to be parsed</param>
        /// <param name="result">Postcode object</param>
        /// <returns>
        /// Boolean indicating whether the string was successfully parsed as a UK Postcode
        /// </returns>
        /// <remarks>Using this overload, the inner code is not mandatory.</remarks>
        public static bool TryParse(string value, out Postcode result)
        {
            return TryParse(value, out result, PostcodeParseOptions.None);
        }

        /// <summary>
        /// Attempts to parse a string as a Postcode.
        /// </summary>
        /// <param name="value">String to be parsed</param>
        /// <param name="result">Postcode object</param>
        /// <param name="options"></param>
        /// <returns>
        /// Boolean indicating whether the string was successfully parsed as a UK Postcode
        /// </returns>
        public static bool TryParse(string value, out Postcode result, PostcodeParseOptions options)
        {
            // Set output to new Postcode
            result = new Postcode();

            // Guard clause - check for null or whitespace
            if (string.IsNullOrWhiteSpace(value)) return false;

            // uppercase input and strip undesirable characters
            value = Regex.Replace(value.ToUpperInvariant(), "[^A-Z0-9]", string.Empty);

            // Work through different options in turn until we have a match.
            return (TryParseBs7666(value, options, ref result) ||
                    TryParseBfpo(value, options, ref result) ||
                    TryParseOverseasTerritories(value, options, ref result) ||
                    TryParseGiroBank(value, options, ref result) ||
                    TryParseSanta(value, options, ref result));
        }

        private static bool TryParseBs7666(string sanitizedInput, PostcodeParseOptions options, ref Postcode result)
        {
            // Try to match full standard postcode
            Match fullMatch = Regex.Match(sanitizedInput, RegexBs7666Full);
            if (fullMatch.Success)
            {
                result.OutCode = fullMatch.Groups["outCode"].Value;
                result.InCode = fullMatch.Groups["inCode"].Value;
                return true;
            }

            // Try to match outer standard postcode only
            if ((options & PostcodeParseOptions.IncodeOptional) != PostcodeParseOptions.None)
            {
                Match outerMatch = Regex.Match(sanitizedInput, RegexBs7666OuterStandAlone);
                if (outerMatch.Success)
                {
                    result.OutCode = outerMatch.Groups["outCode"].Value;
                    return true;
                }
            }
            return false;
        }

        private static bool TryParseBfpo(string sanitizedInput, PostcodeParseOptions options, ref Postcode result)
        {
            if ((options & PostcodeParseOptions.MatchBfpo) == PostcodeParseOptions.None) return false;

            // Try to match full BFPO postcode
            Match bfpoFullMatch = Regex.Match(sanitizedInput, RegexBfpoFull);
            if (bfpoFullMatch.Success)
            {
                result.OutCode = bfpoFullMatch.Groups["outCode"].Value;
                result.InCode = bfpoFullMatch.Groups["inCode"].Value;
                return true;
            }

            // Try to match outer BFPO postcode
            if ((options & PostcodeParseOptions.IncodeOptional) != PostcodeParseOptions.None)
            {
                Match bfpoOuterMatch = Regex.Match(sanitizedInput, RegexBfpoOuterStandalone);
                if (bfpoOuterMatch.Success)
                {
                    result.OutCode = bfpoOuterMatch.Groups["outCode"].Value;
                    return true;
                }
            }

            return false;
        }

        private static bool TryParseOverseasTerritories(string sanitizedInput, PostcodeParseOptions options,
                                                        ref Postcode result)
        {
            if ((options & PostcodeParseOptions.MatchOverseasTerritories) == PostcodeParseOptions.None) return false;

            // Loop through exceptions to the rule
            for (int i = 0; i < OverseasTerritories.GetLength(0); i++)
            {
                // Check for a full match
                if (sanitizedInput == string.Concat(OverseasTerritories[i, 0], OverseasTerritories[i, 1]))
                {
                    result.OutCode = OverseasTerritories[i, 0];
                    result.InCode = OverseasTerritories[i, 1];
                    return true;
                }

                // Check for partial match only
                if ((options & PostcodeParseOptions.IncodeOptional) != PostcodeParseOptions.None)
                {
                    if (sanitizedInput == OverseasTerritories[i, 0])
                    {
                        result.OutCode = OverseasTerritories[i, 0];
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool TryParseGiroBank(string sanitizedInput, PostcodeParseOptions options, ref Postcode result)
        {
            if ((options & PostcodeParseOptions.MatchGirobank) != PostcodeParseOptions.None)
            {
                if (sanitizedInput == "GIR0AA")
                {
                    result.OutCode = "GIR";
                    result.InCode = "0AA";
                    return true;
                }

                if ((options & PostcodeParseOptions.IncodeOptional) != PostcodeParseOptions.None)
                {
                    if (sanitizedInput == "GIR")
                    {
                        result.OutCode = "GIR";
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool TryParseSanta(string sanitizedInput, PostcodeParseOptions options, ref Postcode result)
        {
            if ((options & PostcodeParseOptions.MatchSanta) != PostcodeParseOptions.None)
            {
                if (sanitizedInput == "SANTA1")
                {
                    result.OutCode = "SAN";
                    result.InCode = "TA1";
                    return true;
                }

                if ((options & PostcodeParseOptions.IncodeOptional) != PostcodeParseOptions.None)
                {
                    if (sanitizedInput == "SAN")
                    {
                        result.OutCode = "SAN";
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a string representation of this postcode
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(InCode) ? OutCode : string.Concat(OutCode, " ", InCode);
        }
    }
}