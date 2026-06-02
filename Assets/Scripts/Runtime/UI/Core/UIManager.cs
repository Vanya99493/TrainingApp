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
		private DayOverviewPanel _dayOverviewPanel;

		[SerializeField]
		private TrainingPlansPanel _trainingPlansPanel;

		[SerializeField]
		private TrainingPanel _trainingPanel;

		[SerializeField]
		private EditTrainingPanel _editTrainingPanel;

		[SerializeField]
		private AssignTrainingPanel _assignTrainingPanel;

		private Dictionary<Type, BasePanel> _panels = new();
		private List<BasePanel> _currentActivePanels = new();

		public void Initialize()
		{
			_panels = new()
			{
				{ typeof(MainMenuPanel), _mainMenuPanel },
				{ typeof(CalendarPanel), _calendarPanel },
				{ typeof(DayOverviewPanel), _dayOverviewPanel },
				{ typeof(TrainingPlansPanel), _trainingPlansPanel },
				{ typeof(TrainingPanel), _trainingPanel },
				{ typeof(EditTrainingPanel), _editTrainingPanel },
				{ typeof(AssignTrainingPanel), _assignTrainingPanel },
			};
			
			CloseAllPanels();

			_navigationFooter.Initialize(this);
			
			_mainMenuPanel.Initialize(this);
			_calendarPanel.Initialize(this);
			_dayOverviewPanel.Initialize(this);
			_trainingPlansPanel.Initialize(this);
			_trainingPanel.Initialize(this);
			_editTrainingPanel.Initialize(this);
			_assignTrainingPanel.Initialize(this);
			
			_navigationFooter.Open();
		}

		public void CloseAllPanels()
		{
			CloseMainMenuPanel();
			CloseCalendarPanel();
			CloseDayOverviewPanel();
			CloseTrainingPlansPanel();
			CloseTrainingPanel();
			CloseEditTrainingPanel();
			CloseAssignTrainingPanel();
		}

		public void CloseCurrentActivePanels()
		{
			foreach (var currentActivePanel in _currentActivePanels)
			{
				currentActivePanel.Close();
			}
			_currentActivePanels.Clear();
		}

		public void OpenInstead<T>(BasePanel currentActivePanel) where T : BasePanel
		{
			currentActivePanel.Close();
			if (_currentActivePanels.Contains(currentActivePanel))
			{
				_currentActivePanels.Remove(currentActivePanel);
			}
			var activePanel = _panels[typeof(T)];
			activePanel.Open();
			_currentActivePanels.Add(activePanel);
		}
		
		public void OpenMainMenuPanel()
		{
			_currentActivePanels.Add(_mainMenuPanel);
			_mainMenuPanel.Open();
		}

		public void OpenTrainingPlansPanel()
		{
			_currentActivePanels.Add(_trainingPlansPanel);
			_trainingPlansPanel.Open();
		}

		public void OpenCalendarPanel()
		{
			_currentActivePanels.Add(_calendarPanel);
			_calendarPanel.Open();
		}

		public void OpenDayOverviewPanel(DayData selectedDayData)
		{
			_currentActivePanels.Add(_dayOverviewPanel);
			_dayOverviewPanel.Open(selectedDayData);
		}
		
		public void OpenTrainingPanel(DayData dayData)
		{
			_currentActivePanels.Add(_trainingPanel);
			_trainingPanel.Open(dayData);
		}
		
		public void OpenEditTrainingPanel(TrainingData trainingData)
		{
			_currentActivePanels.Add(_editTrainingPanel);
			_editTrainingPanel.Open(trainingData);
		}

		public void OpenAssignTrainingPanel(DayData dayData)
		{
			_currentActivePanels.Add(_assignTrainingPanel);
			_assignTrainingPanel.Open(dayData);
		}
		
		public void CloseMainMenuPanel()
		{
			_currentActivePanels.Remove(_mainMenuPanel);
			_mainMenuPanel.Close();
		}
		
		public void CloseTrainingPlansPanel()
		{
			_currentActivePanels.Remove(_trainingPlansPanel);
			_trainingPlansPanel.Close();
		}

		public void CloseCalendarPanel()
		{
			_currentActivePanels.Remove(_calendarPanel);
			_calendarPanel.Close();
		}
		
		public void CloseDayOverviewPanel()
		{
			_currentActivePanels.Remove(_dayOverviewPanel);
			_dayOverviewPanel.Close();
		}
		
		public void CloseTrainingPanel()
		{
			_currentActivePanels.Remove(_trainingPanel);
			_trainingPanel.Close();
		}

		public void CloseEditTrainingPanel()
		{
			_currentActivePanels.Remove(_editTrainingPanel);
			_editTrainingPanel.Close();
		}
		
		public void CloseAssignTrainingPanel()
		{
			_currentActivePanels.Remove(_assignTrainingPanel);
			_assignTrainingPanel.Close();
		}
	}
}