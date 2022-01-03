using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
	public class TransformTweenBehaviour : PlayableBehaviour
	{
		public TimelineClip clip;

		[SerializeField] public bool isUsePositionTarget = true;

		[SerializeField] public bool is_use_rotation_target;

		[SerializeField] public bool isUseScaleTarget;


		[SerializeField] public Vector3 positionMultiply = Vector3.one;

		[SerializeField] public Vector3 positionTarget;

		[SerializeField] public Vector3 rotationMultiply = Vector3.one;

		[SerializeField] public Vector3 rotation_target;

		[SerializeField] public Vector3 scaleMultiply = Vector3.one;

		[SerializeField] public Vector3 scaleTarget;
	}
}