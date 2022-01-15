using UnityEngine;

namespace CsCat
{
	[AddComponentMenu("Effects/X Line Renderer")]
	[ExecuteInEditMode]
	public class XLineRenderer : MonoBehaviour
	{
		public Material mat;

		public AnimationCurve widthAnimationCurve =
		  new AnimationCurve(new Keyframe[] { new Keyframe(0, 2), new Keyframe(1, 2) });

		public float lifeTime = 1;
		public int loopCount = 1;
		public int tileColumnCount = 1;
		public int tileRowCount = 1;
		public int tileLastRowColumnCount = 1;
		public float fps = 24;
		public Color[] colors = new Color[] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 1) };
		public bool isTestReplay = false;
		public Transform target;

		private Mesh mesh;
		private Vector3[] vertices;
		private Vector2[] uv;
		private Color[] vertexColors;
		private int[] indices;
		private float currentTime;
		private float lastTime;
		private int currentTileId;

		void Awake()
		{
			Init();
		}

		void Init()
		{
			mesh = new Mesh();
			vertices = new Vector3[4];
			uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
			vertexColors = new Color[] { Color.white, Color.white, Color.white, Color.white };
			indices = new int[] { 0, 1, 2, 2, 1, 3 };
			currentTime = 0;

			if (target == null)
			{
				target = transform.Find("target");
				if (target == null)
				{
					target = new GameObject("target").transform;
					target.SetParent(transform);
					target.localPosition = new Vector3(0, 0, 5);
				}
			}

			if (mat == null)
				mat = new Material(ShaderUtilCat.FindShader("XGame/Particles/Additive"));
		}

		void OnEnable()
		{
			currentTime = 0;
			if (!Application.isPlaying)
				Init();
		}

		void Update()
		{
			currentTime = currentTime + Time.deltaTime;
			if (isTestReplay)
			{
				isTestReplay = false;
				currentTime = 0;
			}
		}

		void OnRenderObject()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				bool isSelected = false;
				for (var i = 0; i < UnityEditor.Selection.transforms.Length; i++)
				{
					Transform selectedTransform = UnityEditor.Selection.transforms[i];
					if (selectedTransform.IsChildOf(transform) || transform.IsChildOf(selectedTransform))
						isSelected = true;
				}

				if (!isSelected)
				{
					currentTime = 0;
					return;
				}

				currentTime = currentTime + Mathf.Max(Time.realtimeSinceStartup - lastTime, 0);
				lastTime = Time.realtimeSinceStartup;
				UnityEditor.SceneView.RepaintAll();
			}
#endif

			if (mat == null || target == null)
				return;
			Camera camera = Camera.current;
			if ((camera.cullingMask & (1 << gameObject.layer)) == 0)
				return;

			float totalTime = lifeTime * loopCount;
			if (totalTime > 0 && currentTime > totalTime)
			{
				if (Application.isEditor && !Application.isPlaying)
				{
					if (currentTime > totalTime + 2)
						currentTime = currentTime % (totalTime + 2);
					return;
				}
			}

			float lifePct = lifeTime > 0 ? currentTime / lifeTime % 1 : 0;
			Vector3 sourcePosition = transform.position;
			Vector3 targetPosition = target.position;
			Vector3 cameraPosition = camera.transform.position;
			Vector3 dir = targetPosition - sourcePosition;
			//    float dist = dir.magnitude;
			Vector3 sourceDir = Vector3.Cross(dir, cameraPosition - sourcePosition);
			Vector3 targetDir = Vector3.Cross(dir, cameraPosition - targetPosition);
			sourceDir.Normalize();
			targetDir.Normalize();

			float offset = widthAnimationCurve.Evaluate(lifePct) / 2;
			vertices[0] = sourcePosition + sourceDir * offset;
			vertices[1] = sourcePosition - sourceDir * offset;
			vertices[2] = targetPosition + targetDir * offset;
			vertices[3] = targetPosition - targetDir * offset;
			mesh.vertices = vertices;

			// uv
			currentTileId = Mathf.FloorToInt((currentTime * fps) % GetTileTotalCount()) + 1;
			Vector2Int currentTileIndex = GetTileRowColumn(currentTileId);
			int currentTileRow = currentTileIndex.x;
			int currentTileColumn = currentTileIndex.y;
			uv[0] = new Vector2((currentTileColumn - 1) / (float)this.tileColumnCount,
			  (currentTileRow - 1) / (float)this.tileRowCount);
			uv[1] = new Vector2((currentTileColumn - 1) / (float)this.tileColumnCount,
			  currentTileRow / (float)this.tileRowCount);
			uv[2] = new Vector2(currentTileColumn / (float)this.tileColumnCount,
			  (currentTileRow - 1) / (float)this.tileRowCount);
			uv[3] = new Vector2(currentTileColumn / (float)this.tileColumnCount,
			  currentTileRow / (float)this.tileRowCount);
			mesh.uv = uv;

			// color
			Color color;
			if (colors.Length == 0)
				color = new Color(1, 1, 1, 1);
			else if (colors.Length == 1)
				color = colors[0];
			else
			{
				float floatK = lifePct * (colors.Length - 1);
				int k = Mathf.FloorToInt(floatK);
				float lerp = floatK % 1;
				color = Color.Lerp(colors[k], colors[k + 1], lerp);
			}

			vertexColors[0] = color;
			vertexColors[1] = color;
			vertexColors[2] = color;
			vertexColors[3] = color;
			mesh.colors = vertexColors;

			// draw
			mesh.SetIndices(indices, MeshTopology.Triangles, 0);
			mat.SetPass(0);
			Graphics.DrawMeshNow(mesh, Vector3.zero, Quaternion.identity);
		}

		private int GetTileTotalCount()
		{
			return this.tileColumnCount * (this.tileRowCount - 1) + tileLastRowColumnCount;
		}

		// all is base on 1
		// tileId base on 1
		// return result row base on 1
		// return result column base on 1
		private Vector2Int GetTileRowColumn(int tileId)
		{
			int row = Mathf.FloorToInt((tileId - 1) / (float)tileColumnCount) + 1;
			int column = tileId - ((row - 1) * tileColumnCount);
			return new Vector2Int(row, column);
		}

		public void OnDestroy()
		{
			mesh.Clear();
			mesh = null;
			vertices = null;
			uv = null;
			vertexColors = null;
			indices = null;
			target = null;
		}


	}

}


