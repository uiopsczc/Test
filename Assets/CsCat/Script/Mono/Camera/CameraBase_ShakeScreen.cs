using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class CameraBase
  {
    private List<CameraShakeData> shakeData_list = new List<CameraShakeData>();

    public void ShakeScreen(float duration, Vector3 pos_shake_range, Vector3 pos_shake_frequency,
      Vector3 eulerAngles_shake_range,
      Vector3 eulerAngles_shake_frequency, float fov_shake_range, float fov_shake_frequency)
    {
      shakeData_list.Add(new CameraShakeData(
        duration, pos_shake_range, pos_shake_frequency, eulerAngles_shake_range, eulerAngles_shake_frequency,
        fov_shake_range,
        fov_shake_frequency
      ));
    }

    public void ApplyShakeScreen(float deltaTime)
    {
      CameraShakeResult shakeResult = GetShakeResult(deltaTime);
      if (shakeResult != null)
      {
        Vector3 shake_position = this.current_rotation * shakeResult.posistion;
        graphicComponent.transform.position = graphicComponent.transform.position + shake_position;
        Quaternion shake_rotation = this.current_rotation * Quaternion.Euler(shakeResult.eulerAngles);
        graphicComponent.transform.rotation = shake_rotation;
        this.camera.fieldOfView = this.camera.fieldOfView + shakeResult.fov;
      }
    }

    public CameraShakeResult GetShakeResult(float deltaTime)
    {
      Vector3 shake_position = new Vector3(0, 0, 0);
      Vector3 shake_eulerAngles = new Vector3(0, 0, 0);
      float shake_fov = 0;
      for (int i = shakeData_list.Count - 1; i >= 0; i--)
      {
        CameraShakeData shakeData = shakeData_list[i];
        shakeData.frame_time = shakeData.frame_time + deltaTime;
        if (shakeData.frame_time >= shakeData.duration)
          shakeData_list.RemoveLast();
        else
        {
          if (shakeData.pos_shake_range != null && shakeData.pos_shake_frequency != null)
            shake_position = shake_position + this.__CalculateShakeResult(shakeData.duration, shakeData.frame_time,
                               shakeData.pos_shake_range.Value, shakeData.pos_shake_frequency.Value);
          if (shakeData.eulerAngles_shake_range != null && shakeData.eulerAngles_shake_frequency != null)
            shake_eulerAngles = shake_eulerAngles + this.__CalculateShakeResult(shakeData.duration,
                                  shakeData.frame_time,
                                  shakeData.eulerAngles_shake_range.Value, shakeData.eulerAngles_shake_frequency.Value);
          if (shakeData.fov_shake_range != null && shakeData.fov_shake_frequency != null)
            shake_fov = shake_fov + __CalculateShakeResult(shakeData.duration, shakeData.frame_time,
                          shakeData.fov_shake_range.Value, shakeData.fov_shake_frequency.Value);
          return new CameraShakeResult(shake_position, shake_eulerAngles, shake_fov);
        }
      }

      return null;
    }

    private Vector3 __CalculateShakeResult(float duration, float frame_time, Vector3 shake_range,
      Vector3 shake_frequency)
    {
      return new Vector3(
        __CalculateShakeResult(duration, frame_time, shake_range.x, shake_frequency.x),
        __CalculateShakeResult(duration, frame_time, shake_range.y, shake_frequency.y),
        __CalculateShakeResult(duration, frame_time, shake_range.z, shake_frequency.z)
      );
    }

    private float __CalculateShakeResult(float duration, float frame_time, float shake_range,
      float shake_frequency)
    {
      float reduction = (duration - frame_time) / duration;
      return Mathf.Sin(2 * Mathf.PI * shake_frequency * frame_time) * shake_range * reduction;
    }


  }
}




