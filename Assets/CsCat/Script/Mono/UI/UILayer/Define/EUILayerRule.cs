namespace CsCat
{
	public enum EUILayerRule : uint
	{
		Null = 0,
		Hide_BackgroundUILayer = 1 << 0,
		Hide_FrontUILayer = 1 << 1,
		Hide_LowerOrderUI = 1 << 2,
		Add_BlackMaskBehide = 1 << 3,
	}
}