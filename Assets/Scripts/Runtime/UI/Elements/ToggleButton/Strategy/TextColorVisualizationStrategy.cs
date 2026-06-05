using System.Collections.Generic;
using TMPro;
using TrainingApp.Configs.UI;
using UnityEngine;

namespace TrainingApp.UI
{
	public class TextColorVisualizationStrategy : StatesVisualizationStrategy
	{
		[SerializeField]
		private List<TMP_Text> _texts = new();

		[SerializeField]
		private TwoStatesButtonPresetConfig _twoStatesButtonPresetConfig;

		public override void SetState(bool isActive)
		{
			foreach (var text in _texts)
			{
				text.color = isActive
					? _twoStatesButtonPresetConfig.ActiveColor
					: _twoStatesButtonPresetConfig.InactiveColor;
			}
		}
	}
}