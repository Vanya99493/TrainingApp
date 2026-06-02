using System;
using TMPro;
using TrainingApp.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class EditExerciseElement : MonoBehaviour, IDestroyableElement<EditExerciseElement>
	{
		[SerializeField]
		private Button _deleteButton;

		[SerializeField]
		private ValidatableInputFieldElement _editNameInputField;
		
		[SerializeField]
		private ValidatableInputFieldElement _editRepetitionsNumberInputField;

		[SerializeField]
		private TMP_InputField _editDescriptionInputField;
		
		public event Action<EditExerciseElement> DestroyEvent;

		private void OnEnable()
		{
			_deleteButton.onClick.AddListener(Destroy);
		}

		private void OnDisable()
		{
			_deleteButton.onClick.RemoveListener(Destroy);
		}

		public void Initialize(ExerciseData exerciseData)
		{
			_editNameInputField.Initialize(exerciseData.Name);
			_editRepetitionsNumberInputField.Initialize(exerciseData.RepetitionsNumber.ToString());
			_editDescriptionInputField.text = exerciseData.Description;
		}

		public bool ValidateData(out ExerciseData exerciseData)
		{
			if (_editNameInputField.Validate() & _editRepetitionsNumberInputField.Validate())
			{
				exerciseData = new ExerciseData()
				{
					Name = _editNameInputField.Text,
					RepetitionsNumber = Convert.ToInt32(_editRepetitionsNumberInputField.Text),
					Description = _editDescriptionInputField.text,
				};

				return true;
			}

			exerciseData = null;
			return false;
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
	}
}