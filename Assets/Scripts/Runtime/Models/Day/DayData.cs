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
		public TeethData TeethData = new();
		public HarmfulActivitiesData HarmfulActivitiesData = new();
		public EnglishLessonData EnglishLessonData = new();
		public bool IsTrainingCompleted;
		public int SetsCount;
		
		public bool IsSubTrainingCompleted => SubTrainingGuid.Length > 0;

		public DayData(string exerciseGuid = "")
		{
			DateTime = DateTime.Today;
			Version = ApplicationInfoProvider.ApplicationVersion;
			ExerciseGuid = exerciseGuid;
		}
	}
}