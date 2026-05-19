using System;
using System.Collections.Generic;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		[SerializeField]
		private TrainingPanel _trainingPanel;

		[SerializeField]
		private TrainingPlansPanel _trainingPlansPanel;

		[SerializeField]
		private EditTrainingPanel _editTrainingPanel;

		private Dictionary<Type, BasePanel> _panels = new();

		public void Initialize()
		{
			_panels = new()
			{
				{ typeof(MainMenuPanel), _mainMenuPanel },
				{ typeof(TrainingPanel), _trainingPanel },
				{ typeof(TrainingPlansPanel), _trainingPlansPanel },
				{ typeof(EditTrainingPanel), _editTrainingPanel }
			};
			
			CloseAllPanels();

			_mainMenuPanel.Initialize(this);
			_trainingPanel.Initialize(this);
			_trainingPlansPanel.Initialize(this);
			_editTrainingPanel.Initialize(this);
		}

		public void CloseAllPanels()
		{
			CloseMainMenuPanel();
			CloseTrainingPanel();
			CloseTrainingPlansPanel();
			CloseEditTrainingPanel();
		}

		public void OpenInstead<T>(BasePanel currentActivePanel) where T : BasePanel
		{
			currentActivePanel.Close();
			_panels[typeof(T)].Open();
		}
		
		public void OpenMainMenuPanel() => _mainMenuPanel.Open();
		public void CloseMainMenuPanel() => _mainMenuPanel.Close();

		public void OpenTrainingPanel(DayData dayData) => _trainingPanel.Open(dayData);
		public void CloseTrainingPanel() => _trainingPanel.Close();

		public void OpenTrainingPlansPanel() => _trainingPlansPanel.Open();
		public void CloseTrainingPlansPanel() => _trainingPlansPanel.Close();

		public void OpenEditTrainingPanel(TrainingData trainingData) => _editTrainingPanel.Open(trainingData);
		public void CloseEditTrainingPanel() => _editTrainingPanel.Close();
	}
}