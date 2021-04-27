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

    private object move_range;
    private float delta_move_scale;
    private float delta_height_sacle;
    private int touch_count; // 触摸数
    private CameraManager cameraManager;

    private Dictionary<int, Vector2>
      modify_camera_height_info = new Dictionary<int, Vector2>(); //用于双指控制调整摄像头高度时使用，记录手指两点距离变化

    private float last_distance = 0;

    public void Init(object move_range)
    {
      base.Init();
      this.move_range = move_range;

      this.graphicComponent.SetPrefabPath("Assets/Resources/common/ui/prefab/UIScreenDragPanel.prefab");

      //用于移动的比例
      this.delta_move_scale = 1 / (Screen.height / ScreenConst.Design_Resolution_Height * 12);
      this.delta_height_sacle = Screen.height / ScreenConst.Design_Resolution_Height * 0.15f; // 屏幕拖拽控制器缩放屏幕灵敏度
      //设置摄像机移动范围
      this.cameraManager = Client.instance.combat.cameraManager;
      this.cameraManager.SetMainCameraMoveRange(move_range);
    }

    protected override void AddUntiyEvnts()
    {
      base.AddUntiyEvnts();
      this.RegisterOnDrag(graphicComponent.gameObject, this.OnUIScreenDrag);
      this.RegisterOnPointerDown(graphicComponent.gameObject, this.OnUIScreenPointerDown);
      this.RegisterOnPointerUp(graphicComponent.gameObject, this.OnUIScreenPointerUp);
    }
    


    protected override void __SetIsEnabled(bool is_enabled)
    {
      base.__SetIsEnabled(is_enabled);
      graphicComponent.SetIsShow(is_enabled);
      if (!is_enabled)
        this.touch_count = 0;
    }

    public void OnUIScreenPointerDown(PointerEventData eventData)
    {
      this.touch_count = this.touch_count + 1;
      // 不处理大于2个触摸点的操作
      if (this.touch_count > 2)
        return;

      //记录当前手指坐标
      this.modify_camera_height_info[eventData.pointerId] = eventData.position;
      //计算当前两个手指的距离
      if (this.touch_count > 1)
        this.last_distance = this.__CalculateTwoPointDistance();

      if (this.cameraManager != null)
        this.cameraManager.MoveByDelta(0, 0, 0);
    }

    // 计算两个触点的距离
    private float __CalculateTwoPointDistance()
    {
      Vector2? other_point = null;
      foreach (var eventData_position in this.modify_camera_height_info.Values)
      {
        if (other_point.HasValue)
        {
          return Vector2.Distance(other_point.Value, eventData_position);
        }

        other_point = eventData_position;
      }

      //如果只剩下一个点，则返回上一次的距离
      return this.last_distance;
    }

    public void OnUIScreenPointerUp(PointerEventData eventData)
    {
      this.modify_camera_height_info.Remove(eventData.pointerId);
      this.touch_count = this.touch_count - 1;
    }


    public void OnUIScreenDrag(PointerEventData eventData)
    {
      if (this.cameraManager == null)
        return;
      //如果是一个触摸点的时候是拖拽屏幕移动
      if (this.touch_count < 1.5f)
      {
        this.cameraManager.MoveByDelta(eventData.delta.x * this.delta_move_scale,
          eventData.delta.y * this.delta_move_scale, 0);
      }
      else
      {
        //如果是大于1个触摸点的时候，是调整摄像头高度
        this.modify_camera_height_info[eventData.pointerId] = eventData.position;
        var distance = this.__CalculateTwoPointDistance();
        this.cameraManager.MoveByDelta(0, 0, (distance - this.last_distance) * this.delta_height_sacle);
        this.last_distance = distance;
      }
    }

    protected override void __Reset()
    {
      base.__Reset();
      cameraManager?.SetMainCameraMoveRange(null);
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      cameraManager?.SetMainCameraMoveRange(null);
    }
  }
}