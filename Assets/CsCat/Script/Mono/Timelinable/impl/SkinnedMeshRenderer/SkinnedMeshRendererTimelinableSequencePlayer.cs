using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //  public class SkinnedMeshRendererTimelinableSequencePlayer : TimelinableSequencePlayerBase
  //  {
  //    public List<SkinnedMeshRenderer> meshRenderer_list = new List<SkinnedMeshRenderer>();
  //    public string[] meshRenderer_names = new string[0];
  //    public string[][] blendShape_names = new string[0][];
  //    private readonly Dictionary<Vector2Int, float> fixed_weight_shape_dict = new Dictionary<Vector2Int, float>();
  //
  //    public SkinnedMeshRendererTimelinableSequencePlayer(Transform transform, params string[] pathes) : base(transform)
  //    {
  //      if (pathes.IsNullOrEmpty())
  //        meshRenderer_list.Add(transform.GetComponent<SkinnedMeshRenderer>());
  //      else
  //        foreach (var path in pathes)
  //          meshRenderer_list.Add(transform.Find(path).GetComponent<SkinnedMeshRenderer>());
  //      meshRenderer_names = new string[meshRenderer_list.Count];
  //      blendShape_names = new string[meshRenderer_list.Count][];
  //      for (int i = 0; i < meshRenderer_list.Count; i++)
  //      {
  //        var meshRenderer = meshRenderer_list[i];
  //        if (meshRenderer == null || meshRenderer.sharedMesh == null)
  //          continue;
  //        meshRenderer_names[i] = meshRenderer.name;
  //        ResetBlendShape(meshRenderer, i);
  //        blendShape_names[i] = new string[meshRenderer.sharedMesh.blendShapeCount];
  //        for (int j = 0; j < meshRenderer.sharedMesh.blendShapeCount; j++)
  //          blendShape_names[i][j] = meshRenderer.sharedMesh.GetBlendShapeName(j);
  //      }
  //    }
  //
  //    public override void Play()
  //    {
  //      base.Play();
  //    }
  //
  //    public override void Stop()
  //    {
  //      base.Stop();
  //    }
  //
  //    public override void Reset()
  //    {
  //      base.Reset();
  //    }
  //
  //    public override void Dispose()
  //    {
  //      base.Dispose();
  //    }
  //
  //    public override void Pause()
  //    {
  //      base.Pause();
  //    }
  //
  //    public override void UnPause()
  //    {
  //      base.UnPause();
  //    }
  //
  //    public override void UpdateTime(float time)
  //    {
  //      cur_time = time;
  //      if (is_playing)
  //        sequence.Tick(time, this);
  //      else
  //        sequence.Retime(time, this);
  //    }
  //////////////////////////////////////////////////////////////////////
  // 
  //////////////////////////////////////////////////////////////////////














  //  }
}