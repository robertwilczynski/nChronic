using System;
using System.Collections.Generic;
using System.Linq;
using Chronic.Tags.Repeaters;

namespace Chronic.Handlers
{
    public class OrdinalGrabberMonthHandler: IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
            var day = tokens[0].GetTag<OrdinalDay>().Value;
            var outerSpan = tokens.Skip(1).Take(2).GetAnchor(options);
            return Utils.DayOrTime(outerSpan.Start.Value.Date.AddDays(day - 1), new Token[0], options);
        }
    }
}