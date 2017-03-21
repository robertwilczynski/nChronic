using System;
using System.Collections.Generic;
using Chronic.Tags.Repeaters;

namespace Chronic.Handlers
{
	public class RdnSdRmnHandler : IHandler
	{
		public Span Handle(IList<Token> tokens, Options options)
		{
			var month = tokens[2].GetTag<RepeaterMonthName>();
			var day = tokens[1].GetTag<ScalarDay>().Value;
			var year = options.Clock().Year;

			if (Time.IsMonthOverflow(year, (int)month.Value, day))
			{
				return null;
			}
			try
			{
				var start = Time.New(year, (int)month.Value, day);
				var end = start.AddDays(1);
				return new Span(start, end);
			}
			catch (ArgumentException)
			{
				return null;
			}
		}
	}
}