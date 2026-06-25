using TrainingApp.Configs.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.Utils
{
	[RequireComponent(typeof(Button))]
	public class ButtonConfigApplier : MonoBehaviour
	{
		[SerializeField]
		private Button _button;
		
		[SerializeField]
		private ButtonConfig _buttonConfig;

		public ButtonConfig ButtonConfig => _buttonConfig;

		private void OnValidate()
		{
			ApplyButtonConfig();
		}

		public void ApplyButtonConfig()
		{
			if (_button == null)
			{
				_button = GetComponent<Button>();
			}

			if (_buttonConfig == null)
			{
				return;
			}

			ColorBlock colorBlock = _button.colors;
			
			colorBlock.normalColor = _buttonConfig.NormalColor;
			colorBlock.highlightedColor = _buttonConfig.HighlightedColor;
			colorBlock.pressedColor = _buttonConfig.PressedColor;
			colorBlock.selectedColor = _buttonConfig.SelectedColor;
			colorBlock.disabledColor = _buttonConfig.DisabledColor;

			colorBlock.colorMultiplier = _buttonConfig.ColorMultiplier;
			colorBlock.fadeDuration = _buttonConfig.FadeDuration;

			_button.colors = colorBlock;
			_button.navigation = _buttonConfig.Navigation;
		}
	}
}