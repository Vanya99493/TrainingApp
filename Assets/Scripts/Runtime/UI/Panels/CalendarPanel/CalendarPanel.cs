using System;
using System.Collections.Generic;
using TMPro;
using TrainingApp.Enums;
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

		private List<CalendarRowElement> _calendarRowElements = new();
		private DateTime _currentSetDate;

		public override void Open()
		{
			base.Open();
			
			SetCalendar(DateTime.Today);
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
			_currentSetDate = date;

			_yearText.text = date.Year.ToString();
			_monthText.text = ((Months)date.Month).ToString();

			List<List<DateTime>> weeks = DivideByWeeks(date);
			
			
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
					week.Add(current);
					current = current.AddDays(1);
				} while (current.DayOfWeek != DayOfWeek.Monday);

				result.Add(week);
			}

			return result;
		}
	}
}