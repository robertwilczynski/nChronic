using System.Collections.Generic;
using System.Linq;
using Chronic.Tags.Repeaters;

namespace Chronic.Handlers
{
    public class RHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
			if (tokens.Count >= 2
			   && tokens[0].IsTaggedAs<Grabber>()
			   && (tokens[1].IsTaggedAs<RepeaterDayName>() || tokens[1].IsTaggedAs<RepeaterWeekend>()))
			{
				var repeaterWeek = new RepeaterWeek(options);
				repeaterWeek.Now = (tokens[1].IsTaggedAs<RepeaterDayName>()) 
									? tokens[1].GetTag<RepeaterDayName>().Now 
									: tokens[1].GetTag<RepeaterWeekend>().Now;

				var token = new Token("week");
				token.Tag(repeaterWeek);
				tokens.Insert(1, token);
			}

            var ddTokens = tokens.DealiasAndDisambiguateTimes(options);

            return ddTokens.GetAnchor(options);
        }
    }
}