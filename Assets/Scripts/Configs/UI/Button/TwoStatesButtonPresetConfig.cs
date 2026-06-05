using UnityEngine;

namespace TrainingApp.Configs.UI
{
	[CreateAssetMenu(fileName = "TwoStatesButtonPresetConfig", menuName = "Scriptable Objects/UI/Two States Button Preset Config")]
	public class TwoStatesButtonPresetConfig : ScriptableObject
	{
		[field: SerializeField]
		public Color InactiveColor { get; private set; }
		
		[field: SerializeField]
		public Color ActiveColor { get; private set; }
	}
}