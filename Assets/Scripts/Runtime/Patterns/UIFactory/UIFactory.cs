using TrainingApp.UI;
using UnityEngine;

namespace TrainingApp.UIFactoryScripts
{
	public class UIFactory<T> : MonoBehaviour where T : MonoBehaviour, IDestroyableElement<T>
	{
		[SerializeField]
		private T _elementPrefab;

		[SerializeField]
		private Transform _elementParent;
		
		public T InstantiateElement()
		{
			var element = Instantiate(_elementPrefab, _elementParent);
			element.DestroyEvent += OnElementDestroyEventHandler;
			return element;
		}

		private void OnElementDestroyEventHandler(T element)
		{
			Destroy(element.gameObject);
		}
	}
}