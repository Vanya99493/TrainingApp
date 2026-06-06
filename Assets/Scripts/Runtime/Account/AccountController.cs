using System;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.SessionScripts;
using TrainingApp.Utils;
using UnityEngine;

namespace TrainingApp.AccountScripts
{
	public class AccountController : MonoBehaviour, IService
	{
		private IClientDataProvider _clientDataProvider;
		
		public ClientData ClientData { get; set; }
		
		public void Initialize()
		{
			_clientDataProvider = new PlayerPrefsClientDataProvider();
			ClientData = _clientDataProvider.LoadData<ClientData>();

			if (ClientData == null)
			{
				ClientData = new();
			}
		}

		public int GetStreak() => 0;

		public DayData GetDayData(DateTime dateTime) => ClientData.DaysData.GetDayData(dateTime);

		public void AddOrUpdateDayData(DayData dayData)
		{
			ClientData.DaysData.SetOrUpdateDayData(dayData);
			_clientDataProvider.SaveData(ClientData);
		}

		public TrainingData GetTraining(string guid) => ClientData.TrainingsDataContainer.GetTrainingData(guid);

		public TrainingData GetTodayTraining() => GetTrainingOfDay(DateTime.Today);
		
		public TrainingData GetTomorrowsTraining() => GetTrainingOfDay(DateTime.Today.AddDays(1));

		public void AddOrUpdateTrainingData(TrainingData trainingData)
		{
			if (trainingData.Guid == "")
			{
				trainingData.Guid = GuidGenerator.GenerateGuid();
			}
			
			ClientData.TrainingsDataContainer.AddOrUpdateTrainingData(trainingData);
			_clientDataProvider.SaveData(ClientData);
		}

		public SubTrainingData GetSubTraining(string guid) => ClientData.TrainingsDataContainer.GetSubTrainingData(guid);
		
		public void AddOrUpdateSubTrainingData(SubTrainingData subTrainingData)
		{
			if (subTrainingData.Guid == "")
			{
				subTrainingData.Guid = GuidGenerator.GenerateGuid();
			}
			
			ClientData.TrainingsDataContainer.AddOrUpdateSubTrainingData(subTrainingData);
			_clientDataProvider.SaveData(ClientData);
		}

		private TrainingData GetTrainingOfDay(DateTime dateTime)
		{
			string dayExerciseGuid = ClientData.DaysData.GetDayData(dateTime).ExerciseGuid;

			return dayExerciseGuid == ""
				? null
				: ClientData.TrainingsDataContainer.GetTrainingData(dayExerciseGuid);
		}
	}
}