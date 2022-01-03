namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUIEnabledScope Enabled(bool newIsEnabled)
		{
			return new GUIEnabledScope(newIsEnabled);
		}
	}
}