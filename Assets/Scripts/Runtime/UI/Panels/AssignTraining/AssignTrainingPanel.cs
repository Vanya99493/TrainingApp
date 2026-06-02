using System.Collections.Generic;
using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class AssignTrainingPanel : BasePanel
	{
		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private TMP_Text _currentTrainingText;

		[SerializeField]
		private Image _currentTrainingLogoImage;

		[SerializeField]
		private TrainingElementFactory _trainingElementFactory;

		private AccountController _accountController;
		private DayData _dayData;
		private List<TrainingElement> _trainingElements = new();

		public void Open(DayData dayData)
		{
			ClearTrainingElements();
			
			_accountController = ServiceLocator.Instance.Get<AccountController>();
			ImageProvider imageProvider = ServiceLocator.Instance.Get<ImageProvider>();

			_dayData = dayData;
			
			if (_dayData.ExerciseGuid != "")
			{
				var restTrainingElement = _trainingElementFactory.InstantiateElement();
				restTrainingElement.Initialize(new() { Guid = "", Name = "Rest", Exercises = new(), LogoSpriteIndex = -1 }, imageProvider.GetRestImage());
				restTrainingElement.SelectTrainingEvent += OnSelectNewTrainingEventHandler;
				_trainingElements.Add(restTrainingElement);
			}

			List<TrainingData> trainings = _accountController.ClientData.TrainingsDataContainer.Trainings;

			foreach (var trainingData in trainings)
			{
				var trainingElement = _trainingElementFactory.InstantiateElement();
				var trainingLogoSprite = imageProvider.GetImage(trainingData.LogoSpriteIndex);
				trainingElement.Initialize(trainingData, trainingLogoSprite);
				trainingElement.SelectTrainingEvent += OnSelectNewTrainingEventHandler;
				_trainingElements.Add(trainingElement);

				if (_dayData.ExerciseGuid == trainingData.Guid)
				{
					_currentTrainingText.text = trainingData.Name;
					_currentTrainingLogoImage.sprite = trainingLogoSprite;
				}
			}
			
			base.Open();
		}

		public override void Close()
		{
			base.Close();
			
			ClearTrainingElements();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_closeButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_closeButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);
		}

		private void ClearTrainingElements()
		{
			foreach (var trainingElement in _trainingElements)
			{
				trainingElement.Destroy();
			}
			_trainingElements.Clear();
		}

		private void OnCloseButtonClickedEventHandler()
		{
			_uiManager.CloseAssignTrainingPanel();
		}

		private void OnSelectNewTrainingEventHandler(string newTrainingGuid)
		{
			_dayData.ExerciseGuid = newTrainingGuid;
			_accountController.AddOrUpdateDayData(_dayData);
			
			_uiManager.CloseAssignTrainingPanel();
		}
	}
}