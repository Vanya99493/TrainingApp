using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingApp.UI
{
	public class ResizableText : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _targetText;

		[SerializeField]
		private LayoutElement _parentLayoutElement;

		private void OnEnable()
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChangedEventHandler);
		}

		private void OnDisable()
		{
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChangedEventHandler);
		}

		private void OnTextChangedEventHandler(Object tmpObj)
		{
			if (tmpObj == _targetText)
			{
				_parentLayoutElement.minHeight = _targetText.transform.localScale.y;
			}
		}
	}
}