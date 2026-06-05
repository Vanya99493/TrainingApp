using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class ShapeButton : Button
	{
		[Space(10)]
		[Header("Shape")]
		[SerializeField]
		private float _alphaThreshold = 0.1f;

		protected override void Start()
		{
			base.Start();

			GetComponent<Image>().alphaHitTestMinimumThreshold = _alphaThreshold;
		}
	}
}