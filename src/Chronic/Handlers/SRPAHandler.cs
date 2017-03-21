using System.Collections.Generic;
using System.Linq;

namespace Chronic.Handlers
{
    public class SRPAHandler : SRPHandler
    {
        public override Span Handle(IList<Token> tokens, Options options)
        {
			if (tokens.First().IsTaggedAs<SeparatorIn>())
				tokens.RemoveAt(0);

            int tokensToSkip = tokens.First().IsTaggedAs<Scalar>() ? 3 : 2;
            var anchorSpan = tokens.Skip(tokensToSkip).GetAnchor(options);
            return Handle(tokens, anchorSpan, options);
        }
    }
}