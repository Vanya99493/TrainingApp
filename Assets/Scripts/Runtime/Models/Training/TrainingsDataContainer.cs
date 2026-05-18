using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingApp.Models
{
	[Serializable]
	public class TrainingsDataContainer
	{
		public List<TrainingData> Trainings = new();

		public TrainingData GetTrainingData(string guid)
		{
			return Trainings.FirstOrDefault(x => x.Guid == guid);
		}
	}
}