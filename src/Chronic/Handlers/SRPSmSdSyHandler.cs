using System.Collections.Generic;
using System.Linq;

namespace Chronic.Handlers
{
	public class SRPSmSdSyHandler : SRPHandler
	{
		public override Span Handle(IList<Token> tokens, Options options)
		{
			var pointerToken = tokens.ToList().Find(t => t.IsTaggedAs<Pointer>());
			var index = tokens.IndexOf(pointerToken);

			var SRPtokens = tokens.Take(++index).ToList();
			var SmSdSyTokens = tokens.Skip(index).ToList();

			SmSdSyHandler handler = new SmSdSyHandler();
			var span = handler.Handle(SmSdSyTokens, options);
			
			if (tokens.First().IsTaggedAs<SeparatorIn>())
				tokens.RemoveAt(0);

			int tokensToSkip = SRPtokens.First().IsTaggedAs<Scalar>() ? 3 : 2;
			return Handle(SRPtokens, span, options);
		}
	}
}