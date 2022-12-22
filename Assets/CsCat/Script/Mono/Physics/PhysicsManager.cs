using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class PhysicsManager : TickObject
	{
		private Camera _camera;
		private LayerMask? _raycastLayerMask;
		private bool _isClickDown;
		private bool _isCanRaycast = false;
		private Dictionary<string, Action> _onPointerDownDict = new Dictionary<string, Action>();
		private Dictionary<string, Action> _onPointerUpDict = new Dictionary<string, Action>();
		private Dictionary<string, Action> _onClickDict = new Dictionary<string, Action>();
		private int _raycastId;
		private RaycastHit? _lastHit;

		public void SetCamera(Camera camera)
		{
			this._camera = camera;
		}

		public void SetRaycastLayer(params string[] layerNames)
		{
			if (layerNames.Length == 0)
				this._raycastLayerMask = null;
			this._raycastLayerMask = LayerMask.GetMask(layerNames);
		}

		private void _SetIsCanRaycast(bool isCanRaycast)
		{
			this._isCanRaycast = isCanRaycast;
		}

		private void _UpdateRaycastState()
		{
			if (_onPointerDownDict.Count == 0 && _onPointerUpDict.Count == 0 && _onClickDict.Count == 0)
				_SetIsCanRaycast(false);
			else
				_SetIsCanRaycast(true);
		}

		private string _GetOrAddRaycastId(GameObject gameObject)
		{
			string raycastId = gameObject.GetOrAddCache("raycastIdCs", () => this._raycastId++.ToString());
			return raycastId;
		}

		private string _GetRaycastIdByHit(RaycastHit hit)
		{
			return _GetOrAddRaycastId(hit.transform.gameObject);
		}

		public void RegisterOnPointerDown(GameObject gameObject, Action callback)
		{
			string raycastId = _GetOrAddRaycastId(gameObject);
			if (this._onPointerDownDict.ContainsKey(raycastId))
				this._onPointerDownDict[raycastId] += callback;
			else
				this._onPointerDownDict[raycastId] = callback;
			this._UpdateRaycastState();
		}

		public void RegisterOnPointerUp(GameObject gameObject, Action callback)
		{
			string raycastId = _GetOrAddRaycastId(gameObject);
			if (this._onPointerUpDict.ContainsKey(raycastId))
				this._onPointerUpDict[raycastId] += callback;
			else
				this._onPointerUpDict[raycastId] = callback;
			this._UpdateRaycastState();
		}

		public void RegisterOnClick(GameObject gameObject, Action callback)
		{
			string raycastId = _GetOrAddRaycastId(gameObject);
			if (this._onClickDict.ContainsKey(raycastId))
				this._onClickDict[raycastId] += callback;
			else
				this._onClickDict[raycastId] = callback;
			this._UpdateRaycastState();
		}

		public void UnRegisterOnPointerDown(GameObject gameObject, Action callback = null)
		{
			string raycastId = _GetOrAddRaycastId(gameObject);
			if (this._onPointerDownDict.ContainsKey(raycastId))
			{
				if (callback == null)
					this._onPointerDownDict[raycastId] = null;
				else
					this._onPointerDownDict[raycastId] -= callback;
				if (this._onPointerDownDict[raycastId] == null)
					this._onPointerDownDict.Remove(raycastId);
			}

			_UpdateRaycastState();
		}

		public void UnRegisterOnPointerUp(GameObject gameObject, Action callback = null)
		{
			string raycastId = _GetOrAddRaycastId(gameObject);
			if (this._onPointerUpDict.ContainsKey(raycastId))
			{
				if (callback == null)
					this._onPointerUpDict[raycastId] = null;
				else
					this._onPointerUpDict[raycastId] -= callback;
				if (this._onPointerUpDict[raycastId] == null)
					this._onPointerUpDict.Remove(raycastId);
			}

			_UpdateRaycastState();
		}

		public void UnRegisterOnClick(GameObject gameObject, Action callback = null)
		{
			string raycastId = _GetOrAddRaycastId(gameObject);
			if (this._onClickDict.ContainsKey(raycastId))
			{
				if (callback == null)
					this._onClickDict[raycastId] = null;
				else
					this._onClickDict[raycastId] -= callback;
				if (this._onClickDict[raycastId] == null)
					this._onClickDict.Remove(raycastId);
			}

			_UpdateRaycastState();
		}


		public void Raycast(Vector3 screenPosition, int? raycastLayerMask = null)
		{
			Camera userCamera = Camera.main;
			if (_camera != null)
				userCamera = _camera;
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
				this._lastHit = hit;
				this.FireEvent(null, PhysicsEventNameConst.On_Raycast, hit);
				XLuaManager.instance.CallLuaFunction("global.client.physicsManager:OnRaycast", hit);
			}
		}

		public override bool IsCanUpdate()
		{
			return _isCanRaycast && base.IsCanUpdate();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			if (Input.GetMouseButtonDown(0) && !_isClickDown)
			{
				if (UIUtil.IsOverUI(Input.mousePosition)) //点击在UI上的不用处理
					return;
				_isClickDown = true;
				Raycast(Input.mousePosition);
				OnPointerDown();
			}
			else
			{
				_isClickDown = false;
				this.OnPointerUp();
			}
		}

		private void OnPointerDown()
		{
			if (this._lastHit.HasValue)
			{
				string raycastId = _GetRaycastIdByHit(this._lastHit.Value);
				if (this._onPointerDownDict.ContainsKey(raycastId))
					this._onPointerDownDict[raycastId]();
			}
		}

		private void OnPointerUp()
		{
			if (this._lastHit.HasValue)
			{
				string raycastId = _GetRaycastIdByHit(this._lastHit.Value);
				if (this._onPointerUpDict.ContainsKey(raycastId))
					this._onPointerUpDict[raycastId]();
				if (this._onClickDict.ContainsKey(raycastId))
					this._onClickDict[raycastId]();
				this._lastHit = null;
			}
		}
	}
}