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
		private EnglishLessonElement _englishLessonElement;

		[SerializeField]
		private SubTrainingElement _subTrainingElement;

		[SerializeField]
		private TeethBrushingElement _teethBrushingElement;

		[SerializeField]
		private HarmfulActivitiesElement _harmfulActivitiesElement;

		[SerializeField]
		private FollowingTrainingElement _todaysTrainingElement;

		[SerializeField]
		private FollowingTrainingElement _tomorrowsTrainingElement;
		
		private AccountController _accountController;

		private DayData _todayData;

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public override void Open(PanelArgs args)
		{
			_todayData = _accountController.GetDayData(DateTime.Today);
			
			_streakElement.Initialize(_accountController.GetStreak());
			_englishLessonElement.Initialize(_todayData.DateTime, _todayData.EnglishLessonData);
			_subTrainingElement.Initialize(_todayData.DateTime, _accountController.GetSubTraining(_todayData.SubTrainingGuid), _todayData.IsSubTrainingCompleted);
			_teethBrushingElement.Initialize(_todayData.TeethData);
			_harmfulActivitiesElement.Initialize(_todayData.HarmfulActivitiesData.IsDrinkingToday);
			_todaysTrainingElement.Initialize(DayType.Today, DateTime.Today, _todayData.IsTrainingCompleted, _accountController.GetTodayTraining());
			_tomorrowsTrainingElement.Initialize(DayType.Tomorrow, DateTime.Today.AddDays(1), false, _accountController.GetTomorrowsTraining());
			
			base.Open(args);
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();

			_englishLessonElement.OpenEnglishLessonEvent += OnOpenEnglishLessonEventHandler;
			_subTrainingElement.OpenSubTrainingEvent += OnOpenSubTrainingEventHandler;
			_teethBrushingElement.TeethBrushingStateChangedEvent += OnTeethBrushingStateChangedEventHandler;
			_harmfulActivitiesElement.DrinkingStateChangedEvent += OnDrinkingStateChangedEventHandler;
			_todaysTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
			_tomorrowsTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_englishLessonElement.OpenEnglishLessonEvent -= OnOpenEnglishLessonEventHandler;
			_subTrainingElement.OpenSubTrainingEvent -= OnOpenSubTrainingEventHandler;
			_teethBrushingElement.TeethBrushingStateChangedEvent -= OnTeethBrushingStateChangedEventHandler;
			_harmfulActivitiesElement.DrinkingStateChangedEvent -= OnDrinkingStateChangedEventHandler;
			_todaysTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
			_tomorrowsTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
		}

		private void OnOpenEnglishLessonEventHandler(DateTime date)
		{
			_uiManager.CloseMainMenuPanel();
			_uiManager.OpenEnglishLessonPanel(_todayData);
		}
		
		private void OnOpenSubTrainingEventHandler(DateTime date)
		{
			_uiManager.CloseMainMenuPanel();
			_uiManager.OpenSubTrainingPanel(_accountController.GetDayData(date));
		}

		private void OnTeethBrushingStateChangedEventHandler(TeethData teethData)
		{
			_todayData.TeethData = teethData;
			_accountController.AddOrUpdateDayData(_todayData);
		}

		private void OnDrinkingStateChangedEventHandler(HarmfulActivitiesData harmfulActivitiesData)
		{
			_todayData.HarmfulActivitiesData = harmfulActivitiesData;
			_accountController.AddOrUpdateDayData(_todayData);
		}

		private void OpenTrainingPanel(DateTime date)
		{
			_uiManager.CloseMainMenuPanel();
			_uiManager.OpenTrainingPanel(_accountController.GetDayData(date));
		}
	}
}