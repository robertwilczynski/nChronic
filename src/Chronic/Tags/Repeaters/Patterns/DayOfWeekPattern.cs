using System;
using System.Text.RegularExpressions;

namespace Chronic.Tags.Repeaters.Patterns
{
    public struct DayOfWeekPattern
    {
        public Regex Pattern;

        public DayOfWeek Day;
    }
}