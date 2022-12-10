using System;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
	public class UIHUDNumber : UIHUDTextBase
	{
		private Vector2 _spawnUIPosition;
		private const float _defaultShowDuration = 1;
		private float _duration;
		private float _durationHalf = _defaultShowDuration * 0.5f;
		public float posDiffX;
		public float posDiffY;
		float _posDiffX;
		float _posDiffY;
		private RandomManager _randomManager;
		private readonly TextProxy _textProxy_This = new TextProxy();
		private float _startFadePct = 0.8f;
		private float _minFadeToAlpha = 0.2f;

		protected override void _Init()
		{
			base._Init();
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
			_duration = _defaultShowDuration;
			Vector3? spawnWorldPosition = spawnWorldPositionFunc();
			if (spawnWorldPosition == null)
			{
				DoReset();
				return;
			}
			_textProxy_This.SetText(showString);
			_textProxy_This.SetColor(color);
			InvokePostPrefabLoad(() =>
			{
				_textProxy_This.ApplyToText(this._TxtC_This);
			});
			this.posDiffX = randomManager.RandomBoolean()
				? randomManager.RandomFloat(30, 100f)
				: randomManager.RandomFloat(-100, -30);
			this.posDiffY = randomManager.RandomFloat(50, 100f);
			SetIsShow(true);
			UpdatePos(0);
		}

		protected override bool _Update(float deltaTime, float unscaledDeltaTime)
		{
			if (!base._Update(deltaTime, unscaledDeltaTime))
				return false;
			this.UpdatePos(deltaTime);
			return true;
		}

		public void UpdatePos(float deltaTime)
		{
			_duration = _duration - deltaTime;
			if (_duration <= 0)
			{
				this.DoReset();
				return;
			}


			if (_duration > _durationHalf)
				_posDiffY = EaseCat.Cubic.EaseOut2(0, this.posDiffY, (_defaultShowDuration - _duration) / _durationHalf);
			else
				_posDiffY = EaseCat.Cubic.EaseIn2(this.posDiffY, 0, (_durationHalf - _duration) / _durationHalf);

			float pct = (_defaultShowDuration - _duration) / _defaultShowDuration;
			_posDiffX = EaseCat.Linear.EaseNone2(0, this.posDiffX, pct);

			if (pct <= _startFadePct)
			{
				var color = _textProxy_This.GetColor();
				var curAlpha = Mathf.Lerp(1, this._minFadeToAlpha, pct/ _startFadePct);
				this._textProxy_This.SetColor(new Color(color.r, color.g, color.b, curAlpha));
			}
			Vector2 pos = _spawnUIPosition + new Vector2(_posDiffX, _posDiffY);
			this.SetPosition(pos);
		}

		protected override void _Reset()
		{
			_duration = _defaultShowDuration;
			_textProxy_This.Reset();
			base._Reset();
		}
	}
}