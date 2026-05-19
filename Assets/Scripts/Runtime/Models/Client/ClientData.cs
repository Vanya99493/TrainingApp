using System;

namespace TrainingApp.Models
{
	[Serializable]
	public class ClientData
	{
		public string Name = "";
		public DaysDataContainer DaysData = new();
		public TrainingsDataContainer TrainingsDataContainer = new();
	}
}