using TrainingApp.ServiceLocatorScripts;
using UnityEngine;

namespace TrainingApp.ServiceScripts
{
	public class ColorProvider : MonoBehaviour, IService
	{
		[SerializeField]
		private Color[] _colors;
		
		public Color GetColor(int index)
		{
			return _colors[index];
		}

		public Color[] GetColors()
		{
			Color[] colors = new Color[_colors.Length];
			for (int i = 0; i < _colors.Length; i++)
			{
				colors[i] = _colors[i];
			}
			return colors;
		}
	}
}