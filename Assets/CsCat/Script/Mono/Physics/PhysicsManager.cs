using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class PhysicsManager : TickObject
	{
		private Camera camera;
		private LayerMask? raycast_layerMask;
		private bool is_click_down;
		private bool is_can_raycast = false;
		private Dictionary<string, Action> on_pointer_down_dict = new Dictionary<string, Action>();
		private Dictionary<string, Action> on_pointer_up_dict = new Dictionary<string, Action>();
		private Dictionary<string, Action> on_click_dict = new Dictionary<string, Action>();
		private int raycast_id;
		private RaycastHit? last_hit;

		public void SetCamera(Camera camera)
		{
			this.camera = camera;
		}

		public void SetRaycastLayer(params string[] layer_names)
		{
			if (layer_names.Length == 0)
				this.raycast_layerMask = null;
			this.raycast_layerMask = LayerMask.GetMask(layer_names);
		}

		private void SetIsCanRaycast(bool is_can_raycast)
		{
			this.is_can_raycast = is_can_raycast;
		}

		private void UpdateRaycastState()
		{
			if (on_pointer_down_dict.Count == 0 && on_pointer_up_dict.Count == 0 && on_click_dict.Count == 0)
				SetIsCanRaycast(false);
			else
				SetIsCanRaycast(true);
		}

		private string GetOrAddRaycastId(GameObject gameObject)
		{
			string raycast_id = gameObject.GetOrAddCache("raycast_id_cs", () => this.raycast_id++.ToString());
			return raycast_id;
		}

		private string GetRaycastIdByHit(RaycastHit hit)
		{
			return GetOrAddRaycastId(hit.transform.gameObject);
		}

		public void RegisterOnPointerDown(GameObject gameObject, Action callback)
		{
			string raycast_id = GetOrAddRaycastId(gameObject);
			if (this.on_pointer_down_dict.ContainsKey(raycast_id))
				this.on_pointer_down_dict[raycast_id] += callback;
			else
				this.on_pointer_down_dict[raycast_id] = callback;
			this.UpdateRaycastState();
		}

		public void RegisterOnPointerUp(GameObject gameObject, Action callback)
		{
			string raycast_id = GetOrAddRaycastId(gameObject);
			if (this.on_pointer_up_dict.ContainsKey(raycast_id))
				this.on_pointer_up_dict[raycast_id] += callback;
			else
				this.on_pointer_up_dict[raycast_id] = callback;
			this.UpdateRaycastState();
		}

		public void RegisterOnClick(GameObject gameObject, Action callback)
		{
			string raycast_id = GetOrAddRaycastId(gameObject);
			if (this.on_click_dict.ContainsKey(raycast_id))
				this.on_click_dict[raycast_id] += callback;
			else
				this.on_click_dict[raycast_id] = callback;
			this.UpdateRaycastState();
		}

		public void UnRegisterOnPointerDown(GameObject gameObject, Action callback = null)
		{
			string raycast_id = GetOrAddRaycastId(gameObject);
			if (this.on_pointer_down_dict.ContainsKey(raycast_id))
			{
				if (callback == null)
					this.on_pointer_down_dict[raycast_id] = null;
				else
					this.on_pointer_down_dict[raycast_id] -= callback;
				if (this.on_pointer_down_dict[raycast_id] == null)
					this.on_pointer_down_dict.Remove(raycast_id);
			}

			UpdateRaycastState();
		}

		public void UnRegisterOnPointerUp(GameObject gameObject, Action callback = null)
		{
			string raycast_id = GetOrAddRaycastId(gameObject);
			if (this.on_pointer_up_dict.ContainsKey(raycast_id))
			{
				if (callback == null)
					this.on_pointer_up_dict[raycast_id] = null;
				else
					this.on_pointer_up_dict[raycast_id] -= callback;
				if (this.on_pointer_up_dict[raycast_id] == null)
					this.on_pointer_up_dict.Remove(raycast_id);
			}

			UpdateRaycastState();
		}

		public void UnRegisterOnClick(GameObject gameObject, Action callback = null)
		{
			string raycast_id = GetOrAddRaycastId(gameObject);
			if (this.on_click_dict.ContainsKey(raycast_id))
			{
				if (callback == null)
					this.on_click_dict[raycast_id] = null;
				else
					this.on_click_dict[raycast_id] -= callback;
				if (this.on_click_dict[raycast_id] == null)
					this.on_click_dict.Remove(raycast_id);
			}

			UpdateRaycastState();
		}



		public void Raycast(Vector3 screen_position, int? raycast_layerMask = null)
		{
			Camera user_camera = Camera.main;
			if (camera != null)
				user_camera = camera;
			var ray = user_camera.ScreenPointToRay(screen_position); //屏幕坐标转射线
			RaycastHit hit; //射线对象是：结构体类型（存储了相关信息）
			bool is_hit;
			if (raycast_layerMask == null)
				is_hit = Physics.Raycast(ray, out hit); //发出射线检测到了碰撞   isHit返回的是 一个bool值
			else
				is_hit = Physics.Raycast(ray, out hit, float.MaxValue,
				  raycast_layerMask.Value); //发出射线检测到了碰撞   isHit返回的是 一个bool值
			if (is_hit)
			{
				//Debug.Log("坐标为：" + hit.point + hit.transform.name);
			}

			OnRaycast(is_hit, hit);
		}

		public void OnRaycast(bool is_hit, RaycastHit hit)
		{
			if (is_hit)
			{
				this.last_hit = hit;
				this.Broadcast(null, PhysicsEventNameConst.On_Raycast, hit);
				XLuaManager.instance.CallLuaFunction("global.client.physicsManager:OnRaycast", hit);
			}
		}

		public override bool IsCanUpdate()
		{
			return is_can_raycast && base.IsCanUpdate();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			if (Input.GetMouseButtonDown(0) && !is_click_down)
			{
				if (UIUtil.IsOverUI(Input.mousePosition)) //点击在UI上的不用处理
					return;
				is_click_down = true;
				Raycast(Input.mousePosition);
				OnPointerDown();
			}
			else
			{
				is_click_down = false;
				this.OnPointerUp();
			}
		}

		private void OnPointerDown()
		{
			if (this.last_hit.HasValue)
			{
				string raycast_id = GetRaycastIdByHit(this.last_hit.Value);
				if (this.on_pointer_down_dict.ContainsKey(raycast_id))
					this.on_pointer_down_dict[raycast_id]();
			}
		}

		private void OnPointerUp()
		{
			if (this.last_hit.HasValue)
			{
				string raycast_id = GetRaycastIdByHit(this.last_hit.Value);
				if (this.on_pointer_up_dict.ContainsKey(raycast_id))
					this.on_pointer_up_dict[raycast_id]();
				if (this.on_click_dict.ContainsKey(raycast_id))
					this.on_click_dict[raycast_id]();
				this.last_hit = null;
			}
		}
	}
}