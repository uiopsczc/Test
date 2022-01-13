using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class SkinnedMeshRendererSubBlendShapeInfo : ICopyable
	{
		public int skinnedMeshRendererIndex;
		public int blendShape_index;

		public SkinnedMeshRendererSubBlendShapeInfo()
		{
		}


		public void CopyTo(object dest)
		{
			var destSkinnedMeshRendererSubBlendShapeInfo = dest as SkinnedMeshRendererSubBlendShapeInfo;
			destSkinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex = skinnedMeshRendererIndex;
			destSkinnedMeshRendererSubBlendShapeInfo.blendShape_index = blendShape_index;
		}

		public void CopyFrom(object source)
		{
			var sourceSkinnedMeshRendererSubBlendShapeInfo = source as SkinnedMeshRendererSubBlendShapeInfo;
			skinnedMeshRendererIndex = sourceSkinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex;
			blendShape_index = sourceSkinnedMeshRendererSubBlendShapeInfo.blendShape_index;
		}
	}
}