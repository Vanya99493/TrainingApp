using System;
using TMPro;
using TrainingApp.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class SubTrainingButtonElement : MonoBehaviour, IDestroyableElement<SubTrainingButtonElement>
	{
		[SerializeField]
		private ToggleButton _subTrainingToggleButton;

		[SerializeField]
		private Image _logoImage;

		[SerializeField]
		private TMP_Text _subTrainingNameText;

		[SerializeField]
		private TMP_Text _subTrainingDescriptionText;

		private SubTrainingData _subTrainingData;

		public event Action<SubTrainingData, bool> SelectSubTrainingEvent;
		public event Action<SubTrainingButtonElement> DestroyEvent;

		public ToggleButton ToggleButton => _subTrainingToggleButton;

		private void OnEnable()
		{
			_subTrainingToggleButton.ToggleButtonStateChangedEvent += OnSubTrainingToggleStateChangedEventHandler;
		}

		private void OnDisable()
		{
			_subTrainingToggleButton.ToggleButtonStateChangedEvent -= OnSubTrainingToggleStateChangedEventHandler;
		}

		public void Initialize(SubTrainingData subTrainingData, Sprite logo)
		{
			_subTrainingData = subTrainingData;
			
			_logoImage.sprite = logo;
			_subTrainingNameText.text = _subTrainingData.Name;
			_subTrainingDescriptionText.text = _subTrainingData.Description;
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		private void OnSubTrainingToggleStateChangedEventHandler(bool isActive)
		{
			SelectSubTrainingEvent?.Invoke(_subTrainingData, isActive);
		}
	}
}