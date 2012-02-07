using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class MultipleScalarRepeaterConjuctionsParsingTests : ParsingTestsBase
    {
        DateTime _now = Time.New(2006, 8, 16, 14, 0, 0);

        protected override DateTime Now()
        {
            return _now;
        }

        [Fact]
        public void _3_years_2_months_3_days_ago_with_punctuation()
        {
            Parse("3 years, 2 months, 3 days ago")
               .AssertEquals(Time.New(2003, 6, 13, 14));
        }

        [Fact]
        public void _3_years_2_months_3_days_ago_with_no_conjunctions_and_punctuation()
        {
            Parse("3 years 2 months 3 days ago")
               .AssertEquals(Time.New(2003, 6, 13, 14));
        }

        [Fact]
        public void _3_years_and_2_months_and_3_days_ago_with_conjunction()
        {
            Parse("3 years and 2 months and 3 days ago")
               .AssertEquals(Time.New(2003, 6, 13, 14));
        }

        [Fact]
        public void _3_years_and_2_months_and_1_week_ago_with_conjunction()
        {
            Parse("3 years and 2 months and 1 week ago")
               .AssertEquals(Time.New(2003, 6, 9, 14));
        }

        [Fact]
        public void _3_years_and_2_months_and_3_days_and_9_hours_and_10_minutes_and_3_seconds_ago_with_conjunction()
        {
            _now = Time.New(2006, 8, 16, 14, 30, 20);
            Parse("3 years and 2 months and 3 days and 9 hours and 10 minutes and 3 seconds ago")
               .AssertEquals(Time.New(2003, 6, 13, 05, 20, 17));
        }

        [Fact]
        public void _3_years_and_2_months_and_3_days_and_9_hours_and_10_minutes_and_3_seconds_ago_with_conjunctions_mixed_with_commas_and_spaces()
        {
            _now = Time.New(2006, 8, 16, 14, 30, 20);
            Parse("3 years and 2 months, 3 days , 9 hours 10 minutes 3 seconds ago")
               .AssertEquals(Time.New(2003, 6, 13, 05, 20, 17));
        }
    }
}
