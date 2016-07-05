using System;
using System.Collections.Generic;
using System.Globalization;

using Chronic.Tags.Repeaters.Patterns;

namespace Chronic.Tags.Repeaters.Cultures
{
    public static class CulturesPatterns
    {
        private static readonly Dictionary<string, CultureItem> _allCultures = new Dictionary<string, CultureItem>();

        public static CultureItem GetCultureItem(string culture)
        {
            if (!_allCultures.ContainsKey(culture))
                _allCultures[culture] = GenerateCultureItem(culture);

            return _allCultures[culture];

        }

        private static CultureItem GenerateCultureItem(string cultureTag)
        {
            var culture = CultureInfo.GetCultureInfo(cultureTag);
            var dateTimeFormat = culture.DateTimeFormat;

            var patternsForDayOfWeek = new List<DayOfWeekPattern>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                var longName = dateTimeFormat.GetDayName(day);
                var abbreviateName = dateTimeFormat.GetAbbreviatedDayName(day);
                patternsForDayOfWeek.Add(new DayOfWeekPattern() { Pattern = string.Format("^{0}$", longName).Compile(), Day = day });
                patternsForDayOfWeek.Add(new DayOfWeekPattern() { Pattern = string.Format("^{0}$", abbreviateName).Compile(), Day = day });
            }

            var patternsForMonth = new List<MonthPattern>();
            foreach (MonthName month in Enum.GetValues(typeof(MonthName)))
            {
                if (month == MonthName._ZERO_MONTH)
                    continue;
                var intMonth = (int)month;
                var longName = dateTimeFormat.GetMonthGenitiveName(intMonth);
                var abbreviateName = dateTimeFormat.GeAbbreviatedMonthGenitiveName(intMonth);
                patternsForMonth.Add(new MonthPattern() { Pattern = string.Format("^{0}$", longName).Compile(), Month = month });
                patternsForMonth.Add(new MonthPattern() { Pattern = string.Format("^{0}$", abbreviateName).Compile(), Month = month });
            }

            return new CultureItem() { DayOfWeekPatterns = patternsForDayOfWeek, MonthPatterns = patternsForMonth };

        }
    }
}