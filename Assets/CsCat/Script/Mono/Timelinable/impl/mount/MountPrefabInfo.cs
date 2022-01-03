
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
			var _dest = dest as MountPrefabInfo;
			_dest.localPosition = localPosition;
			_dest.localEulerAngles = localEulerAngles;
			_dest.localScale = localScale;
			_dest.prefab = prefab;
		}

		public void CopyFrom(object source)
		{
			var _source = source as MountPrefabInfo;
			localPosition = _source.localPosition;
			localEulerAngles = _source.localEulerAngles;
			localScale = _source.localScale;
			prefab = _source.prefab;
		}


	}
}



