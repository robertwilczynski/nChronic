using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class ComplexExpressionsTests : ParsingTestsBase
    {
        static readonly DateTime Date = new DateTime(2011, 10, 18);
        static readonly TimeSpan TimeOfDay = new TimeSpan(15, 34, 44);

        protected override DateTime Now()
        {
            return Date.Add(TimeOfDay);
        }

        [Fact]
        public void days_are_parsed_correctly()
        {
            Parse("7 days ago").AssertStartsAt(
                Time.New(2011, 10, 11, TimeOfDay));
        }

        [Fact]
        public void weeks_are_parsed_correctly()
        {
            Parse("3 weeks ago").AssertStartsAt(
                Time.New(2011, 09, 27, TimeOfDay));
        }

        [Fact]
        public void months_are_parsed_correctly()
        {
            Parse("7 months ago").AssertStartsAt(
                Time.New(2011, 03, 18, TimeOfDay));
        }

        [Fact]
        public void years_are_parsed_correctly()
        {
            Parse("7 years ago").AssertStartsAt(
                Time.New(2004, 10, 18, TimeOfDay));
        }

		[Fact]
		public void dates_next_year_are_parsed_correctly()
		{
			Parse("7 july next year").AssertStartsAt(
				Time.New(2012, 7, 7));
		}


    }
}