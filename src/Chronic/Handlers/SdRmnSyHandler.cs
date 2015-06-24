using Chronic.Tags.Repeaters;
using System.Collections.Generic;
using System.Linq;

namespace Chronic.Handlers
{
    public class SdRmnSyHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
		{
			int distance = 0;
			if (tokens[0].GetTag<RepeaterDayName>() != null)
				distance = 1;

            var monthDayYear = new List<Token> { tokens[distance + 1], tokens[distance + 0], tokens[distance + 2] };
            monthDayYear.AddRange(tokens.Skip(distance + 3).ToList());
            return new RmnSdSyHandler().Handle(monthDayYear, options);
        }
    }
}