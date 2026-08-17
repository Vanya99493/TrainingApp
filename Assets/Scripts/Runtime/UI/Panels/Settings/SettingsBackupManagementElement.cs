using System;
using TMPro;
using TrainingApp.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI.Panels
{
	public class SettingsBackupManagementElement : MonoBehaviour
	{
		[SerializeField]
		private Button _exportButton;

		[SerializeField]
		private Button _importButton;

		[Space(10)]
		[SerializeField]
		private TMP_Text _notificationText;

		public event Action ExportDataEvent;
		public event Action ImportDataEvent;

		private void OnEnable()
		{
			_exportButton.onClick.AddListener(OnExportButtonClickedEventHandler);
			_importButton.onClick.AddListener(OnImportButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_exportButton.onClick.RemoveListener(OnExportButtonClickedEventHandler);
			_importButton.onClick.RemoveListener(OnImportButtonClickedEventHandler);
		}
		
		public void Initialize(DateTime lastBackupDate)
		{
			_notificationText.text = lastBackupDate > DateTime.Now
				? UIConstants.SettingsPanelConstants.NO_BACKUP_MESSAGE
				: string.Format(UIConstants.SettingsPanelConstants.LAST_BACKUP_DATE_MESSAGE_WITH_FORMAT, lastBackupDate);
		}

		public void SetNotificationText(string notification)
		{
			_notificationText.text = notification;
		}
		
		private void OnExportButtonClickedEventHandler()
		{
			ExportDataEvent?.Invoke();
		}
		
		private void OnImportButtonClickedEventHandler()
		{
			ImportDataEvent?.Invoke();
		}
	}
}