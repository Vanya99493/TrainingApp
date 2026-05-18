using System;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.SessionScripts;
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
		}

		public int GetStreak() => 0;

		public TrainingData GetTraining(string guid) => ClientData.TrainingsDataContainer.GetTrainingData(guid);

		public TrainingData GetTodayTraining() => GetTrainingOfDay(DateTime.Today);
		
		public TrainingData GetTomorrowsTraining() => GetTrainingOfDay(DateTime.Today.AddDays(1));

		private TrainingData GetTrainingOfDay(DateTime dateTime)
		{
			string dayExerciseGuid = ClientData.DaysData.GetDayData(dateTime).ExerciseGuid;

			return dayExerciseGuid == ""
				? null
				: ClientData.TrainingsDataContainer.GetTrainingData(dayExerciseGuid);
		}
	}
}