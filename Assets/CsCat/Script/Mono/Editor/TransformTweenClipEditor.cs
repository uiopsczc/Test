using UnityEditor;

namespace CsCat
{
	[CustomEditor(typeof(TransformTweenClip))]
	public class TransformTweenClipEditor : Editor
	{
		private TransformTweenClip _self;

		public override void OnInspectorGUI()
		{
			_self = target as TransformTweenClip;


			using (new EditorGUILayoutBeginHorizontalScope())
			{
				if (EditorGUILayoutUtil.Toggle("UseTarget(T)", ref _self.template.isUsePositionTarget))
					_self.template.positionTarget = EditorGUILayout.Vector3Field("", _self.template.positionTarget);
			}

			using (new EditorGUILayoutBeginHorizontalScope())
			{
				if (EditorGUILayoutUtil.Toggle("UseTarget(R)", ref _self.template.isUseRotationTarget))
					_self.template.rotationTarget = EditorGUILayout.Vector3Field("", _self.template.rotationTarget);
			}


			using (new EditorGUILayoutBeginHorizontalScope())
			{
				if (EditorGUILayoutUtil.Toggle("UseTarget(S)", ref _self.template.isUseScaleTarget))
					_self.template.scaleTarget = EditorGUILayout.Vector3Field("", _self.template.scaleTarget);
			}

			EditorGUILayoutUtil.Space(4);

			using (new EditorGUILayoutBeginHorizontalScope())
			{
				_self.template.positionMultiply =
					EditorGUILayout.Vector3Field("Mutiply(T)", _self.template.positionMultiply);
			}

			using (new EditorGUILayoutBeginHorizontalScope())
			{
				_self.template.rotationMultiply =
					EditorGUILayout.Vector3Field("Mutiply(R)", _self.template.rotationMultiply);
			}

			using (new EditorGUILayoutBeginHorizontalScope())
			{
				_self.template.scaleMultiply = EditorGUILayout.Vector3Field("Mutiply(S)", _self.template.scaleMultiply);
			}
		}
	}
}