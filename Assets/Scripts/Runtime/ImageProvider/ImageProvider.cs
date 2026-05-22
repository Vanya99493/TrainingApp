using TrainingApp.ServiceLocatorScripts;
using UnityEngine;

namespace TrainingApp.ImageProviderScripts
{
	public class ImageProvider : MonoBehaviour, IService
	{
		[SerializeField]
		private Sprite _restImage;
		
		[SerializeField]
		private Sprite[] _images;

		public Sprite GetRestImage()
		{
			return _restImage;
		}
		
		public Sprite GetImage(int index)
		{
			return _images[index];
		}

		public Sprite[] GetImages()
		{
			Sprite[] sprites = new Sprite[_images.Length];
			for (int i = 0; i < _images.Length; i++)
			{
				sprites[i] = _images[i];
			}
			return sprites;
		}
	}
}