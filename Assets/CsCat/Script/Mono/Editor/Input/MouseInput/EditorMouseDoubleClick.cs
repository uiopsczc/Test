using UnityEngine;

namespace CsCat
{
	public class EditorMouseDoubleClick
	{
		public bool isDoubleClick => _isDoubleClick;

		float _lastClickTime = 0f;
		bool _isDoubleClick = false;

		public void Update()
		{
			Event e = Event.current;
			_isDoubleClick = false;
			if (e.isMouse && e.type == EventType.MouseDown)
			{
				_isDoubleClick = (Time.realtimeSinceStartup - _lastClickTime) <= 0.2f;
				_lastClickTime = Time.realtimeSinceStartup;
			}
		}
	}
}