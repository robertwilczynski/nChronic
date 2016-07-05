using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chronic
{
    public class SeparatorScanner : ITokenScanner
    {
        static readonly dynamic[] Patterns = new dynamic[]
            {
                new { Pattern = @"^,$".Compile(), Tag = new SeparatorComma() },
                new { Pattern = @"^and$".Compile(), Tag = new SeparatorComma() },
                new { Pattern = @"^(at|@)$".Compile(), Tag = new SeparatorAt() },
                new { Pattern = @"^in$".Compile(), Tag = new SeparatorIn() },
                new { Pattern = @"^/$".Compile(), Tag = new SeparatorDate(Separator.Type.Slash) },
                new { Pattern = @"^-$".Compile(), Tag = new SeparatorDate(Separator.Type.Dash) },
                new { Pattern = @"^on$".Compile(), Tag = new SeparatorOn() },

                // spanish month and year prefix
                new { Pattern = @"^de$".Compile(), Tag = new SeparatorDate(Separator.Type.At) },
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