using System;
using System.Collections.Generic;

namespace TrainingApp.Models
{
	[Serializable]
	public class YearData
	{
		public SortedDictionary<int, MonthData> Months = new();
	}
}