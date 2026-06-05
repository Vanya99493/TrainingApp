using TrainingApp.Models;

namespace TrainingApp.UI
{
	public class DayDataPanelArgs : PanelArgs
	{
		public DayData DayData;

		public DayDataPanelArgs(DayData dayData)
		{
			DayData = dayData;
		}
	}
}