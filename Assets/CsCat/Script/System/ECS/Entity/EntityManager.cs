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
			PoolObject<Entity> entityPoolObject = this._poolCatManager.Spawn<Entity>(null, null);
			var entityPoolObjectIndex = entityPoolObject.GetPoolObjectIndex();
			entityPoolObject.GetValue().SetPoolObjectIndex(entityPoolObjectIndex);
			return entityPoolObject.GetValue();
		}

		public void DespawnEntity(Entity entity)
		{
			entity.Despawn();
		}
	}
}