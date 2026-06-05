using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class ToggleButton : MonoBehaviour
	{
		[SerializeField]
		private Button _button;

		[SerializeField]
		private List<StatesVisualizationStrategy> _statesVisualizationStrategies = new();

		private bool _isActive;

		public bool IsActive => _isActive;

		public event Action<bool> ToggleButtonStateChangedEvent;

		private void OnEnable()
		{
			_button.onClick.AddListener(ChangeState);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(ChangeState);
		}

		public void Initialize(bool isActive)
		{
			SetState(isActive);
		}

		public void ResetStateChangeEvent()
		{
			ToggleButtonStateChangedEvent = null;
		}

		public void ChangeState()
		{
			SetState(!_isActive);
		}

		public void SetState(bool isActive)
		{
			_isActive = isActive;
			
			foreach (var stateVisualizationStrategy in _statesVisualizationStrategies)
			{
				stateVisualizationStrategy.SetState(isActive);
			}
			
			ToggleButtonStateChangedEvent?.Invoke(_isActive);
		}

		public void SetInteractable(bool isInteractable)
		{
			_button.interactable = isInteractable;
		}
	}
}