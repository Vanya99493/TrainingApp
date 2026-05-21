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
		private TMP_InputField _editNameInputField;
		
		[SerializeField]
		private TMP_InputField _editRepetitionsNumberInputField;

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
			_editNameInputField.text = exerciseData.Name;
			_editRepetitionsNumberInputField.text = exerciseData.RepetitionsNumber.ToString();
			_editDescriptionInputField.text = exerciseData.Description;
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
	}
}