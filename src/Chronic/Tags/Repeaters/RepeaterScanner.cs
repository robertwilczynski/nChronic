using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Chronic.Tags.Repeaters;

namespace Chronic.Tags.Repeaters
{
    public class RepeaterScanner : ITokenScanner
    {
        static readonly List<Func<Token, Options, ITag>> _scanners = new List
            <Func<Token, Options, ITag>>
            {
                //ScanSeasonNames(token, options),
                ScanMonthNames,
                ScanDayNames,
                ScanDayPortions,
                ScanTimes,
                ScanUnits,
            };

        public IList<Token> Scan(IList<Token> tokens, Options options)
        {
            tokens.ForEach(token =>
                {
                    foreach (var scanner in _scanners)
                    {
                        var tag = scanner(token, options);
                        if (tag != null)
                        {
                            token.Tag(tag);
                            break;
                        }
                    }
                });

            return tokens;
        }

        static ITag ScanUnits(Token token, Options options)
        {
            ITag tag = null;
            foreach (var item in UnitPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    // Explicit factory instead of typeof(RepeaterX) + Activator.CreateInstance.
                    // Reflection-based construction over a possibly-trimmed constructor is not
                    // Native-AOT-safe; a delegate that calls the constructor directly is.
                    tag = item.Factory(options);
                }
            }
            return tag;
        }

        static ITag ScanTimes(Token token, Options options)
        {
            var match = _timePattern.Match(token.Value);
            if (match.Success)
            {
                return new RepeaterTime(match.Value);
            }
            return null;
        }

        static ITag ScanDayPortions(Token token, Options options)
        {
            ITag tag = null;
            foreach (var item in DayPortionPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    tag = new EnumRepeaterDayPortion(item.Portion);
                }
            }
            return tag;
        }

        static ITag ScanDayNames(Token token, Options options)
        {
            ITag tag = null;
            foreach (var item in DayPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    tag = new RepeaterDayName(item.Day);
                }
            }
            return tag;
        }

        static ITag ScanMonthNames(Token token, Options options)
        {
            ITag tag = null;
            foreach (var item in MonthPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    tag = new RepeaterMonthName(item.Month);
                }
            }
            return tag;
        }

        static ITag ScanSeasonNames(Token token, Options options)
        {
            throw new NotImplementedException();
        }

        static readonly Regex _timePattern =
            @"^\d{1,2}(:?\d{2})?([\.:]?\d{2})?$".Compile();

        // Concrete types instead of List<dynamic> of anonymous objects. The `dynamic` member
        // access (item.Pattern / .Portion / .Day / .Month) dispatched through the DLR, which is
        // not Native-AOT-safe. UnitPattern additionally replaces the old typeof(RepeaterX) +
        // Activator.CreateInstance reflection with an explicit factory delegate.
        private class DayPortionPattern
        {
            public readonly Regex Pattern;
            public readonly DayPortion Portion;
            public DayPortionPattern(Regex pattern, DayPortion portion) { Pattern = pattern; Portion = portion; }
        }

        private class DayPattern
        {
            public readonly Regex Pattern;
            public readonly DayOfWeek Day;
            public DayPattern(Regex pattern, DayOfWeek day) { Pattern = pattern; Day = day; }
        }

        private class MonthPattern
        {
            public readonly Regex Pattern;
            public readonly MonthName Month;
            public MonthPattern(Regex pattern, MonthName month) { Pattern = pattern; Month = month; }
        }

        private class UnitPattern
        {
            public readonly Regex Pattern;
            public readonly Func<Options, ITag> Factory;
            public UnitPattern(Regex pattern, Func<Options, ITag> factory) { Pattern = pattern; Factory = factory; }
        }

        static readonly DayPortionPattern[] DayPortionPatterns = new DayPortionPattern[]
            {
                new DayPortionPattern("^ams?$".Compile(), DayPortion.AM),
                new DayPortionPattern("^pms?$".Compile(), DayPortion.PM),
                new DayPortionPattern("^mornings?$".Compile(), DayPortion.MORNING),
                new DayPortionPattern("^afternoons?$".Compile(), DayPortion.AFTERNOON),
                new DayPortionPattern("^evenings?$".Compile(), DayPortion.EVENING),
                new DayPortionPattern("^(night|nite)s?$".Compile(), DayPortion.NIGHT),
            };

        static readonly DayPattern[] DayPatterns = new DayPattern[]
            {
                new DayPattern("^m[ou]n(day)?$".Compile(), DayOfWeek.Monday),
                new DayPattern("^t(ue|eu|oo|u|)s(day)?$".Compile(), DayOfWeek.Tuesday),
                new DayPattern("^tue$".Compile(), DayOfWeek.Tuesday),
                new DayPattern("^we(dnes|nds|nns)day$".Compile(), DayOfWeek.Wednesday),
                new DayPattern("^wed$".Compile(), DayOfWeek.Wednesday),
                new DayPattern("^th(urs|ers)day$".Compile(), DayOfWeek.Thursday),
                new DayPattern("^thu$".Compile(), DayOfWeek.Thursday),
                new DayPattern("^fr[iy](day)?$".Compile(), DayOfWeek.Friday),
                new DayPattern("^sat(t?[ue]rday)?$".Compile(), DayOfWeek.Saturday),
                new DayPattern("^su[nm](day)?$".Compile(), DayOfWeek.Sunday),
            };

        static readonly MonthPattern[] MonthPatterns = new MonthPattern[]
            {
                new MonthPattern("^jan\\.?(uary)?$".Compile(), MonthName.January),
                new MonthPattern("^feb\\.?(ruary)?$".Compile(), MonthName.February),
                new MonthPattern("^mar\\.?(ch)?$".Compile(), MonthName.March),
                new MonthPattern("^apr\\.?(il)?$".Compile(), MonthName.April),
                new MonthPattern("^may$".Compile(), MonthName.May),
                new MonthPattern("^jun\\.?e?$".Compile(), MonthName.June),
                new MonthPattern("^jul\\.?y?$".Compile(), MonthName.July),
                new MonthPattern("^aug\\.?(ust)?$".Compile(), MonthName.August),
                new MonthPattern("^sep\\.?(t\\.?|tember)?$".Compile(), MonthName.September),
                new MonthPattern("^oct\\.?(ober)?$".Compile(), MonthName.October),
                new MonthPattern("^nov\\.?(ember)?$".Compile(), MonthName.November),
                new MonthPattern("^dec\\.?(ember)?$".Compile(), MonthName.December),
            };

        static readonly UnitPattern[] UnitPatterns = new UnitPattern[]
            {
                new UnitPattern("^years?$".Compile(), o => new RepeaterYear()),
                new UnitPattern("^seasons?$".Compile(), o => new RepeaterSeason()),
                new UnitPattern("^months?$".Compile(), o => new RepeaterMonth()),
                new UnitPattern("^fortnights?$".Compile(), o => new RepeaterFortnight()),
                new UnitPattern("^weeks?$".Compile(), o => new RepeaterWeek(o)),
                new UnitPattern("^weekends?$".Compile(), o => new RepeaterWeekend()),
                new UnitPattern("^days?$".Compile(), o => new RepeaterDay()),
                new UnitPattern("^hours?$".Compile(), o => new RepeaterHour()),
                new UnitPattern("^minutes?$".Compile(), o => new RepeaterMinute()),
                new UnitPattern("^seconds?$".Compile(), o => new RepeaterSecond()),
            };
    }
}