using UnityEngine;

namespace TrainingApp.UI
{
	public abstract class BasePanel : MonoBehaviour
	{
		protected UIManager _uiManager;
		
		public bool IsOpen { get; protected set; }

		public virtual void Initialize(UIManager uiManager)
		{
			_uiManager = uiManager;
		}
		
		public virtual void Open(PanelArgs args)
		{
			if (!IsOpen)
			{
				SubscribeOnEvents();
				IsOpen = true;
				gameObject.SetActive(true);
			}
			else
			{
				UpdateContent();
			}
		}

		public virtual void Close()
		{
			UnsubscribeFromEvents();
			IsOpen = false;
			gameObject.SetActive(false);
		}

		protected virtual void UpdateContent() { }
		protected virtual void SubscribeOnEvents() { }
		protected virtual void UnsubscribeFromEvents() { }
	}
}