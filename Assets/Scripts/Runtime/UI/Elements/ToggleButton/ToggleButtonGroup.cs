using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingApp.UI
{
	public class ToggleButtonGroup : MonoBehaviour
	{
		[SerializeField]
		private List<ToggleButton> _toggleButtons = new();
		
		private int _currentSelectedIndex;
		
		public event Action<int> SelectToggleButtonEvent;

		public void Initialize(int activeToggleButtonIndex = -1)
		{
			_currentSelectedIndex = -1;
			
			int i = 0;
			foreach (var toggleButton in _toggleButtons)
			{
				int index = i;
				toggleButton.Initialize(false);
				toggleButton.ResetStateChangeEvent();
				toggleButton.ToggleButtonStateChangedEvent += isActive => OnToggleButtonStateChangedEventHandler(isActive, index);
				i++;
			}

			if (activeToggleButtonIndex >= 0 && activeToggleButtonIndex < _toggleButtons.Count)
			{
				_currentSelectedIndex = activeToggleButtonIndex;
				_toggleButtons[_currentSelectedIndex].SetState(true);
			}
		}

		public void AddToggleButton(ToggleButton toggleButton)
		{
			toggleButton.Initialize(false);
			toggleButton.ResetStateChangeEvent();
			toggleButton.ToggleButtonStateChangedEvent += isActive => OnToggleButtonStateChangedEventHandler(isActive, _toggleButtons.Count);
			_toggleButtons.Add(toggleButton);
		}

		public void ClearToggleButtons()
		{
			_toggleButtons.Clear();
		}

		private void OnToggleButtonStateChangedEventHandler(bool isActive, int index)
		{
			if (_currentSelectedIndex < 0)
			{
				if (isActive)
				{
					_currentSelectedIndex = index;
					SelectToggleButtonEvent?.Invoke(index);
				}
			}
			else
			{
				if (isActive)
				{
					if (_currentSelectedIndex != index)
					{
						_currentSelectedIndex = index;
						for (int i = 0; i < _toggleButtons.Count; i++)
						{
							if (i != _currentSelectedIndex)
							{
								_toggleButtons[i].SetState(false);
							}
						}
						SelectToggleButtonEvent?.Invoke(_currentSelectedIndex);
					}
				}
				else
				{
					if (_currentSelectedIndex == index)
					{
						_currentSelectedIndex = -1;
						SelectToggleButtonEvent?.Invoke(-1);
					}
				}
			}
		}
	}
}