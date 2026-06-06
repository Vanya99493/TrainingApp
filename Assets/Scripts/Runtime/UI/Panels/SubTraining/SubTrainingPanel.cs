using System.Collections.Generic;
using TrainingApp.AccountScripts;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class SubTrainingPanel : BasePanel
	{
		[SerializeField]
		private SubTrainingButtonElementFactory _subTrainingButtonElementFactory;

		[Space(10)]
		[SerializeField]
		private Button _closeButton;

		[SerializeField]
		private ToggleButtonGroup _toggleButtonGroup;

		private AccountController _accountController;
		private ImageProvider _imageProvider;
	
		private DayData _dayData;
		private string _selectedSubTrainingGuid;
		private List<SubTrainingButtonElement> _subTrainingButtonElements = new();

		public override void Initialize(UIManager uiManager)
		{
			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_imageProvider = ServiceLocator.Instance.Get<ImageProvider>();
			
			base.Initialize(uiManager);
		}

		public override void Open(PanelArgs args)
		{
			ClearSubTrainingButtonElements();

			_dayData = (args as DayDataPanelArgs).DayData;

			List<SubTrainingData> subTrainingsData = _accountController.ClientData.TrainingsDataContainer.SubTrainings;
			int index = 0;
			int activeIndex = -1;
			
			_selectedSubTrainingGuid = _dayData.SubTrainingGuid;

			foreach (var subTrainingData in subTrainingsData)
			{
				var subTrainingButtonElement = _subTrainingButtonElementFactory.InstantiateElement();
				subTrainingButtonElement.Initialize(subTrainingData, _imageProvider.GetImage(subTrainingData.LogoSpriteIndex));
				subTrainingButtonElement.SelectSubTrainingEvent += OnSelectSubTrainingEventHandler;
				_toggleButtonGroup.AddToggleButton(subTrainingButtonElement.ToggleButton);
				_subTrainingButtonElements.Add(subTrainingButtonElement);

				if (_dayData.SubTrainingGuid == subTrainingData.Guid)
				{
					activeIndex = index;
				}

				index++;
			}
			
			_toggleButtonGroup.Initialize(activeIndex);
			
			base.Open(args);
		}

		public override void Close()
		{
			base.Close();
			
			ClearSubTrainingButtonElements();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_closeButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_closeButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);
		}

		private void ClearSubTrainingButtonElements()
		{
			foreach (var subTrainingButtonElement in _subTrainingButtonElements)
			{
				subTrainingButtonElement.Destroy();
			}
			_subTrainingButtonElements.Clear();
			_toggleButtonGroup.ClearToggleButtons();
		}

		private void OnCloseButtonClickedEventHandler()
		{
			_uiManager.OpenLastClosedPanelInstead(this, new DayDataPanelArgs(_dayData));
		}

		private void OnSelectSubTrainingEventHandler(SubTrainingData subTrainingData, bool isActive)
		{
			if (isActive)
			{
				_selectedSubTrainingGuid = subTrainingData.Guid;
				_dayData.SubTrainingGuid = _selectedSubTrainingGuid;
				_accountController.AddOrUpdateDayData(_dayData);
			}
			else if(subTrainingData.Guid == _selectedSubTrainingGuid)
			{
				_selectedSubTrainingGuid = "";
				_dayData.SubTrainingGuid = _selectedSubTrainingGuid;
				_accountController.AddOrUpdateDayData(_dayData);
			}
		}
	}
}