using System;
using System.Collections.Generic;
using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.Constants;
using TrainingApp.Enums;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class CalendarPanel : BasePanel
	{
		[SerializeField]
		private CalendarRowElementFactory _calendarRowElementFactory;

		[Space(10)]
		[SerializeField]
		private Button _previousButton;

		[SerializeField]
		private Button _nextButton;

		[SerializeField]
		private TMP_Text _yearText;

		[SerializeField]
		private TMP_Text _monthText;

		private AccountController _accountController;
		private ImageProvider _imageProvider;

		private List<CalendarRowElement> _calendarRowElements = new();
		private DateTime _currentSetDate;

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_imageProvider = ServiceLocator.Instance.Get<ImageProvider>();
		}

		public override void Open(PanelArgs args)
		{
			base.Open(args);

			SetCalendar(DateTime.Today);
		}

		public override void Close()
		{
			base.Close();

			ClearCalendar();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_previousButton.onClick.AddListener(OnPreviousButtonClickedEventHandler);
			_nextButton.onClick.AddListener(OnNextButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_previousButton.onClick.RemoveListener(OnPreviousButtonClickedEventHandler);
			_nextButton.onClick.RemoveListener(OnNextButtonClickedEventHandler);
		}

		private void OnPreviousButtonClickedEventHandler()
		{
			SetCalendar(_currentSetDate.AddMonths(-1));
		}

		private void OnNextButtonClickedEventHandler()
		{
			SetCalendar(_currentSetDate.AddMonths(1));
		}

		private void SetCalendar(DateTime date)
		{
			ClearCalendar();
			
			_currentSetDate = date;

			_yearText.text = date.Year.ToString();
			_monthText.text = ((Months)date.Month).ToString();

			List<List<DateTime>> weeks = DivideByWeeks(date);
			List<List<CalendarDayInitializationData>> weeksData = ConvertToDayData(weeks);

			int index = 0;
			foreach (var weekData in weeksData)
			{
				WeekType weekType = WeekType.Common;

				if (index == 0)
				{
					weekType = WeekType.First;
				}

				if (weeksData.Count - (index + 1) == 0)
				{
					weekType = WeekType.Last;
				}

				var calendarRowElement = _calendarRowElementFactory.InstantiateElement();
				calendarRowElement.Initialize(weekData, weekType, OnDayElementButtonClickedEventHandler);
				_calendarRowElements.Add(calendarRowElement);

				index++;
			}
		}

		private List<List<DateTime>> DivideByWeeks(DateTime date)
		{
			var result = new List<List<DateTime>>();

			date = date.Date;

			DateTime monthStart = new DateTime(date.Year, date.Month, 1);
			DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);

			DateTime current = monthStart;

			while (current <= monthEnd)
			{
				var week = new List<DateTime>();

				do
				{
					if (current > monthEnd)
					{
						break;
					}
					
					week.Add(current);
					current = current.AddDays(1);
				} while (current.DayOfWeek != DayOfWeek.Monday);

				result.Add(week);
			}

			return result;
		}

		private List<List<CalendarDayInitializationData>> ConvertToDayData(List<List<DateTime>> weeks)
		{
			List<List<CalendarDayInitializationData>> weeksData = new();

			foreach (var week in weeks)
			{
				List<CalendarDayInitializationData> weekData = new();

				foreach (var day in week)
				{
					DayData dayData = _accountController.GetDayData(day);
					Sprite trainingLogoSprite;
					Color trainingLogoColor = Color.white;
					Sprite subTrainingLogoSprite;
					Color subTrainingLogoColor = Color.white;

					switch (dayData.ExerciseGuid)
					{
						case UIConstants.REST_FROM_TRAINING:
							trainingLogoSprite = _imageProvider.GetRestImage();
							
							break;
						case "":
							trainingLogoSprite = null;
							break;
						default:
							TrainingData trainingData = _accountController.GetTraining(dayData.ExerciseGuid);
							trainingLogoSprite = _imageProvider.GetImage(trainingData.LogoSpriteIndex);
							trainingLogoColor = CollectionsContainer.RGBColors[(ColorType)trainingData.LogoColorIndex];
							break;
					}
					
					switch (dayData.SubTrainingGuid)
					{
						case "":
							subTrainingLogoSprite = null;
							break;
						default:
							SubTrainingData subTrainingData = _accountController.GetSubTraining(dayData.SubTrainingGuid);
							subTrainingLogoSprite = _imageProvider.GetImage(subTrainingData.LogoSpriteIndex);
							subTrainingLogoColor = CollectionsContainer.RGBColors[(ColorType)subTrainingData.LogoColorIndex];
							break;
					}

					weekData.Add(new CalendarDayInitializationData(dayData, trainingLogoSprite, trainingLogoColor, subTrainingLogoSprite, subTrainingLogoColor));
				}

				weeksData.Add(weekData);
			}

			return weeksData;
		}

		private void ClearCalendar()
		{
			foreach (var calendarRowElement in _calendarRowElements)
			{
				calendarRowElement.Destroy();
			}
			_calendarRowElements.Clear();
		}

		private void OnDayElementButtonClickedEventHandler(DayData selectedDayData)
		{
			_uiManager.CloseCalendarPanel();
			_uiManager.OpenDayOverviewPanel(selectedDayData);
		}
	}
}