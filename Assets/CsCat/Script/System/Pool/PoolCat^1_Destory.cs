namespace CsCat
{
	public partial class PoolCat<T>
	{
		protected virtual void _OnDestroy(T value)
		{
		}
		public virtual void Destroy()
		{
			DespawnAll();
			for (var i = 0; i < _poolItemList.Count; i++)
			{
				var poolItem = _poolItemList[i];
				_OnDestroy(poolItem.GetValue());
			}
			_poolItemList.Clear();
			_poolName = null;
			_spawnFunc = null;
		}
	}
}