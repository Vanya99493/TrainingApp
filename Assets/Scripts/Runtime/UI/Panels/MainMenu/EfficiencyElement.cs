using DG.Tweening;
using TMPro;
using TrainingApp.Extensions;
using UnityEngine;

namespace TrainingApp.UI
{
	public class EfficiencyElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _efficiencyValueText;

		[SerializeField]
		private RectTransform _efficiencyFillAreaRectTransform;

		[Space(10)]
		[SerializeField]
		private float _efficiencyFillImageMaxHeight = 100f;

		private Tween _efficiencyFillImageAnimationTween;

		public void Initialize(
			float efficiencyValue, 
			float efficiencyFillImageAnimationTime = 0f)
		{
			StartEfficiencyValueAnimation(
				efficiencyValue, 
				efficiencyFillImageAnimationTime);
		}

		private void StartEfficiencyValueAnimation(
			float efficiencyValue, 
			float efficiencyFillImageAnimationTime = 0f)
		{
			_efficiencyFillImageAnimationTween.To(
				efficiencyFillImageAnimationTime,
				UpdateEfficiencyValue,
				() => UpdateEfficiencyValue(efficiencyValue));
		}

		private void UpdateEfficiencyValue(float efficiencyValue)
		{
			_efficiencyFillAreaRectTransform.SetSizeWithCurrentAnchors(
				RectTransform.Axis.Horizontal,
				efficiencyValue * _efficiencyFillImageMaxHeight);
			_efficiencyValueText.text = Mathf.RoundToInt(efficiencyValue * 100).ToString();
		}
	}
}