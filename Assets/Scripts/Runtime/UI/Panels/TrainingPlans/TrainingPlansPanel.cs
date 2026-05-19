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
		private TrainingPlanFactory _trainingPlanFactory;
		
		[Space(10)]
		[SerializeField]
		private Button _addPlanButton;

		private AccountController _accountController;

		private List<TrainingPlanElement> _trainingPlanElements = new();

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public override void Open()
		{
			base.Open();

			var trainings = _accountController.ClientData.TrainingsDataContainer.Trainings;
			foreach (var trainingData in trainings)
			{
				var trainingPlanElement = _trainingPlanFactory.InstantiateTrainingPlanElement(trainingData);
				trainingPlanElement.OpenTrainingPlanEvent += OnOpenTrainingPlanEventHandler;
				_trainingPlanElements.Add(trainingPlanElement);
			}
		}

		public override void Close()
		{
			base.Close();

			foreach (var trainingPlanElement in _trainingPlanElements)
			{
				trainingPlanElement.Destroy();
			}
			_trainingPlanElements.Clear();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_addPlanButton.onClick.AddListener(OnAddPlanButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_addPlanButton.onClick.RemoveListener(OnAddPlanButtonClickedEventHandler);
		}

		private void OnAddPlanButtonClickedEventHandler()
		{
			_uiManager.OpenEditTrainingPanel(null);
		}
		
		private void OnOpenTrainingPlanEventHandler(TrainingData trainingData)
		{
			_uiManager.OpenEditTrainingPanel(trainingData);
		}
	}
}