using System;
using System.Text.RegularExpressions;

namespace Chronic.Tags.Repeaters.Patterns
{
    public struct UnitPattern
    {
        public Regex Pattern;

        public Type Unit;
    }
}