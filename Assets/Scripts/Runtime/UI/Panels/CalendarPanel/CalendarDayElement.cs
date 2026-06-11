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
		private Image _trainingLogoImage;

		[SerializeField]
		private Image _subTrainingLogoImage;

		[Space(20)]
		[SerializeField]
		private TrainingButtonPresetConfig _trainingButtonPresetConfig;

		public event Action<CalendarDayElement> DestroyEvent;

		public void Initialize(DayData dayData, Sprite trainingLogo, Color trainingLogoColor, Sprite subTrainingLogo, Color subTrainingLogoColor, Action<DayData> daySelectCallback = null)
		{
			_dayText.text = dayData.DateTime.Day.ToString();

			if (trainingLogo)
			{
				_trainingLogoImage.sprite = trainingLogo;
				_trainingLogoImage.color = trainingLogoColor;
			}
			else
			{
				_trainingLogoImage.gameObject.SetActive(false);
			}
			
			if (subTrainingLogo)
			{
				_subTrainingLogoImage.sprite = subTrainingLogo;
				_subTrainingLogoImage.color = subTrainingLogoColor;
			}
			else
			{
				_subTrainingLogoImage.gameObject.SetActive(false);
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

			if (dayData.IsTrainingCompleted)
			{
				backgroundColor = _trainingButtonPresetConfig.CompletedTrainingColor;
			}
			else if (dayData.IsSubTrainingCompleted)
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