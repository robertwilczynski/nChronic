using System.Collections.Generic;
using System.Linq;

namespace Chronic.Handlers
{
    public class SRPAHandler : SRPHandler
    {
        public override Span Handle(IList<Token> tokens, Options options)
        {
            int tokensToSkip = tokens.First().IsTaggedAs<Scalar>() ? 3 : 2;
            var anchorSpan = tokens.Skip(tokensToSkip).GetAnchor(options);
            return Handle(tokens, anchorSpan, options);
        }
    }
}