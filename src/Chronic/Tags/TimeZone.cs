using System.Text.RegularExpressions;

namespace Chronic
{
    // TODO : scanning not implemented
    public class TimeZone : Tag<object>
    {
        private static readonly Regex TIMEZONE_PATTERN = "[pmce][ds]t".Compile();
        public static readonly object TZ = new object();

        public TimeZone()
            : base(null)
        {

        }

        public override string ToString()
        {
            return "timezone";
        }
    }
}