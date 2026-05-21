using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrainingApp.UI
{
	public class CalendarRowElement : MonoBehaviour, IDestroyableElement<CalendarRowElement>
	{
		public event Action<CalendarRowElement> DestroyEvent;

		public void Initialize(List<DateTime> days)
		{
			
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
	}
}