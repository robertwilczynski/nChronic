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
        [Fact]
        public void next_week_is_parsed_correctly()
        {
            Parse("next week").AssertStartsAt(GetNextWeekday(DateTime.Today, DayOfWeek.Sunday));
        }
        [Fact]
        public void week_after_next_week_is_parsed_correctly()
        {
            Parse("week after next week").AssertStartsAt( GetNextWeekday(DateTime.Today, DayOfWeek.Sunday).AddDays(7));
        }
        [Fact]
        public void week_after_next_is_parsed_correctly()
        {
            Parse("week after next").AssertStartsAt(GetNextWeekday(DateTime.Today, DayOfWeek.Sunday).AddDays(7));
        }
        [Fact]
        public void week_after_next_day_is_parsed_correctly()
        {
            Parse("week after next day").AssertStartsAt(GetNextWeekday(DateTime.Today, DateTime.Today.DayOfWeek).AddDays(1).AddDays(7));
        }

        [Fact]
        public void week_after_friday_is_parsed_correctly()
        {
            Parse("week after friday").AssertStartsAt(GetNextWeekday(DateTime.Today, DayOfWeek.Friday).AddDays( 7));
        }

        [Fact]
        public void week_after_next_friday_is_parsed_correctly()
        {
            Parse("week after next friday").AssertStartsAt(GetNextWeekday(DateTime.Today, DayOfWeek.Friday).AddDays(2*7));
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

		[Fact]
		public void day_after_date_is_parsed_correctly()
		{
			Parse("1 day after 11/15/2015").AssertStartsAt(new DateTime(2015, 11, 16));
		}
    }
}