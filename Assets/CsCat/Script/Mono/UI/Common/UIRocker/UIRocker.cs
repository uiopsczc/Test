using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public class UIRocker : UIObject
	{
		private float movePCTX;
		private float movePCTY;
		private int pointId;
		private UIRockerInput uiRockerInput;
		private GameObject uiRockerGameObject;
		private RectTransform uiRockerRectTransform;
		private Vector2 uiRockerRectTransformSizeDelta;
		private Vector2 uiRockerOriginAnchoredPosition;
		private float uiRockerRadius;
		private float uiRockerRadiusInEventData;
		private GameObject bollGameObject;
		private RectTransform bollRectTransform;
		private Vector2 bollOriginAnchoredPosition;
		private GameObject arrowGameObject;
		private RectTransform arrowRectTransform;
		private bool isNeedResponseWithSetAlpha;
		private Vector2 uiRockerDownPosInEventData;
		private Vector2 uiRockerDownAnchoredPosition;
		private Image bollImage;
		private CanvasGroup canvasGroup;
		private bool isDraging;


		public void Init(string prefabPath, Transform parentTransform, UIRockerInput uiRockerInput)
		{
			base.Init();
			this.graphicComponent.SetPrefabPath(prefabPath ?? UIRockerConst.UIRock_Prefab_Path);
			this.graphicComponent.SetParentTransform(parentTransform);
			this.uiRockerInput = uiRockerInput;

			this.AddListener<float, float>(null, GlobalEventNameConst.Update, Update);
		}

		public override void OnAllAssetsLoadDone()
		{
			base.OnAllAssetsLoadDone();
			var uiRockerTriggerAreaGameObject = graphicComponent.transform.Find("uiRocker_trigger_area").gameObject;
			this.RegisterOnDrag(uiRockerTriggerAreaGameObject, this.OnUIRockerDrag);
			this.RegisterOnPointerDown(uiRockerTriggerAreaGameObject, this.OnUIRockerPointerDown);
			this.RegisterOnPointerUp(uiRockerTriggerAreaGameObject, this.OnUIRockerPointerUp);

			this.uiRockerGameObject = graphicComponent.transform.Find("uiRocker").gameObject;
			this.uiRockerRectTransform = this.uiRockerGameObject.GetComponent<RectTransform>();
			this.uiRockerRectTransformSizeDelta = this.uiRockerRectTransform.sizeDelta;
			this.uiRockerOriginAnchoredPosition = this.uiRockerRectTransform.anchoredPosition;
			this.uiRockerRadius = this.uiRockerRectTransformSizeDelta.x / 2;
			this.uiRockerRadiusInEventData = this.uiRockerRadius; //是跟self.rocker_radius一样的

			this.bollGameObject = this.uiRockerGameObject.transform.Find("boll").gameObject;
			this.bollRectTransform = this.bollGameObject.GetComponent<RectTransform>();
			this.bollOriginAnchoredPosition = this.bollRectTransform.anchoredPosition;

			this.arrowGameObject = this.uiRockerGameObject.transform.Find("arrow").gameObject;
			this.arrowRectTransform = this.arrowGameObject.GetComponent<RectTransform>();


			this.bollImage = this.bollGameObject.GetComponent<Image>();
			this.canvasGroup = graphicComponent.gameObject.GetComponent<CanvasGroup>();
		}

		// 响应的时候是否需要设值alpha值
		// 按住时设置alpha为1
		// 松开时设置alpha为0
		public void SetIsNeedResponseWithSetAlpha(bool isNeedResponseWithSetAlpha)
		{
			this.isNeedResponseWithSetAlpha = isNeedResponseWithSetAlpha;
		}

		protected override void _SetIsEnabled(bool isEnabled)
		{
			base._SetIsEnabled(isEnabled);
			if (!isEnabled)
				this.OnUIRockerPointerUp(null);
		}

		public void SetUIRockerAnchoredPosition(float x, float y)
		{
			this.uiRockerRectTransform.anchoredPosition = new Vector2(x, y);
		}

		public void SetBollAnchoredPosition(float x, float y)
		{
			this.bollRectTransform.anchoredPosition = new Vector2(x, y);
		}

		public void SetArrowLocalRotation(float z)
		{
			this.arrowRectTransform.localRotation = Quaternion.Euler(0, 0, z);
		}

		public void SetArrowActive(bool isActive)
		{
			this.arrowGameObject.SetActive(isActive);
		}


		protected void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			if (!this.IsCanUpdate())
				return;
			if (this.movePCTX != 0 || this.movePCTY != 0)
				this.uiRockerInput.AxisMove(this.movePCTX, this.movePCTY);
		}

		public void OnUIRockerPointerDown(PointerEventData eventData)
		{
			if (!this.isEnabled)
				return;
			this.pointId = eventData.pointerId;
			this.uiRockerDownPosInEventData = eventData.pressPosition;
			this.uiRockerDownAnchoredPosition = CameraUtil.ScreenToUIPos(null, null,
			  new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0), this.uiRockerRectTransform.pivot);
			var offset = this.uiRockerRectTransform.pivot - new Vector2(0.5f, 0.5f); // 还原到点击的位置为中心点
			var uiRockerDownAnchoredPosition = this.uiRockerDownAnchoredPosition +
											   new Vector2(offset.x * this.uiRockerRectTransform.sizeDelta.x,
												 offset.y * this.uiRockerRectTransform.sizeDelta.y); // 还原到点击的位置为中心点
			this.SetUIRockerAnchoredPosition(uiRockerDownAnchoredPosition.x, uiRockerDownAnchoredPosition.y);
			this.bollImage.color = new Color(1, 1, 1, 1);

			if (this.isNeedResponseWithSetAlpha)
				this.canvasGroup.alpha = 1;
		}

		public void OnUIRockerPointerUp(PointerEventData eventData)
		{
			this.isDraging = false;
			if (!this.isEnabled)
				return;
			if (eventData != null && this.pointId != eventData.pointerId)
				return;
			this.SetUIRockerAnchoredPosition(this.uiRockerOriginAnchoredPosition.x, this.uiRockerOriginAnchoredPosition.y);
			this.SetBollAnchoredPosition(this.bollOriginAnchoredPosition.x, this.bollOriginAnchoredPosition.y);
			this.SetArrowActive(false);
			this.SetArrowLocalRotation(0);
			this.movePCTX = 0;
			this.movePCTY = 0;

			this.uiRockerInput.AxisMove(0, 0);
			this.bollImage.color = new Color(1, 1, 1, 0.3f);
			if (this.isNeedResponseWithSetAlpha)
				this.canvasGroup.alpha = 0;
		}

		public void OnUIRockerDrag(PointerEventData eventData)
		{
			if (!this.isEnabled)
				return;
			if (this.pointId != eventData.pointerId)
				return;
			var dx = eventData.position.x - this.uiRockerDownPosInEventData.x;
			var dy = eventData.position.y - this.uiRockerDownPosInEventData.y;
			var distance = (float)Math.Sqrt(dx * dx + dy * dy);

			var _dx = Mathf.Clamp(dx, -this.uiRockerRadiusInEventData, this.uiRockerRadiusInEventData); // 不能超过半径
			var _dy = Mathf.Clamp(dy, -this.uiRockerRadiusInEventData, this.uiRockerRadiusInEventData); // 不能超过半径
			var pctX = Mathf.Abs(dx) / distance; // 比例，用于还原到圆内的坐标
			var pctY = Mathf.Abs(dy) / distance; // 比例，用于还原到圆内的坐标

			this.SetBollAnchoredPosition(_dx * pctX, _dy * pctY);
			var dir = new Vector3(dx, dy, 0);
			var angle = Vector3.Angle(dir, Vector3.up);
			if (!(Vector3.Cross(dir, Vector3.forward).y > 0))
				angle = -angle;
			this.SetArrowActive(true);
			this.SetArrowLocalRotation(angle);
			this.movePCTX = distance == 0 ? 0 : _dx * pctX / this.uiRockerRadiusInEventData;
			this.movePCTY = distance == 0 ? 0 : _dy * pctY / this.uiRockerRadiusInEventData;
			if (this.movePCTX == 0 && this.movePCTY == 0)
			{
				this.SetArrowActive(false);
				this.SetBollAnchoredPosition(0, 0);
			}

			this.isDraging = true;
		}

		protected override void _Reset()
		{
			base._Reset();
			if (graphicComponent.gameObject == null)
				return;
			this.OnUIRockerPointerUp(null);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			if (graphicComponent.gameObject == null)
				return;
			this.OnUIRockerPointerUp(null);
		}
	}
}