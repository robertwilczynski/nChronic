using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class DaysOfWeekTests : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return new DateTime(2011, 10, 18, 15, 34, 44);
        }

        [Fact]
        public void monday_is_parsed_correctly()
        {
            Parse("monday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 24));
        }

        [Fact]
        public void tuesday_is_parsed_correctly()
        {
            Parse("tuesday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 25));
        }

        [Fact]
        public void wednesday_is_parsed_correctly()
        {
            Parse("wednesday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 19));
        }

        [Fact]
        public void thursday_is_parsed_correctly()
        {
            Parse("thursday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 20));
        }

        [Fact]
        public void friday_is_parsed_correctly()
        {
            Parse("friday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 21));
        }

        [Fact]
        public void saturday_is_parsed_correctly()
        {
            Parse("saturday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 22));
        }

        [Fact]
        public void sunday_is_parsed_correctly()
        {
            Parse("sunday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 23));
        }
    }
}
