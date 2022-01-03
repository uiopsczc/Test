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
			var _dest = dest as TransformFinder1;
			_dest.humanBodyBones = humanBodyBones;
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var _source = source as TransformFinder1;
			humanBodyBones = _source.humanBodyBones;
			base.CopyFrom(_source);
		}
	}
}