using System.Collections.Generic;
using TrainingApp.Configs.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class ImageColorVisualizationStrategy : StatesVisualizationStrategy
	{
		[SerializeField]
		private List<Image> _images = new();

		[SerializeField]
		private TwoStatesButtonPresetConfig _twoStatesButtonPresetConfig;
		
		public override void SetState(bool isActive)
		{
			foreach (var image in _images)
			{
				image.color = isActive
					? _twoStatesButtonPresetConfig.ActiveColor
					: _twoStatesButtonPresetConfig.InactiveColor;
			}
		}
	}
}