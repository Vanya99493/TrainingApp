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
		private SetsNumberControllerElement _setsNumberControllerElement;

		[SerializeField]
		private ToggleButton _markDoneToggleButton;

		private DayData _dayData;

		private List<ExerciseElement> _exerciseElements = new();

		public void Open(DayData dayData)
		{
			ClearExerciseElements();
			
			base.Open();

			_dayData = dayData;
			TrainingData trainingData = ServiceLocator.Instance.Get<AccountController>().GetTraining(_dayData.ExerciseGuid);
			
			_nameText.text = trainingData.Name;
			foreach (var exerciseData in trainingData.Exercises)
			{
				_exerciseElements.Add(_exerciseElementFactory.InstantiateExerciseElement(exerciseData));
			}

			int setsElementIndex = SetsNumberToElementIndex(dayData.SetsCount);
			_setsNumberControllerElement.Initialize(setsElementIndex);
			_markDoneToggleButton.Initialize(dayData.IsTrainingComplete);
			_markDoneToggleButton.SetInteractable(setsElementIndex != -1);
		}
		
		public override void Close()
		{
			base.Close();

			ClearExerciseElements();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_backButton.onClick.AddListener(OnBackButtonClickedEventHandler);
			_markDoneToggleButton.ToggleButtonStateChangedEvent += OnMarkDoneButtonClickedEventHandler;
			_setsNumberControllerElement.SelectSetIndexEvent += OnSelectSetIndexEventHandler;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_backButton.onClick.RemoveListener(OnBackButtonClickedEventHandler);
			_markDoneToggleButton.ToggleButtonStateChangedEvent -= OnMarkDoneButtonClickedEventHandler;
			_setsNumberControllerElement.SelectSetIndexEvent -= OnSelectSetIndexEventHandler;
		}

		private void OnBackButtonClickedEventHandler()
		{
			_uiManager.CloseTrainingPanel();
		}

		private void OnMarkDoneButtonClickedEventHandler(bool isActive)
		{
			_dayData.IsTrainingComplete = isActive;
		}

		private void OnSelectSetIndexEventHandler(int index)
		{
			int setsNumber = ElementIndexToSetsNumber(index);

			if (index < 0)
			{
				_markDoneToggleButton.SetState(false);
				_markDoneToggleButton.SetInteractable(false);
			}
			else
			{
				_dayData.SetsCount = setsNumber;
				_markDoneToggleButton.SetInteractable(true);
			}
		}

		private void ClearExerciseElements()
		{
			foreach (var exerciseElement in _exerciseElements)
			{
				exerciseElement.Destroy();
			}
			_exerciseElements.Clear();
		}

		private int SetsNumberToElementIndex(int setsNumber)
		{
			return setsNumber switch
			{
				2 => 0,
				3 => 1,
				4 => 2,
				_ => -1,
			};
		}

		private int ElementIndexToSetsNumber(int elementIndex)
		{
			return elementIndex switch
			{
				0 => 2,
				1 => 3,
				2 => 4,
				_ => 0,
			};
		}
	}
}