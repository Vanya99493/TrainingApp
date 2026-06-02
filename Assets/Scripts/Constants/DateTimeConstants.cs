using System;
using System.Collections.Generic;

namespace TrainingApp.Constants
{
	public static class DateTimeConstants
	{
		public static readonly Dictionary<DayOfWeek, string> DayOfWeekStrings = new()
		{
			{ DayOfWeek.Monday, "MON" },
			{ DayOfWeek.Tuesday, "TUE" },
			{ DayOfWeek.Wednesday, "WED" },
			{ DayOfWeek.Thursday, "THU" },
			{ DayOfWeek.Friday, "FRI" },
			{ DayOfWeek.Saturday, "SAT" },
			{ DayOfWeek.Sunday, "SUN" },
		};
	}
}