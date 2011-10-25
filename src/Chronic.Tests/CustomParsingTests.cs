using Chronic;
using Xunit;

namespace Chronic.Tests
{
    public class CustomParsingTests : ParsingTestsBase
    {
        [Fact]
        public void _3_years_2_months_and_3_days_ago()
        {
            var time = Parse("3 years, 2 months and 3 days ago").ToTime();
            Assert.Equal(Time.New(2003, 6, 13, 14), time);            
        }

        [Fact]
        public void may_28_at_5_32_19pm()
        {
            var result = Parse("may 28 at 5:32:19pm",
                      new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 28, 17, 32, 19), result);
        }

        [Fact]
        public void _7_days_from_now()
        {
            var result = Parse(" 7 days from now").ToTime();
            Assert.Equal(Time.New(2006, 8, 23, 14), result);
        }

        [Fact]
        public void _7_days_from_now_at_midnight()
        {
            var result = Parse(" 7 days from now at midnight").ToTime();
            Assert.Equal(Time.New(2006, 8, 24), result);
        }

        [Fact]
        public void seven_days_from_now_at_midnight()
        {
            var result = Parse(" seven days from now at midnight").ToTime();
            Assert.Equal(Time.New(2006, 8, 24), result);
        }

        [Fact]
        public void _2_weeks_ago()
        {
            var result = Parse("2 weeks ago").ToTime();
            Assert.Equal(Time.New(2006, 8, 02, 14), result);
        }

        [Fact]
        public void two_weeks_ago()
        {
            var result = Parse("two weeks ago").ToTime();
            Assert.Equal(Time.New(2006, 8, 02, 14), result);
        }
    }
}
