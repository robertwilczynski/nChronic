using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class TimeExpressionsTests : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return DateTime.Now;
        }

        private DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        [Fact]
        public void time_format_is_parsed_correctly()
        {
            Parse("13:00").Start
                .AssertIsEqual(DateTime.Now.Date.AddHours(13));
        }

        [Fact]
        public void time_format_with_day_is_parsed_correctly()
        {
            var nextMonday = GetNextWeekday(DateTime.Now, DayOfWeek.Monday);

            var span = Parse("mon 2:35");

            Assert.Equal(nextMonday.Date.AddHours(14).AddMinutes(35), span.Start);
        }

        [Fact]
        public void time_AMPM_format_is_parsed_correctly()
        {
            Parse("4pm").Start
                .AssertIsEqual(DateTime.Now.Date.AddHours(16));
        }

        [Fact]
        public void time_is_parsed_correctly()
        {
            Parse("10 to 8").Start
                .AssertIsEqual(DateTime.Now.Date.AddHours(7).AddMinutes(50));
        }
    }
}
