using System.Collections.Generic;

namespace Chronic.Handlers
{
    public class SRPHandler : IHandler
    {
        public virtual Span Handle(IList<Token> tokens, Options options)
        {
            var now = options.Clock();
            var span = new Span(now, now.AddSeconds(1));
            return Handle(tokens, span, options);
        }

        public Span Handle(IList<Token> tokens, Span span, Options options)
        {
            int distance = 1;
            int index = 0;

            if (tokens[0].GetTag<Scalar>() != null)
                distance = tokens[index++].GetTag<Scalar>().Value;

            var repeater = tokens[index++].GetTag<IRepeater>();
            var pointer = tokens[index++].GetTag<Pointer>().Value;
            return repeater.GetOffset(span, distance, pointer);
        }
    }
}