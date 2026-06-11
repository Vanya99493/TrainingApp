using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class CalendarDayInitializationData
	{
		public DayData DayData;
		public Sprite TrainingLogo;
		public Color TrainingLogoColor;
		public Sprite SubTrainingLogo;
		public Color SubTrainingLogoColor;

		public CalendarDayInitializationData(DayData dayData, Sprite trainingLogo, Color trainingLogoColor, Sprite subTrainingLogo, Color subTrainingLogoColor)
		{
			DayData = dayData;
			TrainingLogo = trainingLogo;
			TrainingLogoColor = trainingLogoColor;
			SubTrainingLogo = subTrainingLogo;
			SubTrainingLogoColor = subTrainingLogoColor;
		}
	}
}