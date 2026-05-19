using System.Collections.Generic;
using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class TrainingPanel : BasePanel
	{
		[SerializeField]
		private ExerciseElementFactory _exerciseElementFactory;
		
		[Space(10)]
		[SerializeField]
		private TMP_Text _nameText;

		[SerializeField]
		private Button _backButton;

		[SerializeField]
		private ToggleButton _markDoneToggleButton;

		private DayData _dayData;

		private List<ExerciseElement> _exerciseElements = new();

		public void Open(DayData dayData)
		{
			base.Open();

			_dayData = dayData;
			TrainingData trainingData = ServiceLocator.Instance.Get<AccountController>().GetTraining(_dayData.ExerciseGuid);
			
			_nameText.text = trainingData.Name;
			foreach (var exerciseData in trainingData.Exercises)
			{
				_exerciseElements.Add(_exerciseElementFactory.InstantiateExerciseElement(exerciseData));
			}
		}
		
		public override void Close()
		{
			base.Close();

			foreach (var exerciseElement in _exerciseElements)
			{
				exerciseElement.Destroy();
			}
			_exerciseElements.Clear();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_backButton.onClick.AddListener(OnBackButtonClickedEventHandler);
			_markDoneToggleButton.ToggleButtonStateChangedEvent += OnMarkDoneButtonClickedEventHandler;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_backButton.onClick.RemoveListener(OnBackButtonClickedEventHandler);
			_markDoneToggleButton.ToggleButtonStateChangedEvent -= OnMarkDoneButtonClickedEventHandler;
		}

		private void OnBackButtonClickedEventHandler()
		{
			_uiManager.OpenInstead<MainMenuPanel>(this);
		}

		private void OnMarkDoneButtonClickedEventHandler(bool isActive)
		{
			_dayData.IsTrainingComplete = isActive;
		}
	}
}