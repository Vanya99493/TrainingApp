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
		public string SubTrainingGuid = "";
		public TeethData TeethData;
		public HarmfulActivitiesData HarmfulActivitiesData;
		public bool IsTrainingCompleted;
		public int SetsCount;
		
		public bool IsSubTrainingCompleted => SubTrainingGuid.Length > 0;

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