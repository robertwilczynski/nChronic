using System;
using System.Collections.Generic;

namespace Chronic.Handlers
{
    public class HandlerBuilder
    {
        readonly IList<HandlerPattern> _patternParts =
            new List<HandlerPattern>();

        public Type BaseHandler { get; private set; }

        public HandlerBuilder Using<THandler>()
        {
            BaseHandler = typeof (THandler);
            return this;
        }

        public HandlerBuilder UsingNothing()
        {
            BaseHandler = null;
            return this;
        }

        public HandlerBuilder Optional<THandler>()
        {
            _patternParts.Add(new TagPattern(typeof (THandler), true));
            return this;
        }

        public HandlerBuilder Required<THandler>()
        {
            _patternParts.Add(new TagPattern(typeof (THandler), false));
            return this;
        }

        public HandlerBuilder Required(HandlerType type)
        {
            _patternParts.Add(new HandlerTypePattern(type, false));
            return this;
        }

        public HandlerBuilder Optional(HandlerType type)
        {
            _patternParts.Add(new HandlerTypePattern(type, true));
            return this;
        }

        public static implicit operator ComplexHandler(HandlerBuilder builder)
        {
            return new ComplexHandler(
                builder.BaseHandler != null ? Activator.CreateInstance(builder.BaseHandler) as IHandler : null,
                builder._patternParts) as ComplexHandler;
        }
    }
}