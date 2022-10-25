namespace CsCat
{
	public class EntityManager
	{
		private PoolCatManager _poolCatManager;
		public EntityManager(PoolCatManager poolCatManager)
		{
			this._poolCatManager = poolCatManager;
		}
		public Entity NewEntity()
		{
			PoolItem<Entity> entityPoolItem = this._poolCatManager.Spawn<Entity>(null, null);
			var entityPoolObjectIndex = entityPoolItem.GetPoolObjectIndex();
			entityPoolItem.GetValue().SetPoolObjectIndex(entityPoolObjectIndex);
			return entityPoolItem.GetValue();
		}

		public void DespawnEntity(Entity entity)
		{
			entity.Despawn();
		}
	}
}