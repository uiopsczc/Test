using UnityEngine;

namespace CsCat
{
	public partial class UIGuidePanelBase
	{
		public class FingerItem : UIObject
		{
			protected void _Init(GameObject gameObject)
			{
				base._Init();
				DoSetGameObject(gameObject);
			}

			protected override void _DestroyGameObject()
			{
			}
		}
	}
}