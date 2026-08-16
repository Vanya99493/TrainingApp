using TrainingApp.SessionScripts;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI.Panels
{
	public class SettingsPanel : MonoBehaviour
	{
		[SerializeField]
		private ClientDataBackupService _clientDataBackupService;

		[Header("UI Elements")]
		[SerializeField]
		private Button _closeButton;
		
		[SerializeField]
		private Button _exportButton;

		[SerializeField]
		private Button _importButton;

		private void OnEnable()
		{
			_closeButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
			
			_exportButton.onClick.AddListener(OnExportButtonClickedEventHandler);
			_importButton.onClick.AddListener(OnImportButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_closeButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);

			_exportButton.onClick.RemoveListener(OnExportButtonClickedEventHandler);
			_importButton.onClick.RemoveListener(OnImportButtonClickedEventHandler);
		}

		private void OnCloseButtonClickedEventHandler()
		{
			gameObject.SetActive(false);
		}
		
		private void OnExportButtonClickedEventHandler()
		{
			_clientDataBackupService.Export();
		}
		
		private void OnImportButtonClickedEventHandler()
		{
			_clientDataBackupService.Import();
		}
	}
}