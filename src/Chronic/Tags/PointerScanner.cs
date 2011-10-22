using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chronic
{
    public class PointerScanner : ITokenScanner
    {
        static readonly dynamic[] Patterns = new dynamic[]
            {
                new { Pattern = new Regex(@"\bin\b"), Tag = new Pointer(Pointer.Type.Future) },
                new { Pattern = new Regex(@"\bfuture\b"), Tag = new Pointer(Pointer.Type.Future) },
                new { Pattern = new Regex(@"\bpast\b"), Tag = new Pointer(Pointer.Type.Past) },
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