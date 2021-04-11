using UnityEngine;

namespace CsCat
{
  public partial class CameraBase
  {
    public Vector3 move_by_delta_target_position;

    public void MoveByDelta(float dx, float dy, float dz)
    {
      this.current_operation = CameraOperation.Delta_Move;
      this.move_by_delta_target_position = this.current_position + graphicComponent.transform.right * dx + graphicComponent.transform.up * dy +
                                           graphicComponent.transform.forward * dz;
      //    if (Math.Abs(dy) > 0.15f)
      //      this.move_by_delta_target_position = new Vector3(this.current_position.x, this.current_position.y + dy,
      //        this.current_position.z);
      //    else
      //      this.move_by_delta_target_position = this.current_position + this.transform.right * dx + this.transform.forward * dz;
    }

    public void ApplyMoveByDelta(float deltaTime)
    {
      graphicComponent.transform.position = Vector3.Lerp(this.current_position, this.move_by_delta_target_position,
        this.lerp_speed * deltaTime);
      if (Vector3.SqrMagnitude(graphicComponent.transform.position - this.move_by_delta_target_position) < 0.2)
        MoveByDeltaReset();
    }

    public void MoveByDeltaReset()
    {
      graphicComponent.transform.position = this.move_by_delta_target_position;
      this.current_operation = CameraOperation.None;
    }
  }
}



