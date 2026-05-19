using System;
using TMPro;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class TrainingPlanElement : MonoBehaviour, IDestroyableElement<TrainingPlanElement>
	{
		[SerializeField]
		private Button _openTrainingPlanButton;

		[SerializeField]
		private TMP_Text _trainingPlanNameText;

		[SerializeField]
		private Image _trainingPlanImage;

		private TrainingData _trainingData;

		public event Action<TrainingData> OpenTrainingPlanEvent;
		public event Action<TrainingPlanElement> DestroyEvent;

		private void OnEnable()
		{
			_openTrainingPlanButton.onClick.AddListener(OnOpenTrainingPlanButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_openTrainingPlanButton.onClick.RemoveListener(OnOpenTrainingPlanButtonClickedEventHandler);
		}

		public void Initialize(TrainingData trainingData)
		{
			_trainingData = trainingData;

			_trainingPlanNameText.text = _trainingData.Name;
			_trainingPlanImage.sprite = ServiceLocator.Instance.Get<ImageProvider>().GetImage(_trainingData.ImageIndex);
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		private void OnOpenTrainingPlanButtonClickedEventHandler()
		{
			OpenTrainingPlanEvent?.Invoke(_trainingData);
		}
	}
}