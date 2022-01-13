using System;
using UnityEngine;

namespace CsCat
{
	public class RigidbodyComponent
	{
		public float mass;
		public Vector3 velocity;
		public bool isRising;
		public float risingGravity;
		public float fallingGravity;


		public RigidbodyComponent(float mass)
		{
			this.mass = mass;
		}

		public void ClearVelocityX()
		{
			this.velocity.x = 0;
		}
		public void ClearVelocityY()
		{
			this.velocity.y = 0;
		}
		public void ClearVelocityZ()
		{
			this.velocity.z = 0;
		}
		public void ClearVelocity()
		{
			this.velocity = Vector3.zero;
		}

		public void AddForce(Vector3 force, ForceMode forceMode)
		{
			AddForce(force.x, force.y, force.z, forceMode);
		}

		//公式: a=f/m; v1=v0+at
		public void AddForce(float x, float y, float z, ForceMode forceMode)
		{
			Vector3 acceleration = new Vector3(x, y, z);
			if (acceleration == Vector3.zero)
				return;

			switch (forceMode)
			{
				case ForceMode.Impulse:
					acceleration /= mass;
					this.velocity += acceleration * RigidbodyComponentConst.Force_Duration;
					break;

				case ForceMode.VelocityChange:
					this.velocity += acceleration * RigidbodyComponentConst.Force_Duration;
					break;
			}
		}

		//到达顶端需要的时间
		public float GetToTopDuration()
		{
			float velocityY = this.velocity.y;
			//确保初速度向上，否则无法到达顶端
			if (velocityY <= 0)
				return 0;
			return velocityY / this.risingGravity;
		}

	}
}