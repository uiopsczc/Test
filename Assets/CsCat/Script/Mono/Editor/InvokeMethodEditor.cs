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
		private InvokeMethodMono _self;

		private Type _targetType;
		private MethodInfo _targetMethodInfo;
		private List<object> _targetMethodInfoArgList;


		private AutoCompleteSearchField _targetTypeSearchField;
		private AutoCompleteSearchField _targetMethodInfoSearchField;


		void OnEnable()
		{
			_self = target as InvokeMethodMono;
			_targetTypeSearchField = new AutoCompleteSearchField();
			_targetMethodInfoSearchField = new AutoCompleteSearchField();

			_targetTypeSearchField.onInputChanged = OnInputChangedOfTargetTypeSearchField;
			_targetTypeSearchField.onConfirm = OnConfirmOfTargetTypeSearchField;

			_targetMethodInfoSearchField.onInputChanged = OnInputChangedOfTargetMethodInfoSearchField;
			_targetMethodInfoSearchField.onConfirm = OnConfirmOfTargetMethodInfoSearchField;
		}

		void GUITargetType()
		{
			Assembly assembly = target.GetType().Assembly;
			_targetType = _self.targetTypeName.IsNullOrWhiteSpace() ? null : assembly.GetType(_self.targetTypeName);
			_self.targetTypeName = _targetType == null ? "" : _targetType.Name;
			using (new EditorGUILayoutBeginHorizontalScope())
			{
				EditorGUILayout.LabelField("targetType:", GUILayout.Width(80));
				EditorGUILayout.LabelField(
					_self.targetTypeName.IsNullOrWhiteSpace() ? "null" : _self.targetTypeName,
					GUILayout.Width(180));
				EditorGUILayout.LabelField("search:", GUILayout.Width(80));
				_targetTypeSearchField.OnGUI();
			}
		}


		void GUITargetMethodInfo()
		{
			_targetMethodInfoArgList = _self.targetMethodArgsJsonString.IsNullOrWhiteSpace()
				? new List<object>()
				: JsonSerializer.Deserialize(_self.targetMethodArgsJsonString) as List<object>;
			_targetMethodInfo = _self.targetMethodInfoName.IsNullOrWhiteSpace()
				? null
				: _targetType.GetMethodInfo(_self.targetMethodInfoName, BindingFlagsConst.All,
					_targetMethodInfoArgList.ToArray<object, Type>(e => e.GetType()));
			_self.targetMethodInfoName = _targetMethodInfo == null ? "" : _targetMethodInfo.Name;
			using (new EditorGUILayoutBeginHorizontalScope())
			{
				EditorGUILayout.LabelField("targetMethodInfo:", GUILayout.Width(120));
				EditorGUILayout.LabelField(
					_self.targetMethodInfoName.IsNullOrWhiteSpace() ? "null" : _self.targetMethodInfoName,
					GUILayout.Width(180));
				EditorGUILayout.LabelField("search:", GUILayout.Width(80));
				_targetMethodInfoSearchField.OnGUI();
			}
		}

		void GUITargetMethodArgs()
		{
			ParameterInfo[] parameterInfoList = _targetMethodInfo.GetParameters();
			EditorGUILayout.LabelField(string.Format("methodArgsSize:{0}", parameterInfoList.Length));
			using (new EditorGUIIndentLevelScope())
			{
				for (int i = 0; i < parameterInfoList.Length; i++)
				{
					ParameterInfo parameterInfo = parameterInfoList[i];
					using (new EditorGUILayoutBeginHorizontalScope())
					{
						EditorGUILayout.LabelField(parameterInfo.Name, GUILayout.Width(100));
						if (_targetMethodInfoArgList.Count <= i || _targetMethodInfoArgList[i] == null ||
						    parameterInfo.ParameterType != _targetMethodInfoArgList[i].GetType())
						{
							_targetMethodInfoArgList[i] = parameterInfo.DefaultValue;
							if (_targetMethodInfoArgList[i] == null)
								_targetMethodInfoArgList[i] = parameterInfo.ParameterType.DefaultValue();
						}

						_targetMethodInfoArgList[i] =
							EditorGUILayoutUtil.Field("", parameterInfo.ParameterType, _targetMethodInfoArgList[i]);
					}
				}

				_self.targetMethodArgsJsonString = _targetMethodInfoArgList.Count == 0
					? ""
					: JsonSerializer.Serialize(_targetMethodInfoArgList);
			}
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			GUITargetType();
			if (_targetType != null)
			{
				GUITargetMethodInfo();
				if (_targetMethodInfo != null)
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
			_targetTypeSearchField.ClearResults();
			Assembly assembly = target.GetType().Assembly;
			foreach (var type in assembly.GetTypes().ToList())
			{
				if (type.Name.ToLower().Contains(result.ToLower()))
					_targetTypeSearchField.AddResult(type.Name, type);
			}
		}

		void OnConfirmOfTargetTypeSearchField(KeyValuePair<string, object> result)
		{
			if (!result.Value.Equals(_targetType))
			{
				_targetType = result.Value as Type;
				_self.targetTypeName = _targetType == null ? "" : _targetType.FullName;

				_targetMethodInfo = null;
				_self.targetMethodInfoName = "";
			}
		}

		void OnInputChangedOfTargetMethodInfoSearchField(string result)
		{
			_targetMethodInfoSearchField.ClearResults();
			var infos = _targetType.GetMethods(BindingFlagsConst.All);
			for (var i = 0; i < infos.Length; i++)
			{
				var methodInfo = infos[i];
				if (methodInfo.Name.ToLower().Contains(result.ToLower()))
					_targetMethodInfoSearchField.AddResult(methodInfo.Name, methodInfo);
			}
		}


		void OnConfirmOfTargetMethodInfoSearchField(KeyValuePair<string, object> result)
		{
			if (!result.Value.Equals(_targetMethodInfo))
			{
				_targetMethodInfo = result.Value as MethodInfo;
				_self.targetMethodInfoName = _targetMethodInfo == null ? "" : _targetMethodInfo.Name;

				_targetMethodInfoArgList.Clear();
				ParameterInfo[] parameterInfoList = _targetMethodInfo.GetParameters();
				for (int i = _targetMethodInfoArgList.Count; i < parameterInfoList.Length; i++)
				{
					Type t = parameterInfoList[i].ParameterType;
					_targetMethodInfoArgList.Add(t == typeof(string) ? "" : t.DefaultValue());
				}

				_self.targetMethodArgsJsonString = JsonSerializer.Serialize(_targetMethodInfoArgList);
			}
		}
	}
}