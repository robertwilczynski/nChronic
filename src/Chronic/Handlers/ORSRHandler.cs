using System.Collections.Generic;
using System.Linq;

namespace Chronic.Handlers
{
    public class ORSRHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
			int offset = 0;
			if (tokens.Count < 4) offset = 1;
            var outerSpan = new List<Token> {tokens[3 - offset]}.GetAnchor(options);
            return Utils.HandleORR(tokens.Take(2).ToList(), outerSpan, options);
        }
    }
}