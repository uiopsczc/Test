using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	class GUIScreen : MonoBehaviour
	{
		public static Action action;
		Vector2 scrollPosition;
		public bool isShowLog = true;
		public bool isLockToBottom = true;

		void OnGUI()
		{
			action?.Invoke();
			//      using (new GUILayoutBeginVerticalScope())
			//      {
			//        using (new GUIMatrixScope(Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1.5f, 1.5f, 1))))
			//        {
			//          is_show_log = GUILayout.Toggle(is_show_log, "显示日志");
			//          if (is_show_log)
			//            is_lockTo_bottom = GUILayout.Toggle(is_lockTo_bottom, "锁屏");
			//        }
			//
			//        if (is_show_log)
			//        {
			//          if (is_lockTo_bottom)
			//            scrollPosition = new Vector2(scrollPosition.x, float.MaxValue);
			//          using (new GUILayoutBeginScrollViewScope(ref scrollPosition))
			//          {
			//            LogCat.Flush_GUI();
			//          }
			//        }
			//      }
		}

		void Awake()
		{
			DontDestroyOnLoad(this.gameObject);
		}
	}
}