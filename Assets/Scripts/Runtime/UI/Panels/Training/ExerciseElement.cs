using System;
using TMPro;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class ExerciseElement : MonoBehaviour, IDestroyableElement<ExerciseElement>
	{
		[SerializeField]
		private TMP_Text _nameText;

		[SerializeField]
		private TMP_Text _repetitionsNumberText;

		[SerializeField]
		private TMP_Text _descriptionText;

		public event Action<ExerciseElement> DestroyEvent;
		
		public void Initialize(ExerciseData exerciseData)
		{
			_nameText.text = exerciseData.Name;
			_repetitionsNumberText.text = $"{exerciseData.RepetitionsNumber}x";
			_descriptionText.text = exerciseData.Description;
		}

		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
	}
}