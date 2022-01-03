using UnityEditor;

namespace CsCat
{
	public class LuaAssetProcessEditor : AssetPostprocessor
	{
		static void OnPostprocessAllAssets(
			string[] importedAssets,
			string[] deletedAssets,
			string[] movedAssets,
			string[] movedFromAssetPaths)
		{
			for (var i = 0; i < importedAssets.Length; i++)
			{
				string path = importedAssets[i];
				if (path.Contains(".lua.txt"))
					return;
			}
		}
	}
}