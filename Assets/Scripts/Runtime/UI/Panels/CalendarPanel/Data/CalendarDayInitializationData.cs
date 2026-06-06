using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class CalendarDayInitializationData
	{
		public DayData DayData;
		public Sprite TrainingLogo;
		public Sprite SubTrainingLogo;

		public CalendarDayInitializationData(DayData dayData, Sprite trainingLogo, Sprite subTrainingLogo)
		{
			DayData = dayData;
			TrainingLogo = trainingLogo;
			SubTrainingLogo = subTrainingLogo;
		}
	}
}