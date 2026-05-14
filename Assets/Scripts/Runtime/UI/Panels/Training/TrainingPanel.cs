using TrainingApp.AccountScripts;
using TrainingApp.Models;
using TrainingApp.ServiceLocatorScripts;

namespace TrainingApp.UI
{
	public class TrainingPanel : BasePanel
	{
		public void Open(string guid)
		{
			base.Open();

			TrainingData trainingData = ServiceLocator.Instance.Get<AccountController>().GetTraining(guid);
			
			
		}
	}
}