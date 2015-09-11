using System;
using Xunit;

namespace Chronic.Tests.Parsing
{
    public class KnownPatternsParsingTests : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }

        [Fact]
        public void rmn_sd()
        {
            Parse("december in 17")
                .AssertEquals(Time.New(2006, 12, 17, 12));
            
            Parse("december on 17")
                .AssertEquals(Time.New(2006, 12, 17, 12));

            Parse("aug 3")
                .AssertEquals(Time.New(2006, 8, 3, 12));

            Parse("aug 3", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 8, 3, 12));

            Parse("aug 20")
                .AssertEquals(Time.New(2006, 8, 20, 12));

            Parse("aug 20", new { Context = Pointer.Type.Future })
                .AssertEquals(Time.New(2006, 8, 20, 12));

            Parse("may 27")
                .AssertEquals(Time.New(2007, 5, 27, 12));

            Parse("may 28", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 28, 12));

            Parse("may 28 5pm", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 28, 17));

            Parse("may 28 at 5pm", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 28, 17));

            Parse("may 28 at 5:32.19pm", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 28, 17, 32, 19));
        }

        [Fact]
        public void rmn_sd_on()
        {
            Parse("5pm on may 28")
                .AssertEquals(Time.New(2007, 5, 28, 17));

            Parse("5pm may 28")
                .AssertEquals(Time.New(2007, 5, 28, 17));

            Parse("5 on may 28", new { AmbiguousTimeRange = 0 })
                .AssertEquals(Time.New(2007, 5, 28, 05));
        }

        [Fact]
        public void rmn_od()
        {
            Parse("may 27th")
                .AssertEquals(Time.New(2007, 5, 27, 12));

            Parse("may 27th", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 27, 12));

            Parse("may 27th 5:00 pm", new { Context = Pointer.Type.Past })
                    .AssertEquals(Time.New(2006, 5, 27, 17));

            Parse("may 27th at 5pm", new { Context = Pointer.Type.Past })
                    .AssertEquals(Time.New(2006, 5, 27, 17));

            Parse("may 27th at 5", new { AmbiguousTimeRange = 0 })
                .AssertEquals(Time.New(2007, 5, 27, 5));
        }

        [Fact]
        public void od_rmn()
        {
            Parse("22nd February")
                .AssertEquals(Time.New(2007, 2, 22, 12));

            Parse("31st of may at 6:30pm")
                .AssertEquals(Time.New(2007, 5, 31, 18, 30));

            Parse("11th december 8am")
                .AssertEquals(Time.New(2006, 12, 11, 8));
        }

        [Fact]
        public void sy_rmn_od()
        {
            Parse("2009 May 22nd")
                .AssertEquals(Time.New(2009, 05, 22, 12));
        }

        [Fact]
        public void sd_rmn()
        {
            Parse("22 February")
                .AssertEquals(Time.New(2007, 2, 22, 12));

            Parse("31 of may at 6:30pm")
                .AssertEquals(Time.New(2007, 5, 31, 18, 30));

            Parse("11 december 8am")
                .AssertEquals(Time.New(2006, 12, 11, 8));
        }

        [Fact]
        public void rmn_od_on()
        {
            Parse("5:00 pm may 27th", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 27, 17));

            Parse("05:00 pm may 27th", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 27, 17));

            Parse("5pm on may 27th", new { Context = Pointer.Type.Past })
                .AssertEquals(Time.New(2006, 5, 27, 17));

            Parse("5 on may 27th", new { AmbiguousTimeRange = 0 })
                .AssertEquals(Time.New(2007, 5, 27, 5));
        }

        [Fact]
        public void rmn_sy()
        {
            Parse("may 97")
                .AssertEquals(Time.New(1997, 5, 16, 12));

            Parse("may 33", new { AmbiguousYearFutureBias = 10 })
                .AssertEquals(Time.New(2033, 5, 16, 12));

            Parse("may 32")
                .AssertEquals(Time.New(2032, 5, 16, 12, 0, 0));
        }

        [Fact]
        public void rmn_sd_sy()
        {
            Parse("November 18, 2010")
                .AssertEquals(Time.New(2010, 11, 18, 12));

            Parse("Jan 1,2010")
                .AssertEquals(Time.New(2010, 1, 1, 12));

            Parse("February 14, 2004")
                .AssertEquals(Time.New(2004, 2, 14, 12));

            Parse("jan 3 2010")
                .AssertEquals(Time.New(2010, 1, 3, 12));

            Parse("jan 3 2010 midnight")
                .AssertEquals(Time.New(2010, 1, 4, 0));

            Parse("jan 3 2010 at midnight")
                .AssertEquals(Time.New(2010, 1, 4, 0));

            Parse("jan 3 2010 at 4", new { AmbiguousTimeRange = 0 })
                .AssertEquals(Time.New(2010, 1, 3, 4));

            Parse("may 27, 1979")
                .AssertEquals(Time.New(1979, 5, 27, 12));

            Parse("may 27 79")
                .AssertEquals(Time.New(1979, 5, 27, 12));

            Parse("may 27 79 4:30")
                .AssertEquals(Time.New(1979, 5, 27, 16, 30));

            Parse("may 27 79 at 4:30", new { AmbiguousTimeRange = 0 }).
                AssertEquals(Time.New(1979, 5, 27, 4, 30));

            Parse("may 27 32")
                .AssertEquals(Time.New(2032, 5, 27, 12, 0, 0));
        }

        [Fact]
        public void rmn_od_sy()
        {
            Parse("may 1st 01")
                .AssertEquals(Time.New(2001, 5, 1, 12));

            Parse("November 18th 2010")
                .AssertEquals(Time.New(2010, 11, 18, 12));

            Parse("November 18th, 2010")
                .AssertEquals(Time.New(2010, 11, 18, 12));

            Parse("November 18th 2010 midnight")
                .AssertEquals(Time.New(2010, 11, 19, 0));

            Parse("November 18th 2010 at midnight")
                .AssertEquals(Time.New(2010, 11, 19, 0));

            Parse("November 18th 2010 at 4")
                .AssertEquals(Time.New(2010, 11, 18, 16));


            Parse("November 18th 2010 at 4", new { AmbiguousTimeRange = 0 }).
                AssertEquals(Time.New(2010, 11, 18, 4));

            Parse("March 30th, 1979")
                .AssertEquals(Time.New(1979, 3, 30, 12));

            Parse("March 30th 79")
                .AssertEquals(Time.New(1979, 3, 30, 12));

            Parse("March 30th 79 4:30")
                .AssertEquals(Time.New(1979, 3, 30, 16, 30));

            Parse("March 30th 79 at 4:30", new { AmbiguousTimeRange = 0 }).
                AssertEquals(Time.New(1979, 3, 30, 4, 30));
        }

        [Fact]
        public void od_rmn_sy()
        {
            Parse("22nd February 2012")
                .AssertEquals(Time.New(2012, 2, 22, 12));

            Parse("11th december 79")
                .AssertEquals(Time.New(1979, 12, 11, 12));
        }

        [Fact]
        public void sd_rmn_sy()
        {
            Parse("3 jan 2010")
                .AssertEquals(Time.New(2010, 1, 3, 12));

            Parse("3 jan 2010 4pm")
                .AssertEquals(Time.New(2010, 1, 3, 16));

            Parse("27 Oct 2006 7:30pm")
                .AssertEquals(Time.New(2006, 10, 27, 19, 30));

			Parse("monday 3 jan 2010")
				.AssertEquals(Time.New(2010, 1, 3, 12));
        }

        [Fact]
        public void sm_sd_sy()
        {
            Parse("5/27/1979")
                .AssertEquals(Time.New(1979, 5, 27, 12));

            Parse("5/27/1979 4am")
                .AssertEquals(Time.New(1979, 5, 27, 4));

            Parse("7/12/11")
                .AssertEquals(Time.New(2011, 7, 12, 12));

            Parse("7/12/11", new { EndianPrecedence = EndianPrecedence.Little }).
                AssertEquals(Time.New(2011, 12, 7, 12));

            Parse("9/19/2011 6:05:57 PM")
                .AssertEquals(Time.New(2011, 9, 19, 18, 05, 57));

            // month day overflows
            Parse("30/2/2000").AssertIsNull();
        }

        [Fact]
        public void sd_sm_sy()
        {
            Parse("27/5/1979")
                .AssertEquals(Time.New(1979, 5, 27, 12));

            Parse("27/5/1979 @ 0700")
                .AssertEquals(Time.New(1979, 5, 27, 7));
        }

        [Fact]
        public void sy_sm_sd()
        {
            Parse("2000-1-1")
                .AssertEquals(Time.New(2000, 1, 1, 12));

            Parse("2006-08-20")
                .AssertEquals(Time.New(2006, 8, 20, 12));

            Parse("2006-08-20 7pm")
                .AssertEquals(Time.New(2006, 8, 20, 19));

            Parse("2006-08-20 03:00")
                .AssertEquals(Time.New(2006, 8, 20, 3));

            Parse("2006-08-20 03:30:30")
                .AssertEquals(Time.New(2006, 8, 20, 3, 30, 30));

            Parse("2006-08-20 15:30:30")
                .AssertEquals(Time.New(2006, 8, 20, 15, 30, 30));

            Parse("2006-08-20 15:30.30")
                .AssertEquals(Time.New(2006, 8, 20, 15, 30, 30));

            Parse("1902-08-20")
                .AssertEquals(Time.New(1902, 8, 20, 12, 0, 0));
        }

        [Fact]
        public void sm_sy()
        {
            Parse("05/06")
                .AssertEquals(Time.New(2006, 5, 16, 12));

            Parse("12/06")
                .AssertEquals(Time.New(2006, 12, 16, 12));

            Parse("13/06").AssertIsNull();
        }

        [Fact]
        public void r()
        {
        }

        [Fact]
        public void r_g_r()
        {
        }

        [Fact]
        public void srp()
        {
        }

        [Fact]
        public void s_r_p()
        {
        }

        [Fact]
        public void p_s_r()
        {
        }

        [Fact]
        public void orr()
        {
        }

        [Fact]
        public void o_r_s_r()
        {
            Parse("3rd wednesday in november").
                AssertEquals(Time.New(2006, 11, 15, 12));

			Parse("1 friday in november").
				AssertEquals(Time.New(2006, 11, 3, 12));

            Parse("10th wednesday in november").AssertIsNull();

            // Parse("3rd wednesday in 2007").AssertEquals(
            // Time.New(2007, 1, 20, 12));
        }

        [Fact]
        public void o_r_g_r()
        {
            Parse("3rd month next year")
                .AssertStartsAt(Time.New(2007, 3));

            Parse("3rd month next year")
                .AssertStartsAt(Time.New(2007, 3, 1));

            Parse("3rd thursday this september")
                .AssertEquals(Time.New(2006, 9, 21, 12));

            var now = new DateTime(2010, 2, 1);
            Parse("3rd thursday this november", new Options { Clock = () => now })
                .AssertEquals(Time.New(2010, 11, 18, 12));

            Parse("4th day last week")
                .AssertEquals(Time.New(2006, 8, 9, 12));
        }

        // end of testing handlers

        [Fact]
        public void s_r_p_a()
        {
            // past

            Parse("3 years ago")
                .AssertEquals(Time.New(2003, 8, 16, 14));

            Parse("3 years ago tomorrow")
                .AssertEquals(Time.New(2003, 8, 17, 12));

            Parse("3 months ago saturday at 5:00 pm")
                .AssertEquals(Time.New(2006, 5, 19, 17));

            Parse("2 days from this second")
                .AssertEquals(Time.New(2006, 8, 18, 14));

            Parse("7 hours before tomorrow at midnight")
                .AssertEquals(Time.New(2006, 8, 17, 17));

            Parse("3 years ago this friday")
                .AssertEquals(Time.New(2003, 8, 18, 12));

            // future
        }

        [Fact]
        public void parsing_nonsense()
        {
            Parse("some stupid nonsense").AssertIsNull();

            Parse("Ham Sandwich").AssertIsNull();
        }
    }
}