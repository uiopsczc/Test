using UnityEditor;

namespace CsCat
{
  [CustomEditor(typeof(TransformTweenClip))]
  public class TransformTweenClipEditor : Editor
  {
    private TransformTweenClip self;



    public override void OnInspectorGUI()
    {
      self = target as TransformTweenClip;



      using (new EditorGUILayoutBeginHorizontalScope())
      {
        if (EditorGUILayoutUtil.Toggle("UseTarget(T)", ref self.template.is_use_position_target))
          self.template.position_target = EditorGUILayout.Vector3Field("", self.template.position_target);
      }

      using (new EditorGUILayoutBeginHorizontalScope())
      {
        if (EditorGUILayoutUtil.Toggle("UseTarget(R)", ref self.template.is_use_rotation_target))
          self.template.rotation_target = EditorGUILayout.Vector3Field("", self.template.rotation_target);
      }


      using (new EditorGUILayoutBeginHorizontalScope())
      {
        if (EditorGUILayoutUtil.Toggle("UseTarget(S)", ref self.template.is_use_scale_target))
          self.template.scale_target = EditorGUILayout.Vector3Field("", self.template.scale_target);
      }

      EditorGUILayoutUtil.Space(4);

      using (new EditorGUILayoutBeginHorizontalScope())
      {
        self.template.position_multiply = EditorGUILayout.Vector3Field("Mutiply(T)", self.template.position_multiply);
      }

      using (new EditorGUILayoutBeginHorizontalScope())
      {
        self.template.rotation_multiply = EditorGUILayout.Vector3Field("Mutiply(R)", self.template.rotation_multiply);
      }

      using (new EditorGUILayoutBeginHorizontalScope())
      {
        self.template.scale_multiply = EditorGUILayout.Vector3Field("Mutiply(S)", self.template.scale_multiply);
      }

    }


  }
}
















