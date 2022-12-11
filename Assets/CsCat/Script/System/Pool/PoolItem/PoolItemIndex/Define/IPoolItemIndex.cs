namespace CsCat
{
	public interface IPoolItemIndex
	{
		int GetIndex();

		object GetValue();

		T2 GetValue<T2>() where T2 : class;

		IPoolCat GetIPool();
	}
}