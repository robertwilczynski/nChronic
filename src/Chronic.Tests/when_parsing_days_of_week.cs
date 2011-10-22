using System;
using Xunit;

namespace Chronic.Tests
{
    public class ParserTests
    {
        protected Parser parser = new Parser();
    }

    public class when_parsing_days_of_week : ParserTests
    {
        protected Parser parser = new Parser(new Options { Clock = () => new DateTime(2011, 10, 18, 15, 34, 44) });

        [Fact]
        public void monday_is_parsed_correctly()
        {
            parser.Parse("monday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 24));
        }

        [Fact]
        public void tuesday_is_parsed_correctly()
        {
            parser.Parse("tuesday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 25));
        }

        [Fact]
        public void wednesday_is_parsed_correctly()
        {
            parser.Parse("wednesday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 19));
        }

        [Fact]
        public void thursday_is_parsed_correctly()
        {
            parser.Parse("thursday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 20));
        }

        [Fact]
        public void friday_is_parsed_correctly()
        {
            parser.Parse("friday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 21));
        }

        [Fact]
        public void saturday_is_parsed_correctly()
        {
            parser.Parse("saturday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 22));
        }

        [Fact]
        public void sunday_is_parsed_correctly()
        {
            parser.Parse("sunday").Start
                .AssertDatePartIsEqual(new DateTime(2011, 10, 23));
        }
    }
}
