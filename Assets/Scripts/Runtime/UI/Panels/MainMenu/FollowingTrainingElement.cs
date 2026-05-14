using System;
using TMPro;
using TrainingApp.Enums;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class FollowingTrainingElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _assignedDayText;

		[SerializeField]
		private TMP_Text _dateText;

		[SerializeField]
		private TMP_Text _trainingName;

		[SerializeField]
		private Image _trainingImage;

		[SerializeField]
		private Button _openTrainingButton;

		public event Action<string> OpenTrainingEvent;

		private string _guid;
		
		public void Initialize(DayType assignedDay, DateTime date, TrainingData trainingData)
		{
			_guid = trainingData.Guid;
			
			_assignedDayText.text = assignedDay.ToString();
			_dateText.text = date.ToString("dd.MM");
			_trainingName.text = trainingData.Name;
			_trainingImage.sprite = ServiceLocator.Instance.Get<ImageProvider>().GetImage(trainingData.ImageIndex);
		}

		private void OnEnable()
		{
			_openTrainingButton.onClick.AddListener(OnOpenTrainingButtonClickedEventHandler);
		}

		private void OnDestroy()
		{
			_openTrainingButton.onClick.RemoveListener(OnOpenTrainingButtonClickedEventHandler);
		}

		private void OnOpenTrainingButtonClickedEventHandler()
		{
			OpenTrainingEvent?.Invoke(_guid);
		}
	}
}