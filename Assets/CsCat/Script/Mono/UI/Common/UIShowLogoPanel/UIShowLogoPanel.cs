using UnityEngine;

namespace CsCat
{
	public class UIShowLogoPanel : UIBackgroundPanel
	{
		public override bool isResident => true;


		protected void _Init(GameObject gameObject)
		{
			SetGameObject(gameObject, true);
		}

		protected override void _PostInit()
		{
			base._PostInit();
			this.SetIsShow(false);
		}

		protected override void _Reset()
		{
			base._Reset();
			SetIsShow(false);
		}
	}
}