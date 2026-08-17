using System;
using TrainingApp.AccountScripts;
using TrainingApp.Constants;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.SessionScripts;
using UnityEngine;

namespace TrainingApp.UI.Panels
{
	public class SettingsPanel : BasePanel
	{
		[SerializeField]
		private SettingsHeaderElement _settingsHeaderElement;

		[SerializeField]
		private SettingsBackupManagementElement _settingsBackupManagementElement;

		private AccountController _accountController;
		private ClientDataBackupService _clientDataBackupService;

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();

			_settingsBackupManagementElement.ExportDataEvent += OnExportBackupEventHandler;
			_settingsBackupManagementElement.ImportDataEvent += OnImportBackupEventHandler;
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_settingsBackupManagementElement.ExportDataEvent -= OnExportBackupEventHandler;
			_settingsBackupManagementElement.ImportDataEvent -= OnImportBackupEventHandler;
		}

		public override void Initialize(UIManager uiManager)
		{
			base.Initialize(uiManager);

			_accountController = ServiceLocator.Instance.Get<AccountController>();
			_clientDataBackupService = ServiceLocator.Instance.Get<ClientDataBackupService>();
		}

		public override void Open(PanelArgs args)
		{
			if (_clientDataBackupService != null)
			{
				_clientDataBackupService.ExportStarted += OnExportStartedEventHandler;
				_clientDataBackupService.ExportCompleted += OnExportCompletedEventHandler;
				_clientDataBackupService.ExportFailed += OnExportFailedEventHandler;
			
				_clientDataBackupService.ImportStarted += OnImportStartedEventHandler;
				_clientDataBackupService.ImportCompleted += OnImportCompletedEventHandler;
				_clientDataBackupService.ImportFailed += OnImportFailedEventHandler;
			}
			
			_settingsHeaderElement.Initialize(_accountController.ClientData.Name);
			_settingsBackupManagementElement.Initialize(_accountController.ClientData.BackupDate);
			
			base.Open(args);
		}

		public override void Close()
		{
			if (_clientDataBackupService != null)
			{
				_clientDataBackupService.ExportStarted -= OnExportStartedEventHandler;
				_clientDataBackupService.ExportCompleted -= OnExportCompletedEventHandler;
				_clientDataBackupService.ExportFailed -= OnExportFailedEventHandler;

				_clientDataBackupService.ImportStarted -= OnImportStartedEventHandler;
				_clientDataBackupService.ImportCompleted -= OnImportCompletedEventHandler;
				_clientDataBackupService.ImportFailed -= OnImportFailedEventHandler;
			}

			base.Close();
		}

		private void OnExportBackupEventHandler()
		{
			_clientDataBackupService.Export();
		}

		private void OnImportBackupEventHandler()
		{
			_clientDataBackupService.Import();
		}

		private void OnExportStartedEventHandler()
		{
			_settingsBackupManagementElement.SetNotificationText(UIConstants.SettingsPanelConstants.BACKUP_CREATING_MESSAGE);
		}

		private void OnExportCompletedEventHandler(string backupName)
		{
			_settingsBackupManagementElement.SetNotificationText(UIConstants.SettingsPanelConstants.BACKUP_SUCCESSFULLY_CREATED);
		}

		private void OnExportFailedEventHandler(string failedMessage)
		{
			_settingsBackupManagementElement.SetNotificationText(failedMessage);
		}
		
		private void OnImportStartedEventHandler()
		{
			_settingsBackupManagementElement.SetNotificationText(UIConstants.SettingsPanelConstants.BACKUP_IMPORTING_MESSAGE);
		}

		private void OnImportCompletedEventHandler(ClientData importedData)
		{
			_accountController.ClientData = importedData;
			_accountController.UpdateLastBackupDate(DateTime.Now);
			_settingsBackupManagementElement.SetNotificationText(UIConstants.SettingsPanelConstants.BACKUP_SUCCESSFULLY_IMPORTED);
		}

		private void OnImportFailedEventHandler(string failedMessage)
		{
			_settingsBackupManagementElement.SetNotificationText(failedMessage);
		}
	}
}