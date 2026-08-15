using TrainingApp.Enums;
using TrainingApp.Utils;
using UnityEngine;

namespace TrainingApp.Configs.UI
{
	[CreateAssetMenu(fileName = "EnglishLessonButtonConfig", menuName = "Scriptable Objects/UI/English Lesson Button Config")]
	public class EnglishLessonButtonConfig : ScriptableObject
	{
		[SerializeField]
		private Color _defaultLessonButtonColor;

		[SerializeField]
		private SerializableDictionary<SpentTimeType, Color> _suitableTypeButtonColors = new();

		public Color GetButtonColorByType(SpentTimeType spentTimeType)
		{
			return _suitableTypeButtonColors.GetValueOrDefault(
				spentTimeType,
				_defaultLessonButtonColor);
		}
	}
}