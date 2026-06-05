using TrainingApp.Configs.UI;
using TrainingApp.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class ColorVisualizationStrategy : StatesVisualizationStrategy
	{
		[SerializeField]
		private Button _button;

		[SerializeField]
		private TwoStatesButtonPresetConfig _twoStatesButtonPresetConfig;

		public override void SetState(bool isActive)
		{
			_button.SetNormalColor(isActive ? _twoStatesButtonPresetConfig.ActiveColor : _twoStatesButtonPresetConfig.InactiveColor);
		}
	}
}