using System;
using System.Collections.Generic;

namespace TrainingApp.Models
{
	[Serializable]
	public class TrainingData
	{
		public string Guid = "";
		public string Name = "";
		public int LogoSpriteIndex;
		public List<ExerciseData> Exercises = new();
	}
}