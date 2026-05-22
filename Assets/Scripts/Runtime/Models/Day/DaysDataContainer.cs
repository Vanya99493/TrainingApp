using System;
using System.Collections.Generic;
using TrainingApp.ApplicationScripts;

namespace TrainingApp.Models
{
	[Serializable]
	public class DaysDataContainer
	{
		public SortedDictionary<int, YearData> Years = new();

		public DayData GetDayData(DateTime dateTime)
		{
			if (!Years.TryGetValue(dateTime.Year, out var year) ||
			    !year.Months.TryGetValue(dateTime.Month, out var month) ||
			    !month.Days.TryGetValue(dateTime.Day, out var day))
			{
				return new DayData()
				{
					DateTime = dateTime,
					Version = ApplicationInfoProvider.ApplicationVersion
				};
			}

			return day;
		}
	}
}