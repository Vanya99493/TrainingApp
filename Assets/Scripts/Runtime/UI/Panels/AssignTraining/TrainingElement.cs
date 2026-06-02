using System;
using TMPro;
using TrainingApp.Models;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class TrainingElement : MonoBehaviour, IDestroyableElement<TrainingElement>
	{
		[SerializeField]
		private Button _trainingButton;

		[SerializeField]
		private TMP_Text _trainingName;

		[SerializeField]
		private Image _trainingLogoImage;

		private string _trainingGuid = "";

		public event Action<string> SelectTrainingEvent;
		public event Action<TrainingElement> DestroyEvent;

		private void OnEnable()
		{
			_trainingButton.onClick.AddListener(OnTrainingButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_trainingButton.onClick.RemoveListener(OnTrainingButtonClickedEventHandler);
		}

		public void Initialize(TrainingData trainingData, Sprite logo)
		{
			_trainingGuid = trainingData.Guid;

			_trainingName.text = trainingData.Name;
			_trainingLogoImage.sprite = logo;
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		private void OnTrainingButtonClickedEventHandler()
		{
			SelectTrainingEvent?.Invoke(_trainingGuid);
		}
	}
}