using UnityEngine;

namespace TrainingApp.UI
{
	public abstract class StatesVisualizationStrategy : MonoBehaviour
	{
		public abstract void SetState(bool isActive);
	}
}