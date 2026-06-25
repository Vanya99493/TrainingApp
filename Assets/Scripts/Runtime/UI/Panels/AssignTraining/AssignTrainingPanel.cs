using System.Collections.Generic;
using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.Constants;
using TrainingApp.Enums;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.ServiceScripts;
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
		private ImageProvider _imageProvider;
		private ColorProvider _colorProvider;
		private DayData _dayData;
		private List<TrainingElement> _trainingElements = new();

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);
			
			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_imageProvider = ServiceLocator.Instance.Get<ImageProvider>();
			_colorProvider = ServiceLocator.Instance.Get<ColorProvider>();
		}

		public override void Open(PanelArgs args)
		{
			ClearTrainingElements();
			
			_dayData = (args as DayDataPanelArgs).DayData;
			
			if (_dayData.ExerciseGuid != "")
			{
				var restTrainingElement = _trainingElementFactory.InstantiateElement();
				restTrainingElement.Initialize(new() { Guid = "", Name = "Rest", Exercises = new(), LogoSpriteIndex = -1 }, _imageProvider.GetRestImage(), Color.white);
				restTrainingElement.SelectTrainingEvent += OnSelectNewTrainingEventHandler;
				_trainingElements.Add(restTrainingElement);
			}
			else
			{
				_currentTrainingText.text = "Rest";
				_currentTrainingLogoImage.sprite = _imageProvider.GetRestImage();
				_currentTrainingLogoImage.color = Color.white;
			}

			List<TrainingData> trainings = _accountController.ClientData.TrainingsDataContainer.Trainings;

			foreach (var trainingData in trainings)
			{
				Sprite trainingLogoSprite = _imageProvider.GetImage(trainingData.LogoSpriteIndex);
				Color trainingLogoColor = _colorProvider.GetColor(trainingData.LogoColorIndex);
				
				if (_dayData.ExerciseGuid == trainingData.Guid)
				{
					_currentTrainingText.text = trainingData.Name;
					_currentTrainingLogoImage.sprite = trainingLogoSprite;
					_currentTrainingLogoImage.color = trainingLogoColor;
					continue;
				}
				
				var trainingElement = _trainingElementFactory.InstantiateElement();
				trainingElement.Initialize(trainingData, trainingLogoSprite, trainingLogoColor);
				trainingElement.SelectTrainingEvent += OnSelectNewTrainingEventHandler;
				_trainingElements.Add(trainingElement);
			}
			
			base.Open(args);
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
			_uiManager.OpenDayOverviewPanel(_dayData);
		}

		private void OnSelectNewTrainingEventHandler(string newTrainingGuid)
		{
			_dayData.ExerciseGuid = newTrainingGuid;
			_accountController.AddOrUpdateDayData(_dayData);
			
			_uiManager.CloseAssignTrainingPanel();
			_uiManager.OpenDayOverviewPanel(_dayData);
		}
	}
}