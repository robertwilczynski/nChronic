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
    }
}
