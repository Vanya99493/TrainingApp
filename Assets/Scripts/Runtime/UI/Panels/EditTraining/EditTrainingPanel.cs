using System.Collections.Generic;
using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.ImageProviderScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class EditTrainingPanel : BasePanel
	{
		[SerializeField]
		private SelectLogoSubPanel _selectLogoSubPanel;
		
		[SerializeField]
		private EditExerciseElementFactory _editExerciseElementFactory;
		
		[Space(10)]
		[SerializeField]
		private Button _backButton;

		[SerializeField]
		private Button _selectLogoButton;
		
		[SerializeField]
		private Image _logoImage;

		[SerializeField]
		private TMP_InputField _nameInputField;

		[SerializeField]
		private Button _saveButton;

		[Space(10)]
		[SerializeField]
		private Button _addExerciseButton;

		[SerializeField]
		private Transform _exercisesContainer;

		[SerializeField]
		private Transform _temporaryAddExerciseButtonContainer;

		private AccountController _accountController;
		private ImageProvider _imageProvider;

		private List<EditExerciseElement> _editExerciseElements = new();
		
		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_imageProvider = ServiceLocator.Instance.Get<ImageProvider>();
		}

		public void Open(TrainingData trainingData)
		{
			base.Open();
			
			_nameInputField.text = trainingData.Name;
			_logoImage.sprite = _imageProvider.GetImage(trainingData.LogoSpriteIndex);
			
			foreach (var exerciseData in trainingData.Exercises)
			{
				var exerciseElement = _editExerciseElementFactory.InstantiateElement();
				exerciseElement.Initialize(exerciseData);
				_editExerciseElements.Add(exerciseElement);
			}

			_selectLogoSubPanel.SelectLogoEvent += OnSelectLogoEventHandler;
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_backButton.onClick.AddListener(OnBackButtonClickedEventHandler);
			_selectLogoButton.onClick.AddListener(OnSelectLogoButtonClickedEventHandler);
			_addExerciseButton.onClick.AddListener(OnAddExerciseButtonClickedEventHandler);
			_saveButton.onClick.AddListener(OnSaveButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_backButton.onClick.RemoveListener(OnBackButtonClickedEventHandler);
			_selectLogoButton.onClick.RemoveListener(OnSelectLogoButtonClickedEventHandler);
			_addExerciseButton.onClick.RemoveListener(OnAddExerciseButtonClickedEventHandler);
			_saveButton.onClick.AddListener(OnSaveButtonClickedEventHandler);
		}

		private void OnBackButtonClickedEventHandler()
		{
			_uiManager.OpenInstead<TrainingPlansPanel>(this);
		}

		private void OnSelectLogoButtonClickedEventHandler()
		{
			_selectLogoSubPanel.Open(_imageProvider.GetImages());
		}

		private void OnAddExerciseButtonClickedEventHandler()
		{
			_addExerciseButton.transform.SetParent(_temporaryAddExerciseButtonContainer);
			
			var exerciseElement = _editExerciseElementFactory.InstantiateElement();
			exerciseElement.Initialize(new ExerciseData());
			_editExerciseElements.Add(exerciseElement);
			
			_addExerciseButton.transform.SetParent(_exercisesContainer);
		}

		private void OnSaveButtonClickedEventHandler()
		{
			
		}

		private void OnSelectLogoEventHandler(int logoIndex)
		{
			_logoImage.sprite = _imageProvider.GetImage(logoIndex);
		}
	}
}