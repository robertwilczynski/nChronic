using System;

namespace Chronic
{
    public class Span : IEquatable<Span>
    {
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public Span(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public int Width
        {
            get
            {
                return (int)Math.Floor(Math.Abs((End - Start).TotalSeconds));
            }
        }

        public Span Add(long seconds)
        {
            return new Span(Start.AddSeconds(seconds), End.AddSeconds(seconds));
        }

        public Span Subtract(long seconds)
        {
            return Add(-seconds);
        }

        public bool Contains(DateTime? value)
        {
            return Start <= value && value <= End;
        }

        public DateTime ToTime()
        {
            if (Width > 1)
            {
                return Start.AddSeconds((double)Width / 2);
            }
            else
            {
                return Start;
            }
        }

        public bool Equals(Span other)
        {
            if (other != null)
                return Start == other.Start && End == other.End;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Span)
                return Equals((Span)obj);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                hash = 31 * hash + Start.GetHashCode();
                hash = 31 * hash + End.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return String.Format("({0} - {1})", Start, End);
        }
    }
}
