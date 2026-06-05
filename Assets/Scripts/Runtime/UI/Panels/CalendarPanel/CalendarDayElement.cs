using System;
using TMPro;
using TrainingApp.Configs.UI;
using TrainingApp.Constants;
using TrainingApp.Extensions;
using TrainingApp.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class CalendarDayElement : MonoBehaviour, IDestroyableElement<CalendarDayElement>
	{
		[SerializeField]
		private Button _dayButton;
		
		[SerializeField]
		private TMP_Text _dayText;

		[SerializeField]
		private Image _logoImage;

		[Space(20)]
		[SerializeField]
		private TrainingButtonPresetConfig _trainingButtonPresetConfig;

		public event Action<CalendarDayElement> DestroyEvent;

		public void Initialize(DayData dayData, Sprite exerciseLogo, Action<DayData> daySelectCallback = null)
		{
			_dayText.text = dayData.DateTime.Day.ToString();

			if (exerciseLogo)
			{
				_logoImage.sprite = exerciseLogo;
			}
			else
			{
				_logoImage.gameObject.SetActive(false);
			}

			SetBackgroundColor(dayData);

			_dayButton.onClick.RemoveAllListeners();
			_dayButton.onClick.AddListener(() => daySelectCallback?.Invoke(dayData));
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
		
		private void SetBackgroundColor(DayData dayData)
		{
			Color backgroundColor;

			if (dayData.IsTrainingComplete)
			{
				backgroundColor = _trainingButtonPresetConfig.CompletedTrainingColor;
			}
			else if (dayData.IsTrainingHalfCompletedByActivities)
			{
				backgroundColor = _trainingButtonPresetConfig.HalfCompletedTrainingColor;
			}
			else if (dayData.DateTime.Date >= DateTime.Today.Date || dayData.ExerciseGuid == UIConstants.REST_FROM_TRAINING || dayData.ExerciseGuid == "")
			{
				backgroundColor = _trainingButtonPresetConfig.InactiveColor;
			}
			else
			{
				backgroundColor = _trainingButtonPresetConfig.MissedTrainingColor;
			}

			_dayButton.SetNormalColor(backgroundColor);
		}
	}
}