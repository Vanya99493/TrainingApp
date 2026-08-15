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

		public DayData GetDayData(DateTime dateTime) => ClientData.DaysData.GetDayData(dateTime);

		public int GetStreak()
		{
			int streak = 0;
			bool endOfStreakIsReached = false;
			DateTime currentDate = DateTime.Today.Date;
			DateTime streakCalculationDate = currentDate;
			
			for (int iYear = currentDate.Year; iYear > 0; iYear--)
			{
				if (endOfStreakIsReached)
				{
					break;
				}
				
				int month = streakCalculationDate.Month == currentDate.Month 
					? streakCalculationDate.Month 
					: 12;
				
				for (int iMonth = month; iMonth > 0; iMonth--)
				{
					if (endOfStreakIsReached)
					{
						break;
					}
					
					int day = streakCalculationDate.Day == currentDate.Day
						? streakCalculationDate.Day
						: DateTime.DaysInMonth(iYear, iMonth);
					
					for (int iDay = day; iDay > 0; iDay--)
					{
						if (endOfStreakIsReached)
						{
							break;
						}
						
						DayData dayData = GetDayData(new  DateTime(
							iYear, 
							iMonth, iDay
							));

						if (dayData != null)
						{
							if (dayData.IsTrainingCompleted ||
							    dayData.IsSubTrainingCompleted)
							{
								streak++;
								continue;
							}
							else if (dayData.DateTime.Date == currentDate.Date)
							{
								continue;
							}
						}

						endOfStreakIsReached = true;
					}
				}
			}

			return streak;
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

		private TrainingData GetTrainingOfDay(DateTime dateTime)
		{
			string dayExerciseGuid = ClientData.DaysData.GetDayData(dateTime).ExerciseGuid;

			return dayExerciseGuid == ""
				? null
				: ClientData.TrainingsDataContainer.GetTrainingData(dayExerciseGuid);
		}
	}
}