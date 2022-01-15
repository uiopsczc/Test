using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class CameraManager : TickObject
	{
		public CameraBase mainCameraBase;
		public CameraBase uiCameraBase;
		public List<CameraBase> cameraBaseList = new List<CameraBase>();

		public override void Init()
		{
			base.Init();
			mainCameraBase = this.AddChild<CameraBase>("main_camera",
				GameObject.Find("Main Camera").GetComponent<Camera>(), true);
			uiCameraBase = this.AddChild<CameraBase>("ui_camera",
				GameObject.Find(UIConst.UICamera_Path).GetComponent<Camera>(), true);
			this.AddListener<StageBase>(null, StageEventNameConst.On_Stage_Loaded, OnStageLoaded);
		}

		public void OnStageLoaded(StageBase stage)
		{
			cameraBaseList.Clear();
			Camera[] cameras = Camera.allCameras;
			for (int i = 0; i < cameras.Length; i++)
			{
				Camera camera = cameras[i];
				if (camera == mainCameraBase.camera || camera == uiCameraBase.camera)
					continue;
				CameraBase cameraBase =
					this.AddChild<CameraBase>(string.Format("{0}{1}", camera.name, i), camera, false);
				cameraBaseList.Add(cameraBase);
			}
		}

		public void ShakeScreen(float duration, Vector3 posShakeRange, Vector3 posShakeFrequency,
			Vector3 eulerAnglesShakeRange,
			Vector3 eulerAnglesShakeFrequency, float fovShakeRange, float fovShakeFrequency)
		{
			for (var i = 0; i < cameraBaseList.Count; i++)
			{
				var cameraBase = cameraBaseList[i];
				cameraBase.ShakeScreen(duration, posShakeRange, posShakeFrequency, eulerAnglesShakeRange,
					eulerAnglesShakeFrequency, fovShakeRange,
					fovShakeFrequency);
			}
		}

		public void SetMainCameraMoveRange(object moveRange)
		{
			this.mainCameraBase.SetMoveRange(moveRange);
		}

		public void MoveByDelta(float dx, float dy, float dz)
		{
			this.mainCameraBase.MoveByDelta(dx, dy, dz);
		}

		public void RemoveCameras()
		{
			for (int i = cameraBaseList.Count - 1; i >= 0; i--)
			{
				this.RemoveChild(cameraBaseList[i].key);
				cameraBaseList.RemoveAt(i);
			}
		}


		protected override void _Destroy()
		{
			base._Destroy();
		}
	}
}