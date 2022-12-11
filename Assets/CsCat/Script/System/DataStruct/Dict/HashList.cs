using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	//同HashSet一样，但有序
	public class HashList<T> : List<T>
	{
		private readonly Dictionary<T, bool> _dict = new Dictionary<T, bool>();


		public new void Add(T item)
		{
			if (_dict.ContainsKey(item))
				return;
			_dict[item] = true;
			base.Add(item);
		}

		public new void Clear()
		{
			_dict.Clear();
			base.Clear();
		}

		public new bool Contains(T item)
		{
			return _dict.ContainsKey(item);
		}


		public new bool Remove(T item)
		{
			_dict.Remove(item);
			return base.Remove(item);
		}


		public new void Insert(int index, T item)
		{
			if (_dict.ContainsKey(item))
				return;
			_dict[item] = true;
			base.Insert(index, item);
		}

		public new void RemoveAt(int index)
		{
			T item = base[index];
			_dict.Remove(item);
			base.RemoveAt(index);

		}

		public new T this[int index]
		{
			get => base[index];
			set
			{
				var originItem = base[index];
				_dict.Remove(originItem);
				_dict[value] = true;
				base[index] = value;
			}
		}
	}
}

