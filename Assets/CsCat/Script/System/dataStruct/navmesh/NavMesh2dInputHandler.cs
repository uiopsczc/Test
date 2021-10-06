using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
    public class NavMesh2dInputHandler : ISingleton
    {

        public bool enable = true;
        public List<Vector3> point_list = new List<Vector3>();



        public static NavMesh2dInputHandler instance => SingletonFactory.instance.Get<NavMesh2dInputHandler>();


        public void SingleInit()
        {
        }


        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }


        void OnClick()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool intersect = Physics.Raycast(ray, out hit);
            if (intersect)
            {
                GameObject hit_target = hit.collider.gameObject;

                //点钟场景
                if (hit_target.layer == LayerMask.NameToLayer("Plane"))
                {
                    if (point_list.Count == 2)
                    {
                        point_list.Clear();
                        point_list.Add(hit.point);
                        GameObject.Instantiate(NavMesh2dTest.t, hit.point, Quaternion.identity);
                    }
                    else
                    {
                        point_list.Add(hit.point);
                        GameObject.Instantiate(NavMesh2dTest.t, hit.point, Quaternion.identity);
                    }

                    return;
                }
            }
        }

    }
}