using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class TreeNode : IDespawn
	{
		public Cache _cache = new Cache();
		private IPoolItemIndex _poolItemIndex;
		private IPoolItemIndex _parentPoolItemIndex;
		private string _key;

		public TreeNode()
		{
		}

		public void SetPoolItemIndex(IPoolItemIndex poolItemIndex)
		{
			this._poolItemIndex = poolItemIndex;
		}

		public IPoolItemIndex GetPoolItemIndex()
		{
			return this._poolItemIndex;
		}

		protected void SetKey(string key)
		{
			this._key = key;
		}

		protected string GetKey()
		{
			return this._key;
		}

		public string GetId()
		{
			return this._key;
		}

		public void SetParentPoolItemIndex(IPoolItemIndex parentPoolItemIndex)
		{
			this._parentPoolItemIndex = parentPoolItemIndex;
		}

		public T GetParent<T>() where T : class
		{
			return this._parentPoolItemIndex.GetValue<T>();
		}

		public PoolCatManager GetPoolManager()
		{
			return _poolItemIndex.GetIPool().GetPoolManager();
		}

		public void DoInit(params object[] args)
		{
			_PreInit();
			this.InvokeMethod("_Init", false, args);
			_PostInit();
		}

		protected virtual void _PreInit()
		{
		}

		protected virtual void _Init()
		{
		}

		protected virtual void _PostInit()
		{
		}

		public virtual void Start()
		{
		}


		void _OnDespawn_()
		{
		}
	}
}