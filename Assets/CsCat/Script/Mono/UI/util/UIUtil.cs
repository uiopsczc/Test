

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CsCat
{
	public class UIUtil
	{
		/// <summary>
		/// 是否在UI上
		/// https://www.jianshu.com/p/c7a963da85b3
		/// </summary>
		public static bool IsOverUI(Vector2 screenPosition)
		{
			PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
			eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
			return results.Count > 0;
		}

		public static bool IsOverUI(Vector3 screenPosition)
		{
			return IsOverUI(screenPosition.ToVector2());
		}
	}
}