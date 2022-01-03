namespace CsCat
{
	public static class IRestoreExtension
	{
		public static void AddToList(this IRestore self)
		{
			RestoreManager.instance.Add(self);
		}
	}
}