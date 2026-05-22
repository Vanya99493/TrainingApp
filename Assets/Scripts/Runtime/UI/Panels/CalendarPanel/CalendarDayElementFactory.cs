using TrainingApp.UIFactoryScripts;
using UnityEngine;

namespace TrainingApp.UI
{
	public class CalendarDayElementFactory : UIFactory<CalendarDayElement>
	{
		[SerializeField]
		private GameObject _spaceObject;
		
		public void InstantiateSpace()
		{
			Instantiate(_spaceObject, _elementParent);
		}
	}
}