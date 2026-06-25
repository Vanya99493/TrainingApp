using TrainingApp.AccountScripts;
using TrainingApp.ServiceScripts;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.UI;
using UnityEngine;

namespace TrainingApp.Entry
{
	public class EntryPoint : MonoBehaviour
	{
		[SerializeField]
		private UIManager _uiManager;
		
		[Header("Services")]
		[SerializeField]
		private AccountController _accountController;

		[SerializeField]
		private ImageProvider _imageProvider;

		[SerializeField]
		private ColorProvider _colorProvider;

		private void Awake()
		{
			Initialize();
		}

		private void Initialize()
		{
			ServiceLocator.Instance.Register(_accountController);
			ServiceLocator.Instance.Register(_imageProvider);
			ServiceLocator.Instance.Register(_colorProvider);
			
			_accountController.Initialize();
			_uiManager.Initialize();
			
			_uiManager.OpenMainMenuPanel();
		}
	}
}