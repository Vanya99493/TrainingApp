using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingApp.Models
{
	[Serializable]
	public class DaysDataContainer
	{
		public SortedDictionary<int, YearData> Years = new();

		public DayData GetDayData(DateTime dateTime)
		{
			return Years.FirstOrDefault(x => x.Key == dateTime.Year).Value
				.Month.FirstOrDefault(x => x.Key == dateTime.Month).Value
				.Days.FirstOrDefault(x => x.Key == dateTime.Day).Value;
		}
	}
}