using System;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;

namespace CsCat
{
	public static class ObjectExtension
	{
		public static bool IsValidObject(this object self)
		{
			return self != null && !self.Equals(null);
		}

		#region DOTween

		public static DOTweenId DOTweenId(this object self)
		{
			return DOTweenUtil.GetDOTweenId(self);
		}

		public static void DOKillByDOTweenId(this object self)
		{
			DOTween.Kill(self.DOTweenId());
		}

		#endregion

		#region ToString2 ToLinkedHashtable2

		/// <summary>
		///   将Object中的真实内容以字符串的形式输出
		/// </summary>
		public static string ToString2(this object self, bool isFillStringWithDoubleQuote = false)
		{
			return IsToString2.ToString2(self, isFillStringWithDoubleQuote);
		}


		public static object ToLinkedHashtable2(this object self)
		{
			return IsToLinkedHashtable2.ToLinkedHashtable2(self);
		}

		#endregion

		#region 类型判断

		/// <summary>
		/// 是否是IsIDictionary
		/// </summary>
		public static bool IsDictionary<T, V>(this object self)
		{
			return self is IDictionary<T, V>;
		}

		/// <summary>
		/// 是否是数组
		/// </summary>
		public static bool IsArray(this object self)
		{
			return (self != null && self.GetType().IsArray);
		}

		/// <summary>
		/// 是否是IList
		/// </summary>
		public static bool IsList<T>(this object self)
		{
			return self is IList<T>;
		}

		/// <summary>
		/// 是否是String
		/// </summary>
		public static bool IsString(this object self)
		{
			return (self is string);
		}

		/// <summary>
		/// 是否是boolean
		/// </summary>
		public static bool IsBool(this object self)
		{
			return (self is bool);
		}

		/// <summary>
		/// 是否是数字
		/// </summary>
		public static bool IsNumber(this object self)
		{
			return (IsByte(self) || self.IsShort() || self.IsInt() || self.IsLong() || self.IsFloat() ||
					self.IsDouble());
		}

		/// <summary>
		/// 是否是整数类型，即非小数类型
		/// </summary>
		public static bool IsIntegral(this object self)
		{
			return (IsByte(self) || self.IsShort() || self.IsInt() || self.IsLong());
		}

		/// <summary>
		/// 是否是小数类型，即非整数类型
		/// </summary>
		public static bool IsFloating(this object self)
		{
			return (self.IsFloat() || self.IsDouble());
		}

		/// <summary>
		/// 是否是byte
		/// </summary>
		public static bool IsByte(this object self)
		{
			return (self is byte);
		}

		/// <summary>
		/// 是否是short
		/// </summary>
		public static bool IsShort(this object self)
		{
			return (self is short);
		}

		/// <summary>
		/// 是否是char
		/// </summary>
		public static bool IsChar(this object self)
		{
			return (self is char);
		}

		/// <summary>
		/// 是否是int
		/// </summary>
		public static bool IsInt(this object self)
		{
			return (self is int);
		}

		/// <summary>
		/// 是否是long类型
		/// </summary>
		public static bool IsLong(this object self)
		{
			return (self is long);
		}

		/// <summary>
		/// 是否是float类型
		/// </summary>
		public static bool IsFloat(this object self)
		{
			return (self is float);
		}

		/// <summary>
		/// 是否是double类型
		/// </summary>
		public static bool IsDouble(this object self)
		{
			return (self is double);
		}

		/// <summary>
		/// 是否是DateTime
		/// </summary>
		public static bool IsDateTime(this object self)
		{
			return (self is DateTime);
		}

		/// <summary>
		/// 是否是bytes
		/// </summary>
		public static bool IsBytes(this object self)
		{
			return (self is byte[]);
		}

		/// <summary>
		/// 是否是chars
		/// </summary>
		public static bool IsChars(object self)
		{
			return (self is char[]);
		}

		/// <summary>
		/// 是否是Class
		/// </summary>
		public static bool IsClass(this object self)
		{
			return (self.GetType().IsClass);
		}

		/// <summary>
		/// 是否是方法
		/// </summary>
		public static bool IsMethod(this object self)
		{
			return (self is MethodBase);
		}

		#endregion

		#region 各种ToXX

		/// <summary>
		/// 将o转化为boolean，失败时返回dv
		/// </summary>
		public static bool ToBoolOrToDefault(this object self, bool defaultValue = false)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return (bool)self;
			if (self.IsNumber())
				return ToIntOrToDefault(self, 0) != 0;
			if (self.IsString())
			{
				if (StringConst.String_true.Equals(((string)self).ToLower()))
					return true;
				if (StringConst.String_false.Equals(((string)self).ToUpper()))
					return false;
				try
				{
					return double.Parse((string)self) != 0.0D;
				}
				catch
				{
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为byte，失败时返回dv
		/// </summary>
		public static byte ToByteOrToDefault(this object self, byte defaultValue = 0)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return (bool)self ? (byte)1 : (byte)0;

			if (self.IsByte())
				return ((byte)self);
			if (self.IsShort())
				return ((byte)(short)self);
			if (self.IsInt())
				return ((byte)(int)self);
			if (self.IsLong())
				return ((byte)(long)self);
			if (self.IsFloat())
				return ((byte)(float)self);
			if (self.IsDouble())
				return ((byte)(double)self);
			if (self.IsString())
			{
				try
				{
					return byte.Parse((string)self);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为short，失败时返回dv
		/// </summary>
		public static short ToShortOrToDefault(this object self, short defaultValue = 0)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return ToByteOrToDefault(self, (byte)defaultValue);
			if (self.IsByte())
				return ((byte)self);
			if (self.IsShort())
				return ((short)self);
			if (self.IsInt())
				return ((short)(int)self);
			if (self.IsLong())
				return ((short)(long)self);
			if (self.IsFloat())
				return ((short)(float)self);
			if (self.IsDouble())
				return ((short)(double)self);
			if (self.IsString())
			{
				try
				{
					return short.Parse((string)self);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为char，失败时返回dv
		/// </summary>
		public static char ToCharOrToDefault(this object self, char defaultValue = (char)0x0)
		{
			if (self == null)
				return defaultValue;
			if (IsChar(self))
				return ((char)self);
			if (self.IsByte())
				return (char)((byte)self);
			if (self.IsShort())
				return (char)((short)self);
			if (self.IsInt())
				return (char)((int)self);
			if (self.IsLong())
				return (char)((long)self);
			if (self.IsFloat())
				return (char)((float)self);
			if (self.IsDouble())
				return (char)((double)self);
			if (self.IsString())
			{
				var s = (string)self;
				if (s.Length == 1)
					return s[0];
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为int，失败时返回dv
		/// </summary>
		public static int ToIntOrToDefault(this object self, int defaultValue = 0)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return ToByteOrToDefault(self, 0);
			if (IsByte(self))
				return ((byte)self);
			if (self.IsShort())
				return ((short)self);
			if (self.IsInt())
				return ((int)self);
			if (self.IsLong())
				return (int)((long)self);
			if (self.IsFloat())
				return (int)((float)self);
			if (self.IsDouble())
				return (int)((double)self);
			if (self.IsString())
			{
				try
				{
					return int.Parse((string)self);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为long，失败时返回dv
		/// </summary>
		public static long ToLongOrToDefault(this object self, long defaultValue = 0)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return ToByteOrToDefault(self, 0);
			if (self.IsByte())
				return ((byte)self);
			if (self.IsShort())
				return ((short)self);
			if (self.IsInt())
				return ((int)self);
			if (self.IsLong())
				return ((long)self);
			if (self.IsFloat())
				return (long)((float)self);
			if (self.IsDouble())
				return (long)((double)self);
			if (self.IsString())
			{
				try
				{
					return long.Parse((string)self);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为float，失败时返回dv
		/// </summary>
		public static float ToFloatOrToDefault(this object self, float defaultValue = 0)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return ToByteOrToDefault(self, 0);
			if (self.IsByte())
				return ((byte)self);
			if (self.IsShort())
				return ((short)self);
			if (self.IsInt())
				return ((int)self);
			if (self.IsLong())
				return ((long)self);
			if (self.IsFloat())
				return ((float)self);
			if (self.IsDouble())
				return (float)((double)self);
			if (self.IsString())
			{
				try
				{
					return float.Parse((string)self);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为double，失败时返回dv
		/// </summary>
		public static double ToDoubleOrToDefault(this object self, double defaultValue = 0)
		{
			if (self == null)
				return defaultValue;
			if (self.IsBool())
				return ToByteOrToDefault(self, 0);
			if (IsByte(self))
				return ((byte)self);
			if (self.IsShort())
				return ((short)self);
			if (self.IsInt())
				return ((int)self);
			if (self.IsLong())
				return ((long)self);
			if (self.IsFloat())
				return ((float)self);
			if (self.IsDouble())
				return ((double)self);
			if (self.IsDateTime())
				return ((DateTime)self).Ticks;
			if (self.IsString())
			{
				try
				{
					return double.Parse((string)self);
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照pattern来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateTimeOrToDefault(this object self, string pattern, DateTime defaultValue = default)
		{
			if (self == null)
				return defaultValue;
			if (self.IsLong())
				return new DateTime((long)self);
			if (self.IsDateTime())
				return (DateTime)self;
			return self.IsString() ? ((string)self).ToDateTime(pattern) : defaultValue;
		}

		/// <summary>
		///将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd HH:mm:ss来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateTimOrToDefault(this object self, DateTime defaultValue = default)
		{
			return ToDateTimeOrToDefault(self, StringConst.String_yyyy_MM_dd_HH_mm_ss, defaultValue);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd来转换）失败时返回dv
		/// </summary>
		public static DateTime ToDateOrToDefault(this object self, DateTime dv = default)
		{
			return ToDateTimeOrToDefault(self, StringConst.String_yyyy_MM_dd, dv);
		}

		/// <summary>
		/// 将o转化为DateTime（如果o是string类型，按照HH:mm:ss来转换）失败时返回dv
		/// </summary>
		public static DateTime ToTimeOrToDefault(this object self, DateTime defaultValue = default)
		{
			return ToDateTimeOrToDefault(self, StringConst.String_HH_mm_ss, defaultValue);
		}

		/// <summary>
		/// 将o转化为String,失败时返回dv
		/// </summary>
		public static string ToStringOrToDefault(this object self, string defaultValue = null)
		{
			return self?.ToString() ?? defaultValue;
		}

		/// <summary>
		/// 将o转化为IList,失败时返回dv
		/// </summary>
		public static List<T> ToListOrToDefault<T>(this object self, List<T> defaultValue = null)
		{
			return IsList<T>(self) ? (List<T>)self : defaultValue;
		}

		/// <summary>
		/// 将o转化为IDictionary,失败时返回dv
		/// </summary>
		public static Dictionary<T, V> ToDictionaryOrToDefault<T, V>(this object self, Dictionary<T, V> defaultValue = null)
		{
			return IsDictionary<T, V>(self) ? (Dictionary<T, V>)self : defaultValue;
		}

		/// <summary>
		/// 将o转化为Booleans,失败时返回dv
		/// </summary>
		public static bool[] ToBoolsOrToDefault(this object self, bool[] defaultValue = null)
		{
			if (self is bool[] booleans)
				return booleans;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new bool[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToBoolOrToDefault();
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Bytes,失败时返回dv
		/// </summary>
		public static byte[] ToBytesOrToDefault(this object self, byte[] defaultValue = null)
		{
			if (self is byte[] bytes)
				return bytes;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new byte[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToByteOrToDefault(0);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Shorts,失败时返回dv
		/// </summary>
		public static short[] ToShortsOrToDefault(this object self, short[] defaultValue = null)
		{
			if (self is short[] shorts)
				return shorts;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new short[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToShortOrToDefault(0);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Ints,失败时返回dv
		/// </summary>
		public static int[] ToIntsOrToDefault(this object self, int[] defaultValue = null)
		{
			if (self is int[] ints)
				return ints;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new int[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = array.GetValue(i).ToIntOrToDefault(0);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Longs,失败时返回dv
		/// </summary>
		public static long[] ToLongsOrToDefault(this object self, long[] defaultValue = null)
		{
			var longs = self as long[];
			if (longs != null)
				return longs;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new long[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = ToLongOrToDefault(array.GetValue(i), 0L);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Floats,失败时返回dv
		/// </summary>
		public static float[] ToFloatsOrToDefault(this object self, float[] defaultValue = null)
		{
			if (self is float[] floats)
				return floats;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new float[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = ToFloatOrToDefault(array.GetValue(i));
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Doubles,失败时返回dv
		/// </summary>
		public static double[] ToDoublesOrToDefault(this object self, double[] defaultValue = null)
		{
			var doubles = self as double[];
			if (doubles != null)
				return doubles;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new double[len];
				for (int i = 0; i < len; i++)
					defaultValue[i] = ToDoubleOrToDefault(array.GetValue(i), 0.0d);
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为Strings,失败时返回dv
		/// </summary>
		public static string[] ToStringsOrToDefault(this object self, string[] defaultValue = null)
		{
			var strings = self as string[];
			if (strings != null)
				return strings;
			if (self.IsArray())
			{
				var array = (Array)self;
				int len = array.Length;
				defaultValue = new string[len];

				for (int i = 0; i < len; i++)
					defaultValue[i] = ToStringOrToDefault(array.GetValue(i));
			}

			return defaultValue;
		}

		/// <summary>
		/// 将o转化为ILists,失败时返回dv
		/// </summary>
		/// <param name="self"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static List<T>[] ToListsOrToDefault<T>(this object self, List<T>[] defaultValue = null)
		{
			var iLists = self as List<T>[];
			return iLists ?? defaultValue;
		}

		/// <summary>
		/// 将o转化为IDictionarys,失败时返回dv
		/// </summary>
		public static Dictionary<T, V>[] ToDictionarysOrToDefault<T, V>(this object self, Dictionary<T, V>[] defaultValue = null)
		{
			var dictionarys = self as Dictionary<T, V>[];
			return dictionarys ?? defaultValue;
		}

		/// <summary>
		/// 将o转化为Objects,失败时返回dv
		/// </summary>
		public static Array ToArrayOrToDefault(this object self, Array defaultValue = null)
		{
			if (self is object[] objects)
				return objects;
			if (self.IsArray())
			{
				var array = (Array)self;
				Array dvArray = null;
				int len = array.Length;
				if (self is bool[])
					dvArray = new bool[len];
				if (self is byte[])
					dvArray = new byte[len];
				if (self is char[])
					dvArray = new char[len];
				if (self is short[])
					dvArray = new short[len];
				if (self is int[])
					dvArray = new int[len];
				if (self is long[])
					dvArray = new long[len];
				if (self is float[])
					dvArray = new float[len];
				if (self is double[])
					dvArray = new double[len];

				for (int i = 0; i < len; i++)
					dvArray?.SetValue(array.GetValue(i), i);

				return dvArray;
			}

			return defaultValue;
		}

		public static T To<T>(this object self)
		{
			return (T)Convert.ChangeType(self, typeof(T));
		}

		public static object To(this object self, Type type)
		{
			return Convert.ChangeType(self, type);
		}

		public static T As<T>(this object self) where T : class
		{
			return self as T;
		}

		/// <summary>
		/// 将o转化为string
		/// </summary>
		public static string ObjectToString(this object self)
		{
			if (self == null)
				return StringConst.String_null;
			if (self.IsString())
				return (string)self;
			if (self.IsDateTime())
				return ((DateTime)self).ToString(StringConst.String_yyyy_MM_dd);
			if (IsChar(self))
				return ((char)self).ToString();
			if (IsChars(self))
				return new string((char[])self);
			return IsBytes(self) ? ByteUtil.ToString((byte[])self, 0, ((byte[])self).Length) : self.ToString();
		}

		#endregion

		#region 反射

		public static bool IsHasMethod(this object self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return GetMethodInfo2(self, methodName, bindingFlags) != null;
		}

		public static MethodInfo GetMethodInfo2(this object self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(self).GetMethodInfo2(methodName, bindingFlags);
		}

		public static MethodInfo GetMethodInfo(this object self, string methodName,
			BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetReflectionType(self)
				.GetMethodInfo(methodName, bindingFlags, sourceParameterTypes);
		}

		//////////////////////////Generic//////////////////////////////
		public static MethodInfo GetGenericMethodInfo2(this object self, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(self)
				.GetGenericMethodInfo2(methodName, genericTypes, bindingFlags);
		}

		public static MethodInfo GetGenericMethodInfo(this object self, string methodName, Type[] genericTypes,
			BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetReflectionType(self)
				.GetGenericMethodInfo(methodName, genericTypes, bindingFlags, sourceParameterTypes);
		}

		//////////////////////////ExtensionMethod//////////////////////////////
		public static bool IsHasExtensionMethod(this object self, string methodName)
		{
			return GetExtensionMethodInfo2(self, methodName) != null;
		}

		public static MethodInfo GetExtensionMethodInfo2(this object self, string methodName)
		{
			return ReflectionUtil.GetReflectionType(self).GetExtensionMethodInfo2(methodName);
		}

		public static MethodInfo GetExtensionMethodInfo(this object self, string methodName,
			params Type[] sourceParameterTypes)
		{
			return ReflectionUtil.GetReflectionType(self).GetExtensionMethodInfo(methodName, sourceParameterTypes);
		}

		#region FiledValue

		public static FieldInfo GetFieldInfo(this object self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(self).GetFieldInfo(fieldName, bindingFlags);
		}

		public static void SetFieldValue(this object self, string fieldName, object value,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			GetFieldInfo(self, fieldName, bindingFlags).SetValue(ReflectionUtil.GetReflectionObject(self), value);
		}

		public static T GetFieldValue<T>(this object self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return (T)GetFieldValue(self, fieldName, bindingFlags);
		}

		public static object GetFieldValue(this object self, string fieldName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(self).GetFieldInfo(fieldName, bindingFlags).GetValue(self);
		}

		#endregion

		#region PropertyValue

		public static PropertyInfo GetPropertyInfo(this object self, string propertyName,
			BindingFlags bindingFlags = BindingFlagsConst.All)
		{
			return ReflectionUtil.GetReflectionType(self).GetPropertyInfo(propertyName, bindingFlags);
		}

		public static void SetPropertyValue(this object self, string propertyName, object value, object[] index = null)
		{
			GetPropertyInfo(self, propertyName)
				.SetValue(ReflectionUtil.GetReflectionObject(self), value, index);
		}

		public static T GetPropertyValue<T>(this object self, string propertyName, object[] index = null)
		{
			return (T)GetPropertyValue(self, propertyName, index);
		}

		public static object GetPropertyValue(this object self, string propertyName, object[] index = null)
		{
			return ReflectionUtil.GetReflectionType(self).GetPropertyInfo(propertyName).GetValue(self, index);
		}

		#endregion

		#region Invoke

		public static T InvokeMethod<T>(this object self, MethodInfo methodInfo, params object[] parameters)
		{
			return ReflectionUtil.Invoke<T>(self, methodInfo, parameters);
		}

		public static T InvokeMethod<T>(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ReflectionUtil.Invoke<T>(self, methodName, isMissNotInvoke, parameters);
		}

		public static void InvokeMethod(this object self, MethodInfo methodInfo, params object[] parameters)
		{
			ReflectionUtil.Invoke(self, methodInfo, parameters);
		}

		public static void InvokeMethod(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ReflectionUtil.Invoke<object>(self, methodName, isMissNotInvoke, parameters);
		}

		//////////////////////////Generic//////////////////////////////
		public static T InvokeGenericMethod<T>(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ReflectionUtil.InvokeGeneric<T>(self, methodName, genericTypes, isMissNotInvoke, parameters);
		}

		public static object InvokeGenericMethod(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ReflectionUtil.InvokeGeneric<object>(self, methodName, genericTypes, isMissNotInvoke, parameters);
		}

		//////////////////////////ExtensionMethod//////////////////////////////
		public static T InvokeExtensionMethod<T>(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ExtensionUtil.InvokeExtension<T>(self, methodName, isMissNotInvoke, parameters);
		}

		public static void InvokeExtensionMethod(this object self, string methodName, bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ExtensionUtil.InvokeExtension<object>(self, methodName, isMissNotInvoke, parameters);
		}

		//////////////////////////Generic//////////////////////////////
		public static T InvokeExtensionGenericMethod<T>(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			return ExtensionUtil.InvokeExtensionGeneric<T>(self, methodName, genericTypes, isMissNotInvoke,
				parameters);
		}

		public static void InvokeExtensionGenericMethod(this object self, string methodName, Type[] genericTypes,
			bool isMissNotInvoke = true,
			params object[] parameters)
		{
			ExtensionUtil.InvokeExtensionGeneric<object>(self, methodName, genericTypes, isMissNotInvoke,
				parameters);
		}

		#endregion

		#endregion

		#region SetColor

		public static void SetColorR(this System.Object self, float v, string memberName = StringConst.String_color)
		{
			ColorUtil.SetColor(self, memberName, ColorMode.R, v);
		}

		public static void SetColorG(this System.Object self, float v, string memberName = StringConst.String_color)
		{
			ColorUtil.SetColor(self, memberName, ColorMode.G, v);
		}

		public static void SetColorB(this System.Object self, float v, string memberName = StringConst.String_color)
		{
			ColorUtil.SetColor(self, memberName, ColorMode.B, v);
		}

		public static void SetColorA(this System.Object self, float v, string memberName = StringConst.String_color)
		{
			ColorUtil.SetColor(self, memberName, ColorMode.A, v);
		}

		public static void SetColor(this System.Object self, ColorMode rgbaMode, params float[] rgba)
		{
			ColorUtil.SetColor(self, rgbaMode, rgba);
		}

		public static void SetColor(System.Object self, string memberName, ColorMode rgbaMode, params float[] rgba)
		{
			ColorUtil.SetColor(self, memberName, rgbaMode, rgba);
		}

		#endregion


		/// <summary>
		///用法
		/// stirng s;
		/// s=s.GetOrSetObject("kk");
		/// 采用延迟调用Func
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="defaultObjectFunc"></param>
		/// <returns></returns>
		public static T GetOrSetDefault<T>(this T self, Func<T> defaultObjectFunc = null)
		{
			if (self == null)
				self = defaultObjectFunc == null ? default : defaultObjectFunc();

			return self;
		}

		//a=a.swap(ref b);
		public static T Swap<T>(this T self, ref T b)
		{
			T c = b;
			b = self;
			return c;
		}


		public static bool IsNull(object self)
		{
			return self == null;
		}

		public static T CloneDeep<T>(this T self)
		{
			return CloneUtil.CloneDeep(self);
		}

		public static T Clone<T>(this T self)
		{
			return CloneUtil.Clone(self);
		}

		public static void Despawn(this object self)
		{
			if (self is IDespawn spawn)
			{
				spawn.OnDespawn();
			}
		}

		public static object GetNotNullKey(this object self)
		{
			return self ?? NullUtil.GetDefaultString();
		}

		public static object GetNullableKey(this object self)
		{
			return self.Equals(NullUtil.GetDefaultString()) ? null : self;
		}
	}
}