using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;

#endif

namespace CsCat
{
	public static class ListTExtension
	{
		public static List<T> EmptyIfNull<T>(this List<T> self)
		{
			return self ?? new List<T>();
		}

		public static void RemoveEmpty<T>(this List<T> self)
		{
			for (var i = self.Count - 1; i >= 0; i--)
				if (self[i] == null)
					self.RemoveAt(i);
		}

		/// <summary>
		///   将list[index1]和list[index2]交换
		/// </summary>
		public static void Swap<T>(this List<T> self, int index1, int index2)
		{
			ListUtil.Swap(self, index1, self, index2);
		}

		//超过index或者少于0的循环index表获得
		public static T GetByLoopIndex<T>(this List<T> self, int index)
		{
			while (index < 0) index += self.Count;
			if (index >= self.Count) index %= self.Count;
			return self[index];
		}

		//超过index或者少于0的循环index表设置
		public static void SetByLoopIndex<T>(this List<T> self, int index, T value)
		{
			while (index < 0) index += self.Count;
			if (index >= self.Count) index %= self.Count;
			self[index] = value;
		}

		public static bool ContainsIndex<T>(this List<T> self, int index)
		{
			return index < self.Count && index >= 0;
		}

		/// <summary>
		///   使其内元素单一
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static void Unique<T>(this List<T> self)
		{
			var hashSet = new HashSet<T>();
			for (int i = 0; i < self.Count; i++)
			{
				if (!hashSet.Add(self[i]))
				{
					self.RemoveAt(i);
					i--;
				}
			}
		}


		public static List<T> Combine<T>(this List<T> self, List<T> another)
		{
			var result = new List<T>(self);
			result.AddRange(another);
			result.Unique();
			return result;
		}

		public static void Push<T>(this List<T> self, T t)
		{
			self.Add(t);
		}

		public static T Peek<T>(this List<T> self)
		{
			return self.Last();
		}

		public static T Pop<T>(this IList<T> self)
		{
			return self.RemoveLast();
		}


		#region 查找

		/// <summary>
		///   第一个item
		///   用linq
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T First<T>(this List<T> self)
		{
			return self[0];
		}

		/// <summary>
		///   最后一个item
		///   用linq
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static T Last<T>(this List<T> self)
		{
			return self[self.Count - 1];
		}

		/// <summary>
		///   在list中找sublist的开始位置
		/// </summary>
		/// <returns>-1表示没找到</returns>
		public static int IndexOfSub<T>(this List<T> self, List<T> subList)
		{
			var resultFromIndex = -1; //sublist在list中的开始位置
			for (var i = 0; i < self.Count; i++)
			{
				object o = self[i];
				if (!ObjectUtil.Equals(o, subList[0])) continue;
				var isEquals = true;
				for (var j = 1; j < subList.Count; j++)
				{
					var o1 = subList[j];
					var o2 = i + j > self.Count - 1 ? default : self[i + j];
					if (ObjectUtil.Equals(o1, o2)) continue;
					isEquals = false;
					break;
				}

				if (!isEquals) continue;
				resultFromIndex = i;
				break;
			}

			return resultFromIndex;
		}

		/// <summary>
		///   在list中只保留sublist中的元素
		/// </summary>
		public static bool RetainElementsOfSub<T>(this List<T> self, List<T> subList)
		{
			var isModify = false;
			for (var i = self.Count - 1; i >= 0; i--)
				if (!subList.Contains(self[i]))
				{
					self.RemoveAt(i);
					isModify = true;
				}

			return isModify;
		}

		/// <summary>
		///   包含fromIndx
		/// </summary>
		public static List<T> Sub<T>(this List<T> self, int fromIndex, int length)
		{
			var list = new List<T>();
			length = Math.Min(length, self.Count - fromIndex + 1);
			for (int i = 0; i < length; i++)
				list.Add(self[fromIndex + i]);
			return list;
		}


		/// <summary>
		///   包含fromIndx到末尾
		/// </summary>
		public static List<T> Sub<T>(this List<T> self, int fromIndex)
		{
			var length = self.Count - fromIndex + 1;
			return self.Sub(fromIndex, length);
		}

		#endregion

		#region 插入删除操作

		/// <summary>
		///   当set来使用，保持只有一个
		/// </summary>
		public static List<T> Add<T>(this List<T> self, T element, bool isUnique = false)
		{
			if (isUnique && self.Contains(element))
				return self;
			self.Add(element);
			return self;
		}

		public static List<T> AddRange<T>(this List<T> self, List<T> list, bool isUnique = false)
		{
			if (!isUnique)
			{
				self.AddRange(list);
				return self;
			}

			foreach (var element in list)
			{
				if (self.Contains(element))
					continue;
				self.Add(element);
			}

			return self;
		}

		public static List<T> AddRange<T>(this List<T> self, T[] array, bool isUnique = false)
		{
			if (!isUnique)
			{
				self.AddRange(array);
				return self;
			}

			foreach (var element in array)
			{
				if (self.Contains(element))
					continue;
				self.Add(element);
			}

			return self;
		}

		public static List<T> AddFirst<T>(this List<T> self, T o,
			bool isUnique = false)
		{
			if (isUnique && self.Contains(o))
				return self;
			self.Insert(0, o);
			return self;
		}

		public static List<T> AddLast<T>(this List<T> self, T o,
			bool isUnique = false)
		{
			if (isUnique && self.Contains(o))
				return self;
			self.Insert(self.Count, o);
			return self;
		}


		public static List<T> AddUnique<T>(this List<T> self, T o)
		{
			return AddLast(self, o, true);
		}


		public static T RemoveFirst<T>(this List<T> self)
		{
			var t = self[0];
			self.RemoveAt(0);
			return t;
		}


		public static T RemoveLast<T>(this IList<T> self)
		{
			var o = self[self.Count - 1];
			self.RemoveAt(self.Count - 1);
			return o;
		}

		/// <summary>
		///   跟RemoveAt一样，只是有返回值
		/// </summary>
		public static T RemoveAt2<T>(this List<T> self, int index)
		{
			var t = self[index];
			self.RemoveAt(index);
			return t;
		}


		/// <summary>
		///   跟Remove一样，只是有返回值(是否删除掉)
		/// </summary>
		/// <param name="self"></param>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool Remove2<T>(this List<T> self, T o)
		{
			if (!self.Contains(o))
				return false;
			self.Remove(o);
			return true;
		}

		/// <summary>
		///   删除list中的subList（subList必须要全部在list中）
		/// </summary>
		public static bool RemoveSub<T>(this List<T> self, List<T> subList)
		{
			var fromIndex = self.IndexOfSub(subList);
			if (fromIndex == -1)
				return false;
			var isModify = false;
			for (var i = fromIndex + subList.Count - 1; i >= fromIndex; i--)
			{
				self.RemoveAt(i);
				if (!isModify)
					isModify = true;
			}

			return isModify;
		}

		/// <summary>
		///   跟RemoveRange一样，但返回删除的元素List
		/// </summary>
		public static List<T> RemoveRange2<T>(this List<T> self, int index, int length)
		{
			var result = new List<T>();
			var lastIndex = index + length - 1 <= self.Count - 1 ? index + length - 1 : self.Count - 1;
			for (var i = lastIndex; i >= index; i--)
			{
				result.Add(self[i]);
				self.RemoveAt(i);
			}

			result.Reverse();

			return result;
		}


		/// <summary>
		///   在list中删除subList中出现的元素
		/// </summary>
		public static bool RemoveElementsOfSub<T>(this IList<T> self, IList<T> subList)
		{
			var isModify = false;
			for (var i = self.Count - 1; i >= 0; i++)
				if (subList.Contains(self[i]))
				{
					self.Remove(self[i]);
					isModify = true;
				}

			return isModify;
		}

		#endregion

		#region Random 随机
		public static T Random<T>(this List<T> self)
		{
			return RandomUtil.Random(self);
		}

		/// <summary>
		/// 随机list里面的元素count次
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="count">个数</param>
		/// <param name="isUnique">是否唯一</param>
		/// <param name="weights">权重数组</param>
		/// <returns></returns>
		public static List<T> RandomList<T>(this List<T> self, int count, bool isUnique, IList<float> weights = null)
		{
			return RandomUtil.RandomList(self, count, isUnique, weights);
		}

		public static T[] RandomArray<T>(this List<T> self, int count, bool isUnique, IList<float> weights = null)
		{
			return RandomUtil.RandomArray(self, count, isUnique, weights);
		}

		#endregion


		public static void CopyTo<T>(this List<T> self, List<T> destList, params object[] constructArgs)
			where T : ICopyable
		{
			destList.Clear();
			foreach (var element in self)
			{
				var destElement = typeof(T).CreateInstance<T>(constructArgs);
				destList.Add(destElement);
				element.CopyTo(destElement);
			}
		}

		public static void CopyFrom<T>(this List<T> self, List<T> sourceList, params object[] constructArgs)
			where T : ICopyable
		{
			self.Clear();
			foreach (var element in sourceList)
			{
				var selfElement = typeof(T).CreateInstance<T>(constructArgs);
				self.Add(selfElement);
				selfElement.CopyFrom(element);
			}
		}

		public static ArrayList DoSaveList<T>(this List<T> self, Action<T, Hashtable> doSaveCallback)
		{
			ArrayList result = new ArrayList();
			foreach (var element in self)
			{
				Hashtable elementDict = new Hashtable();
				result.Add(elementDict);
				doSaveCallback(element, elementDict);
			}

			return result;
		}

		public static void DoRestoreList<T>(this List<T> self, ArrayList arrayList,
			Func<Hashtable, T> doRestoreCallback)
		{
			foreach (var element in arrayList)
			{
				var elementDict = element as Hashtable;
				T elementT = doRestoreCallback(elementDict);
				self.Add(elementT);
			}
		}


	}
}