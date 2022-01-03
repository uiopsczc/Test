using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class CZMToolMenu
	{
		[MenuItem(CZMToolConst.Menu_Root + "AStar/创建AStar编辑器")]
		public static void CreateAStar()
		{
			GameObject gameObject = new GameObject("astar");
			gameObject.AddComponent<AStarMonoBehaviour>();
			Selection.activeGameObject = gameObject;
		}
	}
}