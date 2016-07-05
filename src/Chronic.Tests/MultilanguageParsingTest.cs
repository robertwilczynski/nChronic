using System;

using Xunit;

namespace Chronic.Tests
{
    public class MultilanguageParsingTest : ParsingTestsBase
    {
        protected override DateTime Now()
        {
            return Time.New(2006, 8, 16, 14, 0, 0);
        }
        

        [Fact]
        public void _2016_June_8_18_29_42()
        {
            var parsed = Parse("2016-June-8 18:29:42");
            parsed.AssertStartsAt(Time.New(2016, 6, 8, 18, 29, 42));
        }
        [Fact]
        public void _15_марта_2011()
        {
            var parsed = Parse("15 марта 2011", "ru");
            parsed.AssertStartsAt(Time.New(2011, 3, 15));
        }

        [Fact]
        public void miércoles_22_de_junio_de_2016()
        {
            var parsed = Parse("miércoles, 22 de junio de 2016", "es");
            parsed.AssertStartsAt(Time.New(2016, 6, 22));
        }
    }
}