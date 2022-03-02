using System.Collections.Generic;

namespace CsCat
{
	//弹道
	public class LineEffectGraphicComponent : EffectGraphicComponent
	{
		private readonly List<XLineRenderer> _xlineRendererList = new List<XLineRenderer>();

		public override void OnAllAssetsLoadDone()
		{
			base.OnAllAssetsLoadDone();
			var children = transform.GetComponentsInChildren<XLineRenderer>();
			for (var i = 0; i < children.Length; i++)
			{
				var xlineRenderer = children[i];
				_xlineRendererList.Add(xlineRenderer);
			}
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			transform.position = this.effectEntity.transformComponent.position;
			transform.eulerAngles = this.effectEntity.transformComponent.eulerAngles;
			for (var i = 0; i < _xlineRendererList.Count; i++)
			{
				var line = _xlineRendererList[i];
				line.target.position = effectEntity.GetComponent<LineEffectComponent>().targetPosition;
				line.target.eulerAngles = this.effectEntity.transformComponent.eulerAngles;
			}
		}


	}
}


