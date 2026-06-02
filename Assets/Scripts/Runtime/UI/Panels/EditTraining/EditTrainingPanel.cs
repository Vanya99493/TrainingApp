using System.Collections.Generic;
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
		private ValidatableInputFieldElement _nameInputField;

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
		private TrainingData _trainingData;
		private int _logoIndex;
		
		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_imageProvider = ServiceLocator.Instance.Get<ImageProvider>();
		}

		public void Open(TrainingData trainingData)
		{
			base.Open();
			
			_trainingData = trainingData;
			_logoIndex = trainingData.LogoSpriteIndex;
			
			_nameInputField.Initialize(_trainingData.Name);
			_logoImage.sprite = _imageProvider.GetImage(_trainingData.LogoSpriteIndex);
			
			foreach (var exerciseData in _trainingData.Exercises)
			{
				var exerciseElement = _editExerciseElementFactory.InstantiateElement();
				exerciseElement.Initialize(exerciseData);
				_editExerciseElements.Add(exerciseElement);
			}

			_addExerciseButton.transform.SetParent(_temporaryAddExerciseButtonContainer);
			_addExerciseButton.transform.SetParent(_exercisesContainer);
		}

		public override void Close()
		{
			base.Close();

			ClearExerciseElements();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_selectLogoSubPanel.SelectLogoEvent += OnSelectLogoEventHandler;
			
			_backButton.onClick.AddListener(OnBackButtonClickedEventHandler);
			_selectLogoButton.onClick.AddListener(OnSelectLogoButtonClickedEventHandler);
			_addExerciseButton.onClick.AddListener(OnAddExerciseButtonClickedEventHandler);
			_saveButton.onClick.AddListener(OnSaveButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_selectLogoSubPanel.SelectLogoEvent -= OnSelectLogoEventHandler;
			
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
			_selectLogoSubPanel.Open(_imageProvider.GetImages(), _logoIndex);
		}

		private void OnAddExerciseButtonClickedEventHandler()
		{
			_addExerciseButton.transform.SetParent(_temporaryAddExerciseButtonContainer);
			
			var exerciseElement = _editExerciseElementFactory.InstantiateElement();
			exerciseElement.Initialize(new ExerciseData());
			_editExerciseElements.Add(exerciseElement);
			
			_addExerciseButton.transform.SetParent(_exercisesContainer);
		}

		private void OnSelectLogoEventHandler(int logoIndex)
		{
			_logoIndex = logoIndex;
			_logoImage.sprite = _imageProvider.GetImage(_logoIndex);
		}

		private void OnSaveButtonClickedEventHandler()
		{
			List<ExerciseData> exercises = new();
			bool isValidationSuccessful = _nameInputField.Validate() && _editExerciseElements.Count > 0;
			
			foreach (var editExerciseElement in _editExerciseElements)
			{
				bool isValidationPassed = editExerciseElement.ValidateData(out ExerciseData exerciseData);
				exercises.Add(exerciseData);

				if (!isValidationPassed)
				{
					isValidationSuccessful = false;
				}
			}

			if (isValidationSuccessful)
			{
				_trainingData.Name = _nameInputField.Text;
				_trainingData.LogoSpriteIndex = _logoIndex;
				_trainingData.Exercises = exercises;
				
				_accountController.AddOrUpdateTrainingData(_trainingData);
			}
			
			_uiManager.OpenInstead<TrainingPlansPanel>(this);
		}

		private void ClearExerciseElements()
		{
			foreach (var editExerciseElement in _editExerciseElements)
			{
				editExerciseElement.Destroy();
			}
			_editExerciseElements.Clear();
		}
	}
}