using System;
using TrainingApp.Models;
using UnityEngine;

namespace TrainingApp.UI
{
	public class EditExerciseElement : MonoBehaviour, IDestroyableElement<EditExerciseElement>
	{
		
		
		public event Action<EditExerciseElement> DestroyEvent;

		public void Initialize(ExerciseData exerciseData)
		{
			
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}
		
		
	}
}