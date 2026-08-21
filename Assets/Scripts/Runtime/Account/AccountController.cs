using System;
using TrainingApp.Enums;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.SessionScripts;
using TrainingApp.Utils;
using UnityEngine;

namespace TrainingApp.AccountScripts
{
	public class AccountController : MonoBehaviour, IAccountController, IService
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

		public DayData GetDayData(DateTime dateTime) => ClientData.DaysData.GetDayData(dateTime);

		public int GetTrainingStreak()
		{
			int streak = 0;
			DateTime date = DateTime.Today;

			while (true)
			{
				DayData dayData = GetDayData(date);

				if (dayData?.IsTrainingCompleted == true ||
				    dayData?.IsSubTrainingCompleted == true)
				{
					streak++;
				}
				else if (date != DateTime.Today)
				{
					break;
				}

				date = date.AddDays(-1);
			}

			return streak;
		}

		public int GetEnglishStreak()
		{
			int streak = 0;
			DateTime date = DateTime.Today;

			while (true)
			{
				DayData dayData = GetDayData(date);

				if (dayData?.EnglishLessonData.SpentTimeType != SpentTimeType.Min0)
				{
					streak++;
				}
				else if (date != DateTime.Today)
				{
					break;
				}

				date = date.AddDays(-1);
			}

			return streak;
		}

		public int GetSoberStreak()
		{
			int streak = 0;
			DateTime date = DateTime.Today;

			while (true)
			{
				DayData dayData = GetDayData(date);

				if (dayData?.HarmfulActivitiesData.IsDrinkingToday == false)
				{
					streak++;
				}
				else
				{
					break;
				}

				date = date.AddDays(-1);
			}

			return streak;
		}

		public int GetEfficiency()
		{
			// TODO: add efficiency calculation

			return 0;
		}

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

		public void UpdateLastBackupDate(DateTime lastBackupDate)
		{
			ClientData.BackupDate = lastBackupDate;
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