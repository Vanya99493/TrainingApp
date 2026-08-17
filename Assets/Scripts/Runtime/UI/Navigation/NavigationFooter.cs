using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class NavigationFooter : BasePanel
	{
		[SerializeField]
		private Button _trainingPlansButton;

		[SerializeField]
		private Button _calendarButton;
		
		[SerializeField]
		private Button _mainMenuButton;
		
		[SerializeField]
		private Button _settingsButton;

		[Space(20)]
		[SerializeField]
		private ToggleButtonGroup _toggleButtonGroup;

		public override void Open(PanelArgs args)
		{
			_toggleButtonGroup.Initialize(0);
			
			base.Open(args);
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_trainingPlansButton.onClick.AddListener(OnTrainingPlansButtonClickedEventHandler);
			_calendarButton.onClick.AddListener(OnCalendarButtonClickedEventHandler);
			_mainMenuButton.onClick.AddListener(OnMainMenuButtonClickedEventHandler);
			_settingsButton.onClick.AddListener(OnSettingsButtonCLickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_trainingPlansButton.onClick.RemoveListener(OnTrainingPlansButtonClickedEventHandler);
			_calendarButton.onClick.RemoveListener(OnCalendarButtonClickedEventHandler);
			_mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClickedEventHandler);
			_settingsButton.onClick.RemoveListener(OnSettingsButtonCLickedEventHandler);
		}

		private void OnTrainingPlansButtonClickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanels();
			_uiManager.OpenTrainingPlansPanel();
		}

		private void OnCalendarButtonClickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanels();
			_uiManager.OpenCalendarPanel();
		}

		private void OnMainMenuButtonClickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanels();
			_uiManager.OpenMainMenuPanel();
		}

		private void OnSettingsButtonCLickedEventHandler()
		{
			_uiManager.CloseCurrentActivePanels();
			_uiManager.OpenSettingsPanel();
		}
	}
}