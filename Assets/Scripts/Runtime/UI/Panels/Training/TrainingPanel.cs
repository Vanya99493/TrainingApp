using System;
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
		private ToggleButtonGroup _setsNumberToggleButtonGroup;

		[SerializeField]
		private ToggleButton _markDoneToggleButton;

		private AccountController _accountController;
		private DayData _dayData;
		private List<ExerciseElement> _exerciseElements = new();

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public override void Open(PanelArgs args)
		{
			ClearExerciseElements();
			
			base.Open(args);

			_dayData = (args as DayDataPanelArgs).DayData;
			TrainingData trainingData = ServiceLocator.Instance.Get<AccountController>().GetTraining(_dayData.ExerciseGuid);
			
			_nameText.text = trainingData.Name;
			foreach (var exerciseData in trainingData.Exercises)
			{
				_exerciseElements.Add(_exerciseElementFactory.InstantiateExerciseElement(exerciseData));
			}

			if (_dayData.DateTime.Date > DateTime.Today.Date)
			{
				_setsNumberToggleButtonGroup.Initialize();
				_setsNumberToggleButtonGroup.gameObject.SetActive(false);
				_markDoneToggleButton.Initialize(false);
				_markDoneToggleButton.SetInteractable(false);
			}
			else
			{
				_setsNumberToggleButtonGroup.gameObject.SetActive(true);
				int setsElementIndex = SetsNumberToElementIndex(_dayData.SetsCount);
				setsElementIndex = _dayData.IsTrainingCompleted ? setsElementIndex : -1;
				_setsNumberToggleButtonGroup.Initialize(setsElementIndex);
				_markDoneToggleButton.Initialize(_dayData.IsTrainingCompleted);
				_markDoneToggleButton.SetInteractable(setsElementIndex != -1);
			}
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
			_setsNumberToggleButtonGroup.SelectToggleButtonEvent += OnSelectToggleButtonEventHandler;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_backButton.onClick.RemoveListener(OnBackButtonClickedEventHandler);
			_markDoneToggleButton.ToggleButtonStateChangedEvent -= OnMarkDoneButtonClickedEventHandler;
			_setsNumberToggleButtonGroup.SelectToggleButtonEvent -= OnSelectToggleButtonEventHandler;
		}

		private void OnBackButtonClickedEventHandler()
		{
			_uiManager.OpenLastClosedPanelInstead(this, new DayDataPanelArgs(_dayData));
		}

		private void OnMarkDoneButtonClickedEventHandler(bool isActive)
		{
			_dayData.IsTrainingCompleted = isActive;
			_accountController.AddOrUpdateDayData(_dayData);
		}

		private void OnSelectToggleButtonEventHandler(int index)
		{
			int setsNumber = ElementIndexToSetsNumber(index);

			if (index < 0)
			{
				_markDoneToggleButton.SetState(false);
				_markDoneToggleButton.SetInteractable(false);

				_dayData.SetsCount = -1;
				_dayData.IsTrainingCompleted = false;
				_accountController.AddOrUpdateDayData(_dayData);
			}
			else
			{
				_dayData.SetsCount = setsNumber;
				_markDoneToggleButton.SetState(false);
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