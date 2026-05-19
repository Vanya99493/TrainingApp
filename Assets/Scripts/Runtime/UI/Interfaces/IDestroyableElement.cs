using System;

namespace TrainingApp.UI
{
	public interface IDestroyableElement<T>
	{
		public event Action<T> DestroyEvent;

		public void Destroy();
	}
}