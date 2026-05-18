using System;

namespace TrainingApp.Models
{
	[Serializable]
	public class HarmfulActivitiesData
	{
		public bool IsDrinkingToday;
		
		public HarmfulActivitiesData() { }

		public HarmfulActivitiesData(bool isDrinkingToday)
		{
			IsDrinkingToday = isDrinkingToday;
		}
	}
}