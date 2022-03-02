using System;
using UnityEngine;

namespace CsCat
{
	public class UnitBeThrowedInfo
	{
		public Vector3 startPos; //开始位置
		public Vector3 endPos; //结束位置
		public float height;
		public float maxHeight;
		public string animationName = AnimationNameConst.be_throwed;
		public float orgHeight;
		public float duration;
		public float remainDuration;
		public float interp = 1;
		public float heightSpeed;
		public float heightAccelerate;
		public float? rotateDuration;
		public float? rotateRemainDuration;
		public Quaternion startRotation;
		public Quaternion? endRotation;
		public Func<UnitBeThrowedInfo, float> calcHeightFunc;
		public bool isNotStopAnimation;
		public bool isBackToGround = true;

		public bool IsHasAnimationName()
		{
			return !this.animationName.IsNullOrWhiteSpace();
		}
	}
}