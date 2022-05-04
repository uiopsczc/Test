using System;
using System.Collections.Generic;

namespace CsCat
{
	public class PoolCat<T>: IPoolCat
	{
		/// <summary>
		/// 存放object的List
		/// </summary>
		private readonly List<PoolObject<T>> _poolObjectList = new List<PoolObject<T>>();
		private string _poolName;
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

		public void InitPool(int initCount = 1, Action<T> onSpawnCallback = null)
		{
			for (int i = 0; i < initCount; i++)
				DeSpawn(Spawn(onSpawnCallback));
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

		public virtual IPoolObject Spawn()
		{
			return this.Spawn(null);
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
				if (poolObject.IsDeSpawned())
				{
					poolObject.SetIsDeSpawned(false);
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

		public virtual void DeSpawn(PoolObject<T> poolObject)
		{
			T value = poolObject.GetValue();
			IDeSpawn spawnable = value as IDeSpawn;
			spawnable?.OnDeSpawn();
		}

		public void DeSpawn(IPoolObject poolObject)
		{
			DeSpawn((PoolObject<T>)poolObject);
		}


		public void DeSpawnAll()
		{
			for (int i = 0; i < _poolObjectList.Count; i++)
			{
				var poolObject = _poolObjectList[i];
				if (!poolObject.IsDeSpawned())
					DeSpawn(poolObject);
			}
		}

		

		public IPoolObject GetPoolObjectAtIndex(int index)
		{
			return this._poolObjectList[index];
		}

		protected virtual void OnDestroy(T value)
		{
		}


		public virtual void Destroy()
		{
			DeSpawnAll();
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