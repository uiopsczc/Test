using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private List<CameraShakeData> shakeDataList = new List<CameraShakeData>();

		public void ShakeScreen(float duration, Vector3 posShakeRange, Vector3 posShakeFrequency,
			Vector3 eulerAnglesShakeRange,
			Vector3 eulerAnglesShakeFrequency, float fovShakeRange, float fovShakeFrequency)
		{
			shakeDataList.Add(new CameraShakeData(
				duration, posShakeRange, posShakeFrequency, eulerAnglesShakeRange, eulerAnglesShakeFrequency,
				fovShakeRange,
				fovShakeFrequency
			));
		}

		public void ApplyShakeScreen(float deltaTime)
		{
			CameraShakeResult shakeResult = GetShakeResult(deltaTime);
			if (shakeResult != null)
			{
				Vector3 shakePosition = this.currentRotation * shakeResult.position;
				graphicComponent.transform.position = graphicComponent.transform.position + shakePosition;
				Quaternion currentRotation = this.currentRotation * Quaternion.Euler(shakeResult.eulerAngles);
				graphicComponent.transform.rotation = currentRotation;
				this.camera.fieldOfView = this.camera.fieldOfView + shakeResult.fov;
			}
		}

		public CameraShakeResult GetShakeResult(float deltaTime)
		{
			Vector3 shakePosition = new Vector3(0, 0, 0);
			Vector3 shakeEulerAngles = new Vector3(0, 0, 0);
			float shakeFov = 0;
			for (int i = shakeDataList.Count - 1; i >= 0; i--)
			{
				CameraShakeData shakeData = shakeDataList[i];
				shakeData.frameTime = shakeData.frameTime + deltaTime;
				if (shakeData.frameTime >= shakeData.duration)
					shakeDataList.RemoveLast();
				else
				{
					if (shakeData.posShakeRange != null && shakeData.posShakeFrequency != null)
						shakePosition = shakePosition + this.__CalculateShakeResult(shakeData.duration,
							shakeData.frameTime,
							shakeData.posShakeRange.Value, shakeData.posShakeFrequency.Value);
					if (shakeData.eulerAnglesShakeRange != null && shakeData.eulerAnglesShakeFrequency != null)
						shakeEulerAngles = shakeEulerAngles + this.__CalculateShakeResult(shakeData.duration,
							shakeData.frameTime,
							shakeData.eulerAnglesShakeRange.Value,
							shakeData.eulerAnglesShakeFrequency.Value);
					if (shakeData.fovShakeRange != null && shakeData.fovShakeFrequency != null)
						shakeFov = shakeFov + __CalculateShakeResult(shakeData.duration, shakeData.frameTime,
							shakeData.fovShakeRange.Value, shakeData.fovShakeFrequency.Value);
					return new CameraShakeResult(shakePosition, shakeEulerAngles, shakeFov);
				}
			}

			return null;
		}

		private Vector3 __CalculateShakeResult(float duration, float frameTime, Vector3 shakeRange,
			Vector3 shakeFrequency)
		{
			return new Vector3(
				__CalculateShakeResult(duration, frameTime, shakeRange.x, shakeFrequency.x),
				__CalculateShakeResult(duration, frameTime, shakeRange.y, shakeFrequency.y),
				__CalculateShakeResult(duration, frameTime, shakeRange.z, shakeFrequency.z)
			);
		}

		private float __CalculateShakeResult(float duration, float frameTime, float shakeRange,
			float shakeFrequency)
		{
			float reduction = (duration - frameTime) / duration;
			return Mathf.Sin(2 * Mathf.PI * shakeFrequency * frameTime) * shakeRange * reduction;
		}
	}
}