using System;
using Xunit;

namespace System
{
    public static class DateTimeTestingExtensions
    {
        public static void AssertIsEqual(this DateTime? actual, int year, int month, int day, TimeSpan time)
        {
            var expected = new DateTime(year, month, day).Add(time);
            Assert.Equal(expected, actual);
        }

        public static void AssertIsEqual(this DateTime actual, DateTime expectedValue)
        {
            Assert.Equal(expectedValue, actual);
        }

        public static void AssertIsEqual(this DateTime? actual, DateTime? expectedValue)
        {
            Assert.Equal(expectedValue, actual);
        }

        public static void AssertDatePartIsEqual(this DateTime? actual, DateTime? expectedValue)
        {
            Assert.Equal(expectedValue.Value.Date, actual.Value.Date);
        }

    }
}