using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Chronic.Tags.Repeaters.Cultures;
using Chronic.Tags.Repeaters.Patterns;

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
            UnitPatterns.ForEach(item =>
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    var type = (Type)item.Unit;
                    var hasCtorWithOptions = type.GetConstructors().Any(ctor =>
                    {
                        var parameters = ctor.GetParameters().ToArray();
                        return
                            parameters.Length == 1
                            && parameters.First().ParameterType == typeof(Options);
                    });
                    var ctorParameters = hasCtorWithOptions
                        ? new[] { options }
                        : new object[0];

                    tag = Activator.CreateInstance(
                        type,
                        ctorParameters) as ITag;

                    return;
                }
            });
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
            DayPortionPatterns.ForEach(item =>
                {
                    if (item.Pattern.IsMatch(token.Value))
                    {
                        tag = new EnumRepeaterDayPortion(item.Portion);
                        return;
                    }
                });
            return tag;
        }

        static ITag ScanDayNames(Token token, Options options)
        {
            ITag tag = null;
            foreach (var item in DayPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    return new RepeaterDayName(item.Day);
                }
            }

            // next search in localized days
            foreach (var item in CulturesPatterns.GetCultureItem(options.Locale).DayOfWeekPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    return new RepeaterDayName(item.Day);
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
                    return new RepeaterMonthName(item.Month);
                }
            }

            // next search in localized months
            foreach (var item in CulturesPatterns.GetCultureItem(options.Locale).MonthPatterns)
            {
                if (item.Pattern.IsMatch(token.Value))
                {
                    return new RepeaterMonthName(item.Month);
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

        static readonly List<DayPortionPattern> DayPortionPatterns = new List<DayPortionPattern>
            {
                new DayPortionPattern{ Pattern = "^ams?$".Compile(), Portion = DayPortion.AM },
                new DayPortionPattern{ Pattern = "^pms?$".Compile(), Portion = DayPortion.PM },
                new DayPortionPattern{ Pattern = "^mornings?$".Compile(), Portion = DayPortion.MORNING },
                new DayPortionPattern{ Pattern = "^afternoons?$".Compile(), Portion = DayPortion.AFTERNOON },
                new DayPortionPattern{ Pattern = "^evenings?$".Compile(), Portion = DayPortion.EVENING },
                new DayPortionPattern{ Pattern = "^(night|nite)s?$".Compile(), Portion = DayPortion.NIGHT },
            };

        static readonly List<DayOfWeekPattern> DayPatterns = new List<DayOfWeekPattern>
            {
                new DayOfWeekPattern{ Pattern = "^m[ou]n(day)?$".Compile(), Day = DayOfWeek.Monday },
                new DayOfWeekPattern{ Pattern = "^t(ue|eu|oo|u|)s(day)?$".Compile(), Day = DayOfWeek.Tuesday },
                new DayOfWeekPattern{ Pattern = "^tue$".Compile(), Day = DayOfWeek.Tuesday },
                new DayOfWeekPattern{ Pattern = "^we(dnes|nds|nns)day$".Compile(), Day = DayOfWeek.Wednesday },
                new DayOfWeekPattern{ Pattern = "^wed$".Compile(), Day = DayOfWeek.Wednesday },
                new DayOfWeekPattern{ Pattern = "^th(urs|ers)day$".Compile(), Day = DayOfWeek.Thursday },
                new DayOfWeekPattern{ Pattern = "^thu$".Compile(), Day = DayOfWeek.Thursday },
                new DayOfWeekPattern{ Pattern = "^fr[iy](day)?$".Compile(), Day = DayOfWeek.Friday },
                new DayOfWeekPattern{ Pattern = "^sat(t?[ue]rday)?$".Compile(), Day = DayOfWeek.Saturday },
                new DayOfWeekPattern{ Pattern = "^su[nm](day)?$".Compile(), Day = DayOfWeek.Sunday }

            };

        static readonly List<MonthPattern> MonthPatterns = new List<MonthPattern>
            {
                new MonthPattern{ Pattern = "^jan\\.?(uary)?$".Compile(), Month = MonthName.January },
                new MonthPattern{ Pattern = "^feb\\.?(ruary)?$".Compile(), Month = MonthName.February },
                new MonthPattern{ Pattern = "^mar\\.?(ch)?$".Compile(), Month = MonthName.March },
                new MonthPattern{ Pattern = "^apr\\.?(il)?$".Compile(), Month = MonthName.April },
                new MonthPattern{ Pattern = "^may$".Compile(), Month = MonthName.May },
                new MonthPattern{ Pattern = "^jun\\.?e?$".Compile(), Month = MonthName.June },
                new MonthPattern{ Pattern = "^jul\\.?y?$".Compile(), Month = MonthName.July },
                new MonthPattern{ Pattern = "^aug\\.?(ust)?$".Compile(), Month = MonthName.August },
                new MonthPattern{ Pattern = "^sep\\.?(t\\.?|tember)?$".Compile(), Month = MonthName.September },
                new MonthPattern{ Pattern = "^oct\\.?(ober)?$".Compile(), Month = MonthName.October },
                new MonthPattern{ Pattern = "^nov\\.?(ember)?$".Compile(), Month = MonthName.November },
                new MonthPattern{ Pattern = "^dec\\.?(ember)?$".Compile(), Month = MonthName.December }

            };

        static readonly List<UnitPattern> UnitPatterns = new List<UnitPattern>
            {
                new UnitPattern{ Pattern = "^years?$".Compile(), Unit = typeof(RepeaterYear) },
                new UnitPattern{ Pattern = "^seasons?$".Compile(), Unit = typeof(RepeaterSeason) },
                new UnitPattern{ Pattern = "^months?$".Compile(), Unit = typeof(RepeaterMonth) },
                new UnitPattern{ Pattern = "^fortnights?$".Compile(), Unit = typeof(RepeaterFortnight) },
                new UnitPattern{ Pattern = "^weeks?$".Compile(), Unit = typeof(RepeaterWeek) },
                new UnitPattern{ Pattern = "^weekends?$".Compile(), Unit = typeof(RepeaterWeekend) },
                new UnitPattern{ Pattern = "^days?$".Compile(), Unit = typeof(RepeaterDay) },
                new UnitPattern{ Pattern = "^hours?$".Compile(), Unit = typeof(RepeaterHour) },
                new UnitPattern{ Pattern = "^minutes?$".Compile(), Unit = typeof(RepeaterMinute) },
                new UnitPattern{ Pattern = "^seconds?$".Compile(), Unit = typeof(RepeaterSecond) }
            };
    }
}