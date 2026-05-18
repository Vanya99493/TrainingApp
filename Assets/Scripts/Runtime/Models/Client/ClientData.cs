using System;

namespace TrainingApp.Models
{
	[Serializable]
	public class ClientData
	{
		public string Name = "";
		public DaysDataContainer DaysData;
		public TrainingsDataContainer TrainingsDataContainer;
	}
}