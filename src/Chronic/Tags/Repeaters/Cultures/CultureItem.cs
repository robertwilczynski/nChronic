using System.Collections.Generic;

using Chronic.Tags.Repeaters.Patterns;

namespace Chronic.Tags.Repeaters.Cultures
{
    public class CultureItem
    {
        public List<MonthPattern> MonthPatterns { get; set; }

        public List<DayOfWeekPattern> DayOfWeekPatterns { get; set; }
    }
}