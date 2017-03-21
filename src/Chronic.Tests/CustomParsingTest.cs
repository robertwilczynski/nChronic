using System;
using Xunit;

namespace Chronic.Tests
{
    public class CustomParsingTest : ParsingTestsBase
    {

        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void may_28_at_5_32_19pm()
        {
            Parse("may 28 at 5:32:19pm", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 28, 17, 32, 19));
        }

        [Fact]
        public void _7_days_from_now()
        {
            Parse(" 7 days from now")
                .AssertEquals(Time.New(2006, 8, 23, 14));
        }

		[Fact]
		public void _in_7_days_from_now()
		{
			Parse("in 7 days from now")
				.AssertEquals(Time.New(2006, 8, 23, 14));
		}

        [Fact]
        public void _7_days_from_now_at_midnight()
        {
            Parse(" 7 days from now at midnight")
                .AssertEquals(Time.New(2006, 8, 24));
        }

        [Fact]
        public void seven_days_from_now_at_midnight()
        {
            Parse(" seven days from now at midnight")
                .AssertEquals(Time.New(2006, 8, 24));
        }

        [Fact]
        public void _2_weeks_ago()
        {
            Parse("2 weeks ago")
                .AssertEquals(Time.New(2006, 8, 02, 14));
        }

        [Fact]
        public void two_weeks_ago()
        {
            Parse("two weeks ago")
                .AssertEquals(Time.New(2006, 8, 02, 14));
        }

        public class CanExtractTimeSpanFromSpan : ParsingTestsBase
        {
            protected override DateTime Now()
            {
                return Time.New(2006, 8, 16, 14, 34, 13);
            }

            [Fact]
            public void may_28_at_5_32_19pm()
            {
                Parse("7 days and two hours ago", new { Context = Pointer.Type.Past })
                    .AssertEquals(Time.New(2006, 8, 09, 12, 34, 13));
            }

			[Fact]
			public void friday_9_oct()
			{
				Parse("friday 9 oct")
					.AssertEquals(Time.New(2006, 10, 09, 12));
			}

            [Fact]
            public void second_week_in_january_is_parsed_correctly()
            {
                Parse("2nd week in january").AssertEquals(Time.New(2007, 1, 10, 12));
            }

            [Fact]
            public void second_week_january_is_parsed_correctly()
            {
                Parse("2nd week january").AssertEquals(Time.New(2007, 1, 10, 12));
            }

            [Fact]
            public void second_week_of_january_is_parsed_correctly()
            {
                Parse("2nd week of january").AssertEquals(Time.New(2007, 1, 10, 12));
            }
        }
    }
}
