using UnityEngine;

namespace TrainingApp.Configs.UI
{
	[CreateAssetMenu(fileName = "ButtonPresetConfig", menuName = "Scriptable Objects/UI/Button Preset Config")]
	public class ButtonPresetConfig : ScriptableObject
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