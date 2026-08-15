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
		private EnglishLessonPanel _englishLessonPanel;

		[SerializeField]
		private TrainingPlansPanel _trainingPlansPanel;

		[SerializeField]
		private TrainingPanel _trainingPanel;

		[SerializeField]
		private SubTrainingPanel _subTrainingPanel;

		[SerializeField]
		private EditTrainingPanel _editTrainingPanel;

		[SerializeField]
		private EditSubTrainingPanel _editSubTrainingPanel;

		[SerializeField]
		private AssignTrainingPanel _assignTrainingPanel;

		private Dictionary<Type, BasePanel> _panels = new();
		private List<BasePanel> _currentActivePanels = new();
		private BasePanel _lastClosedPanel;

		public void Initialize()
		{
			_panels = new()
			{
				{ typeof(MainMenuPanel), _mainMenuPanel },
				{ typeof(CalendarPanel), _calendarPanel },
				{ typeof(DayOverviewPanel), _dayOverviewPanel },
				{ typeof(EnglishLessonPanel), _englishLessonPanel },
				{ typeof(TrainingPlansPanel), _trainingPlansPanel },
				{ typeof(TrainingPanel), _trainingPanel },
				{ typeof(SubTrainingPanel), _subTrainingPanel },
				{ typeof(EditTrainingPanel), _editTrainingPanel },
				{ typeof(EditSubTrainingPanel), _editSubTrainingPanel },
				{ typeof(AssignTrainingPanel), _assignTrainingPanel },
			};
			
			CloseAllPanels();

			_navigationFooter.Initialize(this);
			
			_mainMenuPanel.Initialize(this);
			_calendarPanel.Initialize(this);
			_dayOverviewPanel.Initialize(this);
			_englishLessonPanel.Initialize(this);
			_trainingPlansPanel.Initialize(this);
			_trainingPanel.Initialize(this);
			_subTrainingPanel.Initialize(this);
			_editTrainingPanel.Initialize(this);
			_editSubTrainingPanel.Initialize(this);
			_assignTrainingPanel.Initialize(this);
			
			_navigationFooter.Open(new PanelArgs());
		}

		public void CloseAllPanels()
		{
			CloseMainMenuPanel();
			CloseCalendarPanel();
			CloseDayOverviewPanel();
			CLoseEnglishLessonPanel();
			CloseTrainingPlansPanel();
			CloseTrainingPanel();
			CloseSubTrainingPanel();
			CloseEditTrainingPanel();
			CloseEditSubTrainingPanel();
			CloseAssignTrainingPanel();
		}

		public void CloseCurrentActivePanels()
		{
			foreach (var currentActivePanel in _currentActivePanels)
			{
				currentActivePanel.Close();
			}
			_currentActivePanels.Clear();
			_lastClosedPanel = null;
		}

		public void OpenInstead<T>(BasePanel currentActivePanel, PanelArgs panelArgs = null) where T : BasePanel
		{
			currentActivePanel.Close();
			_lastClosedPanel = currentActivePanel;
			if (_currentActivePanels.Contains(currentActivePanel))
			{
				_currentActivePanels.Remove(currentActivePanel);
			}
			var activePanel = _panels[typeof(T)];
			activePanel.Open(panelArgs);
			_currentActivePanels.Add(activePanel);
		}

		public void OpenLastClosedPanelInstead(BasePanel currentActivePanel, PanelArgs panelArgs = null)
		{
			_lastClosedPanel.Open(panelArgs);
			_currentActivePanels.Add(_lastClosedPanel);
			currentActivePanel.Close();
			_lastClosedPanel = currentActivePanel;
			if (_currentActivePanels.Contains(currentActivePanel))
			{
				_currentActivePanels.Remove(currentActivePanel);
			}
		}
		
		public void OpenMainMenuPanel()
		{
			_currentActivePanels.Add(_mainMenuPanel);
			_mainMenuPanel.Open(new PanelArgs());
		}

		public void OpenTrainingPlansPanel()
		{
			_currentActivePanels.Add(_trainingPlansPanel);
			_trainingPlansPanel.Open(new PanelArgs());
		}

		public void OpenCalendarPanel()
		{
			_currentActivePanels.Add(_calendarPanel);
			_calendarPanel.Open(new PanelArgs());
		}

		public void OpenDayOverviewPanel(DayData selectedDayData)
		{
			_currentActivePanels.Add(_dayOverviewPanel);
			_dayOverviewPanel.Open(new DayDataPanelArgs(selectedDayData));
		}

		public void OpenEnglishLessonPanel(DayData datData)
		{
			_currentActivePanels.Add(_englishLessonPanel);
			_englishLessonPanel.Open(new DayDataPanelArgs(datData));
		}
		
		public void OpenTrainingPanel(DayData dayData)
		{
			_currentActivePanels.Add(_trainingPanel);
			_trainingPanel.Open(new DayDataPanelArgs(dayData));
		}
		
		public void OpenSubTrainingPanel(DayData dayData)
		{
			_currentActivePanels.Add(_subTrainingPanel);
			_subTrainingPanel.Open(new DayDataPanelArgs(dayData));
		}
		
		public void OpenEditTrainingPanel(TrainingData trainingData)
		{
			_currentActivePanels.Add(_editTrainingPanel);
			_editTrainingPanel.Open(new TrainingDataPanelArgs(trainingData));
		}
		
		public void OpenEditSubTrainingPanel(SubTrainingData subTrainingData)
		{
			_currentActivePanels.Add(_editSubTrainingPanel);
			_editSubTrainingPanel.Open(new SubTrainingDataPanelArgs(subTrainingData));
		}

		public void OpenAssignTrainingPanel(DayData dayData)
		{
			_currentActivePanels.Add(_assignTrainingPanel);
			_assignTrainingPanel.Open(new DayDataPanelArgs(dayData));
		}
		
		public void CloseMainMenuPanel()
		{
			_currentActivePanels.Remove(_mainMenuPanel);
			_mainMenuPanel.Close();
			_lastClosedPanel = _mainMenuPanel;
		}
		
		public void CloseTrainingPlansPanel()
		{
			_currentActivePanels.Remove(_trainingPlansPanel);
			_trainingPlansPanel.Close();
			_lastClosedPanel = _trainingPlansPanel;
		}

		public void CloseCalendarPanel()
		{
			_currentActivePanels.Remove(_calendarPanel);
			_calendarPanel.Close();
			_lastClosedPanel = _calendarPanel;
		}
		
		public void CloseDayOverviewPanel()
		{
			_currentActivePanels.Remove(_dayOverviewPanel);
			_dayOverviewPanel.Close();
			_lastClosedPanel = _dayOverviewPanel;
		}

		public void CLoseEnglishLessonPanel()
		{
			_currentActivePanels.Remove(_englishLessonPanel);
			_englishLessonPanel.Close();
			_lastClosedPanel = _englishLessonPanel;
		}
		
		public void CloseTrainingPanel()
		{
			_currentActivePanels.Remove(_trainingPanel);
			_trainingPanel.Close();
			_lastClosedPanel = _trainingPanel;
		}
		
		public void CloseSubTrainingPanel()
		{
			_currentActivePanels.Remove(_subTrainingPanel);
			_subTrainingPanel.Close();
			_lastClosedPanel = _subTrainingPanel;
		}

		public void CloseEditTrainingPanel()
		{
			_currentActivePanels.Remove(_editTrainingPanel);
			_editTrainingPanel.Close();
			_lastClosedPanel = _editTrainingPanel;
		}

		public void CloseEditSubTrainingPanel()
		{
			_currentActivePanels.Remove(_editSubTrainingPanel);
			_editSubTrainingPanel.Close();
			_lastClosedPanel = _editSubTrainingPanel;
		}
		
		public void CloseAssignTrainingPanel()
		{
			_currentActivePanels.Remove(_assignTrainingPanel);
			_assignTrainingPanel.Close();
			_lastClosedPanel = _assignTrainingPanel;
		}
	}
}