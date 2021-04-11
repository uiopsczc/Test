
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  [Serializable]
  public partial class MountTimelinableItemInfo : TimelinableItemInfoBase
  {
    public TransformFinders mount_point_transformFinders = new TransformFinders();
    public int mount_point_transformFinder_index = 0;
    public List<MountPrefabInfo> mountPrefabInfo_list=new List<MountPrefabInfo>();
    [NonSerialized]
    private List<GameObject> clone_list =new  List<GameObject>();

    public TransformFinderBase mount_point_transformFinder
    {
      get { return mount_point_transformFinders[mount_point_transformFinder_index]; }
    }

    public MountTimelinableItemInfo()
    {
    }

    public MountTimelinableItemInfo(AnimationTimelinableItemInfo other)
    {
      CopyFrom(other);
    }

    public override void CopyTo(object dest)
    {
      var _dest = dest as MountTimelinableItemInfo;
      mount_point_transformFinders.CopyTo(_dest.mount_point_transformFinders);
      mountPrefabInfo_list.CopyTo(_dest.mountPrefabInfo_list);
      base.CopyTo(dest);
    }

    public override void CopyFrom(object source)
    {
      var _source = source as MountTimelinableItemInfo;
      mount_point_transformFinders.CopyFrom(_source.mount_point_transformFinders);
      mountPrefabInfo_list.CopyFrom(_source.mountPrefabInfo_list);
      base.CopyFrom(source);
    }

    public Transform GetMountPointTransform(Transform transform)
    {
      Transform mount_point_transform = null;
      switch (mount_point_transformFinders[mount_point_transformFinder_index])
      {
        case TransformFinder0 transformFinder1:
          mount_point_transform = transformFinder1.Find(transform);
          break;
        case TransformFinder1 transformFinder2:
          mount_point_transform =  transformFinder2.Find(transform.GetComponent<Animator>());
          break;
        default:
          mount_point_transform = mount_point_transformFinders[mount_point_transformFinder_index].Find();
          break;
      }
      return mount_point_transform;
    }

    public override void Play(params object[] args)
    {
      var track = args[args.Length - 1] as MountTimelinableTrack;
      var root_transform = args[0] as Transform;
      var mount_point_transform = GetMountPointTransform(root_transform);
      Func<GameObject, Transform, GameObject> spawn_callback = SpawnUtil.Instantiate;
      if (Application.isPlaying)
        spawn_callback = TimelinableUtil.SpawnGameObject;
      for (int i=0;i< mountPrefabInfo_list.Count;i++)
      {
        var mountPrefabInfo = mountPrefabInfo_list[i];
        var clone = spawn_callback( mountPrefabInfo.prefab, mount_point_transform);
        if (clone != null)
        {
          var clone_transform = clone.transform;
          clone_transform.localPosition = mountPrefabInfo.localPosition;
          clone_transform.localEulerAngles = mountPrefabInfo.localEulerAngles;
          clone_transform.localScale = mountPrefabInfo.localScale;
          clone_list.Add(clone);
        }
      }
      base.Play(args);
    }

    public override void Stop(params object[] args)
    {
      Action<GameObject, Transform> despawn_callback = SpawnUtil.Destroy2;
      if (Application.isPlaying)
        despawn_callback = TimelinableUtil.DespawnGameObject;
      foreach (var clone in clone_list)
        despawn_callback(clone, null);
      clone_list.Clear();
      base.Stop(args);
    }
    
  }
}



