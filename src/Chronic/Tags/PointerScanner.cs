using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chronic
{
    public class PointerScanner : ITokenScanner
    {
        // Concrete type instead of a dynamic[] of anonymous objects (see GrabberScanner) so the
        // member access is static and Native-AOT-safe.
        private class PointerMatch
        {
            public readonly Regex Pattern;
            public readonly ITag Tag;

            public PointerMatch(Regex pattern, ITag tag)
            {
                Pattern = pattern;
                Tag = tag;
            }
        }

        static readonly PointerMatch[] Patterns = new PointerMatch[]
            {
                new PointerMatch(new Regex(@"\bin\b"), new Pointer(Pointer.Type.Future)),
                new PointerMatch(new Regex(@"\bfuture\b"), new Pointer(Pointer.Type.Future)),
                new PointerMatch(new Regex(@"\bpast\b"), new Pointer(Pointer.Type.Past)),
            };

        public IList<Token> Scan(IList<Token> tokens, Options options)
        {
            tokens.ForEach(ApplyTags);
            return tokens;
        }

        private void ApplyTags(Token token)
        {
            foreach (var pattern in Patterns)
            {
                if (pattern.Pattern.IsMatch(token.Value))
                {
                    token.Tag(pattern.Tag);
                }
            }            
        }
    }
}