using TrainingApp.Models;

namespace TrainingApp.UI
{
	public class TrainingDataPanelArgs : PanelArgs
	{
		public TrainingData TrainingData;

		public TrainingDataPanelArgs(TrainingData trainingData)
		{
			TrainingData = trainingData;
		}
	}
}