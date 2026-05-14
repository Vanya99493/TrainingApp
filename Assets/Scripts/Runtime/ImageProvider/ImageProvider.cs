using TrainingApp.ServiceLocatorScripts;
using UnityEngine;

namespace TrainingApp.ImageProviderScripts
{
	public class ImageProvider : MonoBehaviour, IService
	{
		[SerializeField]
		private Sprite[] _images;

		public Sprite GetImage(int index)
		{
			return _images[index];
		}
	}
}