using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class MountPrefabInfo : ICopyable
	{
		public Vector3 localPosition = Vector3.zero;
		public Vector3 localEulerAngles = Vector3.zero;
		public Vector3 localScale = Vector3.one;
		public GameObject prefab;

		public MountPrefabInfo()
		{
		}


		public void CopyTo(object dest)
		{
			var destMountPrefabInfo = dest as MountPrefabInfo;
			destMountPrefabInfo.localPosition = localPosition;
			destMountPrefabInfo.localEulerAngles = localEulerAngles;
			destMountPrefabInfo.localScale = localScale;
			destMountPrefabInfo.prefab = prefab;
		}

		public void CopyFrom(object source)
		{
			var sourceMountPrefabInfo = source as MountPrefabInfo;
			localPosition = sourceMountPrefabInfo.localPosition;
			localEulerAngles = sourceMountPrefabInfo.localEulerAngles;
			localScale = sourceMountPrefabInfo.localScale;
			prefab = sourceMountPrefabInfo.prefab;
		}
	}
}