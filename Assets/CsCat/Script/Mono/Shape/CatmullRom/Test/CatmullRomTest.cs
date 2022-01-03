using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace CsCat
{
	[ExecuteInEditMode]
	public class CatmullRomTest : MonoBehaviour, ISingleton
	{
		public static CatmullRomTest instance => SingletonFactory.instance.GetMono<CatmullRomTest>();

		public new Camera camera;
		public Material material;


		public float life_time = 0.2f; //存活时间
		public float height = 0.05f; //渲染出来的方块的高度
		public float width = 0.05f; //渲染出来的方块的宽度
		public float emission_distance = 0f; //增加Point的最小距离
		private float segment_width = 1; //为了保持图片原来的相对比例，segmentWidth=texture.width * height / texture.height;
		public int subdivision = 10; //每段CR曲线的取样点数
		public List<CatmullRomPoint> catmullRomPoint_list = new List<CatmullRomPoint>(); //组成整条CR曲线的关键点
		private Vector3 normal;
		public float dd;

		private Mesh catmullRom_mesh;
		private GameObject catmullRom_gameObject;
		private string catmullRom_gameObject_name = "CatmullRomGameObject";

		private float epsilon = 0.000001f;


		public void SingleInit()
		{
		}


		void OnEnable()
		{
			catmullRomPoint_list.Clear();
			catmullRom_gameObject = GameObject.Find(catmullRom_gameObject_name);
			if (catmullRom_gameObject == null)
			{
				catmullRom_gameObject = new GameObject();
				catmullRom_gameObject.name = catmullRom_gameObject_name;
			}

			catmullRom_mesh = new Mesh();
			MeshFilter meshFilter = catmullRom_gameObject.GetOrAddComponent<MeshFilter>();
			meshFilter.sharedMesh = catmullRom_mesh;


			MeshRenderer meshRender = catmullRom_gameObject.GetOrAddComponent<MeshRenderer>();
			meshRender.sharedMaterial = material;
			Texture texture = meshRender.sharedMaterial.mainTexture;
			meshRender.lightProbeUsage = LightProbeUsage.Off;
			meshRender.reflectionProbeUsage = ReflectionProbeUsage.Off;
			meshRender.shadowCastingMode = ShadowCastingMode.Off;
			meshRender.receiveShadows = false;


			segment_width = texture.width * height / texture.height;

			normal = -camera.transform.forward;
		}


		private CatmullRomPoint catmullRomPoint;
		private CatmullRomSpline spline;
		private List<Vector3> new_vertice_list = new List<Vector3>();
		private List<Vector2> new_uv_list = new List<Vector2>();
		private List<int> new_triangle_list = new List<int>();
		private CatmullRomPoint cur_point, cur_tangent, last_tangent;
		private List<Vector2> polygon_vertex_list = new List<Vector2>();

		void Update()
		{
			Vector3 current_point = this.transform.position;


			bool is_need_add_point = false;
			if (catmullRomPoint_list.Count < 2)
				is_need_add_point = true;
			else
			{
				float vertex_distance =
					(current_point - catmullRomPoint_list[catmullRomPoint_list.Count - 2].position).magnitude;
				if (vertex_distance > emission_distance)
					is_need_add_point = true;
			}


			if (is_need_add_point)
			{
				catmullRomPoint = new CatmullRomPoint(current_point, Time.realtimeSinceStartup);
				catmullRomPoint_list.Add(catmullRomPoint);
			}
			else
			{
				catmullRomPoint = catmullRomPoint_list[catmullRomPoint_list.Count - 1];
				catmullRomPoint.position = current_point;
				catmullRomPoint.created_time = Time.realtimeSinceStartup;
			}


			RemoveTimeOutPoints();
			if (catmullRomPoint_list.Count < 2)
			{
				catmullRom_mesh.Clear();
				return;
			}

			CatmullRomPoint[] catmullRomPoints = catmullRomPoint_list.ToArray();
			catmullRomPoints.Reverse();

			spline = new CatmullRomSpline(catmullRomPoints, subdivision);

			new_vertice_list.Clear();
			new_uv_list.Clear();
			new_triangle_list.Clear();


			//float splineLength = Mathf.Max(0, spline.GetLength()-0.1f);
			float spline_length = Mathf.Max(0, spline.length);

			int n = 0;
			bool is_angle_diff_big = false; //用于合并，当角度相差太大的时候，不进行合并

			float each_add_distance = 0.25f * width;
			//float eachAddDistance = 0.001f * width;
			for (float distance = 0;
				distance <= spline_length;
				distance = distance + each_add_distance > spline_length ? spline_length : distance + each_add_distance)
			{
				cur_point = spline.GetPointAtDistance(distance);
				cur_tangent = spline.GetTangentAtDistance(distance);

				Vector3 biNormal = CatmullRomSpline.ComputeBinormal(cur_tangent.position, normal);

				float timeSpan = Time.realtimeSinceStartup - cur_point.created_time;
				float lerp_height = height;


				//合并相邻两个切线率相差不多的segment
				//            if (n > 1)
				//            {
				//                float lastDistance = distance - width;
				//                lastTangent = spline.GetTangentAtDistance(lastDistance);
				//                float angle = Vector3.Angle(lastTangent.position, curTangent.position);
				//                if (angle <= 0.5f)
				//                {
				//                    if (!isAngleDiffBig)
				//                    {
				//                        for (int i = 0; i < 2; i++)
				//                        {
				//                            newVerticeList.RemoveLast();
				//                            newUVList.RemoveLast();
				//                        }
				//                        for (int i = 0; i < 6; i++)
				//                            newTriangleList.RemoveLast();
				//                        n--;
				//                    }
				//                    else
				//                        isAngleDiffBig = false;
				//                }
				//                else
				//                {
				//                    isAngleDiffBig = true;
				//                }
				//            }

				//需要添加epsilonAdd细小的offset，才能显示在mobile手机上，手机上刚好的在摄像机的nearClipPlane上是显示不出来的
				Vector3 epsilon_add = camera.transform.forward * epsilon;

				new_vertice_list.Add(cur_point.position - (biNormal * (lerp_height * 0.5f)) + epsilon_add);
				new_vertice_list.Add(cur_point.position + (biNormal * (lerp_height * 0.5f)) + epsilon_add);


				float lerp = 0;
				if (spline_length < width) //少于width的用正常lerp
					lerp = distance / spline_length;
				else //大于width的时候特殊处理
				{
					if (distance <= 0.5f * width) //少于0.5f * width的部分占全图片的50%
						lerp = 0.5f * (distance / (0.5f * width));
					else //线段的其他部分平分剩下图片的50%
						lerp = 0.5f + 0.5f * ((distance - 0.5f * width) / (spline_length - 0.5f * width));
				}


				new_uv_list.Add(new Vector2(lerp, 0));
				new_uv_list.Add(new Vector2(lerp, 1));

				if (n > 0)
				{
					new_triangle_list.Add((n * 2) - 2);
					new_triangle_list.Add((n * 2) - 1);
					new_triangle_list.Add(n * 2);

					new_triangle_list.Add((n * 2) + 1);
					new_triangle_list.Add(n * 2);
					new_triangle_list.Add((n * 2) - 1);
				}

				if (distance >= spline_length - dd)
					break;

				if (distance == spline_length)
					break;

				n++;
			}

			catmullRom_mesh.Clear();
			catmullRom_mesh.vertices = new_vertice_list.ToArray();
			catmullRom_mesh.uv = new_uv_list.ToArray();
			catmullRom_mesh.triangles = new_triangle_list.ToArray();
		}


		void RemoveTimeOutPoints()
		{
			List<CatmullRomPoint> to_remove_list = new List<CatmullRomPoint>();
			foreach (CatmullRomPoint point in catmullRomPoint_list)
			{
				if (Time.realtimeSinceStartup - point.created_time > life_time)
				{
					to_remove_list.Add(point);
				}
			}

			foreach (CatmullRomPoint point in to_remove_list)
			{
				catmullRomPoint_list.Remove(point);
			}
		}
	}
}