using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class TransformFinder0 : TransformFinderBase
	{
		public string path;

		public override Transform Find(params object[] args)
		{
			var rootTransform = args[0] as Transform;
			if (rootTransform == null)
			{
				if (path.IsNullOrEmpty())
					return null;
				var gameObject = GameObject.Find(path);
				return gameObject == null ? null : gameObject.transform;
			}
			if (path.IsNullOrEmpty())
				return rootTransform;
			var transform = rootTransform.FindComponentInChildren<Transform>(path);
			return transform;
		}

		public override void CopyTo(object dest)
		{
			var destTransformFinder = dest as TransformFinder0;
			destTransformFinder.path = path;
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var sourceTransformFinder = source as TransformFinder0;
			path = sourceTransformFinder.path;
			base.CopyFrom(sourceTransformFinder);
		}
	}
}