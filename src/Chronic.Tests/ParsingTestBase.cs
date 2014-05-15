using System;
using System.Globalization;
using System.Linq;

namespace Chronic.Tests
{
    public abstract class ParsingTestsBase
    {
        protected abstract DateTime Now();

        protected Span Parse(string input)
        {
            Parser.IsDebugMode = true;
            var parser = new Parser(new Options
            {
                Clock = () => Now(),
            });
            return parser.Parse(input);
        }

        protected Span Parse(string input, dynamic options)
        {
            Parser.IsDebugMode = true;
            var aggregatedOptions = TestingExtensions
                .Extend(new Options() { Clock = Now }, options);
            var parser = new Parser(aggregatedOptions);
            return parser.Parse(input);
        }
    }
}