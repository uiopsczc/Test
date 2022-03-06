using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu_UI : MonoBehaviour
	{
		[MenuItem(CZMToolConst.Menu_Root + "UI/生成选中预设文件(lua)")]
		public static void GenCode()
		{
			UIAutoGen uiAutoGen = new UIAutoGenLua();
			_GenCode(uiAutoGen);
		}
		static void _GenCode(UIAutoGen uiAutoGen)
		{
			GameObject[] selectedGameObjects = Selection.GetFiltered<GameObject>(SelectionMode.Assets);
			if (!CheckPrefabs(selectedGameObjects))
				return;
			
			List<AutoGenComponentInfo> componentInfoList = new List<AutoGenComponentInfo>();

			for (int i = 0; i < selectedGameObjects.Length; ++i)
			{
				GameObject gameObject = selectedGameObjects[i];

				Debug.LogFormat("Start gen code of {0}", gameObject.name);

				string path = "";
				ParseGameObject(gameObject.transform, ref path, ref componentInfoList, true);


				string fileName = gameObject.name;

				string filePath = uiAutoGen.GetEditorUtilitySaveFilePanel(fileName);

				if (string.IsNullOrEmpty(filePath))
					continue;

				uiAutoGen.GenCode(gameObject, filePath, componentInfoList);
//				string uiPath = "Lua/ui";
//				string matchStr = string.Format("(.*){0}", uiPath);
//				var pathMatch = Regex.Match(filePath, matchStr);
//				var infoPrefabLua = pathMatch.Groups[1].Value + "Lua/config/info_prefab_logic.lua";
//				AddNewPrefab(infoPrefabLua, fileName, filePath, infoPrefabLua);
//
//				Debug.LogFormat("Gen code of {0} {1} {2}!", gameObject.name, ret ? "success" : "failed", filePath);
			}
		}

		static bool CheckPrefabs(GameObject[] prefabs)
		{
			if (prefabs == null || prefabs.Length == 0)
			{
				LogCat.error("请\"Assets/PatchResources/UI\"选中需要生成代码的Prefab!");
				return false;
			}
			if (Selection.assetGUIDs.LongLength <= 0)
			{
				LogCat.error("请到\"Assets/PatchResources/UI\" 目录下选中需要生成代码的Prefab!");
				return false;
			}

			for (int i = 0; i < prefabs.Length; i++)
			{
				var prefab = prefabs[i];
				var prefabPath = AssetDatabase.GetAssetPath(prefab);
				if (!CheckPrefabPath(prefabPath))
					return false;
			}
			return true;
		}

		static bool CheckPrefabPath(string prefabPath)
		{
			Regex regex = new Regex("Assets/PatchResources/UI/(.*).prefab");
			var result = regex.IsMatch(prefabPath);
			LogCat.log(">> prefabPath:" + prefabPath);
			if (!result)
			{
				Debug.LogError("请到\"Assets/PatchResources/UI\" 目录下选中需要生成代码的Prefab!");
				return false;
			}
			return true;
		}

		/// <summary>
		/// 递归分析每个对象;
		/// </summary>
		/// <param name="transform"></param>
		static void ParseGameObject(Transform transform, ref string path, ref List<AutoGenComponentInfo> componentInfoList, bool isRootInvoke = false)
		{
			string finallyPath = "";
			// 根对象
			if (!isRootInvoke)
				finallyPath = path + transform.name;

			Button button = transform.GetComponent<Button>();
			if (button != null)
				componentInfoList.Add(new AutoGenComponentInfo(button.name, "Button", finallyPath));

			UIButton uiButton = transform.GetComponent<UIButton>();
			if (uiButton != null)
				componentInfoList.Add(new AutoGenComponentInfo(uiButton.name, "UIButton", finallyPath));

			InputField inputField = transform.GetComponent<InputField>();
			if (inputField != null)
				componentInfoList.Add(new AutoGenComponentInfo(inputField.name, "InputField", finallyPath));

			Slider slider = transform.GetComponent<Slider>();
			if (slider != null)
				componentInfoList.Add(new AutoGenComponentInfo(slider.name, "Slider", finallyPath));

			Image image = transform.GetComponent<Image>();
			if (image != null && (image.name.Contains("ImgC_") || image.name.Contains("ProgC_")))
				componentInfoList.Add(new AutoGenComponentInfo(image.name, "Image", finallyPath));

//			ImageEx imgex = transform.GetComponent<ImageEx>();
//			if (imgex != null && (imgex.name.Contains("ImgC_") || imgex.name.Contains("ProgC_")))
//				componentInfoList.Add(new ComponentInfo(imgex.name, "UnityEngine.UI.ImageEx", finallyPath));

			Toggle toggle = transform.GetComponent<Toggle>();
			if (toggle != null)
				componentInfoList.Add(new AutoGenComponentInfo(toggle.name, "Toggle", finallyPath));

			Text text = transform.GetComponent<Text>();
			if (text != null && text.name.Contains("TxtC_"))
				componentInfoList.Add(new AutoGenComponentInfo(text.name, "Text", finallyPath));

			ScrollRect scrollRect = transform.GetComponent<ScrollRect>();
			if (scrollRect != null && scrollRect.name.Contains("SrlC_"))
				componentInfoList.Add(new AutoGenComponentInfo(scrollRect.name, "ScrollRect", finallyPath));

//			UIWidget.UILoopScrollRect loopRect = transform.GetComponent<UIWidget.UILoopScrollRect>();
//			if (loopRect != null && loopRect.name.Contains("SrlC_"))
//				componentInfoList.Add(new ComponentInfo(loopRect.name, "UIWidget.UILoopScrollRect", finallyPath));

			//需要响应点击事件的对象
			if (transform.name.Contains("Nego_"))
				componentInfoList.Add(new AutoGenComponentInfo(transform.name, "GameObject", finallyPath));

			Canvas canvas = transform.GetComponent<Canvas>();
			if (canvas != null)
				componentInfoList.Add(new AutoGenComponentInfo(canvas.name, "Canvas", finallyPath));

			string curPath = path;
			// 不是根对象
			if (!isRootInvoke)
				path += transform.name + "/";
			for (int i = 0; i < transform.childCount; ++i)
				ParseGameObject(transform.GetChild(i), ref path, ref componentInfoList);

			//路径回退;
			path = curPath;
		}

	}
}