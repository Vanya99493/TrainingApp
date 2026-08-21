using System;
using DG.Tweening;

namespace TrainingApp.Extensions
{
	public static class DOTweenExtensions
	{
		public static void To(
			this Tween tween, 
			float duration, 
			Action<float> onUpdateCallback = null,
			Action onCompleteCallback = null)
		{
			float passedTime = 0f;
			
			tween?.Kill();
			tween = DOTween.To(() => passedTime, x => passedTime = x, duration, duration)
				.OnUpdate(() => onUpdateCallback?.Invoke(passedTime / duration))
				.OnComplete(() => onCompleteCallback?.Invoke());;
		}
	}
}