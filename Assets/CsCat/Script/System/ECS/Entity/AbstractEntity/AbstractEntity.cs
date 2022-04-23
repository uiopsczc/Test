using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity : IDespawn
	{
		public string key;
		public Cache cache = new Cache();


		public AbstractEntity()
		{
		}


		public virtual void Init()
		{
		}

		public virtual void PostInit()
		{
		}

		public virtual void Start()
		{
		}

		public virtual void Refresh()
		{
		}


		public T GetParent<T>() where T : AbstractEntity
		{
			return (T)parent;
		}

		void _OnDespawn_()
		{
		}
	}
}