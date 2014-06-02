using System;
using System.Runtime.Remoting.Contexts;
using Xunit;
using Xunit.Extensions;

namespace Chronic.Tests.Handlers
{
    public class GRGRHandler_correctly_parses : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void last_day_of_next_month()
        {
            Parse("last day of next month")
                .AssertStartsAt(Time.New(2006, 9, 30));
        }

        [Fact]
        public void last_day_of_next_month_when_next_month_is_February_in_leap_year()
        {
            When
                .ItIs("1996-01-20")
                .Parsing("last day of next month")
                .ReturnsSpan()
                .StartingAt(Time.New(1996, 2, 29));
        }

        [Fact]
        public void last_day_of_next_month_when_next_month_is_February_in_non_leap_year()
        {
            When
                .ItIs("1997-01-20")
                .Parsing("last day of next month")
                .ReturnsSpan()
                .StartingAt(Time.New(1997, 2, 28));
        }

        [Theory]
        [InlineData(Pointer.Type.Future)]
        [InlineData(Pointer.Type.None)]
        [InlineData(Pointer.Type.Past)]
        public void last_day_of_this_month_when_it_is_first_day_of_month(
            Pointer.Type pointer)
        {
            When
                .ItIs("2014-06-01 23:35:00")
                .Parsing("last day of this month")
                .WithOptions(new
                {
                    Context = pointer
                })
                .ReturnsSpan()
                .StartingAt(Time.New(2014, 06, 30));
        }

        [Theory]
        [InlineData(Pointer.Type.Future)]
        [InlineData(Pointer.Type.None)]
        [InlineData(Pointer.Type.Past)]
        public void last_day_of_this_month_when_it_is_last_day_of_month(
            Pointer.Type pointer)
        {
            When
                .ItIs("2014-06-30 23:35:00")
                .Parsing("last day of this month")
                .WithOptions(new
                {
                    Context = pointer
                })
                .ReturnsSpan()
                .StartingAt(Time.New(2014, 06, 30));
        }

        [Theory]
        [InlineData(Pointer.Type.Future)]
        [InlineData(Pointer.Type.None)]
        [InlineData(Pointer.Type.Past)]
        public void last_day_of_this_month_when_it_is_middle_of_month(
            Pointer.Type pointer)
        {
            When
                .ItIs("2014-06-15 23:35:00")
                .Parsing("last day of this month")
                .WithOptions(new
                {
                    Context = pointer
                })
                .ReturnsSpan()
                .StartingAt(Time.New(2014, 06, 30));
        }

        [Fact]
        public void last_day_of_this_month()
        {
            Parse("last day of this month")
                .AssertStartsAt(Time.New(2006, 8, 31));
        }

        [Theory]
        [InlineData(Pointer.Type.Future)]
        [InlineData(Pointer.Type.None)]
        [InlineData(Pointer.Type.Past)]
        public void last_day_of_last_month_when_it_is_first_day_of_month(
            Pointer.Type pointer)
        {
            When
                .ItIs("2014-07-01 23:35:00")
                .Parsing("last day of last month")
                .WithOptions(new
                {
                    FirstDayOfWeek = DayOfWeek.Sunday,
                    Context = pointer
                })
                .ReturnsSpan()
                .StartingAt(Time.New(2014, 06, 30));
        }

        [Theory]
        [InlineData(Pointer.Type.Future)]
        [InlineData(Pointer.Type.None)]
        [InlineData(Pointer.Type.Past)]
        public void last_day_of_last_month_when_it_is_last_day_of_month(
            Pointer.Type pointer)
        {
            When
                .ItIs("2014-07-31 23:35:00")
                .Parsing("last day of last month")
                .WithOptions(new
                {
                    Context = pointer
                })
                .ReturnsSpan()
                .StartingAt(Time.New(2014, 06, 30));
        }

        [Theory]
        [InlineData(Pointer.Type.Future)]
        [InlineData(Pointer.Type.None)]
        [InlineData(Pointer.Type.Past)]
        public void last_day_of_last_month_when_it_is_middle_day_of_month(
            Pointer.Type pointer)
        {
            When
                .ItIs("2014-07-15 23:35:00")
                .Parsing("last day of last month")
                .WithOptions(new
                {
                    Context = pointer
                })
                .ReturnsSpan()
                .StartingAt(Time.New(2014, 06, 30));
        }

        [Fact]
        public void last_day_of_next_week_when_week_starts_on_Monday()
        {
            When
                .ItIs("2006-08-16 14:00:00")
                .Parsing("last day of next week")
                .WithOptions(new { FirstDayOfWeek = DayOfWeek.Monday })
                .ReturnsSpan()
                .StartingAt(Time.New(2006, 8, 27));
        }

        [Fact]
        public void last_day_of_next_week_when_week_starts_on_Sunday()
        {
            When
                .ItIs("2006-08-16 14:00:00")
                .Parsing("last day of next week")
                .WithOptions(new { FirstDayOfWeek = DayOfWeek.Sunday })
                .ReturnsSpan()
                .StartingAt(Time.New(2006, 8, 26));
        }

        [Fact]
        public void last_day_of_this_week_when_week_starts_on_Sunday()
        {
            When
                .ItIs("2006-08-16 14:00:00")
                .Parsing("last day of this week")
                .WithOptions(new { FirstDayOfWeek = DayOfWeek.Sunday })
                .ReturnsSpan()
                .StartingAt(Time.New(2006, 8, 19));
        }

        [Fact]
        public void last_day_of_this_week_when_week_starts_on_Monday()
        {
            When
                .ItIs("2006-08-16 14:00:00")
                .Parsing("last day of this week")
                .WithOptions(new { FirstDayOfWeek = DayOfWeek.Monday })
                .ReturnsSpan()
                .StartingAt(Time.New(2006, 8, 20));
        }

        [Fact]
        public void first_day_of_this_week_when_week_starts_on_Monday()
        {
            When
                .ItIs("2006-08-16 14:00:00")
                .Parsing("first day of this week")
                .WithOptions(new { FirstDayOfWeek = DayOfWeek.Monday })
                .ReturnsSpan()
                .StartingAt(Time.New(2006, 8, 14));
        }

        [Fact]
        public void first_day_of_this_week_when_week_starts_on_Sunday()
        {
            When
                .ItIs("2006-08-16 14:00:00")
                .Parsing("first day of this week")
                .WithOptions(new { FirstDayOfWeek = DayOfWeek.Sunday })
                .ReturnsSpan()
                .StartingAt(Time.New(2006, 8, 13));
        }
    }
}
