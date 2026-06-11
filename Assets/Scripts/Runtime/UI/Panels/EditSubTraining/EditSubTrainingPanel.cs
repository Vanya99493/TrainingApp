using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.Constants;
using TrainingApp.Enums;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class EditSubTrainingPanel : BasePanel
	{
		[SerializeField]
		private SelectLogoSubPanel _selectLogoSubPanel;
		
		[Space(10)]
		[SerializeField]
		private Button _backButton;

		[SerializeField]
		private Button _selectLogoButton;
		
		[SerializeField]
		private Image _logoImage;

		[SerializeField]
		private ValidatableInputFieldElement _nameInputField;

		[SerializeField]
		private TMP_InputField _descriptionInputField;
		
		[SerializeField]
		private Button _saveButton;

		private AccountController _accountController;
		private ImageProvider _imageProvider;
		
		private SubTrainingData _subTrainingData;
		private int _logoIndex;
		private int _logoColorIndex;
		
		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_imageProvider = ServiceLocator.Instance.Get<ImageProvider>();
		}

		public override void Open(PanelArgs args)
		{
			base.Open(args);
			
			_subTrainingData = (args as SubTrainingDataPanelArgs).SubTrainingData;
			_logoIndex = _subTrainingData.LogoSpriteIndex;
			_logoColorIndex = _subTrainingData.LogoColorIndex;
			
			_nameInputField.Initialize(_subTrainingData.Name);
			_logoImage.sprite = _imageProvider.GetImage(_logoIndex);
			_logoImage.color = CollectionsContainer.RGBColors[(ColorType)_logoColorIndex];
			_descriptionInputField.text = _subTrainingData.Description;
		}
		
		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_selectLogoSubPanel.SelectLogoEvent += OnSelectLogoEventHandler;
			
			_backButton.onClick.AddListener(OnBackButtonClickedEventHandler);
			_selectLogoButton.onClick.AddListener(OnSelectLogoButtonClickedEventHandler);
			_saveButton.onClick.AddListener(OnSaveButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_selectLogoSubPanel.SelectLogoEvent -= OnSelectLogoEventHandler;
			
			_backButton.onClick.RemoveListener(OnBackButtonClickedEventHandler);
			_selectLogoButton.onClick.RemoveListener(OnSelectLogoButtonClickedEventHandler);
			_saveButton.onClick.AddListener(OnSaveButtonClickedEventHandler);
		}
		
		private void OnBackButtonClickedEventHandler()
		{
			_uiManager.OpenInstead<TrainingPlansPanel>(this);
		}

		private void OnSelectLogoButtonClickedEventHandler()
		{
			_selectLogoSubPanel.Open(_imageProvider, _logoIndex, _logoColorIndex);
		}

		private void OnSelectLogoEventHandler(int logoIndex, int colorIndex)
		{
			_logoIndex = logoIndex;
			_logoColorIndex = colorIndex;
			_logoImage.sprite = _imageProvider.GetImage(_logoIndex);
			_logoImage.color = CollectionsContainer.RGBColors[(ColorType)_logoColorIndex];
		}

		private void OnSaveButtonClickedEventHandler()
		{
			if (_nameInputField.Validate())
			{
				_subTrainingData.Name = _nameInputField.Text;
				_subTrainingData.LogoSpriteIndex = _logoIndex;
				_subTrainingData.LogoColorIndex = _logoColorIndex;
				_subTrainingData.Description = _descriptionInputField.text;
				
				_accountController.AddOrUpdateSubTrainingData(_subTrainingData);
			}
			
			_uiManager.OpenInstead<TrainingPlansPanel>(this);
		}
	}
}