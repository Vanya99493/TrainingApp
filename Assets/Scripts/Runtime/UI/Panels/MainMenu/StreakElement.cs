using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class StreakElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _streakValueText;

		[SerializeField]
		private TMP_Text _streakTitleText;

		[Space(10)]
		[SerializeField]
		private Image _streakImage;

		public void Initialize(int currentStreak)
		{
			UpdateCurrentStreak(currentStreak);
		}

		private void UpdateCurrentStreak(int currentStreak)
		{
			_streakValueText.text = currentStreak.ToString();
			_streakTitleText.text = currentStreak == 1
				? "DAY"
				: "DAYS";
			_streakImage.gameObject.SetActive(currentStreak > 0);
		}
	}
}