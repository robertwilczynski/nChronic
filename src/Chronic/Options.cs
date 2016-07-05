using System;
using System.Globalization;

namespace Chronic
{
    public class Options
    {
        private string _locale;
        public string Locale
        {
            get
            {
                return _locale;
            }
            set
            {
                if (value == null)
                {
                    _locale = string.Empty;
                    return;
                }

                try
                {
                    var culture = CultureInfo.GetCultureInfo(value);
                }
                catch (CultureNotFoundException)
                {
                    _locale = string.Empty;
                    return;
                }

                _locale = value;
            }
        }

        public static readonly int DefaultAmbiguousTimeRange = 6;

        public Func<DateTime> Clock { get; set; }

        public Pointer.Type Context { get; set; }

        public int AmbiguousTimeRange { get; set; }

        public EndianPrecedence EndianPrecedence { get; set; }

        public string OriginalPhrase { get; set; }
        public DayOfWeek FirstDayOfWeek { get; set; }

        public Options()
        {
            Locale = string.Empty;
            AmbiguousTimeRange = DefaultAmbiguousTimeRange;
            EndianPrecedence = EndianPrecedence.Middle;
            Clock = () => DateTime.Now;
            FirstDayOfWeek = DayOfWeek.Sunday;
        }
    }
}