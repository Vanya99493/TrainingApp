using System;

namespace TrainingApp.Models
{
	[Serializable]
	public class SubTrainingData : IEquatable<SubTrainingData>
	{
		public string Guid = "";
		public string Name = "";
		public int LogoSpriteIndex;
		public int LogoColorIndex;
		public string Description = "";
		
		public bool Equals(SubTrainingData other)
		{
			if (other == null)
			{
				return false;
			}
			
			return Guid == other.Guid;
		}
	}
}