using System.Collections.Generic;

namespace CsCat
{
	//弹道
	public class LineEffectGraphicComponent : EffectGraphicComponent
	{
		private List<XLineRenderer> xlineRendererList = new List<XLineRenderer>();

		public override void OnAllAssetsLoadDone()
		{
			base.OnAllAssetsLoadDone();
			var children = transform.GetComponentsInChildren<XLineRenderer>();
			for (var i = 0; i < children.Length; i++)
			{
				var xlineRenderer = children[i];
				xlineRendererList.Add(xlineRenderer);
			}
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			transform.position = this.effectEntity.transformComponent.position;
			transform.eulerAngles = this.effectEntity.transformComponent.eulerAngles;
			for (var i = 0; i < xlineRendererList.Count; i++)
			{
				var line = xlineRendererList[i];
				line.target.position = effectEntity.GetComponent<LineEffectComponent>().targetPosition;
				line.target.eulerAngles = this.effectEntity.transformComponent.eulerAngles;
			}
		}


	}
}


