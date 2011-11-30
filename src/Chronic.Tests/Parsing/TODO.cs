using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class TODO : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void _3_years_2_months_and_3_days_ago()
        {
             Parse("3 years, 2 months and 3 days ago")
                .AssertEquals(Time.New(2003, 6, 13, 14));
        }
    }
}
