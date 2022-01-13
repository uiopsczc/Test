using System;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class TransformFinder1 : TransformFinderBase
	{
		public HumanBodyBones humanBodyBones;

		public override Transform Find(params object[] args)
		{
			var animator = args[0] as Animator;
			var result = animator.GetBoneTransform(humanBodyBones);
			return result;
		}

		public override void CopyTo(object dest)
		{
			var destTransformFinder = dest as TransformFinder1;
			destTransformFinder.humanBodyBones = humanBodyBones;
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var sourceTransformFinder = source as TransformFinder1;
			humanBodyBones = sourceTransformFinder.humanBodyBones;
			base.CopyFrom(sourceTransformFinder);
		}
	}
}