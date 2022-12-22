using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CsCat
{
	public class TransformTweenMixerBehaviour : PlayableBehaviour
	{
		public List<TimelineClip> clipList;


		public PlayableDirector director;
		private bool _isFirstFrameHappened;


		private Transform _isTrackBinding;

		public Vector3 defaultPosition;

		private Vector3 _endPosition;

		public Vector3 startPosition;
		public Vector3 defaultRotation;
		private Vector3 _endRotation;
		public Vector3 startRotation;
		public Vector3 defaultScale;
		private Vector3 _endScale;
		public Vector3 startScale;


		private void HandleStart()
		{
			startPosition = defaultPosition;
			startRotation = defaultRotation;
			startScale = defaultScale;
			for (var i = 0; i < clipList.Count; i++)
			{
				var e = clipList[i];
				var transformTweenClip = e.asset as TransformTweenClip;
				if (director.time >= transformTweenClip.clip.start + transformTweenClip.clip.duration)
				{
					if (transformTweenClip.template.isUsePositionTarget)
						startPosition = transformTweenClip.template.positionTarget;
					if (transformTweenClip.template.isUseRotationTarget)
						startRotation = transformTweenClip.template.rotationTarget;
					if (transformTweenClip.template.isUseScaleTarget)
						startScale = transformTweenClip.template.scaleTarget;


					startPosition = startPosition.Multiply(transformTweenClip.template.positionMultiply);
					startRotation = startRotation.Multiply(transformTweenClip.template.rotationMultiply);
					startScale = startScale.Multiply(transformTweenClip.template.scaleMultiply);
				}
				else
					break;
			}
		}


		public override void ProcessFrame(Playable playable, FrameData info, object arg)
		{
			_isTrackBinding = arg as Transform;

			if (_isTrackBinding == null)
				return;


			HandleStart();


			var inputCount = playable.GetInputCount();
			var totalBlendWeight = 0f;


			var blendPosition = Vector3.zero;
			var blendRotation = Vector3.zero;
			var blendScale = Vector3.zero;

			for (var i = 0; i < inputCount; i++)
			{
				var inputWeight = playable.GetInputWeight(i);
				var inputPlayable =
				  (ScriptPlayable<TransformTweenBehaviour>)playable.GetInput(i);
				var input = inputPlayable.GetBehaviour();
				totalBlendWeight += inputWeight;

				if (Mathf.Approximately(inputWeight, 0f))
					continue;

				if (input.clip.IsInRange(director.time))
				{
					var percent = input.clip.GetPercent(director.time);


					if (input.isUsePositionTarget)
						blendPosition += Vector3.Lerp(startPosition, input.positionTarget, percent) * inputWeight;
					else
						blendPosition += startPosition * inputWeight;

					if (input.isUseRotationTarget)
						blendRotation += Vector3.Lerp(startRotation, input.rotationTarget, percent) * inputWeight;
					else
						blendRotation += startRotation * inputWeight;


					if (input.isUseScaleTarget)
						blendScale += Vector3.Lerp(startScale, input.scaleTarget, percent) * inputWeight;
					else
						blendScale += startScale * inputWeight;

					blendPosition = blendPosition.Multiply(input.positionMultiply) * inputWeight;
					blendRotation = blendRotation.Multiply(input.rotationMultiply) * inputWeight;
					blendScale = blendScale.Multiply(input.scaleMultiply) * inputWeight;
				}
			}

			var remainDefaultWeight = Mathf.Clamp(1 - totalBlendWeight, 0, 1);


			_isTrackBinding.transform.localPosition = remainDefaultWeight * startPosition + blendPosition;
			_isTrackBinding.transform.localEulerAngles = remainDefaultWeight * startRotation + blendRotation;
			_isTrackBinding.transform.localScale = remainDefaultWeight * startScale + blendScale;
		}
	}
}