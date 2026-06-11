using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class LogoButtonElement : MonoBehaviour, IDestroyableElement<LogoButtonElement>
	{
		[SerializeField]
		private Button _button;

		[SerializeField]
		private Image _logoImage;

		public event Action<int> SelectLogoEvent;
		public event Action<LogoButtonElement> DestroyEvent;

		private int _logoIndex;
		
		private void OnEnable()
		{
			_button.onClick.AddListener(OnSelectLogoButtonClickedEventHandler);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnSelectLogoButtonClickedEventHandler);
		}

		public void Initialize(Sprite logoSprite, Color color, int index, bool isSelected = false)
		{
			_logoImage.sprite = logoSprite;
			_logoImage.color = color;
			_logoIndex = index;
		}
		
		public void Destroy()
		{
			DestroyEvent?.Invoke(this);
		}

		private void OnSelectLogoButtonClickedEventHandler()
		{
			SelectLogoEvent?.Invoke(_logoIndex);
		}
	}
}