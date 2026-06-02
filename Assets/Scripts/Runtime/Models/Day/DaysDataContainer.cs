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
			if (Years.TryGetValue(dateTime.Year, out var year) &&
			    year.Months.TryGetValue(dateTime.Month, out var month) &&
			    month.Days.TryGetValue(dateTime.Day, out var day))
			{
				return day;
			}

			return new DayData()
			{
				DateTime = dateTime,
				Version = ApplicationInfoProvider.ApplicationVersion,
			};
			
		}

		public void SetOrUpdateDayData(DayData dayData)
		{
			if (!Years.TryGetValue(dayData.DateTime.Year, out var yearData))
			{
				yearData = new YearData();
				Years[dayData.DateTime.Year] = yearData;
			}

			if (!yearData.Months.TryGetValue(dayData.DateTime.Month, out var monthData))
			{
				monthData = new MonthData();
				yearData.Months[dayData.DateTime.Month] = monthData;
			}

			monthData.Days[dayData.DateTime.Day] = dayData;
		}
	}
}