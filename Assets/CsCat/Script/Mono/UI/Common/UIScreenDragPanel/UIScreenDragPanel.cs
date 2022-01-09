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

		private object moveRange;
		private float deltaMoveScale;
		private float deltaHeightSacle;
		private int touchCount; // 触摸数
		private CameraManager cameraManager;

		private Dictionary<int, Vector2>
		  modifyCameraHeightInfo = new Dictionary<int, Vector2>(); //用于双指控制调整摄像头高度时使用，记录手指两点距离变化

		private float lastDistance = 0;

		public void Init(object moveRange)
		{
			base.Init();
			this.moveRange = moveRange;

			this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIScreenDragPanel.prefab");

			//用于移动的比例
			this.deltaMoveScale = 1 / (Screen.height / ScreenConst.Design_Resolution_Height * 12);
			this.deltaHeightSacle = Screen.height / ScreenConst.Design_Resolution_Height * 0.15f; // 屏幕拖拽控制器缩放屏幕灵敏度
																									//设置摄像机移动范围
			this.cameraManager = Client.instance.combat.cameraManager;
			this.cameraManager.SetMainCameraMoveRange(moveRange);
		}

		protected override void AddUnityEvents()
		{
			base.AddUnityEvents();
			this.RegisterOnDrag(graphicComponent.gameObject, this.OnUIScreenDrag);
			this.RegisterOnPointerDown(graphicComponent.gameObject, this.OnUIScreenPointerDown);
			this.RegisterOnPointerUp(graphicComponent.gameObject, this.OnUIScreenPointerUp);
		}



		protected override void _SetIsEnabled(bool isEnabled)
		{
			base._SetIsEnabled(isEnabled);
			graphicComponent.SetIsShow(isEnabled);
			if (!isEnabled)
				this.touchCount = 0;
		}

		public void OnUIScreenPointerDown(PointerEventData eventData)
		{
			this.touchCount = this.touchCount + 1;
			// 不处理大于2个触摸点的操作
			if (this.touchCount > 2)
				return;

			//记录当前手指坐标
			this.modifyCameraHeightInfo[eventData.pointerId] = eventData.position;
			//计算当前两个手指的距离
			if (this.touchCount > 1)
				this.lastDistance = this.__CalculateTwoPointDistance();

			cameraManager?.MoveByDelta(0, 0, 0);
		}

		// 计算两个触点的距离
		private float __CalculateTwoPointDistance()
		{
			Vector2? otherPoint = null;
			foreach (var eventDataPosition in this.modifyCameraHeightInfo.Values)
			{
				if (otherPoint.HasValue)
					return Vector2.Distance(otherPoint.Value, eventDataPosition);

				otherPoint = eventDataPosition;
			}

			//如果只剩下一个点，则返回上一次的距离
			return this.lastDistance;
		}

		public void OnUIScreenPointerUp(PointerEventData eventData)
		{
			this.modifyCameraHeightInfo.Remove(eventData.pointerId);
			this.touchCount = this.touchCount - 1;
		}


		public void OnUIScreenDrag(PointerEventData eventData)
		{
			if (this.cameraManager == null)
				return;
			//如果是一个触摸点的时候是拖拽屏幕移动
			if (this.touchCount < 1.5f)
			{
				this.cameraManager.MoveByDelta(eventData.delta.x * this.deltaMoveScale,
				  eventData.delta.y * this.deltaMoveScale, 0);
			}
			else
			{
				//如果是大于1个触摸点的时候，是调整摄像头高度
				this.modifyCameraHeightInfo[eventData.pointerId] = eventData.position;
				var distance = this.__CalculateTwoPointDistance();
				this.cameraManager.MoveByDelta(0, 0, (distance - this.lastDistance) * this.deltaHeightSacle);
				this.lastDistance = distance;
			}
		}

		protected override void _Reset()
		{
			base._Reset();
			cameraManager?.SetMainCameraMoveRange(null);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			cameraManager?.SetMainCameraMoveRange(null);
		}
	}
}