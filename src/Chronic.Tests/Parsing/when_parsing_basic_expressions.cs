using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class when_parsing_basic_expressions : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return DateTime.Now;
        }
        
        [Fact]
        public void today_is_parsed_correctly()
        {
            Parse("today").AssertStartsAt(DateTime.Now.Date);
        }

        [Fact]
        public void yesterday_is_parsed_correctly()
        {
            Parse("yesterday").AssertStartsAt(DateTime.Now.Date.AddDays(-1));
        }

        [Fact]
        public void tomorrow_is_parsed_correctly()
        {
            Parse("tomorrow").AssertStartsAt(DateTime.Now.Date.AddDays(1));
        }
    }
}
