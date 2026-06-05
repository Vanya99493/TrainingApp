using System.Collections.Generic;
using UnityEngine;

namespace TrainingApp.UI
{
	public class ObjectsVisualizationStrategy : StatesVisualizationStrategy
	{
		[SerializeField]
		private List<GameObject> _affirmativeStateObjects = new();

		[SerializeField]
		private List<GameObject> _negativeStateObjects = new();

		public override void SetState(bool isActive)
		{
			foreach (var affirmativeStateObject in _affirmativeStateObjects)
			{
				affirmativeStateObject.SetActive(isActive);
			}

			foreach (var negativeStateObject in _negativeStateObjects)
			{
				negativeStateObject.SetActive(!isActive);
			}
		}
	}
}