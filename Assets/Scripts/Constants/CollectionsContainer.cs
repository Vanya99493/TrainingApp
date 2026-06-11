using TrainingApp.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingApp.Constants
{
	public static class CollectionsContainer
	{
		public static List<ColorType> Colors = new()
		{
			ColorType.White,
			ColorType.Grey,
			ColorType.Black,
			ColorType.Red,
			ColorType.Green,
			ColorType.Blue,
			ColorType.Yellow,
			ColorType.Purple,
			ColorType.Orange,
			ColorType.LightGreen,
			ColorType.PaleBlue,
			ColorType.PaleGreen,
			ColorType.DarkGrey,
			ColorType.PalePurple,
		};
		
		public static Dictionary<ColorType, Color> RGBColors = new()
		{
			{ ColorType.White, new Color(1f, 1f, 1f) },
			{ ColorType.Grey, new Color(0.5f, 0.5f, 0.5f) },
			{ ColorType.Black, new Color(0f, 0f, 0f) },
			{ ColorType.Red, new Color(1f, 0f, 0f) },
			{ ColorType.Green, new Color(0f, 1f, 0f) },
			{ ColorType.Blue, new Color(0f, 0f, 1f) },
			{ ColorType.Yellow, new Color(1f, 1f, 0f) },
			{ ColorType.Purple, new Color(0.5f, 0f, 0.5f) },
			{ ColorType.Orange, new Color(1.0f, 0.55f, 0.0f) },
			{ ColorType.LightGreen, new Color(0.6f, 1.0f, 0.4f) },
			{ ColorType.PaleBlue, new Color(0.118f, 0.471f, 0.596f) },
			{ ColorType.PaleGreen, new Color(0.561f, 0.820f, 0.620f) },
			{ ColorType.DarkGrey, new Color(0.22f, 0.22f, 0.22f) },
			{ ColorType.PalePurple, new Color(0.78f, 0.68f, 0.92f) },
		};
	}
}