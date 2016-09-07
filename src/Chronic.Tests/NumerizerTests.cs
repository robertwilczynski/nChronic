using System;
using Xunit;

namespace Chronic.Tests
{
    public class NumerizerTests
    {
        [Fact]
        public void non_ordinal_numbers_are_parsed_correctly()
        {
            new object[,]
                {
                    {"one", 1},
                    {"five", 5},
                    {"ten", 10},
                    {"eleven", 11},
                    {"twelve", 12},
                    {"thirteen", 13},
                    {"fourteen", 14},
                    {"fifteen", 15},
                    {"sixteen", 16},
                    {"seventeen", 17},
                    {"eighteen", 18},
                    {"nineteen", 19},
                    {"twenty", 20},
                    {"twenty seven", 27},
                    {"thirty-one", 31},
                    {"thirty-seven", 37},
                    {"thirty seven", 37},
                    {"fifty nine", 59},
                    {"forty two", 42},
                    {"fourty two", 42},
                    // { "a hundred", 100 },
                    {"one hundred", 100},
                    {"one hundred and fifty", 150},
                    // { "one fifty", 150 },
                    {"two-hundred", 200},
                    {"5 hundred", 500},
                    {"nine hundred and ninety nine", 999},
                    {"one thousand", 1000},
                    {"twelve hundred", 1200},
                    {"one thousand two hundred", 1200},
                    {"seventeen thousand", 17000},
                    {"twentyone-thousand-four-hundred-and-seventy-three", 21473}
                    ,
                    {"seventy four thousand and two", 74002},
                    {"ninety nine thousand nine hundred ninety nine", 99999},
                    {"100 thousand", 100000},
                    {"two hundred fifty thousand", 250000},
                    {"one million", 1000000},
                    {
                        "one million two hundred fifty thousand and seven",
                        1250007
                    },
                    {"one billion", 1000000000},
                    {"one billion and one", 1000000001}
                }.ForEach<string, int>((phrase, expectedResult) =>
                    {
                        var numerizedString = Numerize(expectedResult, phrase);
                        var number = ConvertToNumber(expectedResult, numerizedString, phrase);
                        Assert.Equal(expectedResult, number);                    
                    });
        }

        [Fact]
        public void ordinal_numbers_are_parsed_correctly()
        {
            new[,]
                {
                    {"first", "1st"},
                    {"second", "second"},
                    {"second day", "2nd day"},
                    {"second of may", "2nd of may"},
                    {"third of may", "2rd of may"},
                    {"fifth", "5th"},
                    {"twenty third", "23rd"},
                    {"first day month two", "1st day month 2"}
                }.ForEach<string, string>(
                    (p, r) =>
                    {
                        // Use pre_normalize here instead of Numerizer directly because
                        // pre_normalize deals with parsing 'second' appropriately
                        Assert.Equal(r, Tokenizer.Normalize(r));
                    });
        }

        [Fact]
        public void test_edges()
        {
            Assert.Equal(
                "27 Oct 2006 7:30am", Numerizer.Numerize("27 Oct 2006 7:30am"));
        }

        static int ConvertToNumber(int r, string numerizedString, string s)
        {
            var value = 0;
            if (!Int32.TryParse(numerizedString, out value))
            {
                throw new Exception(
                    String.Format(
                        "Numerized input '{0}' is expected to be an integral number but it's not. Test case: {1} => {2}",
                        numerizedString,
                        s,
                        r));
            }
            return value;
        }

        static string Numerize(int r, string s)
        {
            string numerizedInput = null;
            try
            {
                numerizedInput = Numerizer.Numerize(s);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    String.Format(
                        "Test case: {0} => {1} :: {2}", s, r, ex.Message),
                    ex);
            }
            return numerizedInput;
        }
    }
}