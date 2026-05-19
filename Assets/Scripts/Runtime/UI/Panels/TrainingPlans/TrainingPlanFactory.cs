using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class TrainingPlanFactory : MonoBehaviour
	{
		[SerializeField]
		private TrainingPlanElement _trainingPlanPrefab;

		[SerializeField]
		private Transform _trainingPlansParent;
		
		public TrainingPlanElement InstantiateTrainingPlanElement(TrainingData trainingData)
		{
			var trainingPlanElement = Instantiate(_trainingPlanPrefab, _trainingPlansParent);
			trainingPlanElement.Initialize(trainingData);
			trainingPlanElement.DestroyEvent += OnProductElementDestroyEventHandler;
			return trainingPlanElement;
		}

		private void OnProductElementDestroyEventHandler(TrainingPlanElement trainingPlanElement)
		{
			Destroy(trainingPlanElement.gameObject);
		}
	}
}