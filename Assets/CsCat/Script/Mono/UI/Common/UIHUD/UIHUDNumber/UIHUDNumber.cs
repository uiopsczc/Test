using System;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public class UIHUDNumber : UIHUDTextBase
	{
		public Vector2 spawnUIPosition;
		private bool isShowing;
		private bool isFading;
		private const float defaultShowDuration = 1;
		public float duration;
		public float durationHalf = defaultShowDuration * 0.5f;
		public float posDiffX;
		public float posDiffY;
		float __posDiffX;
		float __posDiffY;
		private RandomManager _randomManager;

		public override void Init()
		{
			base.Init();
			this.AddListener<float, float>(null, GlobalEventNameConst.Update, Update);
		}

		private RandomManager randomManager => _randomManager ?? Client.instance.randomManager;

		public void SetRandomManager(RandomManager randomManager)
		{
			this._randomManager = randomManager;
		}


		public void Show(Transform transform, string showString, Color color)
		{
			Show(() => transform == null ? null : (Vector3?) transform.position, showString, color);
		}

		public void Show(Vector3 position, string showString, Color color)
		{
			Show(() => position, showString, color);
		}

		public void Show(Func<Vector3?> spawnWorldPositionFunc, string showString, Color color)
		{
			InvokeAfterAllAssetsLoadDone(() =>
			{
				duration = defaultShowDuration;
				Vector3? spawnWorldPosition = spawnWorldPositionFunc();
				if (spawnWorldPosition == null)
				{
					Reset();
					return;
				}

				this.spawnUIPosition = CameraUtil.WorldToUIPos(null,
					Client.instance.combat.cameraManager.mainCameraBase.camera, spawnWorldPosition.Value);
				this.textComp.text = showString;
				this.textComp.color = color;


				this.posDiffX = randomManager.RandomBoolean()
					? randomManager.RandomFloat(30, 100f)
					: randomManager.RandomFloat(-100, -30);
				this.posDiffY = randomManager.RandomFloat(50, 100f);

				this.isShowing = true;
				graphicComponent.SetIsShow(true);
				UpdatePos(0);
			});
		}

		protected void Update(float deltaTime, float unscaledDeltaTime)
		{
			if (!this.IsCanUpdate())
				return;
			this.UpdatePos(deltaTime);
		}

		public void UpdatePos(float deltaTime)
		{
			if (!this.isShowing)
				return;
			duration = duration - deltaTime;
			if (duration <= 0)
			{
				this.Reset();
				return;
			}


			if (duration > durationHalf)
				__posDiffY = EaseCat.Cubic.EaseOut2(0, this.posDiffY, (defaultShowDuration - duration) / durationHalf);
			else
				__posDiffY = EaseCat.Cubic.EaseIn2(this.posDiffY, 0, (durationHalf - duration) / durationHalf);

			float pct = (defaultShowDuration - duration) / defaultShowDuration;
			__posDiffX = EaseCat.Linear.EaseNone2(0, this.posDiffX, pct);

			if (!isFading && pct >= 0.8f)
			{
				isFading = true;
				this.textComp.DOFade(0.2f, duration);
			}

			Vector2 pos = spawnUIPosition + new Vector2(__posDiffX, __posDiffY);
			graphicComponent.rectTransform.anchoredPosition = pos;
		}

		protected override void _Reset()
		{
			base._Reset();
			duration = defaultShowDuration;
			this.isShowing = false;
			this.isFading = false;
			graphicComponent.SetIsShow(false);
		}
	}
}