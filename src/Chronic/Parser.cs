using System;
using System.Collections.Generic;
using System.Linq;
using Chronic.Handlers;
using Chronic.Tags.Repeaters;

namespace Chronic
{
    public class Parser
    {
        public static bool IsDebugMode { get; set; }
        readonly Options _options;
        readonly HandlerRegistry _registry = new MyHandlerRegistry();

        static readonly List<ITokenScanner> _scanners = new List<ITokenScanner>
            {
                new RepeaterScanner(),
                new GrabberScanner(),
                new PointerScanner(),
                new ScalarScanner(), 
                new OrdinalScanner(), 
                new SeparatorScanner(),
                new TimeZoneScanner(),
            };

        public Parser()
            : this(new Options())
        {

        }

        public Parser(Options options)
        {
            _options = options;
            _registry.MergeWith(new EndianSpecificRegistry(options.EndianPrecedence));
        }

        public Span Parse(string phrase)
        {
            return Parse(phrase, _options);
        }

        public Span Parse(string phrase, Options options)
        {
            options.OriginalPhrase = phrase;
            Logger.Log(() => phrase);
            phrase = Normalize(phrase);
            Logger.Log(() => phrase);
            var tokens = Tokenize(phrase, options);            
            _scanners.ForEach(scanner => scanner.Scan(tokens, options));
            var taggedTokens = tokens.Where(token => token.HasTags()).ToList();
            Logger.Log(() => String.Join(",", taggedTokens.Select(t => t.ToString())));
            var span = TokensToSpan(taggedTokens, options);
            Logger.Log(() => "=> " + (span != null ? span.ToString() : "<null>"));
            return span;
        }

        IList<Token> Tokenize(string phrase, Options options)
        {
            var tokens = phrase
                .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => new Token(part))                
                .ToList();
            return tokens;
        }
        
        public static string Normalize(string phrase)
        {
            var normalized = phrase.ToLower();
            normalized = phrase
                .ReplaceAll(@"([/\-,@])", " " + "$1" + " ")
                .ReplaceAll(@"['""\.,]", "")
                .ReplaceAll(@"\bsecond (of|day|month|hour|minute|second)\b", "2nd $1")
                .Numerize()
                .ReplaceAll(@" \-(\d{4})\b", " tzminus$1")
                .ReplaceAll(@"(?:^|\s)0(\d+:\d+\s*pm?\b)", "$1")
                .ReplaceAll(@"\btoday\b", "this day")
                .ReplaceAll(@"\btomm?orr?ow\b", "next day")
                .ReplaceAll(@"\byesterday\b", "last day")
                .ReplaceAll(@"\bnoon\b", "12:00")
                .ReplaceAll(@"\bmidnight\b", "24:00")
                .ReplaceAll(@"\bbefore now\b", "past")
                .ReplaceAll(@"\bnow\b", "this second")
                .ReplaceAll(@"\b(ago|before)\b", "past")
                .ReplaceAll(@"\bthis past\b", "last")
                .ReplaceAll(@"\bthis last\b", "last")
                .ReplaceAll(@"\b(?:in|during) the (morning)\b", "$1")
                .ReplaceAll(@"\b(?:in the|during the|at) (afternoon|evening|night)\b", "$1")
                .ReplaceAll(@"\btonight\b", "this night")
                .ReplaceAll(@"(\d)([ap]m|oclock)\b", "$1 $2")
                .ReplaceAll(@"\b(hence|after|from)\b", "future")
                ;

            return normalized;
        }

        public Span TokensToSpan(IList<Token> tokens, Options options)
        {            
            foreach (var handler in _registry.GetHandlers(HandlerType.Endian).Concat(
                _registry.GetHandlers(HandlerType.Date)))
            {
                  if (handler.Match(tokens, _registry))
                  {
                      var targetTokens = tokens
                          .Where(x => x.IsNotTaggedAs<Separator>())
                          .ToList();
                      return ExecuteHandler(handler, targetTokens, options);
                  }
            }

            foreach (var handler in _registry.GetHandlers(HandlerType.Anchor))
            {
                if (handler.Match(tokens, _registry))
                {
                    var targetTokens = tokens
                        .Where(x => x.IsNotTaggedAs<Separator>())
                        .ToList();
                    return ExecuteHandler(handler, targetTokens, options);
                }
            }

            foreach (var handler in _registry.GetHandlers(HandlerType.Arrow))
            {
                if (handler.Match(tokens, _registry))
                {
                    var targetTokens = tokens
                        .Where(x => 
                            x.IsNotTaggedAs<SeparatorAt>() && 
                            x.IsNotTaggedAs<SeparatorComma>() && 
                            x.IsNotTaggedAs<SeparatorDate>())
                        .ToList();
                    return ExecuteHandler(handler, targetTokens, options);
                }
            }

            foreach (var handler in _registry.GetHandlers(HandlerType.Narrow))
            {
                if (handler.Match(tokens, _registry))
                {
                    return ExecuteHandler(handler, tokens, options);
                }
            }

            return null;
        }

        static Span ExecuteHandler(ComplexHandler handler, IList<Token> tokens, Options options)
        {
            if (handler == null) throw new ArgumentNullException("handler");
            if (tokens == null) throw new ArgumentNullException("tokens");
            if (options == null) throw new ArgumentNullException("options");

            Logger.Log(handler.ToString);
            if (handler.BaseHandler == null)
            {
                throw new InvalidOperationException(String.Format(
                    "No base handler found on complex handler: {0}.",
                    handler));
            }
            return handler.BaseHandler.Handle(tokens, options);
        }
    }
}
