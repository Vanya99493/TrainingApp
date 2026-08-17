using System;
using TrainingApp.Constants;

namespace TrainingApp.Models
{
	[Serializable]
	public class ClientData
	{
		public string Name = DefaultDataConstants.DEFAULT_USER_NAME;
		public DaysDataContainer DaysData = new();
		public TrainingsDataContainer TrainingsDataContainer = new();
		
		public DateTime BackupDate = DateTime.MaxValue;
	}
}