using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.Extensions
{
	public static class ButtonExtensions
	{
		public static void SetNormalColor(this Button button, Color color)
		{
			ColorBlock buttonColors = button.colors;
			buttonColors.normalColor = color;
			button.colors = buttonColors;
		}
	}
}