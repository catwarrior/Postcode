using System;

namespace IanFNelson.Postcode
{
    [Flags]
    public enum PostcodeParseOptions
    {
        None = 0,
        IncodeOptional = 1,
        MatchBfpo = 2,
        MatchOverseasTerritories = 4,
        MatchGirobank = 8,
        MatchSanta = 16
    }
}