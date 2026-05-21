using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class SelectLogoSubPanel : BasePanel
	{
		[SerializeField]
		private LogoButtonElementFactory _logoButtonElementFactory;

		[SerializeField]
		private Button _closeButton;

		private List<LogoButtonElement> _logoButtonElements = new();

		public event Action<int> SelectLogoEvent;

		public void Open(Sprite[] logos)
		{
			base.Open();
			
			for (int i = 0; i < logos.Length; i++)
			{
				int logoIndex = i;
				var logoButton = _logoButtonElementFactory.InstantiateElement();
				logoButton.Initialize(logos[i], logoIndex);
				logoButton.SelectLogoEvent += OnLogoSelectEventHandler;
				_logoButtonElements.Add(logoButton);
			}
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_closeButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_closeButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);
		}

		private void OnCloseButtonClickedEventHandler()
		{
			gameObject.SetActive(false);
		}

		private void OnLogoSelectEventHandler(int logoIndex)
		{
			SelectLogoEvent?.Invoke(logoIndex);
		}
	}
}