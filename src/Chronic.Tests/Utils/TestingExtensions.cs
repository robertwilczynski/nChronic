using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Chronic.Tests
{
    public static class TestingExtensions
    {

        public static void AssertEquals(this Span @this, DateTime expected)
        {
            Assert.NotNull(@this);
            Assert.Equal(expected, @this.ToTime());
        }

        public static void AssertStartsAt(this Span @this, DateTime expected)
        {
            Assert.NotNull(@this);
            Assert.Equal(expected, @this.Start);
        }

        public static void StartingAt(this Span @this, DateTime expected)
        {
            Assert.NotNull(@this);
            Assert.Equal(expected, @this.Start);
        }

        public static Options Extend(this Options @this, dynamic options)
        {
            var type = options.GetType() as Type;
            var properties = type.GetProperties();
            var aggregatedOptions = @this;
            Action<dynamic, dynamic, string> try_extend =
                (target, extension, property) =>
                {
                    if (properties.Any(x => x.Name == property))
                    {
                        var value = type.GetProperty(property)
                            .GetValue(extension, null);
                        typeof(Options)
                            .GetProperty(property)
                            .SetValue(
                                 (object)target,
                                 (object)value,
                                 null);
                    }
                };

            typeof(Options).GetProperties().Select(x => x.Name).ToList()
                .ForEach(property =>
                    try_extend(aggregatedOptions, options, property));

            return aggregatedOptions;
        }

        public static void AssertIsNull(this Span @this)
        {
            Assert.Null(@this);
        }
    }
}
