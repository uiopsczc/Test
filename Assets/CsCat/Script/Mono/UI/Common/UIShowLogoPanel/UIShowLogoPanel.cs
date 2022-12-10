using UnityEngine;

namespace CsCat
{
	public class UIShowLogoPanel : UIBackgroundPanel
	{
		public override bool isResident => true;


		protected void _Init(GameObject gameObject)
		{
			DoSetGameObject(gameObject);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void _DestroyGameObject()
		{
		}
	}
}