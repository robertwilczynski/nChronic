using System;
using Chronic;
using Xunit;

namespace Chronic.Tests
{
    public class SpanTests
    {
        DateTime _now;


        public void SetUp()
        {
        }

        [Fact]
        public void Width_returns_correct_value()
        {
            var span = new Span(
                Time.New(2006, 8, 16, 0),
                Time.New(2006, 8, 17, 0));
            Assert.Equal(60 * 60 * 24, span.Width);
        }

        [Fact]
        public void addition_to_span_updates_start_and_end()
        {
            var span = new Span(
                Time.New(2006, 8, 16, 0, 0, 1),
                Time.New(2006, 8, 17, 0, 0, 2))
                .Add(1);
            Assert.Equal(2, span.Start.Value.Second);
            Assert.Equal(3, span.End.Value.Second);
        }

        [Fact]
        public void subtraction_to_span_updates_start_and_end()
        {
            var span = new Span(
                Time.New(2006, 8, 16, 0, 0, 3),
                Time.New(2006, 8, 17, 0, 0, 4))
                .Subtract(1);
            Assert.Equal(2, span.Start.Value.Second);
            Assert.Equal(3, span.End.Value.Second);
        }
    }
}