using UnityEngine;

namespace TrainingApp.Configs.UI
{
	[CreateAssetMenu(fileName = "TrainingButtonPresetConfig", menuName = "Scriptable Objects/UI/Training Button Preset Config")]
	public class TrainingButtonPresetConfig : ScriptableObject
	{
		[field: SerializeField]
		public Color InactiveColor { get; private set; }
		
		[field: SerializeField]
		public Color CompletedTrainingColor { get; private set; }

		[field: SerializeField]
		public Color HalfCompletedTrainingColor { get; private set; }
		
		[field: SerializeField]
		public Color MissedTrainingColor { get; private set; }
	}
}