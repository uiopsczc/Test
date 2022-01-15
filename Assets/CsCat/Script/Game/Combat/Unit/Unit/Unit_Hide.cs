using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		public void UpdateHideState()
		{
			if (this.IsHide())
				this.__SetHideMode(this.IsExpose() ? "隐身状态被显隐" : "隐身状态没有显隐");
			else
				this.__SetHideMode("非隐形状态");
		}

		private void __SetHideMode(string mode)
		{
			if (!this.isLoadOk)
			{
				this.loadOkListenList.Add(() => { this.____SetHideMode(mode); });
				return;
			}

			this.____SetHideMode(mode);
		}

		private void ____SetHideMode(string mode)
		{
			if ("隐身状态被显隐".Equals(mode))
			{
				graphicComponent.SetIsShow(true);
				this.ChangeColor("隐身", new Color(1, 0.2f, 1, 0.5f)); // 紫色透明
			}
			else if ("隐身状态没有显隐".Equals(mode))
			{
				graphicComponent.SetIsShow(false);
				this.ChangeColor("隐身", null);
			}
			else if ("非隐形状态".Equals(mode))
			{
				graphicComponent.SetIsShow(true);
				this.ChangeColor("隐身", null);
			}
		}

	}
}