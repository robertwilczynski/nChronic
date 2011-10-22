using System;
using System.Linq;

namespace Chronic.Tests
{
    public class ParsingTestsBase
    {
        protected DateTime Now = Time.New(2006, 8, 16, 14, 0, 0);

        protected Span Parse(string input)
        {
            Parser.IsDebugMode = true;
            var parser = new Parser(new Options { Clock = () => Now });
            return parser.Parse(input);
        }

        protected Span Parse(string input, dynamic options)
        {
            Parser.IsDebugMode = true;
            var type = options.GetType() as Type;
            var properties = type.GetProperties();
            var aggregatedOptions = new Options
                {
                    AmbiguousTimeRange = properties.Any(x => x.Name == "AmbiguousTimeRange") 
                        ? options.AmbiguousTimeRange 
                        : Options.DefaultAmbiguousTimeRange,
                    Clock = properties.Any(x => x.Name == "Clock") 
                        ? (Func<DateTime>)options.Clock 
                        : () => Now,
                    Context = properties.Any(x => x.Name == "Context") 
                        ? options.Context 
                        : Pointer.Type.None,
                    EndianPrecedence = properties.Any(x => x.Name == "EndianPrecedence") 
                        ? options.EndianPrecedence 
                        : EndianPrecedence.Little,
                };
            var parser = new Parser(aggregatedOptions);
            return parser.Parse(input);
        }
    }
}