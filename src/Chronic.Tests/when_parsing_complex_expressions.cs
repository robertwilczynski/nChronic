using System;
using Xunit;

namespace Chronic.Tests
{
    public class when_parsing_complex_expressions
    {
        static readonly DateTime date = new DateTime(2011, 10, 18);
        static readonly TimeSpan time = new TimeSpan(15, 34, 44);
        static readonly Parser parser = new Parser(new Options { Clock = () => date.Add(time) });
        
        [Fact]
        public void days_are_parsed_correctly()
        {
            parser.Parse("7 days ago").Start
                .AssertIsEqual(2011, 10, 11, time);
        }

        [Fact]
        public void weeks_are_parsed_correctly()
        {
            parser.Parse("3 weeks ago").Start
                .AssertIsEqual(2011, 09, 27, time);
        }

        [Fact]
        public void months_are_parsed_correctly()
        {
            parser.Parse("7 months ago").Start
                .AssertIsEqual(2011, 03, 18, time);
        }

        [Fact]
        public void years_are_parsed_correctly()
        {
            parser.Parse("7 years ago").Start
                .AssertIsEqual(2004, 10, 18, time);
        }
    }
}
