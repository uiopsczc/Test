using UnityEngine;

namespace CsCat
{
	public class SceneMapInfo
	{
		public int[][] grids; // 自身路径图
		public int[][] projectGrids; // 自身投影图
		public Vector2Int offsetPos; //偏移

		public SceneMapInfo(int[][] grids, int[][] projectGrids, int offsetPosX, int offsetPosY)
		{
			this.grids = grids;
			this.projectGrids = projectGrids;
			this.offsetPos = new Vector2Int(offsetPosX, offsetPosY);
		}
	}
}