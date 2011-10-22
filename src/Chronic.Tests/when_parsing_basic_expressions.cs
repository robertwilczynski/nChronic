using System;
using Xunit;

namespace Chronic.Tests
{
    public class when_parsing_basic_expressions
    {        
        Parser parser = new Parser(new Options());
        
        [Fact]
        public void today_is_parsed_correctly()
        {
            parser.Parse("today").Start
                .AssertIsEqual(DateTime.Now.Date);
        }

        [Fact]
        public void yesterday_is_parsed_correctly()
        {
            parser.Parse("yesterday").Start
                .AssertIsEqual(DateTime.Now.Date.AddDays(-1));
        }

        [Fact]
        public void tomorrow_is_parsed_correctly()
        {
            parser.Parse("tomorrow").Start
                .AssertIsEqual(DateTime.Now.Date.AddDays(1));
        }
        
    }
}
