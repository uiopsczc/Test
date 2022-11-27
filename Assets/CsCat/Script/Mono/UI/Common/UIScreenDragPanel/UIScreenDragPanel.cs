using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CsCat
{
	/// <summary>
	/// 通过拖拽屏幕移动摄像头控制器
	/// 屏幕缩放和左右上下移动
	/// </summary>
	public class UIScreenDragPanel : UIBackgroundPanel
	{

		private object _moveRange;
		private float _deltaMoveScale;
		private float _deltaHeightSacle;
		private int _touchCount; // 触摸数
		private CameraManager _cameraManager;

		private readonly Dictionary<int, Vector2>
		  _modifyCameraHeightInfo = new Dictionary<int, Vector2>(); //用于双指控制调整摄像头高度时使用，记录手指两点距离变化

		private float _lastDistance = 0;

		protected void _Init(object moveRange)
		{
			base._Init();
			this._moveRange = moveRange;

			SetPrefabPath("Assets/PatchResources/UI/UIScreenDragPanel/Prefab/UIScreenDragPanel.prefab");

			//用于移动的比例
			this._deltaMoveScale = 1 / (Screen.height / ScreenConst.Design_Resolution_Height * 12);
			this._deltaHeightSacle = Screen.height / ScreenConst.Design_Resolution_Height * 0.15f; // 屏幕拖拽控制器缩放屏幕灵敏度
																									//设置摄像机移动范围
			this._cameraManager = Client.instance.combat.cameraManager;
			this._cameraManager.SetMainCameraMoveRange(moveRange);
		}

		protected override void AddUnityListeners()
		{
			base.AddUnityListeners();
			this.RegisterOnDrag(this.GetGameObject(), this.OnUIScreenDrag);
			this.RegisterOnPointerDown(this.GetGameObject(), this.OnUIScreenPointerDown);
			this.RegisterOnPointerUp(this.GetGameObject(), this.OnUIScreenPointerUp);
		}



		protected override void _SetIsEnabled(bool isEnabled)
		{
			base._SetIsEnabled(isEnabled);
			SetIsShow(isEnabled);
			if (!isEnabled)
				this._touchCount = 0;
		}

		public void OnUIScreenPointerDown(PointerEventData eventData)
		{
			this._touchCount = this._touchCount + 1;
			// 不处理大于2个触摸点的操作
			if (this._touchCount > 2)
				return;

			//记录当前手指坐标
			this._modifyCameraHeightInfo[eventData.pointerId] = eventData.position;
			//计算当前两个手指的距离
			if (this._touchCount > 1)
				this._lastDistance = this._CalculateTwoPointDistance();

			_cameraManager?.MoveByDelta(0, 0, 0);
		}

		// 计算两个触点的距离
		private float _CalculateTwoPointDistance()
		{
			Vector2? otherPoint = null;
			foreach (var kv in this._modifyCameraHeightInfo)
			{
				var eventDataPosition = kv.Value;
				if (otherPoint.HasValue)
					return Vector2.Distance(otherPoint.Value, eventDataPosition);

				otherPoint = eventDataPosition;
			}

			//如果只剩下一个点，则返回上一次的距离
			return this._lastDistance;
		}

		public void OnUIScreenPointerUp(PointerEventData eventData)
		{
			this._modifyCameraHeightInfo.Remove(eventData.pointerId);
			this._touchCount = this._touchCount - 1;
		}


		public void OnUIScreenDrag(PointerEventData eventData)
		{
			if (this._cameraManager == null)
				return;
			//如果是一个触摸点的时候是拖拽屏幕移动
			if (this._touchCount < 1.5f)
			{
				this._cameraManager.MoveByDelta(eventData.delta.x * this._deltaMoveScale,
				  eventData.delta.y * this._deltaMoveScale, 0);
			}
			else
			{
				//如果是大于1个触摸点的时候，是调整摄像头高度
				this._modifyCameraHeightInfo[eventData.pointerId] = eventData.position;
				var distance = this._CalculateTwoPointDistance();
				this._cameraManager.MoveByDelta(0, 0, (distance - this._lastDistance) * this._deltaHeightSacle);
				this._lastDistance = distance;
			}
		}

		protected override void _Reset()
		{
			base._Reset();
			_cameraManager?.SetMainCameraMoveRange(null);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			_cameraManager?.SetMainCameraMoveRange(null);
		}
	}
}