using System;
using UnityEngine;

namespace TrainingApp.Models
{
	[Serializable]
	public class ClientData
	{
		[field: SerializeField]
		public string Name { get; private set; }
		
		[field: SerializeField]
		public ExerciseData[] Exercises { get; private set; }
	}
}