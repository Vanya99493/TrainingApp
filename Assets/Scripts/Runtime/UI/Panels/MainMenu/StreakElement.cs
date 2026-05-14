using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class StreakElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _streakText;

		[SerializeField]
		private Image _streakImage;

		public void Initialize(int currentStreak)
		{
			UpdateCurrentStreak(currentStreak);
		}

		private void UpdateCurrentStreak(int currentStreak)
		{
			_streakText.text = currentStreak.ToString();
			_streakImage.gameObject.SetActive(currentStreak > 0);
		}
	}
}