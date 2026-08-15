using TrainingApp.Enums;

namespace TrainingApp.Models
{
	public static class SpentTimeTypeConverter
	{
		public static string ConvertToString(SpentTimeType spentTimeType)
		{
			return spentTimeType switch
			{
				SpentTimeType.Min0 => "0",
				SpentTimeType.Min5 => "5",
				SpentTimeType.Min10 => "10",
				SpentTimeType.Min15 => "15",
				SpentTimeType.Min20 => "20",
				SpentTimeType.Min25 => "25",
				SpentTimeType.Min30 => "30",
				SpentTimeType.Min35 => "35",
				SpentTimeType.Min40 => "40",
				SpentTimeType.Min45 => "45",
				SpentTimeType.Min50 => "50",
				SpentTimeType.Min55 => "55",
				SpentTimeType.Min60 => "60",
				SpentTimeType.Larger60Min => "60+",
				_ => "Undefined",
			};
		}
	}
}