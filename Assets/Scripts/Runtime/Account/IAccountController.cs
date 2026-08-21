namespace TrainingApp.AccountScripts
{
	public interface IAccountController
	{
		public int GetTrainingStreak();
		public int GetEnglishStreak();
		public int GetSoberStreak();
		public int GetEfficiency();
	}
}