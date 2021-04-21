using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
  public class UIRocker : UIObject
  {
    private float move_pct_x;
    private float move_pct_y;
    private int point_id;
    private UIRockerInput uiRockerInput;
    private GameObject uiRocker_gameObject;
    private RectTransform uiRocker_rectTransform;
    private Vector2 uiRocker_rectTransform_sizeDelta;
    private Vector2 uiRocker_origin_anchoredPosition;
    private float uiRocker_radius;
    private float uiRocker_radius_in_eventData;
    private GameObject boll_gameObject;
    private RectTransform boll_rectTransform;
    private Vector2 boll_origin_anchoredPosition;
    private GameObject arrow_gameObject;
    private RectTransform arrow_rectTransform;
    private bool is_need_response_with_set_alpha;
    private Vector2 uiRocker_down_pos_in_eventData;
    private Vector2 uiRocker_down_anchoredPosition;
    private Image boll_image;
    private CanvasGroup canvasGroup;
    private bool is_draging;


    public void Init(string prefab_path, Transform parent_transform, UIRockerInput uiRockerInput)
    {
      base.Init();
      this.graphicComponent.SetPrefabPath(prefab_path ?? UIRockerConst.UIRock_Prefab_Path);
      this.graphicComponent.SetParentTransform(parent_transform);
      this.uiRockerInput = uiRockerInput;

      this.AddListener<float, float>(GlobalEventNameConst.Update, Update);
    }

    public override void OnAllAssetsLoadDone()
    {
      base.OnAllAssetsLoadDone();
      var uiRocker_trigger_area_gameObject = graphicComponent.transform.Find("uiRocker_trigger_area").gameObject;
      this.RegisterOnDrag(uiRocker_trigger_area_gameObject, this.OnUIRockerDrag);
      this.RegisterOnPointerDown(uiRocker_trigger_area_gameObject, this.OnUIRockerPointerDown);
      this.RegisterOnPointerUp(uiRocker_trigger_area_gameObject, this.OnUIRockerPointerUp);

      this.uiRocker_gameObject = graphicComponent.transform.Find("uiRocker").gameObject;
      this.uiRocker_rectTransform = this.uiRocker_gameObject.GetComponent<RectTransform>();
      this.uiRocker_rectTransform_sizeDelta = this.uiRocker_rectTransform.sizeDelta;
      this.uiRocker_origin_anchoredPosition = this.uiRocker_rectTransform.anchoredPosition;
      this.uiRocker_radius = this.uiRocker_rectTransform_sizeDelta.x / 2;
      this.uiRocker_radius_in_eventData = this.uiRocker_radius; //是跟self.rocker_radius一样的

      this.boll_gameObject = this.uiRocker_gameObject.transform.Find("boll").gameObject;
      this.boll_rectTransform = this.boll_gameObject.GetComponent<RectTransform>();
      this.boll_origin_anchoredPosition = this.boll_rectTransform.anchoredPosition;

      this.arrow_gameObject = this.uiRocker_gameObject.transform.Find("arrow").gameObject;
      this.arrow_rectTransform = this.arrow_gameObject.GetComponent<RectTransform>();


      this.boll_image = this.boll_gameObject.GetComponent<Image>();
      this.canvasGroup = graphicComponent.gameObject.GetComponent<CanvasGroup>();
    }

    // 响应的时候是否需要设值alpha值
    // 按住时设置alpha为1
    // 松开时设置alpha为0
    public void SetIsNeedResponseWithSetAlpha(bool is_need_response_with_set_alpha)
    {
      this.is_need_response_with_set_alpha = is_need_response_with_set_alpha;
    }

    protected override void __SetIsEnabled(bool is_enabled)
    {
      base.__SetIsEnabled(is_enabled);
      if (!is_enabled)
        this.OnUIRockerPointerUp(null);
    }

    public void SetUIRockerAnchoredPosition(float x, float y)
    {
      this.uiRocker_rectTransform.anchoredPosition = new Vector2(x, y);
    }

    public void SetBollAnchoredPosition(float x, float y)
    {
      this.boll_rectTransform.anchoredPosition = new Vector2(x, y);
    }

    public void SetArrowLocalRotation(float z)
    {
      this.arrow_rectTransform.localRotation = Quaternion.Euler(0, 0, z);
    }

    public void SetArrowAcitve(bool is_active)
    {
      this.arrow_gameObject.SetActive(is_active);
    }


    protected void Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      if (!this.IsCanUpdate())
        return;
      if (this.move_pct_x != 0 || this.move_pct_y != 0)
        this.uiRockerInput.AxisMove(this.move_pct_x, this.move_pct_y);
    }

    public void OnUIRockerPointerDown(PointerEventData eventData)
    {
      if (!this.is_enabled)
        return;
      this.point_id = eventData.pointerId;
      this.uiRocker_down_pos_in_eventData = eventData.pressPosition;
      this.uiRocker_down_anchoredPosition = CameraUtil.ScreenToUIPos(null, null,
        new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0), this.uiRocker_rectTransform.pivot);
      var offset = this.uiRocker_rectTransform.pivot - new Vector2(0.5f, 0.5f); // 还原到点击的位置为中心点
      var uiRocker_down_anchoredPosition = this.uiRocker_down_anchoredPosition +
                                         new Vector2(offset.x * this.uiRocker_rectTransform.sizeDelta.x,
                                           offset.y * this.uiRocker_rectTransform.sizeDelta.y); // 还原到点击的位置为中心点
      this.SetUIRockerAnchoredPosition(uiRocker_down_anchoredPosition.x, uiRocker_down_anchoredPosition.y);
      this.boll_image.color = new Color(1, 1, 1, 1);

      if (this.is_need_response_with_set_alpha)
        this.canvasGroup.alpha = 1;
    }

    public void OnUIRockerPointerUp(PointerEventData eventData)
    {
      this.is_draging = false;
      if (!this.is_enabled)
        return;
      if (eventData != null && this.point_id != eventData.pointerId)
        return;
      this.SetUIRockerAnchoredPosition(this.uiRocker_origin_anchoredPosition.x, this.uiRocker_origin_anchoredPosition.y);
      this.SetBollAnchoredPosition(this.boll_origin_anchoredPosition.x, this.boll_origin_anchoredPosition.y);
      this.SetArrowAcitve(false);
      this.SetArrowLocalRotation(0);
      this.move_pct_x = 0;
      this.move_pct_y = 0;

      this.uiRockerInput.AxisMove(0, 0);
      this.boll_image.color = new Color(1, 1, 1, 0.3f);
      if (this.is_need_response_with_set_alpha)
        this.canvasGroup.alpha = 0;
    }

    public void OnUIRockerDrag(PointerEventData eventData)
    {
      if (!this.is_enabled)
        return;
      if (this.point_id != eventData.pointerId)
        return;
      var dx = eventData.position.x - this.uiRocker_down_pos_in_eventData.x;
      var dy = eventData.position.y - this.uiRocker_down_pos_in_eventData.y;
      var distance = (float)Math.Sqrt(dx * dx + dy * dy);

      var _dx = Mathf.Clamp(dx, -this.uiRocker_radius_in_eventData, this.uiRocker_radius_in_eventData); // 不能超过半径
      var _dy = Mathf.Clamp(dy, -this.uiRocker_radius_in_eventData, this.uiRocker_radius_in_eventData); // 不能超过半径
      var pct_x = Mathf.Abs(dx) / distance; // 比例，用于还原到圆内的坐标
      var pct_y = Mathf.Abs(dy) / distance; // 比例，用于还原到圆内的坐标

      this.SetBollAnchoredPosition(_dx * pct_x, _dy * pct_y);
      var dir = new Vector3(dx, dy, 0);
      var angle = Vector3.Angle(dir, Vector3.up);
      if (!(Vector3.Cross(dir, Vector3.forward).y > 0))
        angle = -angle;
      this.SetArrowAcitve(true);
      this.SetArrowLocalRotation(angle);
      this.move_pct_x = distance == 0 ? 0 : _dx * pct_x / this.uiRocker_radius_in_eventData;
      this.move_pct_y = distance == 0 ? 0 : _dy * pct_y / this.uiRocker_radius_in_eventData;
      if (this.move_pct_x == 0 && this.move_pct_y == 0)
      {
        this.SetArrowAcitve(false);
        this.SetBollAnchoredPosition(0, 0);
      }

      this.is_draging = true;
    }

    protected override void __Reset()
    {
      base.__Reset();
      if (graphicComponent.gameObject == null)
        return;
      this.OnUIRockerPointerUp(null);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      if (graphicComponent.gameObject == null)
        return;
      this.OnUIRockerPointerUp(null);
    }
  }
}