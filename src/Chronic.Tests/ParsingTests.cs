using System;
using System.Threading;
using Xunit;

namespace Chronic.Tests
{
    public class ParsingTests : ParsingTestsBase
    {
        [Fact]
        public void test_handle_rmn_sd()
        {
            var result = Parse("aug 3").ToTime();
            Assert.Equal(Time.New(2006, 8, 3, 12), result);

            result = Parse("aug 3", new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 8, 3, 12), result);

            result = Parse("aug 20").ToTime();

            Assert.Equal(Time.New(2006, 8, 20, 12), result);

            result = Parse("aug 20", new { Context = Pointer.Type.Future }).ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 12), result);

            result = Parse("may 27").ToTime();
            Assert.Equal(Time.New(2007, 5, 27, 12), result);

            result = Parse("may 28", new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 28, 12), result);

            result =
                Parse("may 28 5pm", new { Context = Pointer.Type.Past }).
                    ToTime();
            ;
            Assert.Equal(Time.New(2006, 5, 28, 17), result);

            result =
                Parse("may 28 at 5pm", new { Context = Pointer.Type.Past })
                    .ToTime();
            ;
            Assert.Equal(Time.New(2006, 5, 28, 17), result);

            result =
                Parse("may 28 at 5:32.19pm",
                      new { Context = Pointer.Type.Past }).ToTime();
            ;
            Assert.Equal(Time.New(2006, 5, 28, 17, 32, 19), result);
        }

        [Fact]
        public void test_handle_rmn_sd_on()
        {
            var time = Parse("5pm on may 28").ToTime();
            Assert.Equal(Time.New(2007, 5, 28, 17), time);

            time = Parse("5pm may 28").ToTime();
            Assert.Equal(Time.New(2007, 5, 28, 17), time);

            time = Parse("5 on may 28", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(2007, 5, 28, 05), time);
        }

        [Fact]
        public void test_handle_rmn_od()
        {
            var time = Parse("may 27th").ToTime();
            Assert.Equal(Time.New(2007, 5, 27, 12), time);

            time = Parse("may 27th", new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 27, 12), time);

            time = Parse("may 27th 5:00 pm",
                            new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 27, 17), time);

            time = Parse("may 27th at 5pm",
                            new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 27, 17), time);

            time = Parse("may 27th at 5", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(2007, 5, 27, 5), time);
        }

        [Fact]
        public void test_handle_od_rmn()
        {
            var time = Parse("22nd February").ToTime();
            Assert.Equal(Time.New(2007, 2, 22, 12), time);

            time = Parse("31st of may at 6:30pm").ToTime();
            Assert.Equal(Time.New(2007, 5, 31, 18, 30), time);

            time = Parse("11th december 8am").ToTime();
            Assert.Equal(Time.New(2006, 12, 11, 8), time);
        }

        [Fact]
        public void test_handle_sy_rmn_od()
        {
            var time = Parse("2009 May 22nd").ToTime();
            Assert.Equal(Time.New(2009, 05, 22, 12), time);
        }

        [Fact]
        public void test_handle_sd_rmn()
        {
            var time = Parse("22 February").ToTime();
            Assert.Equal(Time.New(2007, 2, 22, 12), time);

            time = Parse("31 of may at 6:30pm").ToTime();
            Assert.Equal(Time.New(2007, 5, 31, 18, 30), time);

            time = Parse("11 december 8am").ToTime();
            Assert.Equal(Time.New(2006, 12, 11, 8), time);
        }

        [Fact]
        public void test_handle_rmn_od_on()
        {
            var time = Parse("5:00 pm may 27th",
                             new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 27, 17), time);

            time = Parse("05:00 pm may 27th", new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 27, 17), time);

            time = Parse("5pm on may 27th", new { Context = Pointer.Type.Past }).ToTime();
            Assert.Equal(Time.New(2006, 5, 27, 17), time);

            time = Parse("5 on may 27th", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(2007, 5, 27, 5), time);
        }

        [Fact]
        public void test_handle_rmn_sy()
        {
            var time = Parse("may 97").ToTime();
            Assert.Equal(Time.New(1997, 5, 16, 12), time);

            time = Parse("may 33", new { AmbiguousYearFutureBias = 10 }).ToTime();
            Assert.Equal(Time.New(2033, 5, 16, 12), time);

            time = Parse("may 32").ToTime();
            Assert.Equal(Time.New(2032, 5, 16, 12, 0, 0), time);
        }

        [Fact]
        public void test_handle_rdn_rmn_sd_t_tz_sy()
        {
            var time = Parse("Mon Apr 02 17:00:00 PDT 2007").ToTime();
            Assert.True(false, "not implemented");
            Assert.Equal(1175558400, Convert.ToInt32(time));
        }

        [Fact]
        public void test_handle_sy_sm_sd_t_tz()
        {
            var time = Parse("2011-07-03 22:11:35 +0100").ToTime();
            Assert.Equal(1309727495, Convert.ToInt32(time));

            time = Parse("2011-07-03 22:11:35 +01:00").ToTime();
            Assert.Equal(1309727495, Convert.ToInt32(time));

            time = Parse("2011-07-03 21:11:35 UTC").ToTime();
            Assert.Equal(1309727495, Convert.ToInt32(time));
        }

        [Fact]
        public void test_handle_rmn_sd_sy()
        {
            var time = Parse("November 18, 2010").ToTime();
            Assert.Equal(Time.New(2010, 11, 18, 12), time);

            time = Parse("Jan 1,2010").ToTime();
            Assert.Equal(Time.New(2010, 1, 1, 12), time);

            time = Parse("February 14, 2004").ToTime();
            Assert.Equal(Time.New(2004, 2, 14, 12), time);

            time = Parse("jan 3 2010").ToTime();
            Assert.Equal(Time.New(2010, 1, 3, 12), time);

            time = Parse("jan 3 2010 midnight").ToTime();
            Assert.Equal(Time.New(2010, 1, 4, 0), time);

            time = Parse("jan 3 2010 at midnight").ToTime();
            Assert.Equal(Time.New(2010, 1, 4, 0), time);

            time = Parse("jan 3 2010 at 4", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(2010, 1, 3, 4), time);

            time = Parse("may 27, 1979").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 12), time);

            time = Parse("may 27 79").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 12), time);

            time = Parse("may 27 79 4:30").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 16, 30), time);

            time = Parse("may 27 79 at 4:30", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 4, 30), time);

            time = Parse("may 27 32").ToTime();
            Assert.Equal(Time.New(2032, 5, 27, 12, 0, 0), time);
        }

        [Fact]
        public void test_handle_rmn_od_sy()
        {
            var time = Parse("may 1st 01").ToTime();
            Assert.Equal(Time.New(2001, 5, 1, 12), time);

            time = Parse("November 18th 2010").ToTime();
            Assert.Equal(Time.New(2010, 11, 18, 12), time);

            time = Parse("November 18th, 2010").ToTime();
            Assert.Equal(Time.New(2010, 11, 18, 12), time);

            time = Parse("November 18th 2010 midnight").ToTime();
            Assert.Equal(Time.New(2010, 11, 19, 0), time);

            time = Parse("November 18th 2010 at midnight").ToTime();
            Assert.Equal(Time.New(2010, 11, 19, 0), time);

            time = Parse("November 18th 2010 at 4").ToTime();
            Assert.Equal(Time.New(2010, 11, 18, 16), time);

            time =
                Parse("November 18th 2010 at 4", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(2010, 11, 18, 4), time);

            time = Parse("March 30th, 1979").ToTime();
            Assert.Equal(Time.New(1979, 3, 30, 12), time);

            time = Parse("March 30th 79").ToTime();
            Assert.Equal(Time.New(1979, 3, 30, 12), time);

            time = Parse("March 30th 79 4:30").ToTime();
            Assert.Equal(Time.New(1979, 3, 30, 16, 30), time);

            time = Parse("March 30th 79 at 4:30", new { AmbiguousTimeRange = 0 }).ToTime();
            Assert.Equal(Time.New(1979, 3, 30, 4, 30), time);
        }

        [Fact]
        public void test_handle_od_rmn_sy()
        {
            var
            time = Parse("22nd February 2012").ToTime();
            Assert.Equal(Time.New(2012, 2, 22, 12), time);

            time = Parse("11th december 79").ToTime();
            Assert.Equal(Time.New(1979, 12, 11, 12), time);
        }

        [Fact]
        public void test_handle_sd_rmn_sy()
        {
            var
            time = Parse("3 jan 2010").ToTime();
            Assert.Equal(Time.New(2010, 1, 3, 12), time);

            time = Parse("3 jan 2010 4pm").ToTime();
            Assert.Equal(Time.New(2010, 1, 3, 16), time);

            time = Parse("27 Oct 2006 7:30pm").ToTime();
            Assert.Equal(Time.New(2006, 10, 27, 19, 30), time);
        }

        [Fact]
        public void test_handle_sm_sd_sy()
        {
            var time = Parse("5/27/1979").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 12), time);

            time = Parse("5/27/1979 4am").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 4), time);

            time = Parse("7/12/11").ToTime();
            Assert.Equal(Time.New(2011, 7, 12, 12), time);

            time = Parse("7/12/11", new { EndianPrecedence = EndianPrecedence.Little }).ToTime();
            Assert.Equal(Time.New(2011, 12, 7, 12), time);

            time = Parse("9/19/2011 6:05:57 PM").ToTime();
            Assert.Equal(Time.New(2011, 9, 19, 18, 05, 57), time);

            // month day overflows
            var span = Parse("30/2/2000");
            Assert.Null(span);
        }

        [Fact]
        public void test_handle_sd_sm_sy()
        {
            var
            time = Parse("27/5/1979").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 12), time);

            time = Parse("27/5/1979 @ 0700").ToTime();
            Assert.Equal(Time.New(1979, 5, 27, 7), time);
        }

        [Fact]
        public void test_handle_sy_sm_sd()
        {
            var
            time = Parse("2000-1-1").ToTime();
            Assert.Equal(Time.New(2000, 1, 1, 12), time);

            time = Parse("2006-08-20").ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 12), time);

            time = Parse("2006-08-20 7pm").ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 19), time);

            time = Parse("2006-08-20 03:00").ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 3), time);

            time = Parse("2006-08-20 03:30:30").ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 3, 30, 30), time);

            time = Parse("2006-08-20 15:30:30").ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 15, 30, 30), time);

            time = Parse("2006-08-20 15:30.30").ToTime();
            Assert.Equal(Time.New(2006, 8, 20, 15, 30, 30), time);

            time = Parse("1902-08-20").ToTime();
            Assert.Equal(Time.New(1902, 8, 20, 12, 0, 0), time);
        }

        [Fact]
        public void test_handle_sm_sy()
        {
            var
            time = Parse("05/06").ToTime();
            Assert.Equal(Time.New(2006, 5, 16, 12), time);

            time = Parse("12/06").ToTime();
            Assert.Equal(Time.New(2006, 12, 16, 12), time);

            var span = Parse("13/06");
            Assert.Null(span);
        }

        [Fact]
        public void test_handle_r()
        {

        }

        [Fact]
        public void test_handle_r_g_r()
        {

        }

        [Fact]
        public void test_handle_srp()
        {

        }

        [Fact]
        public void test_handle_s_r_p()
        {
        }

        [Fact]
        public void test_handle_p_s_r()
        {
        }

        [Fact]
        public void test_handle_s_r_p_a()
        {
        }

        [Fact]
        public void test_handle_orr()
        {
        }

        [Fact]
        public void test_handle_o_r_s_r()
        {

            var time = Parse("3rd wednesday in november").ToTime();
            Assert.Equal(Time.New(2006, 11, 15, 12), time);
            Assert.Equal(Time.New(2006, 11, 15, 12), time);

            var span = Parse("10th wednesday in november");
            Assert.Null(span);

            // time = Parse("3rd wednesday in 2007").ToTime();
            // Assert.Equal(Time.New(2007, 1, 20, 12), time);

        }

        [Fact]
        public void test_handle_o_r_g_r()
        {
            var span = Parse("3rd month next year");
            Assert.Equal(Time.New(2007, 3), span.Start);

            span = Parse("3rd month next year");
            Assert.Equal(Time.New(2007, 3, 1), span.Start);

            span = Parse("3rd thursday this september");
            Assert.Equal(Time.New(2006, 9, 21, 12), span.ToTime());

            var now = new DateTime(2010, 2, 1);
            span =
                Parse("3rd thursday this november", new Options { Clock = () => now });
            Assert.Equal(Time.New(2010, 11, 18, 12), span.ToTime());

            span = Parse("4th day last week");
            Assert.Equal(Time.New(2006, 8, 9, 12), span.ToTime());
        }

        // end of testing handlers

        [Fact]
        public void test_parse_guess_s_r_p_a()
        {
            // past

            var time = Parse("3 years ago").ToTime();
            Assert.Equal(Time.New(2003, 8, 16, 14), time);

            time = Parse("3 years ago tomorrow").ToTime();
            Assert.Equal(Time.New(2003, 8, 17, 12), time);

            time = Parse("3 months ago saturday at 5:00 pm").ToTime();
            Assert.Equal(Time.New(2006, 5, 19, 17), time);

            time = Parse("2 days from this second").ToTime();
            Assert.Equal(Time.New(2006, 8, 18, 14), time);

            time = Parse("7 hours before tomorrow at midnight").ToTime();
            Assert.Equal(Time.New(2006, 8, 17, 17), time);

            time = Parse("3 years ago this friday").ToTime();
            Assert.Equal(Time.New(2003, 8, 18, 12), time);

            // future
        }

        [Fact]
        public void test_parse_guess_nonsense()
        {
            var result = Parse("some stupid nonsense");
            Assert.Null(result);

            result = Parse("Ham Sandwich");
            Assert.Null(result);
        }

        [Fact]
        public void now_is_parsed_correctly()
        {
            Assert.Equal(Now, Parse("now").ToTime());
        }

        [Fact]
        public void repeated_parsing_of_now_yields_different_values()
        {
            var parser = new Parser();
            var result1 = parser.Parse("now");
            Thread.Sleep(100);
            var result2 = parser.Parse("now");
            Assert.NotEqual(result1.ToTime(), result2.ToTime());
        }
    }
}
