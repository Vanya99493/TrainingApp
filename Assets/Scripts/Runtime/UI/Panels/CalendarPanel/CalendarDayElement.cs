using System;
using System.Collections;
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
		private const float CURRENT_DATE_DEFAULT_WIDTH = 14f;
		
		[SerializeField]
		private Button _dayButton;
		
		[SerializeField]
		private TMP_Text _dayText;

		[SerializeField]
		private Image _currentDateHighlightImage;

		[SerializeField]
		private Image _trainingLogoImage;

		[SerializeField]
		private Image _subTrainingLogoImage;

		[Space(20)]
		[SerializeField]
		private TrainingButtonPresetConfig _trainingButtonPresetConfig;

		public event Action<CalendarDayElement> DestroyEvent;

		public void Initialize(
			DayData dayData, 
			Sprite trainingLogo, 
			Color trainingLogoColor, 
			Sprite subTrainingLogo, 
			Color subTrainingLogoColor, 
			Action<DayData> daySelectCallback = null)
		{
			_dayText.text = dayData.DateTime.Day.ToString();

			InitTrainingLogoImage(dayData, trainingLogo, trainingLogoColor);
			InitSubTrainingLogoImage(subTrainingLogo, subTrainingLogoColor);
			SetBackgroundColor(dayData);

			_dayButton.onClick.RemoveAllListeners();
			_dayButton.onClick.AddListener(() => daySelectCallback?.Invoke(dayData));

			StartCoroutine(InitCurrentDateHighlightImageOnNextFrame(dayData));
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		private IEnumerator InitCurrentDateHighlightImageOnNextFrame(DayData dayData)
		{
			if (dayData.DateTime.Year == DateTime.Today.Year
			    && dayData.DateTime.Month == DateTime.Today.Month
			    && dayData.DateTime.Day == DateTime.Today.Day)
			{
				yield return null;
				
				_currentDateHighlightImage.gameObject.SetActive(true);
				RectTransform rectTransform = _currentDateHighlightImage.rectTransform;
				rectTransform.SetSizeWithCurrentAnchors(
					RectTransform.Axis.Horizontal,
					CURRENT_DATE_DEFAULT_WIDTH + _dayText.GetRenderedValues().x);
			}
			else
			{
				_currentDateHighlightImage.gameObject.SetActive(false);
			}
		}

		private void InitTrainingLogoImage(
			DayData dayData, 
			Sprite trainingLogo, 
			Color trainingLogoColor)
		{
			if (trainingLogo)
			{
				_trainingLogoImage.sprite = trainingLogo;
				trainingLogoColor.a = dayData.IsTrainingCompleted ? 1f : 0.5f;
				_trainingLogoImage.color = trainingLogoColor;
			}
			else
			{
				_trainingLogoImage.gameObject.SetActive(false);
			}
		}

		private void InitSubTrainingLogoImage(
			Sprite subTrainingLogo, 
			Color subTrainingLogoColor)
		{
			if (subTrainingLogo)
			{
				_subTrainingLogoImage.sprite = subTrainingLogo;
				_subTrainingLogoImage.color = subTrainingLogoColor;
			}
			else
			{
				_subTrainingLogoImage.gameObject.SetActive(false);
			}
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
			else if (dayData.DateTime.Date >= DateTime.Today.Date 
			         || dayData.ExerciseGuid == UIConstants.REST_FROM_TRAINING 
			         || dayData.ExerciseGuid == "")
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