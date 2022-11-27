using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public class UIRocker : UIObject
	{
		private float _movePCTX;
		private float _movePCTY;
		private int _pointId;
		private UIRockerInput _uiRockerInput;
		
		private Vector2 _uiRockerRectTransformSizeDelta;
		private Vector2 _uiRockerOriginAnchoredPosition;
		private float _uiRockerRadius;
		private float _uiRockerRadiusInEventData;
		private GameObject _Nego_Boll;
		private RectTransform _Nego_Boll_RectTransform;
		private Vector2 _bollOriginAnchoredPosition;
		private GameObject _Nego_Arrow;
		private RectTransform _Nego_Arrow_RectTransform;
		private bool _isNeedResponseWithSetAlpha;
		private Vector2 _uiRockerDownPosInEventData;
		private Vector2 _uiRockerDownAnchoredPosition;
		private Image _ImgC_Boll;
		private CanvasGroup _canvasGroup;
		private bool _isDraging;

		private GameObject _Nego_RockerTriggerArea;
		private GameObject _Nego_Rocker;
		private RectTransform _Nego_Rocker_RectTransform;


		protected void _Init(string prefabPath, Transform parentTransform, UIRockerInput uiRockerInput)
		{
			base._Init();
			SetPrefabPath(prefabPath ?? UIRockerConst.UIRock_Prefab_Path);
			SetParentTransform(parentTransform);
			this._uiRockerInput = uiRockerInput;

			this.AddListener<float, float>(null, GlobalEventNameConst.Update, Update);
		}

		protected override void InitGameObjectChildren()
		{
			base.InitGameObjectChildren();
			this._Nego_RockerTriggerArea = this.GetTransform().Find("Nego_RockerTriggerArea").gameObject;
			this._Nego_Rocker = this.GetTransform().Find("Nego_Rocker").gameObject;
			this._Nego_Rocker_RectTransform = this._Nego_Rocker.GetComponent<RectTransform>();
			this._ImgC_Boll = this._Nego_Rocker.transform.Find("ImgC_Boll").GetComponent<Image>();
			this._Nego_Boll = this._ImgC_Boll.gameObject;
			this._Nego_Boll_RectTransform = this._Nego_Boll.GetComponent<RectTransform>();
			this._Nego_Arrow = this._Nego_Rocker.transform.Find("Nego_Arrow").gameObject;
			this._Nego_Arrow_RectTransform = this._Nego_Arrow.GetComponent<RectTransform>();
			this._canvasGroup = this.GetGameObject().GetComponent<CanvasGroup>();


			this._uiRockerRectTransformSizeDelta = this._Nego_Rocker_RectTransform.sizeDelta;
			this._uiRockerOriginAnchoredPosition = this._Nego_Rocker_RectTransform.anchoredPosition;
			this._uiRockerRadius = this._uiRockerRectTransformSizeDelta.x / 2;
			this._uiRockerRadiusInEventData = this._uiRockerRadius; //是跟self.rocker_radius一样的
			this._bollOriginAnchoredPosition = this._Nego_Boll_RectTransform.anchoredPosition;
		}

		protected override void AddUnityListeners()
		{
			base.AddUnityListeners();
			this.RegisterOnDrag(this._Nego_RockerTriggerArea, this.OnNego_RockerDrag);
			this.RegisterOnPointerDown(this._Nego_RockerTriggerArea, this.OnNego_RockerPointerDown);
			this.RegisterOnPointerUp(this._Nego_RockerTriggerArea, this.OnNego_RockerPointerUp);
		}

		// 响应的时候是否需要设值alpha值
		// 按住时设置alpha为1
		// 松开时设置alpha为0
		public void SetIsNeedResponseWithSetAlpha(bool isNeedResponseWithSetAlpha)
		{
			this._isNeedResponseWithSetAlpha = isNeedResponseWithSetAlpha;
		}

		protected override void _SetIsEnabled(bool isEnabled)
		{
			base._SetIsEnabled(isEnabled);
			if (!isEnabled)
				this.OnNego_RockerPointerUp(null);
		}

		public void SetNego_RockerAnchoredPosition(float x, float y)
		{
			this._Nego_Rocker_RectTransform.anchoredPosition = new Vector2(x, y);
		}

		public void SetNego_BollAnchoredPosition(float x, float y)
		{
			this._Nego_Boll_RectTransform.anchoredPosition = new Vector2(x, y);
		}

		public void SetNego_ArrowLocalRotation(float z)
		{
			this._Nego_Arrow_RectTransform.localRotation = Quaternion.Euler(0, 0, z);
		}

		public void SetNego_ArrowActive(bool isActive)
		{
			this._Nego_Arrow.SetActive(isActive);
		}


		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update();
			if (this._movePCTX != 0 || this._movePCTY != 0)
				this._uiRockerInput.AxisMove(this._movePCTX, this._movePCTY);
		}

		public void OnNego_RockerPointerDown(PointerEventData eventData)
		{
			if (!this.isEnabled)
				return;
			this._pointId = eventData.pointerId;
			this._uiRockerDownPosInEventData = eventData.pressPosition;
			this._uiRockerDownAnchoredPosition = CameraUtil.ScreenToUIPos(null, null,
			  new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0), this._Nego_Rocker_RectTransform.pivot);
			var offset = this._Nego_Rocker_RectTransform.pivot - new Vector2(0.5f, 0.5f); // 还原到点击的位置为中心点
			var nego_RockerDownAnchoredPosition = this._uiRockerDownAnchoredPosition +
											   new Vector2(offset.x * this._Nego_Rocker_RectTransform.sizeDelta.x,
												 offset.y * this._Nego_Rocker_RectTransform.sizeDelta.y); // 还原到点击的位置为中心点
			this.SetNego_RockerAnchoredPosition(nego_RockerDownAnchoredPosition.x, nego_RockerDownAnchoredPosition.y);
			this._ImgC_Boll.color = new Color(1, 1, 1, 1);

			if (this._isNeedResponseWithSetAlpha)
				this._canvasGroup.alpha = 1;
		}

		public void OnNego_RockerPointerUp(PointerEventData eventData)
		{
			this._isDraging = false;
			if (!this.isEnabled)
				return;
			if (eventData != null && this._pointId != eventData.pointerId)
				return;
			this.SetNego_RockerAnchoredPosition(this._uiRockerOriginAnchoredPosition.x, this._uiRockerOriginAnchoredPosition.y);
			this.SetNego_BollAnchoredPosition(this._bollOriginAnchoredPosition.x, this._bollOriginAnchoredPosition.y);
			this.SetNego_ArrowActive(false);
			this.SetNego_ArrowLocalRotation(0);
			this._movePCTX = 0;
			this._movePCTY = 0;

			this._uiRockerInput.AxisMove(0, 0);
			this._ImgC_Boll.color = new Color(1, 1, 1, 0.3f);
			if (this._isNeedResponseWithSetAlpha)
				this._canvasGroup.alpha = 0;
		}

		public void OnNego_RockerDrag(PointerEventData eventData)
		{
			if (!this.isEnabled)
				return;
			if (this._pointId != eventData.pointerId)
				return;
			var dx = eventData.position.x - this._uiRockerDownPosInEventData.x;
			var dy = eventData.position.y - this._uiRockerDownPosInEventData.y;
			var distance = (float)Math.Sqrt(dx * dx + dy * dy);

			var _dx = Mathf.Clamp(dx, -this._uiRockerRadiusInEventData, this._uiRockerRadiusInEventData); // 不能超过半径
			var _dy = Mathf.Clamp(dy, -this._uiRockerRadiusInEventData, this._uiRockerRadiusInEventData); // 不能超过半径
			var pctX = Mathf.Abs(dx) / distance; // 比例，用于还原到圆内的坐标
			var pctY = Mathf.Abs(dy) / distance; // 比例，用于还原到圆内的坐标

			this.SetNego_BollAnchoredPosition(_dx * pctX, _dy * pctY);
			var dir = new Vector3(dx, dy, 0);
			var angle = Vector3.Angle(dir, Vector3.up);
			if (!(Vector3.Cross(dir, Vector3.forward).y > 0))
				angle = -angle;
			this.SetNego_ArrowActive(true);
			this.SetNego_ArrowLocalRotation(angle);
			this._movePCTX = distance == 0 ? 0 : _dx * pctX / this._uiRockerRadiusInEventData;
			this._movePCTY = distance == 0 ? 0 : _dy * pctY / this._uiRockerRadiusInEventData;
			if (this._movePCTX == 0 && this._movePCTY == 0)
			{
				this.SetNego_ArrowActive(false);
				this.SetNego_BollAnchoredPosition(0, 0);
			}

			this._isDraging = true;
		}
	}
}