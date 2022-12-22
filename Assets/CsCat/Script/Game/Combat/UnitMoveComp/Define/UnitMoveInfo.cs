using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class UnitMoveInfo
	{
		public float animationSpeed = -1;
		public string animationName = AnimationNameConst.Walk;
		public float speed = 1;
		public float remainDuration;
		public Vector3 targetPos;
		public int targetIndexInPath;
		public List<Vector3> path;
		public Unit lookAtUnit;
		public float rotateRemainDuration;
		public Quaternion endRotation;


		public bool IsHasAnimationName()
		{
			return !this.animationName.IsNullOrWhiteSpace();
		}
	}
}