#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
	public class HandleUtil
	{
		public static SetDefaultControlScope SetDefaultControl(FocusType focusType)
		{
			return new SetDefaultControlScope(focusType);
		}
	}
}
#endif