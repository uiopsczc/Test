using Boo.Lang;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public partial class AStarEditor
	{
		private int mouseGridX;
		private int mouseGridY;
		private bool isMouseGridChanged;
		private Vector2 localBrushPosition;
		private int selectedObstacleTypeIndex = 0;
		private bool isSeeObstacleType = true;
		private int selectedTerrainTypeIndex = 0;
		private bool isSeeTerrainType = true;
		private Event e;

		private AStarObstacleType selectedObstacleType => AStarConst.AStarObstacleTypeList[selectedObstacleTypeIndex];

		private AStarTerrainType selectedTerrainType => AStarConst.AStarTerrainType_List[selectedTerrainTypeIndex];


		void OnSceneGUI()
		{
			int controlId = GUIUtility.GetControlID(FocusType.Passive);
			HandleUtility.AddDefaultControl(controlId);
			UpdateLocalBrushPosition();
			UpdateMouseGrid();

			DrawBounds();
			DrawDataDict();
			_brush.DrawBrush(mouseGridX, mouseGridY, isSeeObstacleType, selectedObstacleType.value,
				isSeeTerrainType,
				selectedTerrainType.value);
			DrawInfo();
			DrawMainToolbar();
			DrawTips();
			HandleEvent();

			SceneView.RepaintAll();
		}

		void UpdateLocalBrushPosition()
		{
			Plane plane = new Plane(_target.transform.forward, _target.transform.position);
			Vector2 mousePosition = Event.current.mousePosition;
			mousePosition.y = Screen.height - mousePosition.y;
			Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			float distance;
			if (plane.Raycast(ray, out distance))
			{
				Rect cellRect = new Rect(0, 0, _target.astarConfigData.cellSize.x, _target.astarConfigData.cellSize.y);
				cellRect.position = _target.transform.InverseTransformPoint(ray.GetPoint(distance));

				Vector2 cellRectPosition = cellRect.position;
				if (cellRectPosition.x < 0)
					cellRectPosition.x -= _target.astarConfigData.cellSize.x; //因为负数是从-1开始的，正数是从0开始的
				if (cellRectPosition.y < 0)
					cellRectPosition.y -= _target.astarConfigData.cellSize.y; //因为负数是从-1开始的，正数是从0开始的
				cellRectPosition.x -= cellRectPosition.x % _target.astarConfigData.cellSize.x; //取整
				cellRectPosition.y -= cellRectPosition.y % _target.astarConfigData.cellSize.y; //取整
				cellRect.position = cellRectPosition;
				localBrushPosition = _target.transform.InverseTransformPoint(ray.GetPoint(distance));
			}
		}

		void DrawBounds()
		{
			for (int gridY = _target.astarConfigData.minGridY; gridY <= _target.astarConfigData.maxGridY; gridY++)
			{
				for (int gridX = _target.astarConfigData.minGridX;
					gridX <= _target.astarConfigData.maxGridX;
					gridX++)
				{
					Rect cellRect = new Rect(0, 0, _target.astarConfigData.cellSize.x,
						_target.astarConfigData.cellSize.y);
					cellRect.position = _target.astarConfigData.GetPosition(gridX, gridY);
					DrawUtil.HandlesDrawSolidRectangleWithOutline(cellRect, default(Color), Color.white,
						_target.transform);
				}
			}
		}

		void DrawDataDict()
		{
			for (int gridY = _target.astarConfigData.minGridY; gridY <= _target.astarConfigData.maxGridY; gridY++)
			{
				for (int gridX = _target.astarConfigData.minGridX;
					gridX <= _target.astarConfigData.maxGridX;
					gridX++)
				{
					int value = _target.astarConfigData.GetDataValue(gridX, gridY);
					int obstacleType = AStarUtil.GetObstacleType(value);
					int terrainType = AStarUtil.GetTerrainType(value);
					//draw obstacleType
					if (this.isSeeObstacleType && obstacleType == AStarConst.Default_Obstacle_Type_Value)
						AStarEditorUtil.DrawObstacleTypeRect(_target, gridX, gridY, obstacleType);
					// draw terrainType
					if (this.isSeeTerrainType && obstacleType == AStarConst.Default_Terrain_Type_Value)
						AStarEditorUtil.DrawdTerrainTypeRect(_target, gridX, gridY, terrainType);
				}
			}
		}


		void UpdateMouseGrid()
		{
			e = Event.current;
			int preMouseGridX = mouseGridX;
			int preMouseGridY = mouseGridY;
			if (e.isMouse)
			{
				mouseGridX = _target.astarConfigData.GetPointXWithOffset(localBrushPosition);
				mouseGridY = _target.astarConfigData.GetPointYWithOffset(localBrushPosition);
			}

			isMouseGridChanged = preMouseGridX != mouseGridX || preMouseGridY != mouseGridY;
		}


		void DrawInfo()
		{
			string info = "<b>Grid:(" + mouseGridX + "," + mouseGridY + ") </b>";
			GUIContent infoGUIContent = info.ToGUIContent();
			Rect infoRect = new Rect(new Vector2(4f, 4f), GUIStyleConst.ToolbarBoxStyle.CalcSize(infoGUIContent));
			using (new HandlesBeginGUIScope())
			{
				using (new GUILayoutBeginAreaScope(infoRect))
				{
					DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, infoRect.size),
						new Color(0, 0, 1, 0.2f), Color.black);
					GUILayout.Label(info, GUIStyleConst.ToolbarBoxStyle);
				}
			}
		}


		void DrawMainToolbar()
		{
			using (new HandlesBeginGUIScope())
			{
				using (new GUILayoutBeginAreaScope(new Rect(120, 4, Screen.width - 40, 80)))
				{
					using (new GUILayoutBeginHorizontalScope())
					{
						DrawObstacleTypeTool();
						GUILayout.Space(20);
						DrawTerrainTypeTool();
					}
				}
			}
		}


		void DrawObstacleTypeTool()
		{
			List<GUIContent> obstacleType_guiContent_list = new List<GUIContent>();
			foreach (var astarObstacleType in AStarConst.AStarObstacleTypeList)
			{
				GUIContent guiContent = new GUIContent();
				guiContent.text = astarObstacleType.name;
				guiContent.image = astarObstacleType.GetColorImage();
				obstacleType_guiContent_list.Add(guiContent);
			}

			isSeeObstacleType =
				EditorGUILayout.Toggle(isSeeObstacleType, new GUIStyle("Toggle"), GUILayout.Width(10));
			selectedObstacleTypeIndex = EditorGUILayout.Popup(selectedObstacleTypeIndex,
				obstacleType_guiContent_list.ToArray(), new GUIStyle("Popup"), GUILayout.Width(80));
		}

		void DrawTips()
		{
			GUIContent infoGUIContent = "按住Ctrl进行绘制".ToGUIContent();
			Rect infoRect = new Rect(new Vector2(230, 6), GUIStyleConst.ToolbarBoxStyle.CalcSize(infoGUIContent));
			using (new HandlesBeginGUIScope())
			{
				using (new GUILayoutBeginAreaScope(infoRect))
				{
					DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, infoRect.size),
						new Color(1, 1, 0, 1f), Color.black);
					using (new GUIColorScope(Color.black))
					{
						GUILayout.Label(infoGUIContent, GUIStyleConst.ToolbarBoxStyle);
					}
				}
			}
		}

		void DrawTerrainTypeTool()
		{
			List<GUIContent> terrainTypeGUIContentList = new List<GUIContent>();
			foreach (var astarTerrainType in AStarConst.AStarTerrainType_List)
			{
				GUIContent guiContent = new GUIContent();
				guiContent.text = string.Format("{0}:{1}", astarTerrainType.value, astarTerrainType.name);
				terrainTypeGUIContentList.Add(guiContent);
			}

			isSeeTerrainType =
				EditorGUILayout.Toggle(isSeeTerrainType, new GUIStyle("Toggle"), GUILayout.Width(10));
			selectedTerrainTypeIndex = EditorGUILayout.Popup(selectedTerrainTypeIndex,
				terrainTypeGUIContentList.ToArray(), new GUIStyle("Popup"), GUILayout.Width(80));
		}


		void HandleEvent()
		{
			if (e.control && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0) //press
			{
				if (!_target.astarConfigData.IsInRange(mouseGridX, mouseGridY) &&
				    !_target.astarConfigData.isEnableEditOutsideBounds)
					return;
				int orgValue = _target.astarConfigData.GetDataValue(mouseGridX, mouseGridY);
				int obstacleType = AStarUtil.GetObstacleType(orgValue);
				int terrainType = AStarUtil.GetTerrainType(orgValue);
				if (isSeeObstacleType)
					obstacleType = this.selectedObstacleType.value;
				if (isSeeTerrainType)
					terrainType = this.selectedTerrainType.value;
				int value = AStarUtil.ToGridType(0, terrainType, obstacleType);
				_brush.DoPaintPressed(mouseGridX, mouseGridY, value);
			}
		}
	}
}