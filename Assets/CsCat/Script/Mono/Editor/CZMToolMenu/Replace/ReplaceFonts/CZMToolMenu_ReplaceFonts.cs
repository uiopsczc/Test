using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		public static string fontToReplacePath = Application.dataPath; //Application.dataPath+"/UI"

		public static Dictionary<string, Dictionary<string, string>> fontToReplaceDict
		{
			get
			{
				var result = new Dictionary<string, Dictionary<string, string>>();
				foreach (var fontToReplaceDict in _fontToReplaceDictList)
					result[fontToReplaceDict["old_fileId"]] = fontToReplaceDict;
				return result;
			}
		}

		private static ValueDictList<string, string> _fontToReplaceDictList = new ValueDictList<string, string>()
		{
			{
				new Dictionary<string, string>()
				{
					{"old_guid", ""}, {"old_fileId", ""}, {"old_type", "type:0"}, {"new_guid:", ""},
					{"new_fileId:", ""}, {"new_type", "type:3"}
				}
			},
			{
				new Dictionary<string, string>()
				{
					{"old_guid", ""}, {"old_fileId", ""}, {"old_type", "type:0"}, {"new_guid:", ""},
					{"new_fileId:", ""}, {"new_type", "type:3"}
				}
			},
		};


		[MenuItem(CZMToolConst.Menu_Root + "Relpace/Relpace Fonts")]
		public static void RelpaceFonts()
		{
			var rootPrefabPath = fontToReplacePath;
			if (Directory.Exists(rootPrefabPath))
			{
				string[] allPrefabPathes =
					Directory.GetFiles(rootPrefabPath, "*.prefab", SearchOption.AllDirectories);
				foreach (string prefabPath in allPrefabPathes)
				{
					bool isChanged = false;
					var lines = File.ReadAllLines(prefabPath);
					for (int i = 0; i < lines.Length; i++)
					{
						var line = lines[i];
						string matchedLineContent = MetaConst.Font_Regex.Match(line).Value;
						if (!matchedLineContent.IsNullOrEmpty())
						{
							string oldFiledId = MetaConst.FileID_Regex.Match(matchedLineContent).Value;
							string oldGUID = MetaConst.Guid_Regex.Match(matchedLineContent).Value;

							if (fontToReplaceDict.ContainsKey(oldFiledId) &&
							    oldGUID.Equals(fontToReplaceDict[oldFiledId]["old_guid"]))
							{
								isChanged = true;
								var dict = fontToReplaceDict[oldFiledId];
								lines[i] = Regex.Replace(lines[i], oldFiledId, dict["new_fileId"])
									.Replace(oldGUID, dict["new_guid"]).Replace(dict["old_type"], dict["new_type"]);
							}
						}
					}

					if (isChanged)
						File.WriteAllLines(prefabPath, lines);
				}

				AssetDatabase.Refresh();
				EditorUtilityCat.DisplayDialog("Relpace Fonts finished");
			}
		}
	}
}