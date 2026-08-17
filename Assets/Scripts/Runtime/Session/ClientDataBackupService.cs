using System;
using System.IO;
using Newtonsoft.Json;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using UnityEngine;

namespace TrainingApp.SessionScripts
{
	public class ClientDataBackupService : MonoBehaviour, IService
    {
        private const string CLIENT_DATA_KEY = "ClientData";

        public event Action ExportStarted;
        public event Action<string> ExportCompleted;
        public event Action ExportCancelled;
        public event Action<string> ExportFailed;
        
        public event Action ImportStarted;
        public event Action<ClientData> ImportCompleted;
        public event Action ImportCancelled;
        public event Action<string> ImportFailed;

        private string _temporaryFilePath;

        public void Export()
        {
            if (NativeFilePicker.IsFilePickerBusy())
            {
                return;
            }

            string json = PlayerPrefs.GetString(
                CLIENT_DATA_KEY,
                string.Empty);

            if (string.IsNullOrEmpty(json))
            {
                ExportFailed?.Invoke("ClientData was not found in PlayerPrefs.");
                return;
            }

            if (!TryCreateTemporaryFile(json, out string filePath))
            {
                ExportFailed?.Invoke("Failed to create temporary backup file.");
                return;
            }

            _temporaryFilePath = filePath;

            ExportStarted?.Invoke();

            NativeFilePicker.ExportFile(
                _temporaryFilePath,
                OnExportFinished);
        }
        
        public void Import()
        {
            if (NativeFilePicker.IsFilePickerBusy())
                return;

            ImportStarted?.Invoke();

            NativeFilePicker.PickFile(
                OnImportFileSelected,
                "application/json");
        }

        private bool TryCreateTemporaryFile(
            string json,
            out string filePath)
        {
            string fileName = CreateBackupFileName();

            filePath = Path.Combine(
                Application.temporaryCachePath,
                fileName);

            try
            {
                File.WriteAllText(
                    filePath,
                    json);

                return true;
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                filePath = null;

                return false;
            }
        }

        private void OnExportFinished(bool success)
        {
            if (success)
            {
                ExportCompleted?.Invoke(Path.GetFileName(_temporaryFilePath));
            }
            else
            {
                ExportCancelled?.Invoke();
            }

            DeleteTemporaryFile();
        }

        private void DeleteTemporaryFile()
        {
            if (string.IsNullOrEmpty(_temporaryFilePath))
            {
                return;
            }

            try
            {
                if (File.Exists(_temporaryFilePath))
                {
                    File.Delete(_temporaryFilePath);
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Failed to delete temporary backup file: {exception}");
            }

            _temporaryFilePath = null;
        }
        
        private void OnImportFileSelected(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                ImportCancelled?.Invoke();
                return;
            }

            string json;

            try
            {
                json = File.ReadAllText(path);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);

                ImportFailed?.Invoke("Failed to read the selected file.");

                return;
            }

            if (string.IsNullOrWhiteSpace(json))
            {
                ImportFailed?.Invoke("The selected file is empty.");

                return;
            }

            if (!TryDeserializeClientData(
                    json,
                    out ClientData clientData))
            {
                ImportFailed?.Invoke(
                    "The selected file does not contain valid ClientData.");

                return;
            }
            
            PlayerPrefs.SetString(
                CLIENT_DATA_KEY,
                json);
            PlayerPrefs.Save();

            ImportCompleted?.Invoke(clientData);
        }

        private bool TryDeserializeClientData(
            string json,
            out ClientData clientData)
        {
            clientData = null;

            try
            {
                clientData = JsonConvert.DeserializeObject<ClientData>(json);
                return clientData != null;
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return false;
            }
        }


        private static string CreateBackupFileName()
        {
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

            return $"CD_{timestamp}.json";
        }

        private void OnDestroy()
        {
            DeleteTemporaryFile();
        }
	}
}