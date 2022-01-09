using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class NavMesh2dInputHandler : ISingleton
	{
		public bool isEnable = true;
		public List<Vector3> pointList = new List<Vector3>();
		public static NavMesh2dInputHandler instance => SingletonFactory.instance.Get<NavMesh2dInputHandler>();


		public void SingleInit()
		{
		}


		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
				OnClick();
		}


		void OnClick()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool intersect = Physics.Raycast(ray, out var hit);
			if (intersect)
			{
				GameObject hitTarget = hit.collider.gameObject;

				//点钟场景
				if (hitTarget.layer == LayerMask.NameToLayer(StringConst.String_Plane))
				{
					if (pointList.Count == 2)
					{
						pointList.Clear();
						pointList.Add(hit.point);
						Object.Instantiate(NavMesh2dTest.t, hit.point, Quaternion.identity);
					}
					else
					{
						pointList.Add(hit.point);
						Object.Instantiate(NavMesh2dTest.t, hit.point, Quaternion.identity);
					}

					return;
				}
			}
		}
	}
}