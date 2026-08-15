using System;
using TMPro;
using TrainingApp.Configs.UI;
using TrainingApp.Enums;
using TrainingApp.Extensions;
using TrainingApp.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class EnglishLessonElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _lessonTimeText;
		
		[SerializeField]
		private TMP_Text _lessonMessageText;

		[SerializeField]
		private Button _englishLessonButton;		
		
		[Header("Configs")]
		[SerializeField]
		private EnglishLessonButtonConfig _lessonButtonPresetConfig;

		private DateTime _date;
		
		public event Action<DateTime> OpenEnglishLessonEvent;
		
		private void OnEnable()
		{
			_englishLessonButton.onClick.AddListener(OnOpenEnglishLessonButtonClickedEventHandler);
		}

		private void OnDestroy()
		{
			_englishLessonButton.onClick.RemoveListener(OnOpenEnglishLessonButtonClickedEventHandler);
		}

		public void Initialize(DateTime assignedDay, EnglishLessonData englishLessonData)
		{
			_date = assignedDay;

			_lessonTimeText.text = SpentTimeTypeConverter.ConvertToString(englishLessonData.SpentTimeType);
			_lessonMessageText.text = englishLessonData.LessonMessage;
			
			_englishLessonButton.SetNormalColor(_lessonButtonPresetConfig.GetButtonColorByType(englishLessonData.SpentTimeType));
		}

		private void OnOpenEnglishLessonButtonClickedEventHandler()
		{
			OpenEnglishLessonEvent?.Invoke(_date);
		}
	}
}