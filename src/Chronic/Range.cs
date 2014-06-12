using System;
namespace Chronic
{
    public class Range : IEquatable<Range>
    {
        public long StartSecond { get; private set; }
        public long EndSecond { get; private set; }

        public long Width
        {
            get { return EndSecond - StartSecond; }
        }

        public Range(long start, long end)
        {
            StartSecond = start;
            EndSecond = end;
        }

        public bool Contains(long point)
        {
            return StartSecond <= point && EndSecond >= point;
        }

        public bool Equals(Range other)
        {
            if (other != null)
                return StartSecond == other.StartSecond && EndSecond == other.EndSecond;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Range)
                return Equals((Range)obj);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = 31 * hash + StartSecond.GetHashCode();
                hash = 31 * hash + EndSecond.GetHashCode();
                return hash;
            }
        }
    }
}