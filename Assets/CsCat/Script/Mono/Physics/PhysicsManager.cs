using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class PhysicsManager : TickObject
	{
		private Camera camera;
		private LayerMask? raycastLayerMask;
		private bool isClickDown;
		private bool isCanRaycast = false;
		private Dictionary<string, Action> onPointerDownDict = new Dictionary<string, Action>();
		private Dictionary<string, Action> onPointerUpDict = new Dictionary<string, Action>();
		private Dictionary<string, Action> onClickDict = new Dictionary<string, Action>();
		private int raycastId;
		private RaycastHit? lastHit;

		public void SetCamera(Camera camera)
		{
			this.camera = camera;
		}

		public void SetRaycastLayer(params string[] layerNames)
		{
			if (layerNames.Length == 0)
				this.raycastLayerMask = null;
			this.raycastLayerMask = LayerMask.GetMask(layerNames);
		}

		private void SetIsCanRaycast(bool isCanRaycast)
		{
			this.isCanRaycast = isCanRaycast;
		}

		private void UpdateRaycastState()
		{
			if (onPointerDownDict.Count == 0 && onPointerUpDict.Count == 0 && onClickDict.Count == 0)
				SetIsCanRaycast(false);
			else
				SetIsCanRaycast(true);
		}

		private string GetOrAddRaycastId(GameObject gameObject)
		{
			string raycastId = gameObject.GetOrAddCache("raycast_id_cs", () => this.raycastId++.ToString());
			return raycastId;
		}

		private string GetRaycastIdByHit(RaycastHit hit)
		{
			return GetOrAddRaycastId(hit.transform.gameObject);
		}

		public void RegisterOnPointerDown(GameObject gameObject, Action callback)
		{
			string raycastId = GetOrAddRaycastId(gameObject);
			if (this.onPointerDownDict.ContainsKey(raycastId))
				this.onPointerDownDict[raycastId] += callback;
			else
				this.onPointerDownDict[raycastId] = callback;
			this.UpdateRaycastState();
		}

		public void RegisterOnPointerUp(GameObject gameObject, Action callback)
		{
			string raycastId = GetOrAddRaycastId(gameObject);
			if (this.onPointerUpDict.ContainsKey(raycastId))
				this.onPointerUpDict[raycastId] += callback;
			else
				this.onPointerUpDict[raycastId] = callback;
			this.UpdateRaycastState();
		}

		public void RegisterOnClick(GameObject gameObject, Action callback)
		{
			string raycastId = GetOrAddRaycastId(gameObject);
			if (this.onClickDict.ContainsKey(raycastId))
				this.onClickDict[raycastId] += callback;
			else
				this.onClickDict[raycastId] = callback;
			this.UpdateRaycastState();
		}

		public void UnRegisterOnPointerDown(GameObject gameObject, Action callback = null)
		{
			string raycastId = GetOrAddRaycastId(gameObject);
			if (this.onPointerDownDict.ContainsKey(raycastId))
			{
				if (callback == null)
					this.onPointerDownDict[raycastId] = null;
				else
					this.onPointerDownDict[raycastId] -= callback;
				if (this.onPointerDownDict[raycastId] == null)
					this.onPointerDownDict.Remove(raycastId);
			}

			UpdateRaycastState();
		}

		public void UnRegisterOnPointerUp(GameObject gameObject, Action callback = null)
		{
			string raycastId = GetOrAddRaycastId(gameObject);
			if (this.onPointerUpDict.ContainsKey(raycastId))
			{
				if (callback == null)
					this.onPointerUpDict[raycastId] = null;
				else
					this.onPointerUpDict[raycastId] -= callback;
				if (this.onPointerUpDict[raycastId] == null)
					this.onPointerUpDict.Remove(raycastId);
			}

			UpdateRaycastState();
		}

		public void UnRegisterOnClick(GameObject gameObject, Action callback = null)
		{
			string raycastId = GetOrAddRaycastId(gameObject);
			if (this.onClickDict.ContainsKey(raycastId))
			{
				if (callback == null)
					this.onClickDict[raycastId] = null;
				else
					this.onClickDict[raycastId] -= callback;
				if (this.onClickDict[raycastId] == null)
					this.onClickDict.Remove(raycastId);
			}

			UpdateRaycastState();
		}


		public void Raycast(Vector3 screenPosition, int? raycastLayerMask = null)
		{
			Camera userCamera = Camera.main;
			if (camera != null)
				userCamera = camera;
			var ray = userCamera.ScreenPointToRay(screenPosition); //屏幕坐标转射线
			RaycastHit hit; //射线对象是：结构体类型（存储了相关信息）
			bool isHit;
			if (raycastLayerMask == null)
				isHit = Physics.Raycast(ray, out hit); //发出射线检测到了碰撞   isHit返回的是 一个bool值
			else
				isHit = Physics.Raycast(ray, out hit, float.MaxValue,
					raycastLayerMask.Value); //发出射线检测到了碰撞   isHit返回的是 一个bool值
			if (isHit)
			{
				//Debug.Log("坐标为：" + hit.point + hit.transform.name);
			}

			OnRaycast(isHit, hit);
		}

		public void OnRaycast(bool isHit, RaycastHit hit)
		{
			if (isHit)
			{
				this.lastHit = hit;
				this.Broadcast(null, PhysicsEventNameConst.On_Raycast, hit);
				XLuaManager.instance.CallLuaFunction("global.client.physicsManager:OnRaycast", hit);
			}
		}

		public override bool IsCanUpdate()
		{
			return isCanRaycast && base.IsCanUpdate();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			if (Input.GetMouseButtonDown(0) && !isClickDown)
			{
				if (UIUtil.IsOverUI(Input.mousePosition)) //点击在UI上的不用处理
					return;
				isClickDown = true;
				Raycast(Input.mousePosition);
				OnPointerDown();
			}
			else
			{
				isClickDown = false;
				this.OnPointerUp();
			}
		}

		private void OnPointerDown()
		{
			if (this.lastHit.HasValue)
			{
				string raycastId = GetRaycastIdByHit(this.lastHit.Value);
				if (this.onPointerDownDict.ContainsKey(raycastId))
					this.onPointerDownDict[raycastId]();
			}
		}

		private void OnPointerUp()
		{
			if (this.lastHit.HasValue)
			{
				string raycastId = GetRaycastIdByHit(this.lastHit.Value);
				if (this.onPointerUpDict.ContainsKey(raycastId))
					this.onPointerUpDict[raycastId]();
				if (this.onClickDict.ContainsKey(raycastId))
					this.onClickDict[raycastId]();
				this.lastHit = null;
			}
		}
	}
}