using System.Collections.Generic;
using TrainingApp.AccountScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class TrainingPlansPanel : BasePanel
	{
		[SerializeField]
		private ToggleButtonGroup _headerButtonsGroup;
	
		[SerializeField]
		private TrainingPlanFactory _trainingPlanFactory;
		
		[Space(10)]
		[SerializeField]
		private Button _addPlanButton;

		private AccountController _accountController;

		private int _currentSelectedIndex = -1;
		private List<TrainingPlanElement> _trainingPlanElements = new();

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public override void Open(PanelArgs args)
		{
			_headerButtonsGroup.Initialize(0);

			if (_currentSelectedIndex == -1)
			{
				OnSelectToggleButtonEventHandler(0);
			}
			
			base.Open(args);
		}

		public override void Close()
		{
			base.Close();

			ClearTrainingPlanElements();
			_currentSelectedIndex = -1;
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_addPlanButton.onClick.AddListener(OnAddPlanButtonClickedEventHandler);
			_headerButtonsGroup.SelectToggleButtonEvent += OnSelectToggleButtonEventHandler;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_addPlanButton.onClick.RemoveListener(OnAddPlanButtonClickedEventHandler);
			_headerButtonsGroup.SelectToggleButtonEvent -= OnSelectToggleButtonEventHandler;
		}

		private void OnAddPlanButtonClickedEventHandler()
		{
			int panelIndexToOpen = _currentSelectedIndex;
			_uiManager.CloseTrainingPlansPanel();
			switch (panelIndexToOpen)
			{
				case 0:
					_uiManager.OpenEditTrainingPanel(new TrainingData());	
					break;
				case 1:
					_uiManager.OpenEditSubTrainingPanel(new SubTrainingData());
					break;
			}
		}
		
		private void OnSelectToggleButtonEventHandler(int selectedIndex)
		{
			_currentSelectedIndex = selectedIndex;
			switch (_currentSelectedIndex)
			{
				case 0:
					InitTrainings();
					break;
				case 1:
					InitSubTrainings();
					break;
			}
		}

		private void InitTrainings()
		{
			ClearTrainingPlanElements();

			var trainings = _accountController.ClientData.TrainingsDataContainer.Trainings;
			foreach (var trainingData in trainings)
			{
				var trainingPlanElement = _trainingPlanFactory.InstantiateElement();
				trainingPlanElement.Initialize(trainingData.Guid, trainingData.Name, trainingData.LogoSpriteIndex);
				trainingPlanElement.OpenTrainingPlanEvent += OnOpenTrainingPlanEventHandler;
				_trainingPlanElements.Add(trainingPlanElement);
			}
		}

		private void InitSubTrainings()
		{
			ClearTrainingPlanElements();

			var subTrainings = _accountController.ClientData.TrainingsDataContainer.SubTrainings;
			foreach (var subTrainingData in subTrainings)
			{
				var trainingPlanElement = _trainingPlanFactory.InstantiateElement();
				trainingPlanElement.Initialize(subTrainingData.Guid, subTrainingData.Name, subTrainingData.LogoSpriteIndex);
				trainingPlanElement.OpenTrainingPlanEvent += OnOpenSubTrainingPlanEventHandler;
				_trainingPlanElements.Add(trainingPlanElement);
			}
		}
		
		private void OnOpenTrainingPlanEventHandler(string trainingGuid)
		{
			_uiManager.CloseTrainingPlansPanel();
			_uiManager.OpenEditTrainingPanel(_accountController.GetTraining(trainingGuid));
		}

		private void OnOpenSubTrainingPlanEventHandler(string subTrainingGuid)
		{
			_uiManager.CloseTrainingPlansPanel();
			_uiManager.OpenEditSubTrainingPanel(_accountController.GetSubTraining(subTrainingGuid));
		}
		
		private void ClearTrainingPlanElements()
		{
			foreach (var trainingPlanElement in _trainingPlanElements)
			{
				trainingPlanElement.Destroy();
			}
			_trainingPlanElements.Clear();
		}
	}
}