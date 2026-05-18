using System;
using TrainingApp.AccountScripts;
using TrainingApp.Enums;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;

namespace TrainingApp.UI
{
	public class MainMenuPanel : BasePanel
	{
		[SerializeField]
		private StreakElement _streakElement;

		[SerializeField]
		private TeethBrushingElement _teethBrushingElement;

		[SerializeField]
		private HarmfulActivitiesElement _harmfulActivitiesElement;

		[SerializeField]
		private FollowingTrainingElement _todaysTrainingElement;

		[SerializeField]
		private FollowingTrainingElement _tomorrowsTrainingElement;
		
		private AccountController _accountController;

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public override void Open()
		{
			base.Open();

			var todayData = _accountController.GetDayData(DateTime.Today);
			
			_streakElement.Initialize(_accountController.GetStreak());
			_teethBrushingElement.Initialize(todayData.TeethData);
			_harmfulActivitiesElement.Initialize(todayData.HarmfulActivitiesData.IsDrinkingToday);
			_todaysTrainingElement.Initialize(DayType.Today, DateTime.Today, _accountController.GetTodayTraining());
			_tomorrowsTrainingElement.Initialize(DayType.Tomorrow, DateTime.Today.AddDays(1), _accountController.GetTomorrowsTraining());
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();

			_teethBrushingElement.TeethBrushingStateChangedEvent += OnTeethBrushingStateChangedEventHandler;
			_harmfulActivitiesElement.DrinkingStateChangedEvent += OnDrinkingStateChangedEventHandler;
			_todaysTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
			_tomorrowsTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_teethBrushingElement.TeethBrushingStateChangedEvent -= OnTeethBrushingStateChangedEventHandler;
			_harmfulActivitiesElement.DrinkingStateChangedEvent -= OnDrinkingStateChangedEventHandler;
			_todaysTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
			_tomorrowsTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
		}

		private void OnTeethBrushingStateChangedEventHandler(TeethData teethData)
		{
			_accountController.GetDayData(DateTime.Today).TeethData = teethData;
		}

		private void OnDrinkingStateChangedEventHandler(HarmfulActivitiesData harmfulActivitiesData)
		{
			_accountController.GetDayData(DateTime.Today).HarmfulActivitiesData = harmfulActivitiesData;
		}

		private void OpenTrainingPanel(string guid)
		{
			_uiManager.OpenInstead<TrainingPanel>(this);
		}
	}
}