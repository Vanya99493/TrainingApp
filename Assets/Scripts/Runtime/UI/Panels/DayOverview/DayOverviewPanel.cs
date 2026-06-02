using System;
using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.Constants;
using TrainingApp.Enums;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class DayOverviewPanel : BasePanel
	{
		[SerializeField]
		private TMP_Text _dayOfWeekText;
		
		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private Button _changeTrainingButton;
		
		[SerializeField]
		private FollowingTrainingElement _todaysTrainingElement;
		
		[SerializeField]
		private TeethBrushingElement _teethBrushingElement;

		[SerializeField]
		private HarmfulActivitiesElement _harmfulActivitiesElement;

		private AccountController _accountController;
		private DayData _dayData;

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public void Open(DayData dayData)
		{
			base.Open();

			_dayData = dayData;

			_dayOfWeekText.text = DateTimeConstants.DayOfWeekStrings[_dayData.DateTime.DayOfWeek];

			_teethBrushingElement.Initialize(_dayData.TeethData);
			_harmfulActivitiesElement.Initialize(_dayData.HarmfulActivitiesData.IsDrinkingToday);
			_todaysTrainingElement.Initialize(DayType.Today, _dayData.DateTime, _accountController.GetTraining(_dayData.ExerciseGuid));
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_closeButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
			_changeTrainingButton.onClick.AddListener(OnChangeTrainingButtonClickedEventHandler);

			_teethBrushingElement.TeethBrushingStateChangedEvent += OnTeethBrushingStateChangedEventHandler;
			_harmfulActivitiesElement.DrinkingStateChangedEvent += OnDrinkingStateChangedEventHandler;
			_todaysTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_closeButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);
			_changeTrainingButton.onClick.RemoveListener(OnChangeTrainingButtonClickedEventHandler);
			
			_teethBrushingElement.TeethBrushingStateChangedEvent -= OnTeethBrushingStateChangedEventHandler;
			_harmfulActivitiesElement.DrinkingStateChangedEvent -= OnDrinkingStateChangedEventHandler;
			_todaysTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
		}

		private void OnCloseButtonClickedEventHandler()
		{
			_uiManager.OpenInstead<CalendarPanel>(this);
		}

		private void OnChangeTrainingButtonClickedEventHandler()
		{
			_uiManager.OpenAssignTrainingPanel(_dayData);
		}

		private void OnTeethBrushingStateChangedEventHandler(TeethData teethData)
		{
			_accountController.GetDayData(DateTime.Today).TeethData = teethData;
		}

		private void OnDrinkingStateChangedEventHandler(HarmfulActivitiesData harmfulActivitiesData)
		{
			_accountController.GetDayData(DateTime.Today).HarmfulActivitiesData = harmfulActivitiesData;
		}

		private void OpenTrainingPanel(DateTime date)
		{
			_uiManager.OpenTrainingPanel(_accountController.GetDayData(date));
		}
	}
}