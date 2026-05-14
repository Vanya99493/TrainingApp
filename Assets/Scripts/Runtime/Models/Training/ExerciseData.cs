using System;
using UnityEngine;

namespace TrainingApp.Models
{
	[Serializable]
	public class ExerciseData
	{
		[field: SerializeField]
		public string Name { get; private set; }
		
		[field: SerializeField]
		public int RepetitionsNumber { get; private set; }
		
		[field: SerializeField]
		public string Description { get; private set; }
	}
}