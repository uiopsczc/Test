using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static partial class TimelinableEditorWindowUtil
  {
    public static void DrawGUISetting_Sequence<T>(ref T sequence) where T : TimelinableSequenceBase
    {
      using (new GUILayoutBeginHorizontalScope())
      {
        GUILayout.Label("sequence", GUILayout.Width(64));
        sequence = (T) EditorGUILayout.ObjectField(sequence,
          typeof(T), false);
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("create", GUILayout.Width(64)))
        {
          var path = EditorUtility.SaveFilePanel(
            "保存文件到",
            "",
            string.Format("{0}.asset", typeof(T).GetLastName()), //
            "asset");
          if (!path.IsNullOrEmpty())
          {
            Delegate OnCreateSequence_Delegate = DelegateUtil.CreateGenericAction(new[] {typeof(T)},
              typeof(TimelinableEditorWindowUtil), "OnCreateSequence");
            sequence = typeof(ScriptableObjectUtil).InvokeGenericMethod<T>(
              "CreateAsset", new Type[] {typeof(T)}, false, path, OnCreateSequence_Delegate);
          }
        }

        if (sequence != null)
          if (GUILayout.Button("save", GUILayout.Width(64)))
            sequence.Save();
      }
    }

    public static void OnCreateSequence<T>(T sequence) where T : TimelinableSequenceBase
    {
      sequence.AddTrack(sequence.tracks.GetType().GetElementType().CreateInstance<TimelinableTrackBase>());
    }
    
  }
}