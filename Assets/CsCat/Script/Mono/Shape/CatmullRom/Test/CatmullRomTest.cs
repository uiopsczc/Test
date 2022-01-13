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
		public float emissionDistance = 0f; //增加Point的最小距离
		private float segmentWidth = 1; //为了保持图片原来的相对比例，segmentWidth=texture.width * height / texture.height;
		public int subdivision = 10; //每段CR曲线的取样点数
		public List<CatmullRomPoint> catmullRomPointList = new List<CatmullRomPoint>(); //组成整条CR曲线的关键点
		private Vector3 normal;
		public float dd;

		private Mesh catmullRomMesh;
		private GameObject catmullRomGameObject;
		private string catmullRomGameObjectName = "CatmullRomGameObject";

		private float epsilon = 0.000001f;


		public void SingleInit()
		{
		}


		void OnEnable()
		{
			catmullRomPointList.Clear();
			catmullRomGameObject = GameObject.Find(catmullRomGameObjectName);
			if (catmullRomGameObject == null)
			{
				catmullRomGameObject = new GameObject();
				catmullRomGameObject.name = catmullRomGameObjectName;
			}

			catmullRomMesh = new Mesh();
			MeshFilter meshFilter = catmullRomGameObject.GetOrAddComponent<MeshFilter>();
			meshFilter.sharedMesh = catmullRomMesh;


			MeshRenderer meshRender = catmullRomGameObject.GetOrAddComponent<MeshRenderer>();
			meshRender.sharedMaterial = material;
			Texture texture = meshRender.sharedMaterial.mainTexture;
			meshRender.lightProbeUsage = LightProbeUsage.Off;
			meshRender.reflectionProbeUsage = ReflectionProbeUsage.Off;
			meshRender.shadowCastingMode = ShadowCastingMode.Off;
			meshRender.receiveShadows = false;


			segmentWidth = texture.width * height / texture.height;

			normal = -camera.transform.forward;
		}


		private CatmullRomPoint catmullRomPoint;
		private CatmullRomSpline spline;
		private List<Vector3> newVerticeList = new List<Vector3>();
		private List<Vector2> newUVList = new List<Vector2>();
		private List<int> newTriangleList = new List<int>();
		private CatmullRomPoint curPoint, curTangent, lastTangent;
		private List<Vector2> polygonVertexList = new List<Vector2>();

		void Update()
		{
			Vector3 currentPoint = this.transform.position;


			bool isNeedAddPoint = false;
			if (catmullRomPointList.Count < 2)
				isNeedAddPoint = true;
			else
			{
				float vertexDistance =
					(currentPoint - catmullRomPointList[catmullRomPointList.Count - 2].position).magnitude;
				if (vertexDistance > emissionDistance)
					isNeedAddPoint = true;
			}


			if (isNeedAddPoint)
			{
				catmullRomPoint = new CatmullRomPoint(currentPoint, Time.realtimeSinceStartup);
				catmullRomPointList.Add(catmullRomPoint);
			}
			else
			{
				catmullRomPoint = catmullRomPointList[catmullRomPointList.Count - 1];
				catmullRomPoint.position = currentPoint;
				catmullRomPoint.createTime = Time.realtimeSinceStartup;
			}


			RemoveTimeOutPoints();
			if (catmullRomPointList.Count < 2)
			{
				catmullRomMesh.Clear();
				return;
			}

			CatmullRomPoint[] catmullRomPoints = catmullRomPointList.ToArray();
			catmullRomPoints.Reverse();

			spline = new CatmullRomSpline(catmullRomPoints, subdivision);

			newVerticeList.Clear();
			newUVList.Clear();
			newTriangleList.Clear();


			//float splineLength = Mathf.Max(0, spline.GetLength()-0.1f);
			float splineLength = Mathf.Max(0, spline.length);

			int n = 0;
			bool isAngleDiffBig = false; //用于合并，当角度相差太大的时候，不进行合并

			float eachAddDistance = 0.25f * width;
			//float eachAddDistance = 0.001f * width;
			for (float distance = 0;
				distance <= splineLength;
				distance = distance + eachAddDistance > splineLength ? splineLength : distance + eachAddDistance)
			{
				curPoint = spline.GetPointAtDistance(distance);
				curTangent = spline.GetTangentAtDistance(distance);

				Vector3 biNormal = CatmullRomSpline.ComputeBinormal(curTangent.position, normal);

				float timeSpan = Time.realtimeSinceStartup - curPoint.createTime;
				float lerpHeight = height;


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
				Vector3 epsilonAdd = camera.transform.forward * epsilon;

				newVerticeList.Add(curPoint.position - (biNormal * (lerpHeight * 0.5f)) + epsilonAdd);
				newVerticeList.Add(curPoint.position + (biNormal * (lerpHeight * 0.5f)) + epsilonAdd);


				float lerp = 0;
				if (splineLength < width) //少于width的用正常lerp
					lerp = distance / splineLength;
				else //大于width的时候特殊处理
				{
					if (distance <= 0.5f * width) //少于0.5f * width的部分占全图片的50%
						lerp = 0.5f * (distance / (0.5f * width));
					else //线段的其他部分平分剩下图片的50%
						lerp = 0.5f + 0.5f * ((distance - 0.5f * width) / (splineLength - 0.5f * width));
				}


				newUVList.Add(new Vector2(lerp, 0));
				newUVList.Add(new Vector2(lerp, 1));

				if (n > 0)
				{
					newTriangleList.Add((n * 2) - 2);
					newTriangleList.Add((n * 2) - 1);
					newTriangleList.Add(n * 2);

					newTriangleList.Add((n * 2) + 1);
					newTriangleList.Add(n * 2);
					newTriangleList.Add((n * 2) - 1);
				}

				if (distance >= splineLength - dd)
					break;

				if (distance == splineLength)
					break;

				n++;
			}

			catmullRomMesh.Clear();
			catmullRomMesh.vertices = newVerticeList.ToArray();
			catmullRomMesh.uv = newUVList.ToArray();
			catmullRomMesh.triangles = newTriangleList.ToArray();
		}


		void RemoveTimeOutPoints()
		{
			List<CatmullRomPoint> toRemoveList = new List<CatmullRomPoint>();
			for (var i = 0; i < catmullRomPointList.Count; i++)
			{
				CatmullRomPoint point = catmullRomPointList[i];
				if (Time.realtimeSinceStartup - point.createTime > life_time)
					toRemoveList.Add(point);
			}

			for (var i = 0; i < toRemoveList.Count; i++)
			{
				CatmullRomPoint point = toRemoveList[i];
				catmullRomPointList.Remove(point);
			}
		}
	}
}