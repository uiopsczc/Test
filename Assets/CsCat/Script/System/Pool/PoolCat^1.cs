using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolCat<T>:IPoolCat
	{
		/// <summary>
		/// 存放object的List
		/// </summary>
		private readonly List<PoolObject<T>> _poolObjectList = new List<PoolObject<T>>();
		private string _poolName;
		private PoolCatManager _poolManager;
		private Func<T> _spawnFunc;



		public PoolCat(string poolName)
		{
			this._poolName = poolName;
		}

		public PoolCat(string poolName, Func<T> spawnFunc)
		{
			this._poolName = poolName;
			this._spawnFunc = spawnFunc;
		}

		public void SetPoolManager(PoolCatManager poolManager)
		{
			this._poolManager = poolManager;
		}

		public PoolCatManager GetPoolManager()
		{
			return this._poolManager;
		}

		public void InitPool(int initCount = 1, Action<T> onSpawnCallback = null)
		{
			for (int i = 0; i < initCount; i++)
				Despawn(Spawn(onSpawnCallback));
		}


		#region virtual method

		/// <summary>
		/// 子类中重写spawn中需要用到的newObject
		/// </summary>
		/// <returns></returns>
		protected virtual T _Spawn()
		{
			return _spawnFunc != null ? _spawnFunc() : (T)Activator.CreateInstance(typeof(T));
		}

		#endregion

		public virtual PoolObject<T> Spawn()
		{
			return this.Spawn(null);
		}

		public PoolObject<T> GetPoolObjectAtIndex(int index)
		{
			return this._poolObjectList[index];
		}



		/// <summary>
		/// 创建
		/// </summary>
		/// <returns></returns>
		public virtual PoolObject<T> Spawn(Action<T> onSpawnCallback = null)
		{
			PoolObject<T> poolObject;
			for (var i = 0; i < _poolObjectList.Count; i++)
			{
				poolObject = _poolObjectList[i];
				if (poolObject.IsDespawned())
				{
					poolObject.SetIsDespawned(false);
					onSpawnCallback?.Invoke(poolObject.GetValue());
					return poolObject;
				}
			}
			int index = _poolObjectList.Count;
			T value = _Spawn();
			poolObject = new PoolObject<T>(this, index, value, false);
			onSpawnCallback?.Invoke(poolObject.GetValue());
			_poolObjectList.Add(poolObject);
			return poolObject;
		}

		public virtual void Despawn(PoolObject<T> poolObject)
		{
			T value = poolObject.GetValue();
			IDespawn spawnable = value as IDespawn;
			spawnable?.OnDespawn();
		}


		public void DespawnAll()
		{
			for (int i = 0; i < _poolObjectList.Count; i++)
			{
				var poolObject = _poolObjectList[i];
				if (!poolObject.IsDespawned())
					Despawn(poolObject);
			}
		}
		
		protected virtual void OnDestroy(T value)
		{
		}


		public virtual void Destroy()
		{
			DespawnAll();
			for (var i = 0; i < _poolObjectList.Count; i++)
			{
				var poolObject = _poolObjectList[i];
				OnDestroy(poolObject.GetValue());
			}

			_poolObjectList.Clear();
			_poolName = null;
			_spawnFunc = null;
		}

		
	}
}