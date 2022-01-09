#if MicroTileMap
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace CsCat
{
[Serializable]
public  class TileMapChunkRendererPropertiesData
{
  public ShadowCastingMode shadowCastingMode = ShadowCastingMode.Off;
  public bool is_receive_shadows = false;
  public LightProbeUsage lightProbeUsage = LightProbeUsage.Off;
  public ReflectionProbeUsage reflectionProbeUsage = ReflectionProbeUsage.Off;
  public Transform probeAnchor;
}
}
#endif