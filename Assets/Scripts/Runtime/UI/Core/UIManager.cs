using System;
using System.Collections.Generic;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class UIManager : MonoBehaviour
	{
		[Header("Navigation")]
		[SerializeField]
		private NavigationFooter _navigationFooter;
		
		[Header("Panels")]
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		[SerializeField]
		private CalendarPanel _calendarPanel;

		[SerializeField]
		private TrainingPlansPanel _trainingPlansPanel;

		[SerializeField]
		private TrainingPanel _trainingPanel;

		[SerializeField]
		private EditTrainingPanel _editTrainingPanel;

		private Dictionary<Type, BasePanel> _panels = new();
		private BasePanel _currentActivePanel;

		public void Initialize()
		{
			_panels = new()
			{
				{ typeof(MainMenuPanel), _mainMenuPanel },
				{ typeof(CalendarPanel), _calendarPanel },
				{ typeof(TrainingPlansPanel), _trainingPlansPanel },
				{ typeof(TrainingPanel), _trainingPanel },
				{ typeof(EditTrainingPanel), _editTrainingPanel }
			};
			
			CloseAllPanels();

			_navigationFooter.Initialize(this);
			
			_mainMenuPanel.Initialize(this);
			_calendarPanel.Initialize(this);
			_trainingPlansPanel.Initialize(this);
			_trainingPanel.Initialize(this);
			_editTrainingPanel.Initialize(this);
			
			_navigationFooter.Open();
		}

		public void CloseAllPanels()
		{
			CloseMainMenuPanel();
			CloseCalendarPanel();
			CloseTrainingPlansPanel();
			CloseTrainingPanel();
			CloseEditTrainingPanel();
		}

		public void CloseCurrentActivePanel()
		{
			_currentActivePanel?.Close();
			_currentActivePanel = null;
		}

		public void OpenInstead<T>(BasePanel currentActivePanel) where T : BasePanel
		{
			currentActivePanel.Close();
			_currentActivePanel = _panels[typeof(T)];
			_currentActivePanel.Open();
		}
		
		public void OpenMainMenuPanel()
		{
			_currentActivePanel = _mainMenuPanel;
			_mainMenuPanel.Open();
		}
		public void CloseMainMenuPanel() => _mainMenuPanel.Close();

		public void OpenTrainingPlansPanel()
		{
			_currentActivePanel = _trainingPlansPanel;
			_trainingPlansPanel.Open();
		}
		public void CloseTrainingPlansPanel() => _trainingPlansPanel.Close();

		public void OpenCalendarPanel()
		{
			_currentActivePanel = _calendarPanel;
			_calendarPanel.Open();
		}
		public void CloseCalendarPanel() => _calendarPanel.Close();
		
		public void OpenTrainingPanel(DayData dayData)
		{
			_currentActivePanel = _trainingPanel;
			_trainingPanel.Open(dayData);
		}
		public void CloseTrainingPanel() => _trainingPanel.Close();

		public void OpenEditTrainingPanel(TrainingData trainingData)
		{
			_currentActivePanel = _editTrainingPanel;
			_editTrainingPanel.Open(trainingData);
		}
		public void CloseEditTrainingPanel() => _editTrainingPanel.Close();
	}
}