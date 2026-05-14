using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;
using TrainingApp.SessionScripts;
using UnityEngine;

namespace TrainingApp.AccountScripts
{
	public class AccountController : MonoBehaviour, IService
	{
		private IClientDataProvider _clientDataProvider;
		
		public void Initialize()
		{
			
		}

		public int GetStreak() => 0;

		public TrainingData GetTraining(string guid) => new();
		public TrainingData GetTodayTraining() => new();
		public TrainingData GetTomorrowsTraining() => new();
	}
}