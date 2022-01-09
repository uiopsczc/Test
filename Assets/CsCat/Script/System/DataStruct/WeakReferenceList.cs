using System;
using System.Collections.Generic;
using System.Linq;

namespace CsCat
{
	/// <summary>
	/// WeakReferenceList
	/// </summary>
	/// <typeparam name="V"></typeparam>
	public class WeakReferenceList<V>
	{
		private List<WeakReference> _list = new List<WeakReference>();


		public int Count => this._list.Count;

		public V this[int index]
		{
			get
			{
				var valueResult = _list[index].GetValueResult<V>();
				return valueResult.GetValue();
			}
			set => this.Set(index, value);
		}


		public V Add(V value)
		{
			this._list.Add(new WeakReference(value));
			return value;
		}

		public void Clear()
		{
			this._list.Clear();
		}

		public bool Contains(V value)
		{
			for (int i = 0; i < _list.Count; i++)
			{
				var element = _list[i];
				var valueResult = element.GetValueResult<V>();
				if (valueResult.GetIsHasValue() && ObjectUtil.Equals(value, valueResult.GetValue()))
					return true;
			}
			return false;
		}

		public void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		public void Set(int index, V value)
		{
			this._list[index] = new WeakReference(value);
		}

		public void GC()
		{
			List<WeakReference> toRemoveList = new List<WeakReference>();
			for (var i = 0; i < _list.Count; i++)
			{
				var e = _list[i];
				if (!e.IsAlive)
					toRemoveList.Add(e);
			}

			if (toRemoveList.Count > 0)
			{
				for (var i = 0; i < toRemoveList.Count; i++)
				{
					var e = toRemoveList[i];
					_list.Remove(e);
				}

				System.GC.Collect(0);
			}
		}
	}
}