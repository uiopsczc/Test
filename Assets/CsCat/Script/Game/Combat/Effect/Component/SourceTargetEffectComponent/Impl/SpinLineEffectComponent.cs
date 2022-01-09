using UnityEngine;

namespace CsCat
{
	//电弧
	public class SpinLineEffectComponent : SourceTargetEffectComponent
	{
		private float spin_speed;
		private Vector3 forward_dir;
		private float forward_speed;
		private Vector3 start_position;
		private Vector3 spin_dir;
		private float start_spin_angle;
		private float spin_length;
		private float elapsed_duration;

		public void Init(Vector3 start_position, Vector3 spin_dir, float start_spin_angle,
		  float spin_speed, float spin_length, Vector3 forward_dir, float forward_speed = 0)
		{
			base.Init();
			this.spin_speed = spin_speed;
			this.forward_dir = forward_dir;
			this.forward_speed = forward_speed;
			this.spin_dir = spin_dir;
			this.start_position = start_position;
			this.start_spin_angle = start_spin_angle;
			this.spin_length = spin_length;
			this.elapsed_duration = 0;

			Calculate(0);
			this.effectEntity.ApplyToTransformComponent(this.current_position, this.current_eulerAngles);
		}

		protected override void Calculate(float deltaTime)
		{
			this.elapsed_duration = this.elapsed_duration + deltaTime;
			Vector3 forward_distance = this.forward_speed * this.elapsed_duration * forward_dir;
			Vector3 arc_dir = Quaternion.AngleAxis(this.start_spin_angle + this.spin_speed * this.elapsed_duration, Vector3.up) *
							  this.spin_dir; //电弧当前朝向
			this.current_position = this.start_position + forward_distance + arc_dir * this.spin_length; // 电弧当前结束位置。
			this.current_eulerAngles = Vector3.zero;
		}
	}
}



