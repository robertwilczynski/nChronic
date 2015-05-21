using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class BasicExpressionsTests : ParsingTestsBase
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
        public void uppercase_today_is_parsed_correctly()
        {
            Parse("TODAY").AssertStartsAt(DateTime.Now.Date);
        }

        [Fact]
        public void first_letter_uppercase_today_is_parsed_correctly()
        {
            Parse("Today").AssertStartsAt(DateTime.Now.Date);
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

        [Fact]
        public void day_after_tomorrow_is_parsed_correctly()
        {
            Parse("Day after tomorrow").AssertStartsAt(DateTime.Now.Date.AddDays(2));
        }
    }
}