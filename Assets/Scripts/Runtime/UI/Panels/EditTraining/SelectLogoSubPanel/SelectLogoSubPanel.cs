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

		public void Open(Sprite[] logos, int currentLogoIndex)
		{
			base.Open(new PanelArgs());
			
			for (int i = 0; i < logos.Length; i++)
			{
				int logoIndex = i;
				var logoButton = _logoButtonElementFactory.InstantiateElement();
				logoButton.Initialize(logos[i], logoIndex, i == currentLogoIndex);
				logoButton.SelectLogoEvent += OnLogoSelectEventHandler;
				_logoButtonElements.Add(logoButton);
			}
		}

		public override void Close()
		{
			base.Close();

			ClearLogos();
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
			Close();
		}

		private void OnLogoSelectEventHandler(int logoIndex)
		{
			SelectLogoEvent?.Invoke(logoIndex);
			Close();
		}

		private void ClearLogos()
		{
			foreach (var logoButtonElement in _logoButtonElements)
			{
				logoButtonElement.Destroy();
			}
			_logoButtonElements.Clear();
		}
	}
}