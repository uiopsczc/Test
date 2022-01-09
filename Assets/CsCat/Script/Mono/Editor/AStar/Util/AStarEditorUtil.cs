using UnityEngine;

namespace CsCat
{
	public static class AStarEditorUtil
	{
		public static void DrawObstacleTypeRect(AStarMonoBehaviour astarMonoBehaviour, int gridX, int gridY,
			int obstacleType)
		{
			Color obstacleTypeColor = AStarConst.AStarObstacleType_Dict[obstacleType].color;
			Rect cellRect = new Rect(0, 0, astarMonoBehaviour.astarConfigData.cellSize.x,
				astarMonoBehaviour.astarConfigData.cellSize.y);
			cellRect.position = astarMonoBehaviour.astarConfigData.GetPosition(gridX, gridY);
			DrawUtil.HandlesDrawSolidRectangleWithOutline(cellRect, obstacleTypeColor, Color.green,
				astarMonoBehaviour.transform);
		}

		public static void DrawdTerrainTypeRect(AStarMonoBehaviour astarMonoBehaviour, int gridX, int gridY,
			int terrainType)
		{
			Rect cellRect = new Rect(0, 0, astarMonoBehaviour.astarConfigData.cellSize.x,
				astarMonoBehaviour.astarConfigData.cellSize.y);
			cellRect.position = astarMonoBehaviour.astarConfigData.GetPosition(gridX, gridY);
			DrawUtil.HandlesDrawSolidRectangleWithOutline(cellRect, default(Color), Color.green,
				astarMonoBehaviour.transform);
			if (terrainType != 0)
				DrawUtil.HandlesDrawString(astarMonoBehaviour.transform.TransformPoint(cellRect.center),
					terrainType.ToString());
		}
	}
}