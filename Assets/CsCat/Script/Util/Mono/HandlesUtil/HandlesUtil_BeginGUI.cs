#if UNITY_EDITOR
namespace CsCat
{
	public partial class HandlesUtil
	{
		public static HandlesBeginGUIScope BeginGUI()
		{
			return new HandlesBeginGUIScope();
		}
	}
}
#endif