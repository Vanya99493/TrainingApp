using System;
using System.Collections.Generic;
using System.Linq;
using TrainingApp.Constants;
using TrainingApp.Enums;
using TrainingApp.ServiceScripts;
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

		[SerializeField]
		private Image _currentLogoImage;

		[SerializeField]
		private ToggleButtonGroup _logoNavigationToggleButtonGroup;
		
		[SerializeField]
		private Button _logosButton;

		[SerializeField]
		private Button _colorsButton;

		[SerializeField]
		private int _logoElementsInRow = 4;

		private ImageProvider _imageProvider;
		private ColorProvider _colorProvider;
		private List<LogoButtonElement> _logoButtonElements = new();
		private int _logoIndex;
		private int _logoColorIndex;

		public event Action<int, int> SelectLogoEvent;

		public void Open(ImageProvider imageProvider, ColorProvider colorProvider, int currentLogoIndex, int logoColorIndex)
		{
			_imageProvider = imageProvider;
			_colorProvider = colorProvider;
			_logoIndex = currentLogoIndex;
			_logoColorIndex = logoColorIndex;
			
			_logoButtonElementFactory.Initialize(_logoElementsInRow);
			_logoNavigationToggleButtonGroup.Initialize(0);

			UpdateLogoView();
			InitLogos();
			
			base.Open(new PanelArgs());
		}

		public override void Close()
		{
			base.Close();

			ClearLogoElements();
		}

		protected override void SubscribeOnEvents()
		{
			base.SubscribeOnEvents();
			
			_closeButton.onClick.AddListener(OnCloseButtonClickedEventHandler);
			_logosButton.onClick.AddListener(OnLogosButtonClickedEventHandler);
			_colorsButton.onClick.AddListener(OnColorsButtonClickedEventHandler);
		}

		protected override void UnsubscribeFromEvents()
		{
			base.UnsubscribeFromEvents();
			
			_closeButton.onClick.RemoveListener(OnCloseButtonClickedEventHandler);
			_logosButton.onClick.RemoveListener(OnLogosButtonClickedEventHandler);
			_colorsButton.onClick.RemoveListener(OnColorsButtonClickedEventHandler);
		}

		private void OnCloseButtonClickedEventHandler()
		{
			Close();
		}

		private void OnLogosButtonClickedEventHandler()
		{
			ClearLogoElements();
			InitLogos();
		}
		
		private void OnColorsButtonClickedEventHandler()
		{
			ClearLogoElements();
			InitColors();
		}
			
		private void InitLogos()
		{
			Sprite[] logos = _imageProvider.GetImages();
			
			for (int i = 0; i < logos.Length; i++)
			{
				int logoIndex = i;
				var logoButton = _logoButtonElementFactory.InstantiateElement();
				logoButton.Initialize(logos[i], _colorProvider.GetColor(_logoColorIndex), logoIndex, i == _logoIndex);
				logoButton.SelectLogoEvent += OnLogoSelectEventHandler;
				_logoButtonElements.Add(logoButton);
			}
		}

		private void InitColors()
		{
			Color[] colors = _colorProvider.GetColors();

			for (int i = 0; i < colors.Length; i++)
			{
				int colorIndex = i;
				var logoButton = _logoButtonElementFactory.InstantiateElement();
				logoButton.Initialize(null, colors[colorIndex], colorIndex, i == _logoColorIndex);
				logoButton.SelectLogoEvent += OnLogoColorSelectEventHandler;
				_logoButtonElements.Add(logoButton);
			}
		}
		
		private void OnLogoSelectEventHandler(int logoIndex)
		{
			_logoIndex = logoIndex;
			
			UpdateLogoView();
			SelectLogoEvent?.Invoke(_logoIndex, _logoColorIndex);
		}

		private void OnLogoColorSelectEventHandler(int colorIndex)
		{
			_logoColorIndex = colorIndex;
			
			UpdateLogoView();
			SelectLogoEvent?.Invoke(_logoIndex, _logoColorIndex);
		}

		private void UpdateLogoView()
		{
			_currentLogoImage.sprite = _imageProvider.GetImage(_logoIndex);
			_currentLogoImage.color = _colorProvider.GetColor(_logoColorIndex);
		}

		private void ClearLogoElements()
		{
			foreach (var logoButtonElement in _logoButtonElements)
			{
				logoButtonElement.Destroy();
			}
			_logoButtonElements.Clear();
		}
	}
}