using TMPro;
using TrainingApp.AccountScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class EditTrainingPanel : BasePanel
	{
		[SerializeField]
		private EditExerciseElementFactory _editExerciseElementFactory;
		
		[SerializeField]
		private Button _backButton;

		[SerializeField]
		private Button _editLogoButton;

		[SerializeField]
		private TMP_InputField _nameInputField;

		private AccountController _accountController;
		
		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
		}

		public void Open(TrainingData trainingData)
		{
			base.Open();
			
			
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			
		}
	}
}