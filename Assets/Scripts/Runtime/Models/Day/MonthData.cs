using System;
using System.Collections.Generic;

namespace TrainingApp.Models
{
	[Serializable]
	public class MonthData
	{
		public SortedDictionary<int, DayData> Days = new();
	}
}