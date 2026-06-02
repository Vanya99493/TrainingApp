using System;

namespace TrainingApp.UI
{
	[Flags]
	public enum ValidationType
	{
		NotEmpty = 1 << 0,
		IsNumber = 1 << 1,
		IsPositiveNumber = 1 << 2,
		IsZeroOrPositiveNumber = 1 << 3,
	}
}