using UnityEngine;

namespace CsCat
{
	public partial class CameraBase
	{
		private Transform move_to_target_transform;
		private Vector3? move_to_target_position;
		private Quaternion move_to_target_rotation;

		private Vector3 move_to_target_start_position;
		private Quaternion move_to_target_start_rotation;

		private float move_to_target_duration;
		private float move_to_target_current_time;


		private bool is_reach_need_stop;

		public void SetMoveToTarget(Transform move_to_target_transform, float move_to_target_duration,
		  Vector3 move_to_target_eulerAngles, Vector3 move_to_target_look_position, bool is_reach_need_stop = false)
		{
			this.move_to_target_transform = move_to_target_transform;
			SetMoveToTarget(this.move_to_target_transform.position, move_to_target_duration, move_to_target_eulerAngles,
			  move_to_target_look_position, is_reach_need_stop);
		}

		public void SetMoveToTarget(GameObject move_to_gameObject, float move_to_target_duration,
		  Vector3 move_to_target_eulerAngles, Vector3 move_to_target_look_position, bool is_reach_need_stop = false)
		{
			SetMoveToTarget(move_to_gameObject.transform, move_to_target_duration, move_to_target_eulerAngles,
			  move_to_target_look_position, is_reach_need_stop);
		}

		public void SetMoveToTarget(Vector3 move_to_target_position, float move_to_target_duration,
		  Vector3? move_to_target_eulerAngles, Vector3? move_to_target_look_position, bool is_reach_need_stop = false)

		{
			this.move_to_target_position = move_to_target_position;

			this.move_to_target_duration = move_to_target_duration;
			if (move_to_target_eulerAngles != null)
				this.move_to_target_rotation = Quaternion.Euler(move_to_target_eulerAngles.Value);
			if (move_to_target_look_position != null)
				this.move_to_target_rotation =
				  Quaternion.LookRotation(move_to_target_look_position.Value - move_to_target_position);

			this.is_reach_need_stop = is_reach_need_stop;

			this.move_to_target_start_position = this.current_position;
			this.move_to_target_start_rotation = this.current_rotation;

			move_to_target_current_time = 0;
		}

		public void ApplyMoveToTarget(float deltaTime)
		{
			if (move_to_target_transform != null)
				move_to_target_position = move_to_target_transform.position;
			this.move_to_target_current_time = this.move_to_target_current_time + deltaTime;
			Vector3 position;
			Quaternion rotation;
			if (this.move_to_target_duration == 0 || move_to_target_current_time >= this.move_to_target_duration)
			{
				position = move_to_target_position.Value;
				rotation = move_to_target_start_rotation;
				if (this.is_reach_need_stop)
					MoveToTargetReset();
			}
			else
			{
				float percent = move_to_target_current_time.GetPercent(0, move_to_target_duration);
				position = Vector3.Lerp(this.move_to_target_start_position, move_to_target_position.Value, percent);
				rotation = Quaternion.Slerp(move_to_target_start_rotation, move_to_target_start_rotation, percent);
			}

			graphicComponent.transform.position = position;
			graphicComponent.transform.rotation = rotation;
		}

		public void MoveToTargetReset()
		{
			current_operation = CameraOperation.None;
			move_to_target_position = null;
			move_to_target_transform = null;
			this.is_reach_need_stop = false;
		}
	}
}




