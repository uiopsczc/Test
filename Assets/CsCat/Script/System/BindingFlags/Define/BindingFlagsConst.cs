using System.Reflection;

namespace CsCat
{
	public class BindingFlagsConst
	{
		public const BindingFlags All = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static |
										BindingFlags.Instance;

		public const BindingFlags All_Public = BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
		public const BindingFlags All_Private = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

		public const BindingFlags Instance = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
		public const BindingFlags Instance_Public = BindingFlags.Public | BindingFlags.Instance;
		public const BindingFlags Instance_Private = BindingFlags.NonPublic | BindingFlags.Instance;

		public const BindingFlags Static = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
		public const BindingFlags Static_Public = BindingFlags.Public | BindingFlags.Static;
		public const BindingFlags Static_Private = BindingFlags.NonPublic | BindingFlags.Static;
	}
}