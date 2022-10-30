using System.Collections.Generic;

namespace CsCat
{
	public class EntityManager
	{
		private readonly PoolCatManager _poolManager;
		private Dictionary<PoolItemIndex<Entity>, bool> _entityPoolItemIndexDict = new Dictionary<PoolItemIndex<Entity>, bool>();
		public EntityManager(PoolCatManager poolManager)
		{
			this._poolManager = poolManager;
		}
		public Entity NewEntity()
		{
			var (entityPoolItem, entityPoolItemIndex) = this._poolManager.Spawn<Entity>(null, null);
			var entity = entityPoolItem.GetValue();
			entity.SetPoolItemIndex(entityPoolItemIndex);
			_entityPoolItemIndexDict[entityPoolItemIndex] = true;
			return entity;
		}

		public void DespawnEntity(Entity entity)
		{
			var poolItemIndex = entity.GetPoolItemIndex();
			DespawnEntity(poolItemIndex);
		}

		public void DespawnEntity(PoolItemIndex<Entity> entityPoolItemIndex)
		{
			this._entityPoolItemIndexDict.Remove(entityPoolItemIndex);
			entityPoolItemIndex.Despawn();
		}

		public void DespawnAll()
		{
			foreach (var kv in _entityPoolItemIndexDict)
			{
				var entityPoolItemIndex = kv.Value;
				entityPoolItemIndex.Despawn();
			}
			_entityPoolItemIndexDict.Clear();
		}

		public void Destroy()
		{
			DespawnAll();
		}
	}
}