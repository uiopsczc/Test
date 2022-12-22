using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class SkinnedMeshRendererSubBlendShapeInfo : ICopyable
	{
		public int skinnedMeshRendererIndex;
		public int blendShapeIndex;

		public SkinnedMeshRendererSubBlendShapeInfo()
		{
		}


		public void CopyTo(object dest)
		{
			var destSkinnedMeshRendererSubBlendShapeInfo = dest as SkinnedMeshRendererSubBlendShapeInfo;
			destSkinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex = skinnedMeshRendererIndex;
			destSkinnedMeshRendererSubBlendShapeInfo.blendShapeIndex = blendShapeIndex;
		}

		public void CopyFrom(object source)
		{
			var sourceSkinnedMeshRendererSubBlendShapeInfo = source as SkinnedMeshRendererSubBlendShapeInfo;
			skinnedMeshRendererIndex = sourceSkinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex;
			blendShapeIndex = sourceSkinnedMeshRendererSubBlendShapeInfo.blendShapeIndex;
		}
	}
}