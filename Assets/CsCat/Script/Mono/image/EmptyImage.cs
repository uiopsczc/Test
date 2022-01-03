using UnityEngine.UI;

// 空白区域点击
public class EmptyImage : MaskableGraphic
{

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	protected EmptyImage()
	{
		useLegacyMeshGeneration = false;
	}
}
