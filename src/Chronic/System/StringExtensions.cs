using System.Text.RegularExpressions;

namespace Chronic
{
    public static class StringExtensions
    {
        public static string ReplaceAll(this string @this, string pattern, string replacement)
        {
            return Regex.Replace(@this, pattern, replacement);            
        }

        public static Regex Compile(this string @this)
        {
            return new Regex(@this, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}
