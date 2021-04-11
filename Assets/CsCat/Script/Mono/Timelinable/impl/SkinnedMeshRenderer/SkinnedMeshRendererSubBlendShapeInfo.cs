using System;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class SkinnedMeshRendererSubBlendShapeInfo : ICopyable
  {
    public int skinnedMeshRenderer_index;
    public int blendShape_index;

    public SkinnedMeshRendererSubBlendShapeInfo()
    {
    }


    public void CopyTo(object dest)
    {
      var _dest = dest as SkinnedMeshRendererSubBlendShapeInfo;
      _dest.skinnedMeshRenderer_index = skinnedMeshRenderer_index;
      _dest.blendShape_index = blendShape_index;
    }

    public void CopyFrom(object source)
    {
      var _source = source as SkinnedMeshRendererSubBlendShapeInfo;
      skinnedMeshRenderer_index = _source.skinnedMeshRenderer_index;
      blendShape_index = _source.blendShape_index;
    }
  }
}