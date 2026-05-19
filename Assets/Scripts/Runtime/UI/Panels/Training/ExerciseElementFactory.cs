using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class ExerciseElementFactory : MonoBehaviour
	{
		[SerializeField]
		private ExerciseElement _exerciseElementPrefab;

		[SerializeField]
		private Transform _exercisesParent;
		
		public ExerciseElement InstantiateExerciseElement(ExerciseData exerciseData)
		{
			var exerciseElement = Instantiate(_exerciseElementPrefab, _exercisesParent);
			exerciseElement.Initialize(exerciseData);
			exerciseElement.DestroyEvent += OnProductElementDestroyEventHandler;
			return exerciseElement;
		}

		private void OnProductElementDestroyEventHandler(ExerciseElement exerciseElement)
		{
			Destroy(exerciseElement.gameObject);
		}
	}
}