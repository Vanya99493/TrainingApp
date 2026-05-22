using TrainingApp.UI;
using UnityEngine;

namespace TrainingApp.UIFactoryScripts
{
	public class UIFactory<T> : MonoBehaviour where T : MonoBehaviour, IDestroyableElement<T>
	{
		[SerializeField]
		protected T _elementPrefab;

		[SerializeField]
		protected Transform _elementParent;
		
		public virtual T InstantiateElement()
		{
			var element = Instantiate(_elementPrefab, _elementParent);
			element.DestroyEvent += OnElementDestroyEventHandler;
			return element;
		}

		protected virtual void OnElementDestroyEventHandler(T element)
		{
			Destroy(element.gameObject);
		}
	}
}