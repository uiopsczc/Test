using UnityEngine;

namespace CsCat
{
	public class UIShowLogoPanel : UIBackgroundPanel
	{
		public override bool isResident => true;


		protected void _Init(GameObject gameObject)
		{
			_SetGameObject(gameObject, true);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}
	}
}