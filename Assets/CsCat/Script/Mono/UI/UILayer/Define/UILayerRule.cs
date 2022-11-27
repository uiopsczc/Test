namespace CsCat
{
	public class UILayerRule
	{
		private readonly uint _rule;
		public UILayerRule(uint rule)
		{
			this._rule = rule;
		}

		public bool IsHideBackgroundUILayer()
		{
			return IsContains((uint)EUILayerRule.Hide_BackgroundUILayer);
		}

		public bool IsHideFrontUILayer()
		{
			return IsContains((uint)EUILayerRule.Hide_FrontUILayer);
		}

		public bool IsHideLowerOrderUI()
		{
			return IsContains((uint)EUILayerRule.Hide_LowerOrderUI);
		}

		public bool IsAddBlackMaskBehind()
		{
			return IsContains((uint)EUILayerRule.Add_BlackMaskBehind);
		}


		public bool IsContains(uint beContainedValue)
		{
			return this._rule.IsContains(beContainedValue);
		}
	}
}