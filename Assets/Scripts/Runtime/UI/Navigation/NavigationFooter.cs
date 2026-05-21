using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class NavigationFooter : BasePanel
	{
		[SerializeField]
		private Button _profileButton;
		
		[SerializeField]
		private Button _trainingPlansButton;

		[SerializeField]
		private Button _calendarButton;
		
		[SerializeField]
		private Button _mainMenuButton;

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_profileButton.onClick.AddListener(OnProfileButtonCLickedEventHandler);
			_trainingPlansButton.onClick.AddListener(OnTrainingPlansButtonClickedEventHandler);
			_calendarButton.onClick.AddListener(OnCalendarButtonClickedEventHandler);
			_mainMenuButton.onClick.AddListener(OnMainMenuButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_profileButton.onClick.RemoveListener(OnProfileButtonCLickedEventHandler);
			_trainingPlansButton.onClick.RemoveListener(OnTrainingPlansButtonClickedEventHandler);
			_calendarButton.onClick.RemoveListener(OnCalendarButtonClickedEventHandler);
			_mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClickedEventHandler);
		}

		private void OnProfileButtonCLickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanel();
			// TODO: add profile panel opening
		}

		private void OnTrainingPlansButtonClickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanel();
			_uiManager.OpenTrainingPlansPanel();
		}

		private void OnCalendarButtonClickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanel();
			_uiManager.OpenCalendarPanel();
		}

		private void OnMainMenuButtonClickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanel();
			_uiManager.OpenMainMenuPanel();
		}
	}
}