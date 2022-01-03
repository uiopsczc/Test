using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using CsCat;

public partial class CZMToolMenu
{
	[MenuItem(CZMToolConst.Menu_Root + "GroupGameObjects/Group打包(中心) %g")] //crtl+g
	public static void GroupGameObject_Center()
	{
		Object[] selectedObjects = Selection.objects;
		List<Transform> transformList = new List<Transform>();

		foreach (GameObject gameObject1 in selectedObjects)
		{
			bool isNeedToAdd = true;
			foreach (GameObject gameObject2 in selectedObjects)
			{
				if (gameObject1.transform.parent == gameObject2.transform)
				{
					isNeedToAdd = false;
					break;
				}
			}

			if (isNeedToAdd)
				transformList.Add(gameObject1.transform);
		}

		Vector3 min = Vector3Const.Max;
		Vector3 max = Vector3Const.Min;
		foreach (Transform transform in transformList)
			GetMinMax(transform, ref min, ref max);


		if (max == Vector3Const.Max)
		{
			LogCat.error("没有Meshes");
			return;
		}

		GameObject group = new GameObject();
		group.name = "Group";
		group.transform.position = (min + max) / 2;

		foreach (Transform transform in transformList)
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
			List<Vector3> boundsVectorList = GetBoundsVectorList(mesh.bounds);
			foreach (Vector3 boundsVector in boundsVectorList)
			{
				Vector3 worldBoundsVector = parent.localToWorldMatrix * boundsVector;
				worldBoundsVector = worldBoundsVector + parent.position;
				min = new Vector3(Mathf.Min(worldBoundsVector.x, min.x), Mathf.Min(worldBoundsVector.y, min.y),
					Mathf.Min(worldBoundsVector.z, min.z));
				max = new Vector3(Mathf.Max(worldBoundsVector.x, max.x), Mathf.Max(worldBoundsVector.y, max.y),
					Mathf.Max(worldBoundsVector.z, max.z));
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


	[MenuItem(CZMToolConst.Menu_Root + "GroupGameObjects/Group打包(非中心) &g")] //alt+g
	public static void GroupGameObject_NotCenter()
	{
		Object[] selectedObjects = Selection.objects;
		List<Transform> transformList = new List<Transform>();

		foreach (GameObject gameObject1 in selectedObjects)
		{
			bool isNeedToAdd = true;
			foreach (GameObject gameObject2 in selectedObjects)
			{
				if (gameObject1.transform.parent == gameObject2.transform)
				{
					isNeedToAdd = false;
					break;
				}
			}

			if (isNeedToAdd)
				transformList.Add(gameObject1.transform);
		}


		GameObject group = new GameObject();
		group.name = "Group";


		foreach (Transform transform in transformList)
			transform.parent = group.transform;

		LogCat.log("GroupGameObjects finished");
	}

	[MenuItem(CZMToolConst.Menu_Root + "GroupGameObjects/Group打包(解除Group) %&g")] //crtl+alt+g
	public static void GroupGameObject_DisGroup()
	{
		Object[] selectedObjects = Selection.objects;

		GameObject groupGameObject = selectedObjects[0] as GameObject;

		List<Transform> childrenTransformList = new List<Transform>();
		for (int i = 0; i < groupGameObject.transform.childCount; i++)
			childrenTransformList.Add(groupGameObject.transform.GetChild(i));

		Vector3 min = Vector3Const.Max;
		Vector3 max = Vector3Const.Min;
		GetMinMax(groupGameObject.transform, ref min, ref max);
		if (max == Vector3Const.Max)
		{
			LogCat.error("没有Meshes");
			return;
		}


		groupGameObject.transform.DetachChildren();

		groupGameObject.transform.position = (min + max) / 2;

		foreach (Transform transform in childrenTransformList)
			transform.parent = groupGameObject.transform;

		LogCat.log("GroupGameObjects finished");
	}
}