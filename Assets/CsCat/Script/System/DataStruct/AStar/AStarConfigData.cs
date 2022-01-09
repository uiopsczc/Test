using LitJson;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class AStarConfigData
	{
		private Transform transform;
		private Vector3 transform_position;
		private Vector3 transform_eulerAngles;

		[SerializeField] public TextAsset textAsset;
		[SerializeField] public Vector2 cellSize = new Vector2(0.32f, 0.32f);
		[SerializeField] public int minGridX;
		[SerializeField] public int minGridY;
		[SerializeField] public int maxGridX;
		[SerializeField] public int maxGridY;
		[SerializeField] public bool isEnableEditOutsideBounds = false;

		[SerializeField]
		public SerializableDictionary_Vector2Int_Int data_dict =
		  SerializableDictionary_Vector2Int_Int.New<SerializableDictionary_Vector2Int_Int>();


		public void SetTransform(Transform transform)
		{
			this.transform = transform;
		}

		public Transform GetTransform(Transform transform)
		{
			return this.transform;
		}

		public Vector2 GetPosition(int point_x_with_offset, int point_y_with_offset)
		{
			return Vector2.Scale(new Vector2(point_x_with_offset, point_y_with_offset), cellSize);
		}


		public Vector2Int GetPointWithOffset(Vector2 position)
		{
			Vector2 result = Vector2.Scale(position, new Vector2(1 / cellSize.x, 1 / cellSize.y));
			return new Vector2Int(Mathf.FloorToInt(result.x), Mathf.FloorToInt(result.y));
		}

		public int GetPointXWithOffset(Vector2 position)
		{
			return GetPointWithOffset(position).x;
		}

		public int GetPointYWithOffset(Vector2 position)
		{
			return GetPointWithOffset(position).y;
		}

		public void Resize()
		{
			data_dict.dict.RemoveByFunc((key, value) =>
			{
				if (IsInRange(key.x, key.y))
					return false;
				return true;
			});
		}

		public bool IsInRange(Vector2Int pointWithOffset)
		{
			return IsInRange(pointWithOffset.x, pointWithOffset.y);
		}

		public bool IsInRange(int x, int y)
		{
			if (x < minGridX || x > maxGridX || y < minGridY || y > maxGridY)
				return false;
			return true;
		}

		public void SetDataValue(Vector2Int pointWithOffset, int value)
		{
			SetDataValue(pointWithOffset.x, pointWithOffset.y, value);
		}

		public void SetDataValue(int x_with_offset, int y_with_offset, int value)
		{
			bool is_need_resize = false;
			if (x_with_offset < minGridX)
			{
				minGridX = x_with_offset;
				is_need_resize = true;
			}

			if (x_with_offset > maxGridX)
			{
				maxGridX = x_with_offset;
				is_need_resize = true;
			}

			if (y_with_offset < minGridY)
			{
				minGridY = y_with_offset;
				is_need_resize = true;
			}

			if (y_with_offset > maxGridY)
			{
				maxGridY = y_with_offset;
				is_need_resize = true;
			}

			if (is_need_resize)
			{
				Resize();
			}

			data_dict[new Vector2Int(x_with_offset, y_with_offset)] = value;
		}


		public int GetDataValue(int x_with_offset, int y_with_offset)
		{
			Vector2Int point = new Vector2Int(x_with_offset, y_with_offset);
			if (data_dict.ContainsKey(point))
				return data_dict[point];
			return AStarUtil.ToGridType(0, 0, 0);
		}

		public int GetDataValue(Vector2Int point_with_offset)
		{
			return GetDataValue(point_with_offset.x, point_with_offset.y);
		}

		//返回的grids已经是可以直接给astar使用
		//    public (int[][], Vector2Int) GetGridsInfo()
		//    {
		//      Vector2Int offset = new Vector2Int(min_grid_x, min_grid_y);
		//      int width = max_grid_x - min_grid_x + 1;
		//      int height = max_grid_y - min_grid_y + 1;
		//      int[][] grids = new int[width][];
		//      for (int x = 0; x < width; x++)
		//      {
		//        grids[x] = new int[height];
		//        for (int y = 0; y < height; y++)
		//        {
		//          var xy_with_offset = new Vector2Int(x, y) + offset;
		//          if (data_dict.ContainsKey(xy_with_offset))
		//            grids[x][y] = data_dict[xy_with_offset];
		//        }
		//      }
		//
		//      return (grids, offset);
		//    }

		public void Save()
		{
			Resize();
			Hashtable dict = new Hashtable();
			dict["cell_size"] = cellSize.ToString();
			dict["min_grid_x"] = minGridX;
			dict["min_grid_y"] = minGridY;
			dict["max_grid_x"] = maxGridX;
			dict["max_grid_y"] = maxGridY;
			dict["is_enable_edit_outside_bounds"] = isEnableEditOutsideBounds;
			dict["data_dict"] = new Hashtable();
			foreach (var key in data_dict.Keys)
				((Hashtable)dict["data_dict"])[key.ToString()] = data_dict[key];

			dict["position"] = transform.position.ToString();
			dict["eulerAngles"] = transform.eulerAngles.ToString();

#if UNITY_EDITOR
			string content = JsonMapper.ToJson(dict);
			string file_path = Application.dataPath.Replace("Assets", "") + AssetDatabase.GetAssetPath(textAsset);
			var fileInfo = new FileInfo(file_path);
			var fw = new StreamWriter(fileInfo.FullName, false);
			try
			{
				fw.WriteLine(content);
				fw.Flush();
			}
			finally
			{
				fw.Close();
			}

			AssetDatabase.Refresh();
#endif
		}

		public void LoadFromFilePath(string filePath)
		{
			Client.instance.assetBundleManager.LoadAssetAsync(filePath, (assetCat) =>
			{
				textAsset = assetCat.Get<TextAsset>();
				Load();
			});
		}

		public void Load()
		{
			string content = textAsset.text;
			var jsonData = JsonMapper.ToObject(content);
			data_dict.Clear();
			cellSize = jsonData["cell_size"].ToString().ToVector2();
			minGridX = int.Parse(jsonData["min_grid_x"].ToString());
			minGridY = int.Parse(jsonData["min_grid_y"].ToString());
			;
			maxGridX = int.Parse(jsonData["max_grid_x"].ToString());
			;
			maxGridY = int.Parse(jsonData["max_grid_y"].ToString());
			;
			isEnableEditOutsideBounds = bool.Parse(jsonData["is_enable_edit_outside_bounds"].ToString());
			foreach (var _key in jsonData["data_dict"].Keys)
			{
				Vector2 v = _key.ToVector2();
				Vector2Int key = new Vector2Int((int)v.x, (int)v.y);
				int value = int.Parse(jsonData["data_dict"][_key].ToString());
				data_dict[key] = value;
			}

			transform_position = jsonData["position"].ToString().ToVector3();
			transform_eulerAngles = jsonData["eulerAngles"].ToString().ToVector3();
		}

		public void ResetTransformInfo()
		{
			transform.position = transform_position;
			transform.eulerAngles = transform_eulerAngles;
		}
	}
}