using System;
using System.Globalization;

namespace Chronic
{
    public static class DateTimeFormatInfoHelpers
    {
        public static string GetMonthGenitiveName(this DateTimeFormatInfo dateTimeFormatInfo, int month)
        {
            if (month < 1 || month > 13)
            {
                throw new ArgumentOutOfRangeException("month", string.Format("Month argument should be between {0} and {1}", 1, 13));
            }
            return dateTimeFormatInfo.MonthGenitiveNames[month - 1];
        }

        public static string GeAbbreviatedMonthGenitiveName(this DateTimeFormatInfo dateTimeFormatInfo, int month)
        {
            if (month < 1 || month > 13)
            {
                throw new ArgumentOutOfRangeException("month", string.Format("Month argument should be between {0} and {1}", 1, 13));
            }
            return dateTimeFormatInfo.AbbreviatedMonthGenitiveNames[month - 1];
        }
    }
}