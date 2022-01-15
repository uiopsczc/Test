using System;

namespace CsCat
{
	public partial class UnitMoveComp : TickObject
	{
		private Unit unit;
		private float walkStepLength;
		public string moveType; // move, be_throwed
		private bool isMoveWithMoveAnimation = true;
		private float adjustDistSqr = 3 * 3;
		public bool isGetCaught;
		private UnitMoveInfo unitMoveInfo = new UnitMoveInfo();
		private UnitLookAtInfo unitLookAtInfo = new UnitLookAtInfo();

		public void Init(Unit unit)
		{
			base.Init();
			this.unit = unit;
			this.walkStepLength = this.unit.cfgUnitData.walk_step_length;
			this.unitMoveInfo.speed = this.unit.GetSpeed();
			this.unitMoveInfo.targetPos = this.unit.GetPosition();
			this.unitMoveInfo.endRotation = this.unit.GetRotation();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this.__UpdateMove(deltaTime);

			//    if (this.unitLookAtInfo.HasLookAt())
			//      this.__UpdateLookAt(deltaTime);
			if (this.unitBeThrowedInfo != null)
				this.__UpdateBeThrowed(deltaTime);
		}

		public void Destroy()
		{
			this.unit = null;
		}

		public void OnBuild()
		{
		}

		public void OnBuildOk()
		{
			Unit unit = this.unit;
			if ("move".Equals(this.moveType))
			{
				if (this.unitMoveInfo.IsHasAnimationName() && this.isMoveWithMoveAnimation)
					unit.PlayAnimation(this.unitMoveInfo.animationName, 0, this.unitMoveInfo.animationSpeed);
				unit.__MoveTo(this.unitMoveInfo.targetPos, this.unitMoveInfo.remainDuration);
			}
		}

		public void OnSpeedChange(float oldValue, float newValue)
		{
			var unit = this.unit;
			float factor = newValue / oldValue;
			this.unitMoveInfo.speed = this.unitMoveInfo.speed * factor;
			if (this.moveType.Equals("move"))
			{
				this.unitMoveInfo.remainDuration = this.unitMoveInfo.remainDuration / factor;
				var oldMoveAnimationSpeed = this.unitMoveInfo.animationSpeed;
				this.unitMoveInfo.animationSpeed = this.unitMoveInfo.animationSpeed * factor;
				if (unit.graphicComponent.transform != null)
				{
					unit.__MoveTo(this.unitMoveInfo.targetPos, this.unitMoveInfo.remainDuration);
					if (this.unitMoveInfo.IsHasAnimationName() && this.isMoveWithMoveAnimation &&
						Math.Abs(this.unitMoveInfo.animationSpeed - oldMoveAnimationSpeed) > 0.2f)
						unit.PlayAnimation(this.unitMoveInfo.animationName, 0.2f, this.unitMoveInfo.animationSpeed);
				}
			}
		}

		public void SetIsMoveWithMoveAnimation(bool isMoveWithMoveAnimation)
		{
			var unit = this.unit;
			this.isMoveWithMoveAnimation = isMoveWithMoveAnimation;
			if (this.moveType.Equals("move"))
			{
				if (isMoveWithMoveAnimation)
				{
					if (this.unitMoveInfo.IsHasAnimationName())
						unit.PlayAnimation(this.unitMoveInfo.animationName, null, this.unitMoveInfo.animationSpeed);
				}
				else
					unit.StopAnimation(this.unitMoveInfo.animationName, 0.2f);
			}
		}
	}
}