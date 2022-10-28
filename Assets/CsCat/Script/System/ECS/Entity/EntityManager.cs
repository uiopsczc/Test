using System.Collections.Generic;

namespace CsCat
{
	public class EntityManager
	{
		private readonly PoolCatManager _poolManager;
		private Dictionary<int, PoolIndex<Entity>> _entityPoolIndexDict = new Dictionary<int, PoolIndex<Entity>>();
		public EntityManager(PoolCatManager poolManager)
		{
			this._poolManager = poolManager;
		}
		public Entity NewEntity()
		{
			var (entityPoolItem, entityPoolIndex) = this._poolManager.Spawn<Entity>(null, null);
			var entity = entityPoolItem.GetValue();
			var index = entityPoolIndex.GetIndex();
			entity.SetId(index);
			entity.SetPoolManager(this._poolManager);
			_entityPoolIndexDict[index] = entityPoolIndex;
			return entity;
		}

		public void DespawnEntity(Entity entity)
		{
			int index = entity.GetId();
			DespawnEntity(index);
		}

		public void DespawnEntity(int entityId)
		{
			var entityPoolIndex = this._entityPoolIndexDict[entityId];
			this._entityPoolIndexDict.Remove(entityId);
			entityPoolIndex.Despawn();
		}

		public void DespawnAll()
		{
			foreach (var kv in _entityPoolIndexDict)
			{
				var entityPoolIndex = kv.Value;
				entityPoolIndex.Despawn();
			}
			_entityPoolIndexDict.Clear();
		}

		public void Destroy()
		{
			DespawnAll();
		}
	}
}