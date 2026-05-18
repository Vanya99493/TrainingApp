using System;

namespace TrainingApp.Models
{
	[Serializable]
	public class TeethData
	{
		public bool IsBrushInTheMorning;
		public bool IsBrushInTheEvening;
		
		public TeethData() { }
		
		public TeethData(bool isBrushInTheMorning, bool isBrushInTheEvening)
		{
			IsBrushInTheMorning = isBrushInTheMorning;
			IsBrushInTheEvening = isBrushInTheEvening;
		}
	}
}