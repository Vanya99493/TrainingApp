using System;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class HarmfulActivitiesElement : MonoBehaviour
	{
		[SerializeField]
		private ToggleButton _drinkingToggleButton;

		public event Action<HarmfulActivitiesData> DrinkingStateChangedEvent;

		private void OnEnable()
		{
			_drinkingToggleButton.ToggleButtonStateChangedEvent += OnDrinkingToggleStateChanged;
		}

		private void OnDestroy()
		{
			_drinkingToggleButton.ToggleButtonStateChangedEvent -= OnDrinkingToggleStateChanged;
		}

		public void Initialize(bool isDrinkingToday)
		{
			_drinkingToggleButton.Initialize(isDrinkingToday);
		}

		private void OnDrinkingToggleStateChanged(bool isActive)
		{
			DrinkingStateChangedEvent?.Invoke(new HarmfulActivitiesData(isActive));
		}
	}
}