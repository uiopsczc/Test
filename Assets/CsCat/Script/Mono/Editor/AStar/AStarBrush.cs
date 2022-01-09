using UnityEditor;

namespace CsCat
{
	public class AStarBrush
	{
		public AStarMonoBehaviour astarMonoBehaviour;


		public void DoPaintPressed(int mouseGridX, int mouseGridY, int value)
		{
			Paint(mouseGridX, mouseGridY, value);
		}

		public void Paint(int mouseGridX, int mouseGridY, int value)
		{
#if UNITY_EDITOR
			Undo.RegisterCompleteObjectUndo(astarMonoBehaviour, "UnDo_AStar");
#endif
			astarMonoBehaviour.astarConfigData.SetDataValue(mouseGridX, mouseGridY, value);
		}

		public void DrawBrush(int mouseGridX, int mouseGridY, bool isSeeObstacleType, int obstacleType,
			bool isSeeTerrainType, int terrainType)
		{
			if (isSeeObstacleType)
				AStarEditorUtil.DrawObstacleTypeRect(astarMonoBehaviour, mouseGridX, mouseGridY, obstacleType);
			if (isSeeTerrainType)
				AStarEditorUtil.DrawdTerrainTypeRect(astarMonoBehaviour, mouseGridX, mouseGridY, terrainType);
		}
	}
}