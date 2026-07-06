using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chronic
{
    public class SeparatorScanner : ITokenScanner
    {
        // Concrete type instead of a dynamic[] of anonymous objects (see GrabberScanner) so the
        // member access is static and Native-AOT-safe.
        private class SeparatorMatch
        {
            public readonly Regex Pattern;
            public readonly ITag Tag;

            public SeparatorMatch(Regex pattern, ITag tag)
            {
                Pattern = pattern;
                Tag = tag;
            }
        }

        static readonly SeparatorMatch[] Patterns = new SeparatorMatch[]
            {
                new SeparatorMatch(@"^,$".Compile(), new SeparatorComma()),
                new SeparatorMatch(@"^and$".Compile(), new SeparatorComma()),
                new SeparatorMatch(@"^(at|@)$".Compile(), new SeparatorAt()),
                new SeparatorMatch(@"^in$".Compile(), new SeparatorIn()),
                new SeparatorMatch(@"^/$".Compile(), new SeparatorDate(Separator.Type.Slash)),
                new SeparatorMatch(@"^-$".Compile(), new SeparatorDate(Separator.Type.Dash)),
                new SeparatorMatch(@"^on$".Compile(), new SeparatorOn()),
            };

        public IList<Token> Scan(IList<Token> tokens, Options options)
        {
            tokens.ForEach(ApplyTags);
            return tokens;
        }

        static void ApplyTags(Token token)
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