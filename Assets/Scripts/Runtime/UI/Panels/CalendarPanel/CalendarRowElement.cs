using System;
using System.Collections.Generic;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class CalendarRowElement : MonoBehaviour, IDestroyableElement<CalendarRowElement>
	{
		[SerializeField]
		private CalendarDayElementFactory _calendarDayElementFactory;

		public event Action<CalendarRowElement> DestroyEvent;

		private List<CalendarDayElement> _calendarDayElements = new();

		public void Initialize(
			List<CalendarDayInitializationData> days, 
			WeekType weekType, 
			Action<DayData> daySelectCallback = null)
		{
			if (weekType == WeekType.First)
			{
				for (int i = 0; i < 7 - days.Count; i++)
				{
					_calendarDayElementFactory.InstantiateSpace();
				}
			}
			
			foreach (var day in days)
			{
				var calendarDayElement = _calendarDayElementFactory.InstantiateElement();
				calendarDayElement.Initialize(
					day.DayData, 
					day.TrainingLogo, 
					day.TrainingLogoColor, 
					day.SubTrainingLogo, 
					day.SubTrainingLogoColor, 
					daySelectCallback);
				_calendarDayElements.Add(calendarDayElement);
			}
			
			if (weekType == WeekType.Last)
			{
				for (int i = 0; i < 7 - days.Count; i++)
				{
					_calendarDayElementFactory.InstantiateSpace();
				}
			}
		}
		
		public void Destroy()
		{
			foreach (var calendarDayElement in _calendarDayElements)
			{
				calendarDayElement.Destroy();
			}
			_calendarDayElements.Clear();
			
			DestroyEvent?.Invoke(this);
		}
	}
}