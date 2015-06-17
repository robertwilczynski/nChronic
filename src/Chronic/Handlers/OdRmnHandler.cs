using System;
using System.Collections.Generic;
using System.Linq;
using Chronic.Tags.Repeaters;
using Chronic;

namespace Chronic.Handlers
{
    public class OdRmnHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
            var day = tokens[0].GetTag<OrdinalDay>().Value;

            var now = options.Clock();
            var monthSpecified = tokens.Count > 1 && tokens[1].IsTaggedAs<RepeaterMonthName>();
            var month = monthSpecified ? tokens[1].GetTag<RepeaterMonthName>() : GetMonth(day, now);            

            if (Time.IsMonthOverflow(now.Year, (int)month.Value, day))
            {
                return null;
            }
            return Utils.HandleMD(month, day, tokens.Skip(monthSpecified ? 2 : 1).ToList(), options);
        }

        private RepeaterMonthName GetMonth(int day, DateTime now)
        {          
            if (now.Day < day)
                return new RepeaterMonthName((MonthName)(now.Month));
            return new RepeaterMonthName((MonthName)now.AddMonths(1).Month);
        }
    }
}