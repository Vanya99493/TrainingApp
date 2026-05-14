using System;
using UnityEngine;

namespace TrainingApp.Models
{
	[Serializable]
	public class TrainingData
	{
		[field: SerializeField]
		public string Guid { get; private set; }
		
		[field: SerializeField]
		public string Name { get; private set; }
		
		[field: SerializeField]
		public int ImageIndex { get; private set; }
		
		[field: SerializeField]
		public ExerciseData[] Exercises { get; private set; }
	}
}