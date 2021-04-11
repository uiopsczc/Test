using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  [CustomEditor(typeof(InvokeMethodMono))]
  public class InvokeMethodMonoEditor : Editor
  {
    private InvokeMethodMono self;

    private Type targetType;
    private MethodInfo targetMethodInfo;
    private List<object> targetMethodInfoArgList;


    private AutoCompleteSearchField targetTypeSearchField;
    private AutoCompleteSearchField targetMethodInfoSearchField;


    void OnEnable()
    {
      self = target as InvokeMethodMono;
      targetTypeSearchField = new AutoCompleteSearchField();
      targetMethodInfoSearchField = new AutoCompleteSearchField();

      targetTypeSearchField.onInputChanged = OnInputChangedOfTargetTypeSearchField;
      targetTypeSearchField.onConfirm = OnConfirmOfTargetTypeSearchField;

      targetMethodInfoSearchField.onInputChanged = OnInputChangedOfTargetMethodInfoSearchField;
      targetMethodInfoSearchField.onConfirm = OnConfirmOfTargetMethodInfoSearchField;

    }

    void GUITargetType()
    {
      Assembly assembly = target.GetType().Assembly;
      targetType = self.target_type_name.IsNullOrWhiteSpace() ? null : assembly.GetType(self.target_type_name);
      self.target_type_name = targetType == null ? "" : targetType.Name;
      using (new EditorGUILayoutBeginHorizontalScope())
      {
        EditorGUILayout.LabelField("targetType:", GUILayout.Width(80));
        EditorGUILayout.LabelField(self.target_type_name.IsNullOrWhiteSpace() ? "null" : self.target_type_name,
          GUILayout.Width(180));
        EditorGUILayout.LabelField("search:", GUILayout.Width(80));
        targetTypeSearchField.OnGUI();
      }
    }


    void GUITargetMethodInfo()
    {
      targetMethodInfoArgList = self.target_methodArgs_json_string.IsNullOrWhiteSpace()
        ? new List<object>()
        : JsonSerializer.Deserialize(self.target_methodArgs_json_string) as List<object>;
      targetMethodInfo = self.target_methodInfo_name.IsNullOrWhiteSpace()
        ? null
        : targetType.GetMethodInfo(self.target_methodInfo_name, BindingFlagsConst.All,
          targetMethodInfoArgList.ToArray<object, Type>(e => e.GetType()));
      self.target_methodInfo_name = targetMethodInfo == null ? "" : targetMethodInfo.Name;
      using (new EditorGUILayoutBeginHorizontalScope())
      {
        EditorGUILayout.LabelField("targetMethodInfo:", GUILayout.Width(120));
        EditorGUILayout.LabelField(self.target_methodInfo_name.IsNullOrWhiteSpace() ? "null" : self.target_methodInfo_name,
          GUILayout.Width(180));
        EditorGUILayout.LabelField("search:", GUILayout.Width(80));
        targetMethodInfoSearchField.OnGUI();
      }

    }

    void GUITargetMethodArgs()
    {
      ParameterInfo[] parameterInfoList = targetMethodInfo.GetParameters();
      EditorGUILayout.LabelField(string.Format("methodArgsSize:{0}", parameterInfoList.Length));
      using (new EditorGUIIndentLevelScope())
      {
        for (int i = 0; i < parameterInfoList.Length; i++)
        {
          ParameterInfo parameterInfo = parameterInfoList[i];
          using (new EditorGUILayoutBeginHorizontalScope())
          {
            EditorGUILayout.LabelField(parameterInfo.Name, GUILayout.Width(100));
            if (targetMethodInfoArgList.Count <= i || targetMethodInfoArgList[i] == null ||
                parameterInfo.ParameterType != targetMethodInfoArgList[i].GetType())
            {
              targetMethodInfoArgList[i] = parameterInfo.DefaultValue;
              if (targetMethodInfoArgList[i] == null)
                targetMethodInfoArgList[i] = parameterInfo.ParameterType.DefaultValue();
            }

            targetMethodInfoArgList[i] =
              EditorGUILayoutUtil.Field("", parameterInfo.ParameterType, targetMethodInfoArgList[i]);
          }
        }

        self.target_methodArgs_json_string = targetMethodInfoArgList.Count == 0
          ? ""
          : JsonSerializer.Serialize(targetMethodInfoArgList);

      }
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();
      GUITargetType();
      if (targetType != null)
      {
        GUITargetMethodInfo();
        if (targetMethodInfo != null)
        {
          GUITargetMethodArgs();
        }
      }

      if (GUI.changed)
      {
        EditorUtility.SetDirty(target);
        EditorApplication.MarkSceneDirty();
      }


    }


    void OnInputChangedOfTargetTypeSearchField(string result)
    {
      targetTypeSearchField.ClearResults();
      Assembly assembly = target.GetType().Assembly;
      foreach (var type in assembly.GetTypes().ToList())
      {
        if (type.Name.ToLower().Contains(result.ToLower()))
          targetTypeSearchField.AddResult(type.Name, type);
      }

    }

    void OnConfirmOfTargetTypeSearchField(KeyValuePair<string, object> result)
    {
      if (!result.Value.Equals(targetType))
      {
        targetType = result.Value as Type;
        self.target_type_name = targetType == null ? "" : targetType.FullName;

        targetMethodInfo = null;
        self.target_methodInfo_name = "";


      }
    }

    void OnInputChangedOfTargetMethodInfoSearchField(string result)
    {
      targetMethodInfoSearchField.ClearResults();
      foreach (var methodInfo in targetType.GetMethods(BindingFlagsConst.All))
      {
        if (methodInfo.Name.ToLower().Contains(result.ToLower()))
          targetMethodInfoSearchField.AddResult(methodInfo.Name, methodInfo);
      }
    }


    void OnConfirmOfTargetMethodInfoSearchField(KeyValuePair<string, object> result)
    {
      if (!result.Value.Equals(targetMethodInfo))
      {
        targetMethodInfo = result.Value as MethodInfo;
        self.target_methodInfo_name = targetMethodInfo == null ? "" : targetMethodInfo.Name;

        targetMethodInfoArgList.Clear();
        ParameterInfo[] parameterInfoList = targetMethodInfo.GetParameters();
        for (int i = targetMethodInfoArgList.Count; i < parameterInfoList.Length; i++)
        {
          Type t = parameterInfoList[i].ParameterType;
          if (t == typeof(string))
            targetMethodInfoArgList.Add("");
          else
            targetMethodInfoArgList.Add(t.DefaultValue());
        }

        self.target_methodArgs_json_string = JsonSerializer.Serialize(targetMethodInfoArgList);
      }
    }








  }
}






