using System;
using TMPro;
using TrainingApp.Configs.UI;
using TrainingApp.Extensions;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class SubTrainingElement : MonoBehaviour
	{
		[SerializeField]
		private Button _subTrainingButton;

		[SerializeField]
		private TMP_Text _subTrainingName;

		[SerializeField]
		private Image _subTrainingLogoImage;
		 
		[Header("Configs")]
		[SerializeField]
		private TrainingButtonPresetConfig _trainingButtonPresetConfig;

		private ImageProvider _imageProvider;
		private DateTime _date;
			
		public event Action<DateTime> OpenSubTrainingEvent;

		private void OnEnable()
		{
			_subTrainingButton.onClick.AddListener(OnSubTrainingButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_subTrainingButton.onClick.RemoveListener(OnSubTrainingButtonClickedEventHandler);
		}

		public void Initialize(DateTime date, SubTrainingData subTrainingData, bool isSubTrainingCompleted)
		{
			_imageProvider ??= ServiceLocator.Instance.Get<ImageProvider>();

			_date = date;

			if (subTrainingData == null)
			{
				_subTrainingName.text = "No sub-training selected";
				_subTrainingLogoImage.sprite = _imageProvider.GetRestImage();
			}
			else
			{
				_subTrainingName.text = subTrainingData.Name;
				_subTrainingLogoImage.sprite = _imageProvider.GetImage(subTrainingData.LogoSpriteIndex);
			}
			
			_subTrainingButton.SetNormalColor(isSubTrainingCompleted ? _trainingButtonPresetConfig.CompletedTrainingColor : _trainingButtonPresetConfig.InactiveColor);
		}

		private void OnSubTrainingButtonClickedEventHandler()
		{
			OpenSubTrainingEvent?.Invoke(_date);
		}
	}
}