using TrainingApp.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.Configs.UI
{
	[CreateAssetMenu(fileName = "ButtonConfig", menuName = "Scriptable Objects/UI/Button Config")]
	public class ButtonConfig : ScriptableObject
	{
		[field: SerializeField]
		public Color NormalColor { get; private set; }
		
		[field: SerializeField]
		public Color HighlightedColor { get; private set; }
		
		[field: SerializeField]
		public Color PressedColor { get; private set; }
		
		[field: SerializeField]
		public Color SelectedColor { get; private set; }
		
		[field: SerializeField]
		public Color DisabledColor { get; private set; }
		
		[field: SerializeField] 
		[Range(0f, 5f)]
		public float ColorMultiplier { get; private set; }
		
		[field: SerializeField]
		public float FadeDuration { get; private set; }
		
		[field: SerializeField]
		public Navigation Navigation { get; private set; }
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			ButtonConfigApplier[] buttons = Resources.FindObjectsOfTypeAll<ButtonConfigApplier>();

			foreach (ButtonConfigApplier button in buttons)
			{
				if (button == null || button.ButtonConfig != this)
				{
					continue;
				}

				button.ApplyButtonConfig();
			}
		}
#endif
	}
}