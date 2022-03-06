using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class UIAutoGenLua : UIAutoGen
	{
		protected override string cfgPartStartsWith => UIAutoGenConst.Lua_Line_Cfg_Part_Starts_With;

		public override string GetEditorUtilitySaveFilePanel(string fileName)
		{
			return EditorUtility.SaveFilePanel("Select Path Of " + fileName, "Assets/Lua/Game/UI", fileName, "lua.txt");
		}

		public override void GenCode(GameObject prefab, string saveToFilePath,
			List<AutoGenComponentInfo> componentInfoList)
		{
			base.GenCode(prefab, saveToFilePath, componentInfoList);
			// Require
			GenRequires();
			// 类声明
			GenClassDeclare();
			//Init函数
			GenInitFunc();
			//InitUI函数
			GenInitUIFunc();
			//DestroyUI函数
			GenDestroyUIFunc();
			//AddUIEvents函数
			GenAddUIEventsFunc();
			//OnClick函数
			GenOnClickFuncs();
			//AddLogicEvent函数
			GenAddLogicEvents();
			//_Destroy函数
			Gen_DestroyFunc();
			//Return函数
			GenReturn();
			StdioUtil.WriteTextFile(saveToFilePath, AutoGenLineInfoUtil.ToLineList(lineInfoList));
		}

		public void GenRequires()
		{
			string toInsertLine = null;
			if(IsPanel())
				toInsertLine = "local UIPanel = require(\"LuaCat.UI.UIPanel\")";
			else
				toInsertLine = "local UIObject = require(\"LuaCat.UI.UIObject\")";
			lastCheckedLineIndex =
				AutoGenLineInfoUtil.CheckInsert(toInsertLine.ToAutoGenLineInfo(), this.lineInfoList, 0);
			GenNewLine();
		}

		// 类声明
		public void GenClassDeclare()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("---@class {0}:{1}", GetClassName(), GetParentClassName()).ToAutoGenLineInfo(),
				string.Format("local {0} = Class(\"{1}\", {2})", GetClassName(), GetClassName(),GetParentClassName()).ToAutoGenLineInfo(),
			};
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		// 构造函数Init
		public void GenInitFunc()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("function {0}:Init(...)", GetClassName()).ToAutoGenLineInfo(cfgPartStartsWith, "Init Start"),
				string.Format("\t{0}.Init(self, ...)", GetParentClassName()).ToAutoGenLineInfo(cfgPartStartsWith, GetParentClassName() + ".Init"),
				"end".ToAutoGenLineInfo(cfgPartStartsWith, "Init End"),
			};
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		public void GenInitUIFunc()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("function {0}:InitUI()", GetClassName()).ToAutoGenLineInfo(cfgPartStartsWith, "InitUI Start"),
				string.Format("\t{0}.InitUI(self)", GetParentClassName()).ToAutoGenLineInfo(),
				"\tlocal rootTransform = self:GetGraphicComponent():GetTransform()".ToAutoGenLineInfo(),
			};
			for (int i = 0; i < this.componentInfoList.Count; i++)
			{
				var componentInfo = componentInfoList[i];
				if (componentInfo.name.Contains("Nego_"))
					toInsertLineInfoList.Add(string.Format("\tself._{0} = rootTransform:Find(\"{1}\")", componentInfo.name,
						componentInfo.path).ToAutoGenLineInfo(cfgPartStartsWith, "Set self._"+ componentInfo.name), true);
				else
					toInsertLineInfoList.Add(string.Format("\tself._{0} = rootTransform:Find(\"{1}\"):GetComponent(\"{2}\")",
						componentInfo.name, componentInfo.path, componentInfo.type).ToAutoGenLineInfo(cfgPartStartsWith, "Set self._", true));
			}

			toInsertLineInfoList.Add("end".ToAutoGenLineInfo(cfgPartStartsWith, "InitUI End"));
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		public void GenDestroyUIFunc()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("function {0}:DestroyUI()", GetClassName()).ToAutoGenLineInfo(cfgPartStartsWith, "DestroyUI Start"),
				string.Format("\t{0}.DestroyUI(self)", GetParentClassName()).ToAutoGenLineInfo(),
			};
			for (int i = 0; i < this.componentInfoList.Count; i++)
			{
				var componentInfo = componentInfoList[i];
				toInsertLineInfoList.Add(string.Format("\tself._{0} = nil", componentInfo.name).ToAutoGenLineInfo(cfgPartStartsWith, "Set Nil Of self._"+componentInfo.name, true));
			}

			toInsertLineInfoList.Add("end".ToAutoGenLineInfo(cfgPartStartsWith, "DestroyUI End"));
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		public void GenAddUIEventsFunc()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("function {0}:AddUIEvents()", GetClassName()).ToAutoGenLineInfo(cfgPartStartsWith, "AddUIEvents Start"),
				string.Format("\t{0}.AddUIEvents(self)", GetParentClassName()).ToAutoGenLineInfo(),
			};
			bool isCell = Regex.IsMatch(this.prefab.name, "Cel") || Regex.IsMatch(prefab.name, "Btn");
			for (int i = 0; i < this.componentInfoList.Count; i++)
			{
				var componentInfo = componentInfoList[i];
				if (IsUIEventComponentType(componentInfo.type))
				{
					if (isCell && !componentInfo.type.Equals(typeof(Toggle).Name))
						toInsertLineInfoList.Add(string.Format(
							"\tself:RegisterOnClickWithoutDrag(self._{0}, function() self:OnClick{1}() end)",
							componentInfo.name, componentInfo.name).ToAutoGenLineInfo(cfgPartStartsWith, componentInfo.name + " Register UIEvent", true));
					else
						toInsertLineInfoList.Add(string.Format(
							"\tself:RegisterOnClick(self._{0}, function() self:OnClick{1}() end)",
							componentInfo.name, componentInfo.name).ToAutoGenLineInfo(cfgPartStartsWith, componentInfo.name + " Register UIEvent", true));
				}
			}

			toInsertLineInfoList.Add("end".ToAutoGenLineInfo(cfgPartStartsWith, "AddUIEvents End"));
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		public void GenOnClickFuncs()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>();
			for (int i = 0; i < this.componentInfoList.Count; i++)
			{
				var componentInfo = componentInfoList[i];
				if (IsUIEventComponentType(componentInfo.type))
				{
					toInsertLineInfoList.Clear();
					toInsertLineInfoList.Add(string.Format("--{0}响应函数", componentInfo.name).ToAutoGenLineInfo());
					toInsertLineInfoList.Add(string.Format("function {0}:OnClick{1}()", GetClassName(), componentInfo.name).ToAutoGenLineInfo(cfgPartStartsWith, "OnClick"+componentInfo.name + " Start"));
					toInsertLineInfoList.Add("end".ToAutoGenLineInfo(cfgPartStartsWith, "OnClick" + componentInfo.name + " End"));
					lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
						this.lineInfoList, lastCheckedLineIndex + 1);
				}
			}

			
		}

		public void GenAddLogicEvents()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("function {0}:AddLogicEvents()", GetClassName()).ToAutoGenLineInfo(cfgPartStartsWith, "AddLogicEvents Start"),
				string.Format("\t{0}.AddLogicEvents(self)", GetParentClassName()).ToAutoGenLineInfo(),
				"end".ToAutoGenLineInfo(cfgPartStartsWith, "AddLogicEvents End"),
			};
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		public void Gen_DestroyFunc()
		{
			List<AutoGenLineInfo> toInsertLineInfoList = new List<AutoGenLineInfo>
			{
				string.Format("function {0}:_Destroy()", GetClassName()).ToAutoGenLineInfo(cfgPartStartsWith, "_Destroy Start"),
				string.Format("\t{0}._Destroy(self)", GetParentClassName()).ToAutoGenLineInfo(),
				"end".ToAutoGenLineInfo(cfgPartStartsWith, "_Destroy End"),
			};
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(toInsertLineInfoList,
				this.lineInfoList, lastCheckedLineIndex + 1);
			GenNewLine();
		}

		public void GenReturn()
		{
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(string.Format("return {0}", GetClassName()).ToAutoGenLineInfo(),
				this.lineInfoList, lastCheckedLineIndex + 1);
		}

		public string GetParentClassName()
		{
			return IsPanel() ? "UIPanel" : "UIObject";
		}
	}
}