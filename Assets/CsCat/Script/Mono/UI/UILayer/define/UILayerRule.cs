namespace CsCat
{
  public class UILayerRule
  {
    private uint rule;
    public UILayerRule(uint rule)
    {
      this.rule = rule;
    }

    public bool IsHideBackgroundUILayer()
    {
      return IsContains((uint)EUILayerRule.Hide_BackgroundUILayer);
    }

    public bool IsHideFrontUILayer()
    {
      return IsContains((uint)EUILayerRule.Hide_FrontUILayer);
    }

    public  bool IsHideLowerOrderUI()
    {
      return IsContains((uint)EUILayerRule.Hide_LowerOrderUI);
    }

    public bool IsAddBlackMaskBehide()
    {
      return IsContains((uint)EUILayerRule.Add_BlackMaskBehide);
    }


    public bool IsContains(uint be_contained_value)
    {
      return this.rule.IsContains(be_contained_value);
    }
  }
}