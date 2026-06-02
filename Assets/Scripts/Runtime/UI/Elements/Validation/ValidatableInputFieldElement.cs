using TMPro;
using UnityEngine;

namespace TrainingApp.UI
{
	public class ValidatableInputFieldElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField _inputField;

		[SerializeField]
		private GameObject _failedValidationView;

		[SerializeField]
		private ValidationType _validationType;

		public string Text => _inputField.text;

		private void OnEnable()
		{
			_inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
		}

		private void OnDisable()
		{
			_inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
		}

		public void Initialize(string content)
		{
			_inputField.text = content;
			
			_failedValidationView.SetActive(false);
		}

		public bool Validate()
		{
			if (_validationType.HasFlag(ValidationType.NotEmpty))
			{
				if (_inputField.text.Length <= 0)
				{
					_failedValidationView.SetActive(true);
					return false;
				}
			}
			
			if (_validationType.HasFlag(ValidationType.IsNumber))
			{
				if (!int.TryParse(_inputField.text, out var number))
				{
					_failedValidationView.SetActive(true);
					return false;
				}
			}
			
			if (_validationType.HasFlag(ValidationType.IsPositiveNumber))
			{
				if (!int.TryParse(_inputField.text, out var number) || number <= 0)
				{
					_failedValidationView.SetActive(true);
					return false;
				}
			}
			
			if (_validationType.HasFlag(ValidationType.IsZeroOrPositiveNumber))
			{
				if (!int.TryParse(_inputField.text, out var number) || number < 0)
				{
					_failedValidationView.SetActive(true);
					return false;
				}
			}

			return true;
		}

		private void OnInputFieldValueChanged(string newValue)
		{
			
		}
	}
}