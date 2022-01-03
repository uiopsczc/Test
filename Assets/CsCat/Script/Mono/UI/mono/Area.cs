using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class Area : Graphic
	{
		public override Texture mainTexture => material.mainTexture;

		// Update is called once per frame
		private void Update()
		{
			SetAllDirty();
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			if (transform.childCount == 0) return;

			Color32 color32 = color;
			vh.Clear();

			// 几何图形的顶点，本例中根据子节点坐标确定顶点
			//        foreach (Transform child in transform)
			//        {
			//            vh.AddVert(child.localPosition, color32, new Vector2(0f, 0f));
			//        }

			vh.AddVert(transform.GetChild(0).localPosition, color32, new Vector2(0f, 0f));
			vh.AddVert(transform.GetChild(1).localPosition, color32, new Vector2(0f, 1f));
			vh.AddVert(transform.GetChild(2).localPosition, color32, new Vector2(1f, 1f));
			vh.AddVert(transform.GetChild(3).localPosition, color32, new Vector2(1f, 0f));
			Debug.LogError(transform.GetChild(0).localPosition);

			// 几何图形中的三角形
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}
	}
}