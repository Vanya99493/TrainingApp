using TMPro;
using TrainingApp.Enums;
using TrainingApp.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class LessonSpentTimeElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _spentTimeText;
		
		[SerializeField]
		private Slider _spentTimeSlider;

		private int _spentTimeTypeIndex;
		
		public SpentTimeType SpentTimeType => (SpentTimeType)_spentTimeTypeIndex;

		private void OnEnable()
		{
			_spentTimeSlider.onValueChanged.AddListener(OnSpentTimeSliderValueChanged);
		}

		private void OnDisable()
		{
			_spentTimeSlider.onValueChanged.RemoveListener(OnSpentTimeSliderValueChanged);
		}

		public void Initialize(SpentTimeType spentTimeType)
		{
			_spentTimeText.text = SpentTimeTypeConverter.ConvertToString((SpentTimeType)_spentTimeTypeIndex);
			_spentTimeSlider.value = (int)spentTimeType;
		}
		
		private void OnSpentTimeSliderValueChanged(float value)
		{
			_spentTimeTypeIndex = (int)value;
			_spentTimeText.text = SpentTimeTypeConverter.ConvertToString((SpentTimeType)_spentTimeTypeIndex);
		}
	}
}