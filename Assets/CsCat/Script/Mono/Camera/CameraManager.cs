using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class CameraManager : TickObject
  {
    public CameraBase main_cameraBase;
    public CameraBase ui_cameraBase;
    public List<CameraBase> cameraBase_list = new List<CameraBase>();

    public override void Init()
    {
      base.Init();
      main_cameraBase = this.AddChild<CameraBase>("main_camera",
        GameObject.Find("Main Camera").GetComponent<Camera>(), true);
      ui_cameraBase = this.AddChild<CameraBase>("ui_camera",
        GameObject.Find(UIConst.UICamera_Path).GetComponent<Camera>(), true);
      this.AddListener<StageBase>(null,StageEventNameConst.On_Stage_Loaded, OnStageLoaded);
    }

    public void OnStageLoaded(StageBase stage)
    {
      cameraBase_list.Clear();
      Camera[] cameras = Camera.allCameras;
      for (int i = 0; i < cameras.Length; i++)
      {
        Camera camera = cameras[i];
        if (camera == main_cameraBase.camera || camera == ui_cameraBase.camera)
          continue;
        CameraBase cameraBase = this.AddChild<CameraBase>(string.Format("{0}{1}", camera.name, i), camera, false);
        cameraBase_list.Add(cameraBase);
      }
    }

    public void ShakeScreen(float duration, Vector3 pos_shake_range, Vector3 pos_shake_frequency,
      Vector3 eulerAngles_shake_range,
      Vector3 eulerAngles_shake_frequency, float fov_shake_range, float fov_shake_frequency)
    {
      foreach (var cameraBase in cameraBase_list)
      {
        cameraBase.ShakeScreen(duration, pos_shake_range, pos_shake_frequency, eulerAngles_shake_range,
          eulerAngles_shake_frequency, fov_shake_range,
          fov_shake_frequency);
      }
    }

    public void SetMainCameraMoveRange(object move_range)
    {
      this.main_cameraBase.SetMoveRange(move_range);
    }

    public void MoveByDelta(float dx, float dy, float dz)
    {
      this.main_cameraBase.MoveByDelta(dx, dy, dz);
    }

    public void RemoveCameras()
    {
      for (int i = cameraBase_list.Count - 1; i >= 0; i--)
      {
        this.RemoveChild(cameraBase_list[i].key);
        cameraBase_list.RemoveAt(i);
      }
    }



    protected override void __Destroy()
    {
      base.__Destroy();
    }




  }
}




