using TMPro;
using UnityEngine;

namespace TrainingApp.UI.Panels
{
	public class SettingsHeaderElement : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _profileNameText;

		public void Initialize(string profileName)
		{
			_profileNameText.text = profileName;
		}
	}
}