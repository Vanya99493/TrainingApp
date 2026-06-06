using TrainingApp.Models;

namespace TrainingApp.UI
{
	public class SubTrainingDataPanelArgs : PanelArgs
	{
		public SubTrainingData SubTrainingData;

		public SubTrainingDataPanelArgs(SubTrainingData subTrainingData)
		{
			SubTrainingData = subTrainingData;
		}
	}
}