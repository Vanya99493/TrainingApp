using System;
using TMPro;
using TrainingApp.Configs.UI;
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
		private Image _backgroundImage;

		[SerializeField]
		private Image _logoImage;

		[Space(20)]
		[SerializeField]
		private ButtonPresetConfig _buttonPresetConfig;

		private string _exerciseGuid;
		
		public event Action<string> SelectDayEvent;
		public event Action<CalendarDayElement> DestroyEvent;

		private void OnEnable()
		{
			_dayButton.onClick.AddListener(OnDayButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_dayButton.onClick.RemoveListener(OnDayButtonClickedEventHandler);
		}
		
		public void Initialize(DayData dayData, Sprite exerciseLogo)
		{
			_exerciseGuid = dayData.ExerciseGuid;
			
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
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
		
		private void OnDayButtonClickedEventHandler()
		{
			SelectDayEvent?.Invoke(_exerciseGuid);
		}
		
		private void SetBackgroundColor(DayData dayData)
		{
			Color backgroundColor;

			if (dayData.IsTrainingComplete)
			{
				backgroundColor = _buttonPresetConfig.CompletedTrainingColor;
			}
			else if (dayData.IsTrainingHalfCompletedByActivities)
			{
				backgroundColor = _buttonPresetConfig.HalfCompletedTrainingColor;
			}
			else if (dayData.DateTime.Date >= DateTime.Today.Date)
			{
				backgroundColor = _buttonPresetConfig.InactiveColor;
			}
			else
			{
				backgroundColor = _buttonPresetConfig.MissedTrainingColor;
			}

			_backgroundImage.color = backgroundColor;
		}
	}
}