using System;
using Xunit;

namespace Chronic.Tests.Handlers
{
    public class GRGRHandler_correctly_parses : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void last_day_of_next_month()
        {
            Parse("last day of next month")
                .AssertStartsAt(Time.New(2006, 9, 30));
        }

        [Fact]
        public void last_day_of_this_month()
        {
            Parse("last day of this month")
                .AssertStartsAt(Time.New(2006, 8, 31));
        }

    }
}
