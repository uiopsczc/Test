using UnityEngine;

namespace CsCat
{
  [AddComponentMenu("Effects/X Line Renderer")]
  [ExecuteInEditMode]
  public class XLineRenderer : MonoBehaviour
  {
    public Material mat;

    public AnimationCurve width_animationCurve =
      new AnimationCurve(new Keyframe[] {new Keyframe(0, 2), new Keyframe(1, 2)});

    public float life_time = 1;
    public int loop_count = 1;
    public int tile_column_count = 1;
    public int tile_row_count = 1;
    public int tile_last_row_column_count = 1;
    public float fps = 24;
    public Color[] colors = new Color[] {new Color(1, 1, 1, 1), new Color(1, 1, 1, 1)};
    public bool is_test_replay = false;
    public Transform target;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private Color[] vertex_colors;
    private int[] indices;
    private float current_time;
    private float last_time;
    private int current_tile_id;

    void Awake()
    {
      Init();
    }

    void Init()
    {
      mesh = new Mesh();
      vertices = new Vector3[4];
      uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1)};
      vertex_colors = new Color[] {Color.white, Color.white, Color.white, Color.white};
      indices = new int[] {0, 1, 2, 2, 1, 3};
      current_time = 0;

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
      current_time = 0;
      if (!Application.isPlaying)
        Init();
    }

    void Update()
    {
      current_time = current_time + Time.deltaTime;
      if (is_test_replay)
      {
        is_test_replay = false;
        current_time = 0;
      }
    }

    void OnRenderObject()
    {
#if UNITY_EDITOR
      if (!Application.isPlaying)
      {
        bool is_selected = false;
        foreach (Transform selected_transform in UnityEditor.Selection.transforms)
          if (selected_transform.IsChildOf(transform) || transform.IsChildOf(selected_transform))
            is_selected = true;
        if (!is_selected)
        {
          current_time = 0;
          return;
        }

        current_time = current_time + Mathf.Max(Time.realtimeSinceStartup - last_time, 0);
        last_time = Time.realtimeSinceStartup;
        UnityEditor.SceneView.RepaintAll();
      }
#endif

      if (mat == null || target == null)
        return;
      Camera camera = Camera.current;
      if ((camera.cullingMask & (1 << gameObject.layer)) == 0)
      {
        return;
      }

      float total_time = life_time * loop_count;
      if (total_time > 0 && current_time > total_time)
      {
        if (Application.isEditor && !Application.isPlaying)
        {
          if (current_time > total_time + 2)
            current_time = current_time % (total_time + 2);
          return;
        }
      }

      float life_pct = life_time > 0 ? current_time / life_time % 1 : 0;
      Vector3 source_position = transform.position;
      Vector3 target_position = target.position;
      Vector3 camera_position = camera.transform.position;
      Vector3 dir = target_position - source_position;
//    float dist = dir.magnitude;
      Vector3 source_dir = Vector3.Cross(dir, camera_position - source_position);
      Vector3 target_dir = Vector3.Cross(dir, camera_position - target_position);
      source_dir.Normalize();
      target_dir.Normalize();

      float offset = width_animationCurve.Evaluate(life_pct) / 2;
      vertices[0] = source_position + source_dir * offset;
      vertices[1] = source_position - source_dir * offset;
      vertices[2] = target_position + target_dir * offset;
      vertices[3] = target_position - target_dir * offset;
      mesh.vertices = vertices;

      // uv
      current_tile_id = Mathf.FloorToInt((current_time * fps) % GetTileTotalCount()) + 1;
      Vector2Int current_tile_index = GetTileRowColumn(current_tile_id);
      int current_tile_row = current_tile_index.x;
      int current_tile_column = current_tile_index.y;
      uv[0] = new Vector2((current_tile_column - 1) / (float) this.tile_column_count,
        (current_tile_row - 1) / (float) this.tile_row_count);
      uv[1] = new Vector2((current_tile_column - 1) / (float) this.tile_column_count,
        current_tile_row / (float) this.tile_row_count);
      uv[2] = new Vector2(current_tile_column / (float) this.tile_column_count,
        (current_tile_row - 1) / (float) this.tile_row_count);
      uv[3] = new Vector2(current_tile_column / (float) this.tile_column_count,
        current_tile_row / (float) this.tile_row_count);
      mesh.uv = uv;

      // color
      Color color;
      if (colors.Length == 0)
        color = new Color(1, 1, 1, 1);
      else if (colors.Length == 1)
        color = colors[0];
      else
      {
        float float_k = life_pct * (colors.Length - 1);
        int k = Mathf.FloorToInt(float_k);
        float lerp = float_k % 1;
        color = Color.Lerp(colors[k], colors[k + 1], lerp);
      }

      vertex_colors[0] = color;
      vertex_colors[1] = color;
      vertex_colors[2] = color;
      vertex_colors[3] = color;
      mesh.colors = vertex_colors;

      // draw
      mesh.SetIndices(indices, MeshTopology.Triangles, 0);
      mat.SetPass(0);
      Graphics.DrawMeshNow(mesh, Vector3.zero, Quaternion.identity);
    }

    private int GetTileTotalCount()
    {
      return this.tile_column_count * (this.tile_row_count - 1) + tile_last_row_column_count;
    }

    // all is base on 1
    // tileId base on 1
    // return result row base on 1
    // return result column base on 1
    private Vector2Int GetTileRowColumn(int tile_id)
    {
      int row = Mathf.FloorToInt((tile_id - 1) / (float)tile_column_count) + 1;
      int column = tile_id - ((row - 1) * tile_column_count);
      return new Vector2Int(row, column);
    }

    public void OnDestroy()
    {
      mesh.Clear();
      mesh = null;
      vertices = null;
      uv = null;
      vertex_colors = null;
      indices = null;
      target = null;
    }


  }

}


