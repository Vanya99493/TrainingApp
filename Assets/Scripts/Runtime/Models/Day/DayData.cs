using System;
using TrainingApp.ApplicationScripts;

namespace TrainingApp.Models
{
	[Serializable]
	public class DayData
	{
		public DateTime DateTime;
		public string Version = "";
		public string ExerciseGuid = "";
		public TeethData TeethData;
		public HarmfulActivitiesData HarmfulActivitiesData;
		public bool IsTrainingComplete;
		public int SetsCount;

		public DayData(string exerciseGuid = "")
		{
			DateTime = DateTime.Today;
			Version = ApplicationInfoProvider.ApplicationVersion;
			ExerciseGuid = exerciseGuid;
			TeethData = new();
			HarmfulActivitiesData = new();
		}
	}
}