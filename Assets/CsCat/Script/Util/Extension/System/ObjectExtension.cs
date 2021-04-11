using System;
using System.Collections.Generic;
using System.IO;
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
    public static string ToString2(this object self, bool is_fill_string_with_double_quote = false)
    {
      return IsToString2.ToString2(self, is_fill_string_with_double_quote);
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
      if (self is IDictionary<T, V>)
        return true;
      else
        return false;
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
      if (self is IList<T>)
        return true;
      else
        return false;
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
      return (IsByte(self) || self.IsShort() || self.IsInt() || self.IsLong() || self.IsFloat() || self.IsDouble());
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
    public static bool ToBoolOrToDefault(this object self, bool dv = false)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
        return (bool) self;
      if (self.IsNumber())
        return ToIntOrToDefault(self, 0) != 0;
      if (self.IsString())
      {
        if ("true".Equals(((string) self).ToLower()))
          return true;
        if ("false".Equals(((string) self).ToUpper()))
          return false;
        try
        {
          return double.Parse((string) self) != 0.0D;
        }
        catch
        {
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为byte，失败时返回dv
    /// </summary>
    public static byte ToByteOrToDefault(this object self, byte dv = 0)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
      {
        if ((bool) self)
          return 1;
        return 0;
      }

      if (self.IsByte())
        return ((byte) self);
      if (self.IsShort())
        return ((byte) (short) self);
      if (self.IsInt())
        return ((byte) (int) self);
      if (self.IsLong())
        return ((byte) (long) self);
      if (self.IsFloat())
        return ((byte) (float) self);
      if (self.IsDouble())
        return ((byte) (double) self);
      if (self.IsString())
      {
        try
        {
          return byte.Parse((string) self);
        }
        catch
        {
          return dv;
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为short，失败时返回dv
    /// </summary>
    public static short ToShortOrToDefault(this object self, short dv = 0)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
        return ToByteOrToDefault(self, 0);
      if (self.IsByte())
        return ((byte) self);
      if (self.IsShort())
        return ((short) self);
      if (self.IsInt())
        return ((short) (int) self);
      if (self.IsLong())
        return ((short) (long) self);
      if (self.IsFloat())
        return ((short) (float) self);
      if (self.IsDouble())
        return ((short) (double) self);
      if (self.IsString())
      {
        try
        {
          return short.Parse((string) self);
        }
        catch
        {
          return dv;
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为char，失败时返回dv
    /// </summary>
    public static char ToCharOrToDefault(this object self, char dv = (char) 0x0)
    {
      if (self == null)
        return dv;
      if (IsChar(self))
        return ((char) self);
      if (self.IsByte())
        return (char) ((byte) self);
      if (self.IsShort())
        return (char) ((short) self);
      if (self.IsInt())
        return (char) ((int) self);
      if (self.IsLong())
        return (char) ((long) self);
      if (self.IsFloat())
        return (char) ((float) self);
      if (self.IsDouble())
        return (char) ((double) self);
      if (self.IsString())
      {
        var s = (string) self;
        if (s.Length == 1)
          return s[0];
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为int，失败时返回dv
    /// </summary>
    public static int ToIntOrToDefault(this object self, int dv = 0)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
        return ToByteOrToDefault(self, 0);
      if (IsByte(self))
        return ((byte) self);
      if (self.IsShort())
        return ((short) self);
      if (self.IsInt())
        return ((int) self);
      if (self.IsLong())
        return (int) ((long) self);
      if (self.IsFloat())
        return (int) ((float) self);
      if (self.IsDouble())
        return (int) ((double) self);
      if (self.IsString())
      {
        try
        {
          return int.Parse((string) self);
        }
        catch
        {
          return dv;
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为long，失败时返回dv
    /// </summary>
    public static long ToLongOrToDefault(this object self, long dv = 0)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
        return ToByteOrToDefault(self, 0);
      if (self.IsByte())
        return ((byte) self);
      if (self.IsShort())
        return ((short) self);
      if (self.IsInt())
        return ((int) self);
      if (self.IsLong())
        return ((long) self);
      if (self.IsFloat())
        return (long) ((float) self);
      if (self.IsDouble())
        return (long) ((double) self);
      if (self.IsString())
      {
        try
        {
          return long.Parse((string) self);
        }
        catch
        {
          return dv;
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为float，失败时返回dv
    /// </summary>
    public static float ToFloatOrToDefault(this object self, float dv = 0)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
        return ToByteOrToDefault(self, 0);
      if (self.IsByte())
        return ((byte) self);
      if (self.IsShort())
        return ((short) self);
      if (self.IsInt())
        return ((int) self);
      if (self.IsLong())
        return ((long) self);
      if (self.IsFloat())
        return ((float) self);
      if (self.IsDouble())
        return (float) ((double) self);
      if (self.IsString())
      {
        try
        {
          return float.Parse((string) self);
        }
        catch
        {
          return dv;
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为double，失败时返回dv
    /// </summary>
    public static double ToDoubleOrToDefault(this object self, double dv = 0)
    {
      if (self == null)
        return dv;
      if (self.IsBool())
        return ToByteOrToDefault(self, 0);
      if (IsByte(self))
        return ((byte) self);
      if (self.IsShort())
        return ((short) self);
      if (self.IsInt())
        return ((int) self);
      if (self.IsLong())
        return ((long) self);
      if (self.IsFloat())
        return ((float) self);
      if (self.IsDouble())
        return ((double) self);
      if (self.IsDateTime())
        return ((DateTime) self).Ticks;
      if (self.IsString())
      {
        try
        {
          return double.Parse((string) self);
        }
        catch
        {
          return dv;
        }
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为DateTime（如果o是string类型，按照pattern来转换）失败时返回dv
    /// </summary>
    public static DateTime ToDateTimeOrToDefault(this object self, string pattern, DateTime dv = default(DateTime))
    {
      if (self == null)
        return dv;
      if (self.IsLong())
        return new DateTime((long) self);
      if (self.IsDateTime())
        return (DateTime) self;
      if (self.IsString())
      {
        return ((string) self).ToDateTime(pattern);
      }

      return dv;
    }

    /// <summary>
    ///将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd HH:mm:ss来转换）失败时返回dv
    /// </summary>
    public static DateTime ToDateTimOrToDefault(this object self, DateTime dv = default(DateTime))
    {
      return ToDateTimeOrToDefault(self, "yyyy-MM-dd HH:mm:ss", dv);
    }

    /// <summary>
    /// 将o转化为DateTime（如果o是string类型，按照yyyy-MM-dd来转换）失败时返回dv
    /// </summary>
    public static DateTime ToDateOrToDefault(this object self, DateTime dv = default(DateTime))
    {
      return ToDateTimeOrToDefault(self, "yyyy-MM-dd", dv);
    }

    /// <summary>
    /// 将o转化为DateTime（如果o是string类型，按照HH:mm:ss来转换）失败时返回dv
    /// </summary>
    public static DateTime ToTimeOrToDefault(this object self, DateTime dv = default(DateTime))
    {
      return ToDateTimeOrToDefault(self, "HH:mm:ss", dv);
    }

    /// <summary>
    /// 将o转化为String,失败时返回dv
    /// </summary>
    public static string ToStringOrToDefault(this object self, string dv = null)
    {
      if (self == null)
        return dv;
      return self.ToString();
    }

    /// <summary>
    /// 将o转化为IList,失败时返回dv
    /// </summary>
    public static List<T> ToListOrToDefault<T>(this object self, List<T> dv = null)
    {
      if (IsList<T>(self))
        return (List<T>) self;
      return dv;
    }

    /// <summary>
    /// 将o转化为IDictionary,失败时返回dv
    /// </summary>
    public static Dictionary<T, V> ToDictionaryOrToDefault<T, V>(this object self, Dictionary<T, V> dv = null)
    {
      if (IsDictionary<T, V>(self))
        return (Dictionary<T, V>) self;
      return dv;
    }

    /// <summary>
    /// 将o转化为Booleans,失败时返回dv
    /// </summary>
    public static bool[] ToBoolsOrToDefault(this object self, bool[] dv = null)
    {
      var booleans = self as bool[];
      if (booleans != null)
        return booleans;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new bool[len];
        for (int i = 0; i < len; i++)
          dv[i] = array.GetValue(i).ToBoolOrToDefault(false);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Bytes,失败时返回dv
    /// </summary>
    public static byte[] ToBytesOrToDefault(this object self, byte[] dv = null)
    {
      var bytes = self as byte[];
      if (bytes != null)
        return bytes;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new byte[len];
        for (int i = 0; i < len; i++)
          dv[i] = array.GetValue(i).ToByteOrToDefault(0);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Shorts,失败时返回dv
    /// </summary>
    public static short[] ToShortsOrToDefault(this object self, short[] dv = null)
    {
      var shorts = self as short[];
      if (shorts != null)
        return shorts;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new short[len];
        for (int i = 0; i < len; i++)
          dv[i] = array.GetValue(i).ToShortOrToDefault(0);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Ints,失败时返回dv
    /// </summary>
    public static int[] ToIntsOrToDefault(this object self, int[] dv = null)
    {
      var ints = self as int[];
      if (ints != null)
        return ints;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new int[len];
        for (int i = 0; i < len; i++)
          dv[i] = array.GetValue(i).ToIntOrToDefault(0);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Longs,失败时返回dv
    /// </summary>
    public static long[] ToLongsOrToDefault(this object self, long[] dv = null)
    {
      var longs = self as long[];
      if (longs != null)
        return longs;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new long[len];
        for (int i = 0; i < len; i++)
          dv[i] = ToLongOrToDefault(array.GetValue(i), 0L);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Floats,失败时返回dv
    /// </summary>
    public static float[] ToFloatsOrToDefault(this object self, float[] dv = null)
    {
      var floats = self as float[];
      if (floats != null)
        return floats;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new float[len];
        for (int i = 0; i < len; i++)
          dv[i] = ToFloatOrToDefault(array.GetValue(i), 0.0F);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Doubles,失败时返回dv
    /// </summary>
    public static double[] ToDoublesOrToDefault(this object self, double[] dv = null)
    {
      var doubles = self as double[];
      if (doubles != null)
        return doubles;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new double[len];
        for (int i = 0; i < len; i++)
          dv[i] = ToDoubleOrToDefault(array.GetValue(i), 0.0d);
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为Strings,失败时返回dv
    /// </summary>
    public static string[] ToStringsOrToDefault(this object self, string[] dv = null)
    {
      var strings = self as string[];
      if (strings != null)
        return strings;
      if (self.IsArray())
      {
        var array = (Array) self;
        int len = array.Length;
        dv = new string[len];

        for (int i = 0; i < len; i++)
          dv[i] = ToStringOrToDefault(array.GetValue(i), "");
      }

      return dv;
    }

    /// <summary>
    /// 将o转化为ILists,失败时返回dv
    /// </summary>
    /// <param name="self"></param>
    /// <param name="dv"></param>
    /// <returns></returns>
    public static List<T>[] ToListsOrToDefault<T>(this object self, List<T>[] dv = null)
    {
      var iLists = self as List<T>[];
      if (iLists != null)
        return iLists;
      return dv;
    }

    /// <summary>
    /// 将o转化为IDictionarys,失败时返回dv
    /// </summary>
    public static Dictionary<T, V>[] ToDictionarysOrToDefault<T, V>(this object self, Dictionary<T, V>[] dv = null)
    {
      var iDictionarys = self as Dictionary<T, V>[];
      if (iDictionarys != null)
        return iDictionarys;
      return dv;
    }

    /// <summary>
    /// 将o转化为Objects,失败时返回dv
    /// </summary>
    public static Array ToArrayOrToDefault(this object self, Array dv = null)
    {
      var objects = self as object[];
      if (objects != null)
        return objects;
      if (self.IsArray())
      {
        var array = (Array) self;
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
          if (dvArray != null)
            dvArray.SetValue(array.GetValue(i), i);
        return dvArray;
      }

      return dv;
    }

    public static T To<T>(this object self)
    {
      return (T) Convert.ChangeType(self, typeof(T));
    }

    public static object To(this object self, Type type)
    {
      return Convert.ChangeType(self, type);
    }

    public static T AS<T>(this object self) where T : class
    {
      return self as T;
    }

    /// <summary>
    /// 将o转化为string
    /// </summary>
    public static string ObjectToString(this object self)
    {
      if (self == null)
        return "null";
      if (self.IsString())
        return (string) self;
      if (self.IsDateTime())
        return ((DateTime) self).ToString("yyyy-MM-dd HH:mm:ss");
      if (IsChar(self))
        return ((char) self) + "";
      if (IsChars(self))
        return new string((char[]) self);
      if (IsBytes(self))
        return ByteUtil.ToString((byte[]) self, 0, ((byte[]) self).Length);
      return self.ToString();
    }



    #endregion

    #region 反射
    public static bool IsHasMethod(this object self, string method_name, BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return GetMethodInfo2(self, method_name, bindingFlags) != null;
    }

    public static MethodInfo GetMethodInfo2(this object self, string method_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetReflectionType(self).GetMethodInfo2(method_name, bindingFlags);
    }

    public static MethodInfo GetMethodInfo(this object self, string method_name,
      BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
    {
      return ReflectionUtil.GetReflectionType(self).GetMethodInfo( method_name, bindingFlags, sourceParameterTypes);
    }
    //////////////////////////Generic//////////////////////////////
    public static MethodInfo GetGenericMethodInfo2(this object self, string method_name, Type[] generic_types,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetReflectionType(self).GetGenericMethodInfo2(method_name, generic_types, bindingFlags);
    }
    public static MethodInfo GetGenericMethodInfo(this object self, string method_name, Type[] generic_types,
      BindingFlags bindingFlags = BindingFlagsConst.All, params Type[] sourceParameterTypes)
    {
      return ReflectionUtil.GetReflectionType(self).GetGenericMethodInfo(method_name, generic_types, bindingFlags, sourceParameterTypes);
    }
    //////////////////////////ExtensionMethod//////////////////////////////
    public static bool IsHasExtensionMethod(this object self, string method_name)
    {
      return GetExtensionMethodInfo2(self, method_name) != null;
    }

    public static MethodInfo GetExtensionMethodInfo2(this object self, string method_name)
    {
      return ReflectionUtil.GetReflectionType(self).GetExtensionMethodInfo2(method_name);
    }

    public static MethodInfo GetExtensionMethodInfo(this object self, string method_name,
      params Type[] sourceParameterTypes)
    {
      return ReflectionUtil.GetReflectionType(self).GetExtensionMethodInfo(method_name, sourceParameterTypes);
    }

    #region FiledValue

    public static FieldInfo GetFieldInfo(this object self, string field_name,BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetReflectionType(self).GetFieldInfo(field_name, bindingFlags);
    }

    public static void SetFieldValue(this object self, string field_name, object value,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      GetFieldInfo(self, field_name, bindingFlags).SetValue(ReflectionUtil.GetReflectionObject(self), value);
    }

    public static T GetFieldValue<T>(this object self, string field_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return (T) GetFieldValue(self, field_name, bindingFlags);
    }

    public static object GetFieldValue(this object self, string field_name,
      BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetReflectionType(self).GetFieldInfo( field_name, bindingFlags).GetValue(self);
    }

    #endregion

    #region PropertyValue

    public static PropertyInfo GetPropertyInfo(this object self, string property_name, BindingFlags bindingFlags = BindingFlagsConst.All)
    {
      return ReflectionUtil.GetReflectionType(self).GetPropertyInfo(property_name, bindingFlags);
    }

    public static void SetPropertyValue(this object self, string property_name, object value, object[] index = null)
    {
      GetPropertyInfo(self, property_name)
        .SetValue(ReflectionUtil.GetReflectionObject(self), value, index);
    }

    public static T GetPropertyValue<T>(this object self, string property_name, object[] index = null)
    {
      return (T) GetPropertyValue(self, property_name, index);
    }

    public static object GetPropertyValue(this object self, string property_name, object[] index = null)
    {
      return ReflectionUtil.GetReflectionType(self).GetPropertyInfo(property_name).GetValue(self, index);
    }



    #endregion

    #region Invoke

    public static T InvokeMethod<T>(this object self, MethodInfo methodInfo, params object[] parameters)
    {
      return ReflectionUtil.Invoke<T>(self, methodInfo, parameters);
    }

    public static T InvokeMethod<T>(this object self, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return ReflectionUtil.Invoke<T>(self, method_name, is_miss_not_invoke, parameters);
    }

    public static void InvokeMethod(this object self, MethodInfo methodInfo, params object[] parameters)
    {
      ReflectionUtil.Invoke(self, methodInfo, parameters);
    }

    public static void InvokeMethod(this object self, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      ReflectionUtil.Invoke<object>(self, method_name, is_miss_not_invoke, parameters);
    }
    //////////////////////////Generic//////////////////////////////
    public static T InvokeGenericMethod<T>(this object self, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return ReflectionUtil.InvokeGeneric<T>(self, method_name, generic_types, is_miss_not_invoke, parameters);
    }
    
    public static void InvokeGenericMethod(this object self, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      ReflectionUtil.InvokeGeneric<object>(self, method_name, generic_types, is_miss_not_invoke, parameters);
    }

    //////////////////////////ExtensionMethod//////////////////////////////
    public static T InvokeExtensionMethod<T>(this object self, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return ExtensionUtil.InvokeExtension<T>(self, method_name, is_miss_not_invoke, parameters);
    }
    public static void InvokeExtensionMethod(this object self, string method_name, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      ExtensionUtil.InvokeExtension<object>(self, method_name, is_miss_not_invoke, parameters);
    }
    //////////////////////////Generic//////////////////////////////
    public static T InvokeExtensionGenericMethod<T>(this object self, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      return ExtensionUtil.InvokeExtensionGeneric<T>(self, method_name, generic_types, is_miss_not_invoke, parameters);
    }
    public static void InvokeExtensionGenericMethod(this object self, string method_name, Type[] generic_types, bool is_miss_not_invoke = true,
      params object[] parameters)
    {
      ExtensionUtil.InvokeExtensionGeneric<object>(self, method_name, generic_types, is_miss_not_invoke, parameters);
    }
    #endregion

    #endregion

    #region SetColor

    public static void SetColorR(this System.Object self, float v, string member_name = "color")
    {
      ColorUtil.SetColor(self, member_name, ColorMode.R, v);
    }

    public static void SetColorG(this System.Object self, float v, string member_name = "color")
    {
      ColorUtil.SetColor(self, member_name, ColorMode.G, v);
    }

    public static void SetColorB(this System.Object self, float v, string member_name = "color")
    {
      ColorUtil.SetColor(self, member_name, ColorMode.B, v);
    }

    public static void SetColorA(this System.Object self, float v, string member_name = "color")
    {
      ColorUtil.SetColor(self, member_name, ColorMode.A, v);
    }

    public static void SetColor(this System.Object self, ColorMode rgbaMode, params float[] rgba)
    {
      ColorUtil.SetColor(self, rgbaMode, rgba);
    }

    public static void SetColor(System.Object self, string member_name, ColorMode rgbaMode, params float[] rgba)
    {
      ColorUtil.SetColor(self, member_name, rgbaMode, rgba);
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
    /// <param name="defalutObjectFunc"></param>
    /// <returns></returns>
    public static T GetOrSetDefault<T>(this T self, Func<T> defalutObjectFunc = null)
    {
      if (self == null)
      {
        if (defalutObjectFunc == null)
          self = default(T);
        else
          self = defalutObjectFunc();
      }

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

    public static T Clone_Deep<T>(this T self)
    {
      return CloneUtil.Clone_Deep(self);
    }

    public static T Clone<T>(this T self)
    {
      return CloneUtil.Clone(self);
    }

    public static void Despawn(this object self)
    {
      PoolCatManagerUtil.Despawn(self);
    }

    public static object GetNotNullKey(this object self)
    {
      if (self != null)
        return self;
      return NullUtil.GetDefaultString();
    }

    public static object GetNullableKey(this object self)
    {
      if (self.Equals(NullUtil.GetDefaultString()))
        return null;
      return self;
    }


  }
}