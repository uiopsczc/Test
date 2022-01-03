using System;
using System.Text;

namespace CsCat
{
	public static class ObjectUtil
	{
		/// <summary>
		/// o1是否和o2相等
		/// </summary>
		public new static bool Equals(object o1, object o2)
		{
			return o1?.Equals(o2) ?? o2 == null;
		}

		/// <summary>
		/// o1和o2比较大小
		/// </summary>
		public static int Compares(object o1, object o2)
		{
			if (o1 == o2)
				return 0;
			if (o1 != null && o2 == null)
				return 1;
			switch (o1)
			{
				case null when o2 != null:
					return -1;
				case IComparable comparable:
					return comparable.CompareTo(o2);
			}

			return o2 is IComparable comparable1 ? comparable1.CompareTo(o1) : o1.ToString().CompareTo(o2.ToString());
		}


		public static int GetHashCode(params object[] objs)
		{
			int result = int.MinValue;
			bool isFoundFirstNotNullObject = false;
			for (var i = 0; i < objs.Length; i++)
			{
				var obj = objs[i];
				if (obj == null) continue;
				if (isFoundFirstNotNullObject)
					result ^= obj.GetHashCode();
				else
				{
					result = obj.GetHashCode();
					isFoundFirstNotNullObject = true;
				}
			}

			return result;
		}

		/// <summary>
		/// 交换两个object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void Swap<T>(ref T a, ref T b)
		{
			T c = b;
			b = a;
			a = c;
		}

		public static string ToString(params object[] objs)
		{
			using (var scope = new StringBuilderScope())
			{
				for (int i = 0; i < objs.Length; i++)
				{
					var obj = objs[i];
					if (i == objs.Length - 1)
						scope.stringBuilder.Append(obj);
					else
						scope.stringBuilder.Append(obj + StringConst.String_Space);
				}

				return scope.stringBuilder.ToString();
			}
		}

		public static string ToString2(params object[] objs)
		{
			using (var scope = new StringBuilderScope())
			{
				for (int i = 0; i < objs.Length; i++)
				{
					var obj = objs[i];
					if (i == objs.Length - 1)
						scope.stringBuilder.Append(obj.ToString2());
					else
						scope.stringBuilder.Append(obj.ToString2() + StringConst.String_Space);
				}

				return scope.stringBuilder.ToString();
			}
		}
	}
}