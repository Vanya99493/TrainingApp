using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class EditExerciseElementFactory : MonoBehaviour
	{
		[SerializeField]
		private EditExerciseElement _editExerciseElementPrefab;

		[SerializeField]
		private Transform _editExerciseElementParent;
		
		public EditExerciseElement InstantiateEditExerciseElement(ExerciseData exerciseData)
		{
			var editExerciseElement = Instantiate(_editExerciseElementPrefab, _editExerciseElementParent);
			editExerciseElement.Initialize(exerciseData);
			editExerciseElement.DestroyEvent += OnEditExerciseElementDestroyEventHandler;
			return editExerciseElement;
		}

		private void OnEditExerciseElementDestroyEventHandler(EditExerciseElement editExerciseElement)
		{
			Destroy(editExerciseElement.gameObject);
		}
	}
}