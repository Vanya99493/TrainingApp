using System;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class TeethBrushingElement : MonoBehaviour
	{
		[SerializeField]
		private ToggleButton _morningTeethBrushingToggleButton;

		[SerializeField]
		private ToggleButton _eveningTeethBrushingToggleButton;
		
		public event Action<TeethData> TeethBrushingStateChangedEvent;

		private void OnEnable()
		{
			_morningTeethBrushingToggleButton.ToggleButtonStateChangedEvent += OnTeethBrushingToggleStateChanged;
			_eveningTeethBrushingToggleButton.ToggleButtonStateChangedEvent += OnTeethBrushingToggleStateChanged;
		}

		private void OnDestroy()
		{
			_morningTeethBrushingToggleButton.ToggleButtonStateChangedEvent -= OnTeethBrushingToggleStateChanged;
			_eveningTeethBrushingToggleButton.ToggleButtonStateChangedEvent -= OnTeethBrushingToggleStateChanged;
		}

		public void Initialize(TeethData teethData)
		{
			_morningTeethBrushingToggleButton.Initialize(teethData.IsBrushInTheMorning);
			_eveningTeethBrushingToggleButton.Initialize(teethData.IsBrushInTheEvening);
		}

		private void OnTeethBrushingToggleStateChanged(bool isActive)
		{
			TeethBrushingStateChangedEvent?.Invoke(new TeethData(_morningTeethBrushingToggleButton.IsActive, _eveningTeethBrushingToggleButton.IsActive));
		}
	}
}