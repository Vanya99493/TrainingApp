using System;
using System.Collections.Generic;

namespace TrainingApp.Models
{
	[Serializable]
	public class TrainingData : IEquatable<TrainingData>
	{
		public string Guid = "";
		public string Name = "";
		public int LogoSpriteIndex;
		public List<ExerciseData> Exercises = new();
		
		public bool Equals(TrainingData other)
		{
			if (other == null)
			{
				return false;
			}
			
			return Guid == other.Guid;
		}
	}
}