//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.RegularExpressions;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;
//
//namespace CsCat
//{
//	/// <summary>
//	///   CZM工具菜单
//	/// </summary>
//	public partial class CZMToolMenu_UI : MonoBehaviour
//	{
//		[MenuItem(CZMToolConst.Menu_Root + "UI")]
//		public static void GenCode()
//		{
//			StartGenLuaCode();
//		}
//		static void StartGenLuaCode()
//		{
//			Object[] selectedGameObjects = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets);
//
//			if (selectedGameObjects == null || selectedGameObjects.Length == 0)
//			{
//				Debug.LogError("请\"Assets/PatchResources/UI\"选中需要生成代码的Prefab!");
//				return;
//			}
//			if (Selection.assetGUIDs.LongLength <= 0)
//			{
//				Debug.LogError("请到\"Assets/PatchResources/UI\" 目录下选中需要生成代码的Prefab!");
//				return;
//			}
//			string prefabPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
//			Regex regex = new Regex("Assets/PatchResources/UI/(.*).prefab");
//			var result = regex.IsMatch(prefabPath);
//			LogCat.log(">> prefabPath:" + prefabPath);
//			if (!result)
//			{
//				Debug.LogError("请到\"Assets/PatchResources/UI\" 目录下选中需要生成代码的Prefab!");
//				return;
//			}
//			List<ComponentInfo> componentInfoList = new List<ComponentInfo>();
//
//			for (int i = 0; i < selectedGameObjects.Length; ++i)
//			{
//				GameObject gameObject = (GameObject)selectedGameObjects[i];
//
//				Debug.LogFormat("Start gen code of {0}", gameObject.name);
//
//				string path = "";
//				ParseGameObject(gameObject.transform, ref path, ref componentInfoList, true);
//
//
//				string fileName = gameObject.name;
//
//				string filePath = EditorUtility.SaveFilePanel("Select Path Of " + fileName, "Assets/Lua/ui/", ConvertNameToLowerUnderline(fileName), "lua");
//
//				if (string.IsNullOrEmpty(filePath))
//					continue;
//
//				bool ret = GenCode(gameObject, filePath, ref componentInfoList);
//				string uiPath = "Lua/ui";
//				string matchStr = string.Format("(.*){0}", uiPath);
//				var pathMatch = Regex.Match(filePath, matchStr);
//				var infoPrefabLua = pathMatch.Groups[1].Value + "Lua/config/info_prefab_logic.lua";
//				AddNewPrefab(infoPrefabLua, fileName, filePath, infoPrefabLua);
//
//				Debug.LogFormat("Gen code of {0} {1} {2}!", gameObject.name, ret ? "success" : "failed", filePath);
//			}
//		}
//
//		/// <summary>
//		/// 递归分析每个对象;
//		/// </summary>
//		/// <param name="transform"></param>
//		static void ParseGameObject(Transform transform, ref string path, ref List<ComponentInfo> componentInfoList, bool isRootInvoke = false)
//		{
//			string finallyPath = "";
//			// 根对象
//			if (!isRootInvoke)
//				finallyPath = path + transform.name;
//
//			Button button = transform.GetComponent<Button>();
//			if (button != null)
//				componentInfoList.Add(new ComponentInfo(button.name, "Button", finallyPath));
//
//			UIButton uiButton = transform.GetComponent<UIButton>();
//			if (uiButton != null)
//				componentInfoList.Add(new ComponentInfo(uiButton.name, "UIButton", finallyPath));
//
//			InputField inputField = transform.GetComponent<InputField>();
//			if (inputField != null)
//				componentInfoList.Add(new ComponentInfo(inputField.name, "InputField", finallyPath));
//
//			Slider slider = transform.GetComponent<Slider>();
//			if (slider != null)
//				componentInfoList.Add(new ComponentInfo(slider.name, "Slider", finallyPath));
//
//			Image image = transform.GetComponent<Image>();
//			if (image != null && (image.name.Contains("ImgC_") || image.name.Contains("ProgC_")))
//				componentInfoList.Add(new ComponentInfo(image.name, "Image", finallyPath));
//
////			ImageEx imgex = transform.GetComponent<ImageEx>();
////			if (imgex != null && (imgex.name.Contains("ImgC_") || imgex.name.Contains("ProgC_")))
////				componentInfoList.Add(new ComponentInfo(imgex.name, "UnityEngine.UI.ImageEx", finallyPath));
//
//			Toggle toggle = transform.GetComponent<Toggle>();
//			if (toggle != null)
//				componentInfoList.Add(new ComponentInfo(toggle.name, "Toggle", finallyPath));
//
//			Text text = transform.GetComponent<Text>();
//			if (text != null && text.name.Contains("TxtC_"))
//				componentInfoList.Add(new ComponentInfo(text.name, "Text", finallyPath));
//
//			ScrollRect scrollRect = transform.GetComponent<ScrollRect>();
//			if (scrollRect != null && scrollRect.name.Contains("SrlC_"))
//				componentInfoList.Add(new ComponentInfo(scrollRect.name, "ScrollRect", finallyPath));
//
////			UIWidget.UILoopScrollRect loopRect = transform.GetComponent<UIWidget.UILoopScrollRect>();
////			if (loopRect != null && loopRect.name.Contains("SrlC_"))
////				componentInfoList.Add(new ComponentInfo(loopRect.name, "UIWidget.UILoopScrollRect", finallyPath));
//
//			//需要响应点击事件的对象
//			if (transform.name.Contains("Nego_"))
//				componentInfoList.Add(new ComponentInfo(transform.name, "GameObject", finallyPath));
//
//			Canvas canvas = transform.GetComponent<Canvas>();
//			if (canvas != null)
//				componentInfoList.Add(new ComponentInfo(canvas.name, "Canvas", finallyPath));
//
//			string curPath = path;
//			// 不是根对象
//			if (!isRootInvoke)
//				path += transform.name + "/";
//			for (int i = 0; i < transform.childCount; ++i)
//				ParseGameObject(transform.GetChild(i), ref path, ref componentInfoList);
//
//			//路径回退;
//			path = curPath;
//		}
//
//		static bool GenLuaCodeOfPanel(GameObject gameboejct, string saveToFilePath, ref List<ComponentInfo> componentInfoList, bool isUpdate = false)
//		{
//			// 如果文件已存在，则为更新模式，更新必要的内容
//			bool isExists = File.Exists(saveToFilePath);
//			Debug.Log("  saveTo is:" + saveToFilePath);
//			FileStream fileStream = new FileStream(saveToFilePath, isExists ? FileMode.Open : FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
//
//			StreamReader reader = new StreamReader(fileStream);
//			List<string> lineList = new List<string>();
//
//			string line = reader.ReadLine();
//			while (line != null)
//			{
//				lineList.Add(line);
//				line = reader.ReadLine();
//			}
//
//			reader.Close();
//
//			// 文件头声明
//
//			// 类声明
//			string className = gameboejct.name;
//			string classDeclare = string.Format("\nlocal {0} = Framework.Base.Class(ui.CUIBasePanel)", className);
//
//			int classIndex = StringUtilCat.CheckInsertLine(classDeclare, lineList.Count, lineList);
//
//			// 构造函数Init
//			List<string> funcInit = new List<string>
//			{
//				string.Format("\n{0}.Init = function(self, parent, prefabPath)", className),
//				"\tui.CUIBasePanel.Init(self, parent, prefabPath)",
//			};
//
//			int endIndex = GenLuaCodeOfFunction(ref funcInit, classIndex + 1, ref lineList);
//
//			if (endIndex < 0)
//				return false;
//
//
//			// 初始化UI控件函数_InitUI
//			List<string> funcInitUI = new List<string>
//			{
//				string.Format("\n{0}._InitUI = function(self, prefabPath)", className),
//				"\tself.gameObject = Framework.UI.LoadPrefab(prefabPath)",
//				"\tUTool.ConvertUIGameObject(self.gameObject)",
//				"\tlocal rootTransform = self.gameObject.transform\n",
//			};
//
//			var et = componentInfoList.GetEnumerator();
//
//			while (et.MoveNext())
//			{
//				if (et.Current.name.Contains("Nego_"))
//				{
//					funcInitUI.Add(string.Format("\tself._{0} = rootTransform:Find(\"{1}\")", et.Current.name, et.Current.path));
//				}
//				else
//					funcInitUI.Add(string.Format("\tself._{0} = rootTransform:Find(\"{1}\"):GetComponent(\"{2}\")", et.Current.name, et.Current.path, et.Current.type));
//			}
//
//			et.Dispose();
//
//			endIndex = GenLuaCodeOfFunction(ref funcInitUI, endIndex + 1, ref lineList, true);
//
//			if (endIndex < 0)
//				return false;
//
//			endIndex = StringUtilCat.CheckInsertLine(string.Format("\n{0}._InitUIEvent = function(self)", className), endIndex + 1, lineList);
//			if (endIndex < 0)
//				return false;
//
//			et = componentInfoList.GetEnumerator();
//
//			List<List<string>> funcList = new List<List<string>>();
//
//			bool isCell = Regex.IsMatch(gameboejct.name, "Cel") || Regex.IsMatch(gameboejct.name, "Btn");
//
//			while (et.MoveNext())
//			{
//				if (et.Current.type == typeof(UIButton).Name || (et.Current.type == typeof(Toggle).Name) || (et.Current.type == typeof(Button).Name))
//				{
//					string funcEventName = string.Format("\tself._{0}OnClick", et.Current.name);
//					string funcEvent = string.Format("\tself._{0}OnClick = self._{0}:AddListener(Framework.UI.UIEventPointerClick, function() self:On{0}Click() end)", et.Current.name);
//					if (isCell && et.Current.type != typeof(Toggle).Name)
//						funcEvent = string.Format("\tself._{0}OnClick = ui.tool.SetButtonClickWithoutDrag(self._{0}, function() self:On{0}Click() end)", et.Current.name);
//
//					int checkIndex = StringUtilCat.CheckInsertLine(funcEventName, endIndex + 1, lineList);
//					if (checkIndex == -1)
//						endIndex = StringUtilCat.CheckInsertLine(funcEvent, endIndex + 1, lineList);
//					else
//						endIndex = checkIndex;
//
//					if (string.Equals(et.Current.name, "BtnClose"))
//					{
//						funcList.Add(new List<string>{
//							string.Format("\n--{0}响应函数", et.Current.name),
//							string.Format("{0}.On{1}Click = function(self)", className, et.Current.name),
//							string.Format("\tui.CUIBasePanel.On{0}Click(self)", et.Current.name),
//						});
//					}
//					else
//					{
//						funcList.Add(new List<string>{
//							string.Format("\n--{0}响应函数", et.Current.name),
//							string.Format("{0}.On{1}Click = function(self)", className, et.Current.name),
//						});
//
//					}
//				}
//			}
//
//			et.Dispose();
//
//			endIndex = StringUtilCat.CheckInsertLine("end\n", endIndex + 1, lineList);
//			if (endIndex < 0)
//				return false;
//
//			if (!isUpdate)
//			{
//				funcList.Add(new List<string>{
//						string.Format("\n{0}.OnShow = function(self)", className),
//						"\t--TODO",
//					});
//				funcList.Add(new List<string>{
//						string.Format("\n{0}.OnOpenedFinish = function(self)", className),
//						"\t--TODO",
//					});
//				funcList.Add(new List<string>{
//						string.Format("\n{0}.OnHide = function(self)", className),
//						"\t--TODO",
//					});
//				funcList.Add(new List<string>{
//						string.Format("\n{0}.OnDestroy = function(self)", className),
//						"\tui.CUIBasePanel.OnDestroy(self)",
//					});
//			}
//			// 按顺序找到（或加入）函数内容
//			for (int i = 0; i < funcList.Count; i++)
//			{
//				List<string> codeLineList = funcList[i];
//				endIndex = GenLuaCodeOfFunction(ref codeLineList, endIndex + 1, ref lineList);
//			}
//
//			if (endIndex < 0)
//				return false;
//
//			// 结束返回
//			string endDeclare = string.Format("\nreturn {0}", className);
//			endIndex = StringUtilCat.CheckInsertLine(endDeclare, endIndex + 1, lineList);
//
//			if (endIndex < 0)
//				return false;
//
//			// 文件内容生成好后，写入文件。
//			StdioUtil.WriteTextFile(saveToFilePath, lineList);
//			return true;
//		}
//
//	}
//}