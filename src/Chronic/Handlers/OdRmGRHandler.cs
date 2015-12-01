using System;
using System.Collections.Generic;
using System.Linq;

namespace Chronic.Handlers
{
    public class OdRmGRHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
			OdRmnHandler odRmnHandler = new OdRmnHandler();
	        Span t = odRmnHandler.Handle(tokens.Take(2).ToList(), options);


            var outerSpan = tokens.Skip(2).Take(2).GetAnchor(options);
	        return new Span(new DateTime(outerSpan.Start.Value.Year, t.Start.Value.Month, t.Start.Value.Day),
							new DateTime(outerSpan.Start.Value.Year, t.End.Value.Month, t.End.Value.Day));
        }
    }
}