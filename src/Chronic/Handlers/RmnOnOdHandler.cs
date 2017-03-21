﻿using Chronic.Tags.Repeaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronic.Handlers
{
    public class RmnOnSdHandler : IHandler
    {
        public Span Handle(IList<Token> tokens, Options options)
        {
            var month = tokens[0].GetTag<RepeaterMonthName>();
            var day = tokens[1].GetTag<ScalarDay>().Value;
            var now = options.Clock();

            if (Time.IsMonthOverflow(now.Year, (int)month.Value, day))
            {
                return null;
            }
            return Utils.HandleMD(month, day, tokens.Skip(2).ToList(), options);
        }
    }
}
