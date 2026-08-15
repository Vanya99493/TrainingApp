using TrainingApp.AccountScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class EnglishLessonPanel : BasePanel
	{
		[SerializeField]
		private LessonSpentTimeElement _lessonSpentTimeElement;
		
		[SerializeField]
		private ValidatableInputFieldElement _messageValidatableInputFieldElement;
		
		[Space(10)]
		[SerializeField]
		private Button _saveButton;

		[SerializeField]
		private Button _backButton;
		
		private AccountController _accountController;
		private DayData _dayData;
		
		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public override void Open(PanelArgs args)
		{
			base.Open(args);

			_dayData = (args as DayDataPanelArgs).DayData;
			
			_lessonSpentTimeElement.Initialize(_dayData.EnglishLessonData.SpentTimeType);
			_messageValidatableInputFieldElement.Initialize(_dayData.EnglishLessonData.LessonMessage);
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();

			_saveButton.onClick.AddListener(OnSaveButtonClickedEventHandler);
			_backButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();

			_saveButton.onClick.RemoveListener(OnSaveButtonClickedEventHandler);
			_backButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);
		}

		private void OnCloseButtonClickedEventHandler()
		{
			ClosePanel();
		}

		private void OnSaveButtonClickedEventHandler()
		{
			if (!_messageValidatableInputFieldElement.Validate())
			{
				return;
			}

			_dayData.EnglishLessonData = new()
			{
				SpentTimeType = _lessonSpentTimeElement.SpentTimeType,
				LessonMessage = _messageValidatableInputFieldElement.Text,
			};
			_accountController.AddOrUpdateDayData(_dayData);
			
			ClosePanel();
		}

		private void ClosePanel()
		{
			_uiManager.OpenLastClosedPanelInstead(this, new DayDataPanelArgs(_dayData));
		}
	}
}