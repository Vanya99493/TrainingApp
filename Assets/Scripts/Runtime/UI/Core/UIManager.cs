using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingApp.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		[SerializeField]
		private TrainingPanel _trainingPanel;

		private Dictionary<Type, BasePanel> _panels = new();

		public void Initialize()
		{
			_panels = new()
			{
				{ typeof(MainMenuPanel), _mainMenuPanel },
				{ typeof(TrainingPanel), _trainingPanel },
			};
			
			CloseAllPanels();

			_mainMenuPanel.Initialize(this);
			_trainingPanel.Initialize(this);
		}

		public void CloseAllPanels()
		{
			_mainMenuPanel.Close();
			_trainingPanel.Close();
		}

		public void OpenInstead<T>(BasePanel currentActivePanel) where T : BasePanel
		{
			currentActivePanel.Close();
			_panels[typeof(T)].Open();
		}
		
		public void OpenMainMenuPanel() => _mainMenuPanel.Open();
		public void CloseMainMenuPanel() => _mainMenuPanel.Close();

		public void OpenTrainingPanel(string guid) => _trainingPanel.Open(guid);
		public void CloseTrainingPanel() => _trainingPanel.Close();
	}
}