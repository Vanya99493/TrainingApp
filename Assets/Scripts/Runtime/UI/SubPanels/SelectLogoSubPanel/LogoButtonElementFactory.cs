using TrainingApp.UIFactoryScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class LogoButtonElementFactory : UIFactory<LogoButtonElement>
	{
		[SerializeField]
		private GridLayoutGroup _gridLayoutGroup;

		public void Initialize(int elementsInRow)
		{
			float width = _gridLayoutGroup.GetComponent<RectTransform>().rect.width;
			float contentWidth = width - (_gridLayoutGroup.spacing.x * (elementsInRow - 1) + _gridLayoutGroup.padding.left + _gridLayoutGroup.padding.right);
			float elementSize = contentWidth / elementsInRow;
			_gridLayoutGroup.cellSize = new Vector2(elementSize, elementSize);
		}
	}
}