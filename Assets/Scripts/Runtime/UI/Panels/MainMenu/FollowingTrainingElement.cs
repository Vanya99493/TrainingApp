using System;
using TMPro;
using TrainingApp.Configs.UI;
using TrainingApp.Enums;
using TrainingApp.Extensions;
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
		private Image _trainingLogoImage;

		[SerializeField]
		private Button _trainingButton;

		[Header("Configs")]
		[SerializeField]
		private TrainingButtonPresetConfig _trainingButtonPresetConfig;

		private ImageProvider _imageProvider;
		private DateTime _date;
		
		public event Action<DateTime> OpenTrainingEvent;

		public void Initialize(DayType assignedDay, DateTime date, bool isCompleted, TrainingData trainingData)
		{
			_imageProvider ??= ServiceLocator.Instance.Get<ImageProvider>();
			
			_date = date;
			_assignedDayText.text = assignedDay.ToString();
			_dateText.text = date.ToString("dd.MM");
			
			if (trainingData == null)
			{
				_trainingButton.interactable = false;
				
				_trainingName.text = "Rest";
				_trainingLogoImage.sprite = _imageProvider.GetRestImage();

				_trainingButton.SetNormalColor(_trainingButtonPresetConfig.InactiveColor);
			}
			else
			{
				_trainingButton.interactable = true;
				
				_trainingName.text = trainingData.Name;
				_trainingLogoImage.sprite = _imageProvider.GetImage(trainingData.LogoSpriteIndex);

				Color trainingButtonColor = isCompleted
					? _trainingButtonPresetConfig.CompletedTrainingColor
					: date.Date < DateTime.Today.Date
						? _trainingButtonPresetConfig.MissedTrainingColor
						: _trainingButtonPresetConfig.InactiveColor;
				_trainingButton.SetNormalColor(trainingButtonColor);
			}
		}

		private void OnEnable()
		{
			_trainingButton.onClick.AddListener(OnOpenTrainingButtonClickedEventHandler);
		}

		private void OnDestroy()
		{
			_trainingButton.onClick.RemoveListener(OnOpenTrainingButtonClickedEventHandler);
		}

		private void OnOpenTrainingButtonClickedEventHandler()
		{
			OpenTrainingEvent?.Invoke(_date);
		}
	}
}