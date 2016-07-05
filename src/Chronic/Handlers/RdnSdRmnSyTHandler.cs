using System;
using System.Collections.Generic;
using System.Linq;

using Chronic.Tags.Repeaters;

namespace Chronic.Handlers
{
    public class RdnSdRmnSyTHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
            var day = tokens[0].GetTag<RepeaterDayName>().Value;
            var scalarDay = tokens[1].GetTag<ScalarDay>().Value;
            var month = tokens[2].GetTag<RepeaterMonthName>();
            var year = tokens[3].GetTag<ScalarYear>().Value;
            var time_tokens = tokens.Skip(4).ToList();
            if (Time.IsMonthOverflow(year, (int)month.Value, scalarDay))
            {
                return null;
            }
            try
            {
                var dayStart = Time.New(year, (int)month.Value, scalarDay);
                return Utils.DayOrTime(dayStart, time_tokens, options);

            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}