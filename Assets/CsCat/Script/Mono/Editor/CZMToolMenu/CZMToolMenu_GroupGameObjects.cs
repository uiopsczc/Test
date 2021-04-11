using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using CsCat;

public partial class CZMToolMenu
{
  [MenuItem(CZMToolConst.MenuRoot + "GroupGameObjects/Group打包(中心) %g")] //crtl+g
  public static void GroupGameObject_Center()
  {
    Object[] selected_objects = Selection.objects;
    List<Transform> transform_list = new List<Transform>();

    foreach (GameObject gameObject1 in selected_objects)
    {
      bool is_need_to_add = true;
      foreach (GameObject gameObject2 in selected_objects)
      {
        if (gameObject1.transform.parent == gameObject2.transform)
        {
          is_need_to_add = false;
          break;
        }
      }

      if (is_need_to_add)
        transform_list.Add(gameObject1.transform);
    }

    Vector3 min = Vector3Const.Max;
    Vector3 max = Vector3Const.Min;
    foreach (Transform transform in transform_list)
      GetMinMax(transform, ref min, ref max);

    
    if (max == Vector3Const.Max)
    {
      LogCat.error("没有Meshes");
      return;
    }

    GameObject group = new GameObject();
    group.name = "Group";
    group.transform.position = (min + max) / 2;

    foreach (Transform transform in transform_list)
      transform.parent = group.transform;

    LogCat.log("GroupGameObjects finished");
  }

  static void GetMinMax(Transform parent, ref Vector3 min, ref Vector3 max)
  {
    Mesh mesh = null;
    if (parent.GetComponent<MeshFilter>() != null && parent.GetComponent<MeshFilter>().sharedMesh != null)
      mesh = parent.GetComponent<MeshFilter>().sharedMesh;


    if (mesh != null)
    {
      List<Vector3> bounds_vector_list = GetBoundsVectorList(mesh.bounds);
      foreach (Vector3 bounds_vector in bounds_vector_list)
      {
        Vector3 world_bounds_vector = parent.localToWorldMatrix * bounds_vector;
        world_bounds_vector = world_bounds_vector + parent.position;
        min = new Vector3(Mathf.Min(world_bounds_vector.x, min.x), Mathf.Min(world_bounds_vector.y, min.y),
          Mathf.Min(world_bounds_vector.z, min.z));
        max = new Vector3(Mathf.Max(world_bounds_vector.x, max.x), Mathf.Max(world_bounds_vector.y, max.y),
          Mathf.Max(world_bounds_vector.z, max.z));
      }
    }

    if (parent.childCount == 0)
      return;
    for (int i = 0; i < parent.childCount; i++)
      GetMinMax(parent.GetChild(i), ref min, ref max);
  }

  static List<Vector3> GetBoundsVectorList(Bounds bounds)
  {
    List<Vector3> list = new List<Vector3>();
    Vector3 min = bounds.min;
    Vector3 max = bounds.max;
    list.Add(new Vector3(min.x, min.y, min.z));
    list.Add(new Vector3(min.x, min.y, max.z));
    list.Add(new Vector3(min.x, max.y, min.z));
    list.Add(new Vector3(min.x, max.y, max.z));

    list.Add(new Vector3(max.x, min.y, min.z));
    list.Add(new Vector3(max.x, min.y, max.z));
    list.Add(new Vector3(max.x, max.y, min.z));
    list.Add(new Vector3(max.x, max.y, max.z));

    return list;
  }


  [MenuItem(CZMToolConst.MenuRoot + "GroupGameObjects/Group打包(非中心) &g")] //alt+g
  public static void GroupGameObject_NotCenter()
  {
    Object[] selected_objects = Selection.objects;
    List<Transform> transform_list = new List<Transform>();

    foreach (GameObject gameObject1 in selected_objects)
    {
      bool is_need_to_add = true;
      foreach (GameObject gameObject2 in selected_objects)
      {
        if (gameObject1.transform.parent == gameObject2.transform)
        {
          is_need_to_add = false;
          break;
        }
      }

      if (is_need_to_add)
        transform_list.Add(gameObject1.transform);
    }


    GameObject group = new GameObject();
    group.name = "Group";


    foreach (Transform transform in transform_list)
      transform.parent = group.transform;

    LogCat.log("GroupGameObjects finished");
  }

  [MenuItem(CZMToolConst.MenuRoot + "GroupGameObjects/Group打包(解除Group) %&g")] //crtl+alt+g
  public static void GroupGameObject_DisGroup()
  {
    Object[] selected_objects = Selection.objects;

    GameObject group_gameObject = selected_objects[0] as GameObject;

    List<Transform> children_transform_list = new List<Transform>();
    for (int i = 0; i < group_gameObject.transform.childCount; i++)
      children_transform_list.Add(group_gameObject.transform.GetChild(i));

    Vector3 min = Vector3Const.Max;
    Vector3 max = Vector3Const.Min;
    GetMinMax(group_gameObject.transform, ref min, ref max);
    if (max == Vector3Const.Max)
    {
      LogCat.error("没有Meshes");
      return;
    }


    group_gameObject.transform.DetachChildren();

    group_gameObject.transform.position = (min + max) / 2;

    foreach (Transform transform in children_transform_list)
      transform.parent = group_gameObject.transform;

    LogCat.log("GroupGameObjects finished");
  }
}