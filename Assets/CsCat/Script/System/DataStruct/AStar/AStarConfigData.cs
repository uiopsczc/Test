using LitJson;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class AStarConfigData
	{
		private Transform _transform;
		private Vector3 _transformPosition;
		private Vector3 _transformEulerAngles;

		[SerializeField] public TextAsset textAsset;
		[SerializeField] public Vector2 cellSize = new Vector2(0.32f, 0.32f);
		[SerializeField] public int minGridX;
		[SerializeField] public int minGridY;
		[SerializeField] public int maxGridX;
		[SerializeField] public int maxGridY;
		[SerializeField] public bool isEnableEditOutsideBounds = false;

		[SerializeField]
		public SerializableDictionary_Vector2Int_Int dataDict =
		  SerializableDictionary_Vector2Int_Int.New<SerializableDictionary_Vector2Int_Int>();


		public void SetTransform(Transform transform)
		{
			this._transform = transform;
		}

		public Transform GetTransform(Transform transform)
		{
			return this._transform;
		}

		public Vector2 GetPosition(int pointXWithOffset, int pointYWithOffset)
		{
			return Vector2.Scale(new Vector2(pointXWithOffset, pointYWithOffset), cellSize);
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
			dataDict.dict.RemoveByFunc((key, value) => !IsInRange(key.x, key.y));
		}

		public bool IsInRange(Vector2Int pointWithOffset)
		{
			return IsInRange(pointWithOffset.x, pointWithOffset.y);
		}

		public bool IsInRange(int x, int y)
		{
			return x >= minGridX && x <= maxGridX && y >= minGridY && y <= maxGridY;
		}

		public void SetDataValue(Vector2Int pointWithOffset, int value)
		{
			SetDataValue(pointWithOffset.x, pointWithOffset.y, value);
		}

		public void SetDataValue(int xWithOffset, int yWithOffset, int value)
		{
			bool isNeedResize = false;
			if (xWithOffset < minGridX)
			{
				minGridX = xWithOffset;
				isNeedResize = true;
			}

			if (xWithOffset > maxGridX)
			{
				maxGridX = xWithOffset;
				isNeedResize = true;
			}

			if (yWithOffset < minGridY)
			{
				minGridY = yWithOffset;
				isNeedResize = true;
			}

			if (yWithOffset > maxGridY)
			{
				maxGridY = yWithOffset;
				isNeedResize = true;
			}

			if (isNeedResize)
			{
				Resize();
			}

			dataDict[new Vector2Int(xWithOffset, yWithOffset)] = value;
		}


		public int GetDataValue(int xWithOffset, int yWithOffset)
		{
			Vector2Int point = new Vector2Int(xWithOffset, yWithOffset);
			if (dataDict.ContainsKey(point))
				return dataDict[point];
			return AStarUtil.ToGridType(0, 0, 0);
		}

		public int GetDataValue(Vector2Int pointWithOffset)
		{
			return GetDataValue(pointWithOffset.x, pointWithOffset.y);
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
			foreach (var key in dataDict.Keys)
				((Hashtable)dict["data_dict"])[key.ToString()] = dataDict[key];

			dict["position"] = _transform.position.ToString();
			dict["eulerAngles"] = _transform.eulerAngles.ToString();

#if UNITY_EDITOR
			string content = JsonMapper.ToJson(dict);
			string filePath = Application.dataPath.Replace("Assets", "") + AssetDatabase.GetAssetPath(textAsset);
			var fileInfo = new FileInfo(filePath);
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
			dataDict.Clear();
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
				dataDict[key] = value;
			}

			_transformPosition = jsonData["position"].ToString().ToVector3();
			_transformEulerAngles = jsonData["eulerAngles"].ToString().ToVector3();
		}

		public void ResetTransformInfo()
		{
			_transform.position = _transformPosition;
			_transform.eulerAngles = _transformEulerAngles;
		}
	}
}