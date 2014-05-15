using System;
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

        public static void AssertIsNull(this Span @this)
        {
            Assert.Null(@this);
        }
    }
}
