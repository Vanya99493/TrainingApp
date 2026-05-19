using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class ToggleButton : MonoBehaviour
	{
		[SerializeField]
		private Button _button;

		[SerializeField]
		private GameObject _affirmativeState;

		[SerializeField]
		private GameObject _negativeState;

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

		public void ChangeState()
		{
			SetState(!_isActive);
		}

		public void SetState(bool isActive)
		{
			_isActive = isActive;
			
			if (isActive)
			{
				_affirmativeState?.SetActive(true);
				_negativeState?.SetActive(false);
			}
			else
			{
				_affirmativeState?.SetActive(false);
				_negativeState?.SetActive(true);
			}
			
			ToggleButtonStateChangedEvent?.Invoke(_isActive);
		}
	}
}