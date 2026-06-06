using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainingApp.Models
{
	[Serializable]
	public class TrainingsDataContainer
	{
		public List<TrainingData> Trainings = new();
		public List<SubTrainingData> SubTrainings = new();

		public TrainingData GetTrainingData(string guid)
		{
			return Trainings.FirstOrDefault(x => x.Guid == guid);
		}

		public void AddOrUpdateTrainingData(TrainingData trainingData)
		{
			int index = Trainings.FindIndex(x => x.Guid == trainingData.Guid);

			if (index != -1)
			{
				Trainings[index] = trainingData;
			}
			else
			{
				Trainings.Add(trainingData);
			}
		}

		public SubTrainingData GetSubTrainingData(string guid)
		{
			return SubTrainings.FirstOrDefault(x => x.Guid == guid);
		}

		public void AddOrUpdateSubTrainingData(SubTrainingData subTrainingData)
		{
			int index = SubTrainings.FindIndex(x => x.Guid == subTrainingData.Guid);

			if (index != -1)
			{
				SubTrainings[index] = subTrainingData;
			}
			else
			{
				SubTrainings.Add(subTrainingData);
			}
		}
	}
}