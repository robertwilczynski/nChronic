using System;
using System.Threading;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class when_parsing_now : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void now_is_parsed_correctly()
        {
            Parse("now").AssertEquals(Now());
        }

        [Fact]
        public void repeated_parsing_yields_different_values()
        {
            var parser = new Parser();
            var result1 = parser.Parse("now");
            Thread.Sleep(100);
            var result2 = parser.Parse("now");
            Assert.NotEqual(result1.ToTime(), result2.ToTime());
        }
    }
}
