using System;
using System.Collections.Generic;

namespace TrainingApp.Models
{
	[Serializable]
	public class TrainingData
	{
		public string Guid = "";
		public string Name = "";
		public int ImageIndex;
		public List<ExerciseData> Exercises = new();
	}
}