using System.Collections.Generic;

namespace Chronic
{
    public class GrabberScanner : ITokenScanner
    {
        // Use a concrete type instead of a dynamic[] of anonymous objects. The `dynamic` member
        // access (match.Pattern / match.Tag) is dispatched through the DLR, which does not work
        // under Native AOT and is needless overhead for what is a plain field read. A small
        // private class makes the access static.
        private class GrabberMatch
        {
            public readonly string Pattern;
            public readonly ITag Tag;

            public GrabberMatch(string pattern, ITag tag)
            {
                Pattern = pattern;
                Tag = tag;
            }
        }

        static readonly GrabberMatch[] _matches = new GrabberMatch[]
            {
                new GrabberMatch("last", new Grabber(Grabber.Type.Last)),
                new GrabberMatch("next", new Grabber(Grabber.Type.Next)),
                new GrabberMatch("this", new Grabber(Grabber.Type.This)),
            };

        public IList<Token> Scan(IList<Token> tokens, Options options)
        {
            tokens.ForEach(ApplyGrabberTags);
            return tokens;
        }

        static void ApplyGrabberTags(Token token)
        {
            foreach (var match in _matches)
            {
                if (match.Pattern == token.Value)
                {
                    token.Tag(match.Tag);
                }
            }
        }
    }
}