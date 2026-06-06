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

		private string _trainingGuid;

		public event Action<string> OpenTrainingPlanEvent;
		public event Action<TrainingPlanElement> DestroyEvent;

		private void OnEnable()
		{
			_openTrainingPlanButton.onClick.AddListener(OnOpenTrainingPlanButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_openTrainingPlanButton.onClick.RemoveListener(OnOpenTrainingPlanButtonClickedEventHandler);
		}

		public void Initialize(string guid, string name, int logoIndex)
		{
			_trainingGuid = guid;

			_trainingPlanNameText.text = name;
			_trainingPlanImage.sprite = ServiceLocator.Instance.Get<ImageProvider>().GetImage(logoIndex);
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		private void OnOpenTrainingPlanButtonClickedEventHandler()
		{
			OpenTrainingPlanEvent?.Invoke(_trainingGuid);
		}
	}
}