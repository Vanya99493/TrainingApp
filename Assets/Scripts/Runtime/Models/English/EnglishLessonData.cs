using System;
using TrainingApp.Enums;

namespace TrainingApp.Models
{
	[Serializable]
	public class EnglishLessonData
	{
		public string LessonMessage = "";
		public SpentTimeType SpentTimeType;
	}
}