using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public abstract class UIAutoGen
	{
		protected List<AutoGenLineInfo> lineInfoList;
		protected int lastCheckedLineIndex = 0;
		protected GameObject prefab;
		protected string saveToFilePath;
		protected List<AutoGenComponentInfo> componentInfoList;

		protected virtual string cfgPartStartsWith { get; }

		public abstract string GetEditorUtilitySaveFilePanel(string fileName);

		public virtual void GenCode(GameObject prefab, string saveToFilePath,
			List<AutoGenComponentInfo> componentInfoList)
		{
			this.prefab = prefab;
			this.saveToFilePath = saveToFilePath;
			this.componentInfoList = componentInfoList;
			StdioUtil.CreateFileIfNotExist(saveToFilePath);
			LogCat.log("saveTo is:" + saveToFilePath);
			var lineList = StdioUtil.ReadAsLineList(saveToFilePath);
			this.lineInfoList =
				AutoGenLineInfoUtil.ToAutoGenLineInfoList(lineList, cfgPartStartsWith);
		}

		protected bool IsPanel()
		{
			return prefab.name.StartsWith("Pnl");
		}

		protected string GetClassName()
		{
			return prefab.name;
		}

		protected bool IsUIEventComponentType(string componentType)
		{
			return componentType.Equals(typeof(UIButton).Name) ||
			       componentType.Equals(typeof(Toggle).Name) ||
			       componentType.Equals(typeof(Button).Name);
		}

		public void GenNewLine()
		{
			var line = "";
			lastCheckedLineIndex = AutoGenLineInfoUtil.CheckInsert(line.ToAutoGenLineInfo(),
				this.lineInfoList, lastCheckedLineIndex + 1);
		}

	}
}