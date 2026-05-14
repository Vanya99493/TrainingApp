using System;
using TrainingApp.AccountScripts;
using TrainingApp.Enums;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;

namespace TrainingApp.UI
{
	public class MainMenuPanel : BasePanel
	{
		[SerializeField]
		private StreakElement _streakElement;

		[SerializeField]
		private FollowingTrainingElement _todaysTrainingElement;

		[SerializeField]
		private FollowingTrainingElement _tomorrowsTrainingElement;
		
		public override void Open()
		{
			base.Open();

			var accountController = ServiceLocator.Instance.Get<AccountController>();
			_streakElement.Initialize(accountController.GetStreak());
			_todaysTrainingElement.Initialize(DayType.Today, DateTime.Today, accountController.GetTodayTraining());
			_tomorrowsTrainingElement.Initialize(DayType.Tomorrow, DateTime.Today.AddDays(1), accountController.GetTomorrowsTraining());
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();

			_todaysTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
			_tomorrowsTrainingElement.OpenTrainingEvent += OpenTrainingPanel;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_todaysTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
			_tomorrowsTrainingElement.OpenTrainingEvent -= OpenTrainingPanel;
		}

		private void OpenTrainingPanel(string guid)
		{
			_uiManager.OpenInstead<TrainingPanel>(this);
		}
	}
}