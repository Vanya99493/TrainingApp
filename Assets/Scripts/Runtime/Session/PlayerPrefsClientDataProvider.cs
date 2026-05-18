using Newtonsoft.Json;
using UnityEngine;

namespace TrainingApp.SessionScripts
{
	public class PlayerPrefsClientDataProvider : IClientDataProvider
	{
		private const string CLIENT_DATA_KEY = "ClientData";

		public T LoadData<T>()
		{
			string data = PlayerPrefs.GetString(CLIENT_DATA_KEY);
			T result = JsonConvert.DeserializeObject<T>(data);
			return result;
		}
		
		public void SaveData<T>(T data)
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			string jsonData = JsonConvert.SerializeObject(data, jsonSerializerSettings);
			PlayerPrefs.SetString(CLIENT_DATA_KEY, jsonData);
		}
	}
}