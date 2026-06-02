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

		[SerializeField]
		private GameObject _dayWithoutTrainingView;

		public event Action<DateTime> OpenTrainingEvent;

		private DateTime _date;
		
		public void Initialize(DayType assignedDay, DateTime date, TrainingData trainingData)
		{
			_date = date;
			_assignedDayText.text = assignedDay.ToString();
			_dateText.text = date.ToString("dd.MM");
			
			if (trainingData == null)
			{
				_dayWithoutTrainingView.SetActive(true);
				return;
			}
			
			_dayWithoutTrainingView.SetActive(false);

			_trainingName.text = trainingData.Name;
			_trainingImage.sprite = ServiceLocator.Instance.Get<ImageProvider>().GetImage(trainingData.LogoSpriteIndex);
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
			OpenTrainingEvent?.Invoke(_date);
		}
	}
}