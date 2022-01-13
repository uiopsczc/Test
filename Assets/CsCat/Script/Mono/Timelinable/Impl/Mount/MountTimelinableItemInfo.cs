
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class MountTimelinableItemInfo : TimelinableItemInfoBase
	{
		public TransformFinders mountPointTransformFinders = new TransformFinders();
		public int mountPointTransformFinderIndex = 0;
		public List<MountPrefabInfo> mountPrefabInfoList = new List<MountPrefabInfo>();
		[NonSerialized]
		private List<GameObject> cloneList = new List<GameObject>();

		public TransformFinderBase mountPointTransformFinder => mountPointTransformFinders[mountPointTransformFinderIndex];

		public MountTimelinableItemInfo()
		{
		}

		public MountTimelinableItemInfo(AnimationTimelinableItemInfo other)
		{
			CopyFrom(other);
		}

		public override void CopyTo(object dest)
		{
			var destMountTimelinableItemInfo = dest as MountTimelinableItemInfo;
			mountPointTransformFinders.CopyTo(destMountTimelinableItemInfo.mountPointTransformFinders);
			mountPrefabInfoList.CopyTo(destMountTimelinableItemInfo.mountPrefabInfoList);
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var sourceMountTimelinableItemInfo = source as MountTimelinableItemInfo;
			mountPointTransformFinders.CopyFrom(sourceMountTimelinableItemInfo.mountPointTransformFinders);
			mountPrefabInfoList.CopyFrom(sourceMountTimelinableItemInfo.mountPrefabInfoList);
			base.CopyFrom(source);
		}

		public Transform GetMountPointTransform(Transform transform)
		{
			Transform mountPointTransform = null;
			switch (mountPointTransformFinders[mountPointTransformFinderIndex])
			{
				case TransformFinder0 transformFinder1:
					mountPointTransform = transformFinder1.Find(transform);
					break;
				case TransformFinder1 transformFinder2:
					mountPointTransform = transformFinder2.Find(transform.GetComponent<Animator>());
					break;
				default:
					mountPointTransform = mountPointTransformFinders[mountPointTransformFinderIndex].Find();
					break;
			}
			return mountPointTransform;
		}

		public override void Play(params object[] args)
		{
			var track = args[args.Length - 1] as MountTimelinableTrack;
			var rootTransform = args[0] as Transform;
			var mountPointTransform = GetMountPointTransform(rootTransform);
			Func<GameObject, Transform, GameObject> spawnCallback = SpawnUtil.Instantiate;
			if (Application.isPlaying)
				spawnCallback = TimelinableUtil.SpawnGameObject;
			for (int i = 0; i < mountPrefabInfoList.Count; i++)
			{
				var mountPrefabInfo = mountPrefabInfoList[i];
				var clone = spawnCallback(mountPrefabInfo.prefab, mountPointTransform);
				if (clone != null)
				{
					var clone_transform = clone.transform;
					clone_transform.localPosition = mountPrefabInfo.localPosition;
					clone_transform.localEulerAngles = mountPrefabInfo.localEulerAngles;
					clone_transform.localScale = mountPrefabInfo.localScale;
					cloneList.Add(clone);
				}
			}
			base.Play(args);
		}

		public override void Stop(params object[] args)
		{
			Action<GameObject, Transform> despawnCallback = SpawnUtil.Destroy2;
			if (Application.isPlaying)
				despawnCallback = TimelinableUtil.DespawnGameObject;
			for (var i = 0; i < cloneList.Count; i++)
			{
				var clone = cloneList[i];
				despawnCallback(clone, null);
			}

			cloneList.Clear();
			base.Stop(args);
		}

	}
}



