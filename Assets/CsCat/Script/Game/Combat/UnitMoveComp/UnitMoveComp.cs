using System;

namespace CsCat
{
	public partial class UnitMoveComp : TickObject
	{
		private Unit _unit;
		private float _walkStepLength;
		private bool _isMoveWithMoveAnimation = true;
		private readonly float adjustDistSqr = 3 * 3;
		private readonly UnitMoveInfo _unitMoveInfo = new UnitMoveInfo();
		private readonly UnitLookAtInfo _unitLookAtInfo = new UnitLookAtInfo();

		public string moveType; // move, beThrowed
		public bool isGetCaught;

		public void Init(Unit unit)
		{
			base.Init();
			this._unit = unit;
			this._walkStepLength = this._unit.cfgUnitData.walkStepLength;
			this._unitMoveInfo.speed = this._unit.GetSpeed();
			this._unitMoveInfo.targetPos = this._unit.GetPosition();
			this._unitMoveInfo.endRotation = this._unit.GetRotation();
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			this._UpdateMove(deltaTime);

			//    if (this.unitLookAtInfo.HasLookAt())
			//      this.__UpdateLookAt(deltaTime);
			if (this.unitBeThrowedInfo != null)
				this._UpdateBeThrowed(deltaTime);
		}

		protected override void _Destroy()
		{
			base._Destroy();
			this._unit = null;
		}

		public void OnBuild()
		{
		}

		public void OnBuildOk()
		{
			Unit unit = this._unit;
			if ("move".Equals(this.moveType))
			{
				if (this._unitMoveInfo.IsHasAnimationName() && this._isMoveWithMoveAnimation)
					unit.PlayAnimation(this._unitMoveInfo.animationName, 0, this._unitMoveInfo.animationSpeed);
				unit.MoveTo(this._unitMoveInfo.targetPos, this._unitMoveInfo.remainDuration);
			}
		}

		public void OnSpeedChange(float oldValue, float newValue)
		{
			var unit = this._unit;
			float factor = newValue / oldValue;
			this._unitMoveInfo.speed = this._unitMoveInfo.speed * factor;
			if (this.moveType.Equals("move"))
			{
				this._unitMoveInfo.remainDuration = this._unitMoveInfo.remainDuration / factor;
				var oldMoveAnimationSpeed = this._unitMoveInfo.animationSpeed;
				this._unitMoveInfo.animationSpeed = this._unitMoveInfo.animationSpeed * factor;
				if (unit.graphicComponent.transform != null)
				{
					unit.MoveTo(this._unitMoveInfo.targetPos, this._unitMoveInfo.remainDuration);
					if (this._unitMoveInfo.IsHasAnimationName() && this._isMoveWithMoveAnimation &&
						Math.Abs(this._unitMoveInfo.animationSpeed - oldMoveAnimationSpeed) > 0.2f)
						unit.PlayAnimation(this._unitMoveInfo.animationName, 0.2f, this._unitMoveInfo.animationSpeed);
				}
			}
		}

		public void SetIsMoveWithMoveAnimation(bool isMoveWithMoveAnimation)
		{
			var unit = this._unit;
			this._isMoveWithMoveAnimation = isMoveWithMoveAnimation;
			if (this.moveType.Equals("move"))
			{
				if (isMoveWithMoveAnimation)
				{
					if (this._unitMoveInfo.IsHasAnimationName())
						unit.PlayAnimation(this._unitMoveInfo.animationName, null, this._unitMoveInfo.animationSpeed);
				}
				else
					unit.StopAnimation(this._unitMoveInfo.animationName, 0.2f);
			}
		}
	}
}