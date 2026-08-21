using TrainingApp.AccountScripts;
using UnityEngine;

namespace TrainingApp.UI
{
	public class HeaderElement : MonoBehaviour
	{
		[SerializeField]
		private StreakElement _soberStreakElement;

		[SerializeField]
		private StreakElement _englishStreakElement;

		[SerializeField]
		private StreakElement _trainingStreakElement;
		
		[SerializeField]
		private EfficiencyElement _efficiencyElement;
		
		private IAccountController _accountController;

		public void Initialize(IAccountController accountController)
		{
			_accountController = accountController;
			
			_soberStreakElement.Initialize(_accountController.GetSoberStreak());
			_englishStreakElement.Initialize(_accountController.GetEnglishStreak());
			_trainingStreakElement.Initialize(_accountController.GetTrainingStreak());
			_efficiencyElement.Initialize(_accountController.GetEfficiency());
		}

		public void UpdateSoberStreak()
		{
			_soberStreakElement.Initialize(_accountController.GetSoberStreak());
		}
	}
}