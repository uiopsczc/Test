#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class MountTimelinableTrack
	{
		public override void DrawGUISettingDetail()
		{
			transform = EditorGUILayout.ObjectField("transform", transform, typeof(Transform), true) as Transform;
		}
	}
}
#endif