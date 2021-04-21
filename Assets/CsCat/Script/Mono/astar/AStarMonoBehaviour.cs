
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
  [ExecuteInEditMode]
  public partial class AStarMonoBehaviour : MonoBehaviour
  {
    [SerializeField] public TextAsset textAsset;
    [SerializeField] public bool is_enable_edit_outside_bounds = false;
    [SerializeField] public Vector2 cell_size = new Vector2(0.32f, 0.32f);
    [SerializeField] public int min_grid_x;
    [SerializeField] public int min_grid_y;
    [SerializeField] public int max_grid_x;
    [SerializeField] public int max_grid_y;
    [SerializeField]
    public SerializableDictionary_Vector2Int_Int data_dict =
      SerializableDictionary_Vector2Int_Int.New<SerializableDictionary_Vector2Int_Int>();


    public Vector2 GetPosition(int grid_x, int grid_y)
    {
      return Vector2.Scale(new Vector2(grid_x, grid_y), cell_size);
    }

    public Vector3 GetWorldPosition(Vector2 position)
    {
      return transform.TransformPoint(position);
    }

    public Vector2Int GetGridXY(Vector2 position)
    {
      Vector2 result = Vector2.Scale(position, new Vector2(1 / cell_size.x, 1 / cell_size.y));
      return new Vector2Int(Mathf.FloorToInt(result.x), Mathf.FloorToInt(result.y));
    }

    public int GetGridX(Vector2 position)
    {
      return GetGridXY(position).x;
    }

    public int GetGridY(Vector2 position)
    {
      return GetGridXY(position).y;
    }

    public void Resize()
    {
      data_dict.dict.RemoveByFunc<Vector2Int, int>((key, value) =>
      {
        if (IsInRange(key.x, key.y))
          return false;
        return true;
      });
    }

    public bool IsInRange(Vector2Int xy)
    {
      return IsInRange(xy.x, xy.y);
    }

    public bool IsInRange(int x, int y)
    {
      if (x < min_grid_x || x > max_grid_x || y < min_grid_y || y > max_grid_y)
        return false;
      return true;
    }

    public void SetDataValue(Vector2Int xy, int value)
    {
      SetDataValue(xy.x, xy.y, value);
    }

    public void SetDataValue(int x, int y, int value)
    {
      bool is_need_resize = false;
      if (x < min_grid_x)
      {
        min_grid_x = x;
        is_need_resize = true;
      }

      if (x > max_grid_x)
      {
        max_grid_x = x;
        is_need_resize = true;
      }

      if (y < min_grid_y)
      {
        min_grid_y = y;
        is_need_resize = true;
      }

      if (y > max_grid_y)
      {
        max_grid_y = y;
        is_need_resize = true;
      }

      if (is_need_resize)
      {
        Resize();
      }

      data_dict[new Vector2Int(x, y)] = value;
    }



    public int GetDataValue(int x, int y)
    {
      Vector2Int xy = new Vector2Int(x, y);
      if (data_dict.ContainsKey(xy))
        return data_dict[xy];
      return AStarMonoBehaviourConst.Default_Data_Value;
    }

    public int GetDataValue(Vector2Int xy)
    {
      return GetDataValue(xy.x, xy.y);
    }

    public void Save()
    {
      Resize();
      Hashtable dict = new Hashtable();
      dict["cell_size"] = cell_size.ToString();
      dict["min_grid_x"] = min_grid_x;
      dict["min_grid_y"] = min_grid_y;
      dict["max_grid_x"] = max_grid_x;
      dict["max_grid_y"] = max_grid_y;
      dict["is_enable_edit_outside_bounds"] = is_enable_edit_outside_bounds;
      dict["data_dict"] = new Hashtable();
      foreach (var key in data_dict.Keys)
        ((Hashtable)dict["data_dict"])[key.ToString()] = data_dict[key];

      dict["position"] = this.transform.position.ToString();
      dict["eulerAngles"] = this.transform.eulerAngles.ToString();

#if UNITY_EDITOR
      string content = MiniJson.JsonEncode(dict);
      string file_path = textAsset.GetAssetPath().WithRootPath(FilePathConst.ProjectPath);
      StdioUtil.WriteTextFile(file_path, content);
      AssetDatabase.Refresh();
#endif
    }

    public void Load()
    {

      string content = textAsset.text;
      var dict = MiniJson.JsonDecode(content) as Hashtable;
      data_dict.Clear();
      cell_size = dict["cell_size"].To<string>().ToVector2();
      min_grid_x = dict["min_grid_x"].To<int>();
      min_grid_y = dict["min_grid_y"].To<int>();
      max_grid_x = dict["max_grid_x"].To<int>();
      max_grid_y = dict["max_grid_y"].To<int>();
      is_enable_edit_outside_bounds = dict["is_enable_edit_outside_bounds"].To<bool>();
      Hashtable _data_dict = dict["data_dict"].To<Hashtable>();
      foreach (var _key in _data_dict.Keys)
      {
        Vector2 v = _key.To<string>().ToVector2();
        Vector2Int key = new Vector2Int((int)v.x, (int)v.y);
        int value = _data_dict[_key].To<int>();
        data_dict[key] = value;
      }

      this.transform.position = dict["position"].To<string>().ToVector3();
      this.transform.eulerAngles = dict["eulerAngles"].To<string>().ToVector3();
    }
  }
}
