using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
	public class JsonSerializer
	{
		#region field

		private const int TYPE_REF = -1;
		private const int TYPE_CLASS = 0;
		private const int TYPE_ARRAY = 1;
		private const int TYPE_LIST = 2;
		private const int TYPE_ENUM = 3;
		private const int TYPE_DICT = 4;
		private const int TYPE_VALUE = 5;
		private const string STR_REF_ID = "id";
		private const string STR_CLS_TYPE_ID = "classId";
		private const string STR_CLS_TYPE = "type";
		private const string STR_CLS_DATA_VALUE = "value";
		private const string STR_CLS_TABLE = "classTable";
		private const BindingFlags InstanceFlags = BindingFlagsConst.Instance;



		private static Dictionary<Type, Dictionary<object, JsonSerializer.SerializeObjInfo>> _serializeCache =
		  new Dictionary<Type, Dictionary<object, JsonSerializer.SerializeObjInfo>>();

		private static readonly Hashtable _classTypeTable = new Hashtable();
		private static long _curClassId;
		private static long _curObjectId;
		private static Dictionary<long, Type> _typeCache;
		private static Dictionary<long, object> _deserializeCache;

		#endregion

		#region static method

		#region public

		public static void AddToCache(object newObj, Hashtable table)
		{
			object obj = table[STR_REF_ID];
			if (obj != null)
			{
				long key = Convert.ToInt64(obj);
				JsonSerializer._deserializeCache[key] = newObj;
			}
		}

		public static object Deserialize(string txt, object context = null)
		{
			JsonSerializer._deserializeCache = new Dictionary<long, object>();
			JsonSerializer._typeCache = new Dictionary<long, Type>();
			object obj = MiniJson.JsonDecode(txt);
			object result = null;
			if (obj != null)
			{
				JsonSerializer._ConstructClassTable((Hashtable)((Hashtable)obj)[STR_CLS_TABLE]);
				result = JsonSerializer.Deserialize(obj, obj.GetType(), context);
			}

			JsonSerializer._deserializeCache.Clear();
			JsonSerializer._deserializeCache = null;
			JsonSerializer._typeCache.Clear();
			JsonSerializer._typeCache = null;

			return result;
		}

		public static object Deserialize(object value, Type type, object context)
		{
			if (value is Hashtable hashtable)
			{
				return JsonSerializer.Deserialize(hashtable, context);
			}

			if (type == typeof(string))
			{
				return value.ToString();
			}

			if (type == typeof(bool))
			{
				try
				{
					object result = Convert.ToBoolean(value);
					return result;
				}
				catch (Exception)
				{
					object result = false;
					return result;
				}
			}

			if (type == typeof(short))
			{
				try
				{
					object result = Convert.ToInt16(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(ushort))
			{
				try
				{
					object result = Convert.ToUInt16(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(int))
			{
				try
				{
					object result = Convert.ToInt32(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(uint))
			{
				try
				{
					object result = Convert.ToUInt32(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(float))
			{
				try
				{
					object result = Convert.ToSingle(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(double))
			{
				try
				{
					object result = Convert.ToDouble(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(byte))
			{
				try
				{
					object result = Convert.ToByte(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(sbyte))
			{
				try
				{
					object result = Convert.ToSByte(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(long))
			{
				try
				{
					object result = Convert.ToInt64(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			if (type == typeof(ulong))
			{
				try
				{
					object result = Convert.ToUInt64(value);
					return result;
				}
				catch (Exception)
				{
					object result = 0;
					return result;
				}
			}

			return value;
		}

		public static object Deserialize(Hashtable table, object context)
		{
			object result = null;
			try
			{
				switch (Convert.ToInt32(table[STR_CLS_TYPE]))
				{
					case TYPE_REF:
						result = JsonSerializer.DeserializeRef(table);
						break;
					case TYPE_CLASS:
						result = JsonSerializer.DeserializeClass(table, context);
						break;
					case TYPE_ARRAY:
						result = JsonSerializer.DeserializeArray(table, context);
						break;
					case TYPE_LIST:
						result = JsonSerializer.DeserializeList(table, context);
						break;
					case TYPE_DICT:
						result = JsonSerializer.DeserializeDict(table, context);
						break;
					case TYPE_ENUM:
						result = JsonSerializer.DeserializeEnum(table);
						break;
					case TYPE_VALUE:
						result = JsonSerializer.DeserializeValue(table);
						break;
					default:
						LogCat.LogError("Deserialize unknown type:" +
										JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])]);
						break;
				}
			}
			catch (Exception ex)
			{
				LogCat.LogError("Deserialize failed:" + ex.ToString());
			}

			return result;
		}

		public static object DeserializeArray(Hashtable table, object context)
		{
			Type type = JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])];
			ArrayList arrayList = (ArrayList)table[STR_CLS_DATA_VALUE];
			int count = arrayList.Count;
			Array array = Array.CreateInstance(type, count);
			JsonSerializer.AddToCache(array, table);
			Type type2 = type.GetElementType() ?? type;
			for (int i = 0; i < count; i++)
				array.SetValue(JsonSerializer.Deserialize(arrayList[i], type2, context), i);

			return array;
		}

		public static object DeserializeClass(Hashtable table, object context)
		{
			Type type = JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])];
			ArrayList arrayList = (ArrayList)table[STR_CLS_DATA_VALUE];
			object obj = Activator.CreateInstance(type, true);
			JsonSerializer.AddToCache(obj, table);
			if (obj is ISerializable serializable)
				serializable.Deserialize(new SerializationInfo(arrayList, context), context);
			else
			{
				int count = arrayList.Count;
				for (int i = 0; i < count; i++)
				{
					IDictionaryEnumerator iterator = ((Hashtable) arrayList[i]).GetEnumerator();
					iterator.MoveNext();
					string text = iterator.Key.ToString();
					object value = iterator.Value;
					FieldInfo field = obj.GetType().GetFieldInfo(text, InstanceFlags);
					if (field != null)
					{
						object obj2 = JsonSerializer.Deserialize(value, field.FieldType, context);
						if (obj2 != null)
						{
							if (!obj2.GetType().IsSubTypeOf(field.FieldType))
							{
								LogCat.LogWarningFormat("Type dismatch of [{0} [{1} <-> {2}] when DeserializeClass {3}",
									text,
									field.FieldType,
									obj2.GetType(),
									type
								);
							}
							else
							{
								try
								{
									field.SetValue(obj, obj2);
								}
								catch (Exception e)
								{
									LogCat.LogErrorFormat(e.ToString(), new object[0]);
								}
							}
						}
					}
				}
			}

			return obj;
		}

		public static object DeserializeEnum(Hashtable table)
		{
			Type type = JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])];
			long value = Convert.ToInt64(table[STR_CLS_DATA_VALUE].ToString());
			return Enum.ToObject(type, value);
		}

		public static object DeserializeValue(Hashtable table)
		{
			Type type = JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])];
			object value = table[STR_CLS_DATA_VALUE].To(type);
			return value;
		}

		public static object DeserializeList(Hashtable table, object context)
		{
			Type type = JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])];
			ArrayList arrayList = (ArrayList)table[STR_CLS_DATA_VALUE];
			IList list = (IList)Activator.CreateInstance(type);
			JsonSerializer.AddToCache(list, table);
			Type type2 = type.GetGenericArguments()[0];
			int count = arrayList.Count;
			for (int i = 0; i < count; i++)
			{
				list.Add(JsonSerializer.Deserialize(arrayList[i], type2, context));
			}

			return list;
		}

		public static object DeserializeDict(Hashtable table, object context)
		{
			Type dictType = JsonSerializer._typeCache[Convert.ToInt64(table[STR_CLS_TYPE_ID])];
			ArrayList dictList = (ArrayList)table[STR_CLS_DATA_VALUE];
			IDictionary result = (IDictionary)Activator.CreateInstance(dictType);
			JsonSerializer.AddToCache(result, table);
			Type keyType = dictType.GetGenericArguments()[0];
			Type valueType = dictType.GetGenericArguments()[1];
			foreach (Hashtable dict in dictList)
			{
				object key = dict["key"];
				object value = dict["value"];
				result[JsonSerializer.Deserialize(key, keyType, context)] =
				  JsonSerializer.Deserialize(value, valueType, context);
			}

			return result;
		}

		public static object DeserializeRef(Hashtable table)
		{
			long num = Convert.ToInt64(table[STR_REF_ID]);
			if (!JsonSerializer._deserializeCache.TryGetValue(num, out var result))
				LogCat.LogErrorFormat("DeserializeRef {0} failed!", num);

			return result;
		}

		public static object SerializeObject(object value, object context)
		{
			Type type = value.GetType();
			JsonSerializer.SerializeObjInfo serializeObjInfo = JsonSerializer.TryGetSerializedObject(type, value);
			if (serializeObjInfo != null)
			{
				Hashtable hashtable = new Hashtable();
				hashtable[STR_CLS_TYPE] = -1;
				hashtable[STR_REF_ID] = serializeObjInfo.id;
				serializeObjInfo.classTable[STR_REF_ID] = serializeObjInfo.id;
				return hashtable;
			}

			object result = null;
			try
			{
				if (type.IsEnum)
					result = JsonSerializer.SerializeEnum(value, type);
				else
				{
					if (type.IsArray)
						result = JsonSerializer._SerializeArray(value, type, context);
					else
					{
						if (value is IList)
							result = JsonSerializer.SerializeList(value, type, context);
						else if (value is IDictionary)
							result = JsonSerializer.SerializeDict(value, type, context);
						else
						{
							if (JsonSerializer.IsBaseType(type))
							{
								//result = value;
								result = CreateValueTable(value);
							}
							else
							{
								if (UnitySerializeObjectType.IsSerializeType(type))
									result = JsonSerializer.SerializeUnityStruct(value, type, context);
								else
								{
									if (type.IsClass || type.IsValueType)
										result = JsonSerializer._SerializeClass(value, type, context);
									else
										LogCat.LogError("unsupport serialize type:" + type.ToString());
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogCat.LogError("SerializeObject failed:" + ex.ToString());
			}

			return result;
		}

		#endregion

		#region private

		private static void _AddToRefCache(Type type, object value, Hashtable classTable)
		{
			if (classTable != null)
			{
				Dictionary<object, JsonSerializer.SerializeObjInfo> dict = JsonSerializer._serializeCache[type];
				long id = JsonSerializer._curObjectId += 1;
				dict[value] = new JsonSerializer.SerializeObjInfo
				{
					id = id,
					classTable = classTable
				};
			}
		}

		private static void _Clear()
		{
			JsonSerializer._curObjectId = 0;
			JsonSerializer._curClassId = 0;
			JsonSerializer._classTypeTable.Clear();
			JsonSerializer._serializeCache.Clear();
		}

		private static void _ConstructClassTable(Hashtable table)
		{
			IDictionaryEnumerator enumerator = table.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Type type = Type.GetType(enumerator.Value.ToString());
				if (type == null)
					LogCat.LogError("type is null:" + enumerator.Value.ToString());
				else
					JsonSerializer._typeCache[Convert.ToInt64(enumerator.Key)] = type;
			}
		}

		private static Hashtable CreateClassTable(object value, bool needList = true, int typeClass = TYPE_CLASS)
		{
			Type type = value.GetType();
			Hashtable hashtable = new Hashtable();
			hashtable[STR_CLS_TYPE] = typeClass;
			hashtable[STR_CLS_TYPE_ID] = JsonSerializer.GetClassTypeId(type);
			if (needList)
			{
				hashtable[STR_CLS_DATA_VALUE] = new ArrayList();
				//如果是value是dict<K,V> 则ArrayList里面的是元素是hashtable，hashtable里面分别用字段"key"，"value"来保存key和value
				//为什么不直接用Hashtable，因为json不解析hashtable中的key(如果key是类的话)
			}

			return hashtable;
		}

		private static Hashtable CreateValueTable(object value, int typeClass = TYPE_VALUE)
		{
			Type type = value.GetType();
			Hashtable hashtable = new Hashtable();
			hashtable[STR_CLS_TYPE] = typeClass;
			hashtable[STR_CLS_TYPE_ID] = JsonSerializer.GetClassTypeId(type);
			hashtable[STR_CLS_DATA_VALUE] = value;
			return hashtable;
		}

		private static long GetClassTypeId(Type type)
		{
			if (JsonSerializer._classTypeTable.ContainsValue(type.AssemblyQualifiedName))
			{
				IDictionaryEnumerator enumerator = JsonSerializer._classTypeTable.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Value.ToString() == type.AssemblyQualifiedName)
						return (long) enumerator.Key;
				}

				throw new Exception("GetClassTypeId failed! " + type.AssemblyQualifiedName);
			}

			long num = JsonSerializer._curClassId += 1;
			JsonSerializer._classTypeTable[num] = type.AssemblyQualifiedName;
			return num;
		}

		private static bool IsBaseType(Type type)
		{
			return type == typeof(string) || (type.IsValueType && type.FullName == "System." + type.Name) ||
				   type.FullName == "System.Reflection." + type.Name;
		}

		public static string Serialize(object value, object context = null)
		{
			JsonSerializer._Clear();
			Hashtable hashtable = JsonSerializer.SerializeObject(value, context) as Hashtable;
			hashtable[STR_CLS_TABLE] = JsonSerializer._classTypeTable;
			return MiniJson.JsonEncode(hashtable);
		}

		private static Hashtable _SerializeArray(object value, Type aryType, object context)
		{
			Hashtable hashtable = JsonSerializer.CreateClassTable(value, true, TYPE_ARRAY);
			JsonSerializer._AddToRefCache(aryType, value, hashtable);
			Array array = value as Array;
			Type elementType = aryType.GetElementType();
			hashtable[STR_CLS_TYPE_ID] = JsonSerializer.GetClassTypeId(elementType);
			ArrayList arrayList = hashtable[STR_CLS_DATA_VALUE] as ArrayList;
			for (int i = 0; i < array.Length; i++)
				arrayList.Add(JsonSerializer.SerializeObject(array.GetValue(i), context));

			return hashtable;
		}

		private static Hashtable _SerializeClass(object value, Type type, object context)
		{
			Hashtable hashtable = JsonSerializer.CreateClassTable(value, true, TYPE_CLASS);
			JsonSerializer._AddToRefCache(type, value, hashtable);
			ArrayList arrayList = hashtable[STR_CLS_DATA_VALUE] as ArrayList;
			if (value is ISerializable serializable)
				serializable.Serialize(new SerializationInfo(arrayList, context), context);
			else
			{
				FieldInfo[] fields = type.GetFields(BindingFlagsConst.Instance);
				if (fields.Length == 0)
					return hashtable;

				if (fields.Length != 0)
				{
					FieldInfo[] array = fields;
					for (int i = 0; i < array.Length; i++)
					{
						FieldInfo fieldInfo = array[i];

						if (Attribute.GetCustomAttribute(fieldInfo, typeof(SerializeAttribute)) != null)
						{
							object value2 = fieldInfo.GetValue(value);
							if (value2 != null)
							{
								object obj = JsonSerializer.SerializeObject(value2, context);
								if (obj != null)
								{
									Hashtable hashtable2 = new Hashtable();
									hashtable2[fieldInfo.Name] = obj;
									arrayList.Add(hashtable2);
								}
							}
						}
					}
				}
			}

			return hashtable;
		}

		private static Hashtable SerializeEnum(object value, Type enumType)
		{
			Hashtable hashtable = JsonSerializer.CreateClassTable(value, false, TYPE_ENUM);
			hashtable[STR_CLS_DATA_VALUE] = Enum.Format(enumType, value, "d");
			return hashtable;
		}

		private static Hashtable SerializeList(object value, Type collectionType, object context)
		{
			Hashtable hashtable = JsonSerializer.CreateClassTable(value, true, TYPE_LIST);
			JsonSerializer._AddToRefCache(collectionType, value, hashtable);
			IEnumerable iterator = value as IList;
			hashtable[STR_CLS_TYPE_ID] = JsonSerializer.GetClassTypeId(collectionType);
			ArrayList arrayList = hashtable[STR_CLS_DATA_VALUE] as ArrayList;
			foreach (object current in iterator)
				arrayList.Add(JsonSerializer.SerializeObject(current, context));

			return hashtable;
		}

		private static Hashtable SerializeDict(object value, Type dictType, object context)
		{
			Hashtable hashtable = JsonSerializer.CreateClassTable(value, true, TYPE_DICT);
			JsonSerializer._AddToRefCache(dictType, value, hashtable);
			IEnumerator iterator = (value as IDictionary).GetEnumerator();

			hashtable[STR_CLS_TYPE_ID] = JsonSerializer.GetClassTypeId(dictType);
			ArrayList dictList = hashtable[STR_CLS_DATA_VALUE] as ArrayList;
			while (iterator.MoveNext())
			{
				object current = iterator.Current;
				object dictKey = current.GetFieldValue("_key");
				object dictValue = current.GetFieldValue("_value");
				Hashtable dict = new Hashtable();
				dict["key"] = JsonSerializer.SerializeObject(dictKey, context);
				dict["value"] = JsonSerializer.SerializeObject(dictValue, context);
				dictList.Add(dict);
			}

			return hashtable;
		}


		private static Hashtable SerializeUnityStruct(object value, Type type, object context)
		{
			Hashtable hashtable = JsonSerializer.CreateClassTable(value, true, TYPE_CLASS);
			JsonSerializer._AddToRefCache(type, value, hashtable);
			ArrayList arrayList = hashtable[STR_CLS_DATA_VALUE] as ArrayList;
			FieldInfo[] fields = type.GetFields(InstanceFlags);
			if (fields.Length == 0)
				return hashtable;

			if (fields.Length != 0)
			{
				FieldInfo[] array = fields;
				for (int i = 0; i < array.Length; i++)
				{
					FieldInfo fieldInfo = array[i];
					object value2 = fieldInfo.GetValue(value);
					if (value2 != null)
					{
						object obj = JsonSerializer.SerializeObject(value2, context);
						if (obj != null)
						{
							Hashtable hashtable2 = new Hashtable();
							hashtable2[fieldInfo.Name] = obj;
							arrayList.Add(hashtable2);
						}
					}
				}
			}

			return hashtable;
		}

		private static JsonSerializer.SerializeObjInfo TryGetSerializedObject(Type type, object obj)
		{
			if (!JsonSerializer._serializeCache.TryGetValue(type, out var dictionary))
			{
				dictionary = new Dictionary<object, JsonSerializer.SerializeObjInfo>();
				JsonSerializer._serializeCache[type] = dictionary;
				return null;
			}

			if (dictionary.TryGetValue(obj, out var result))
				return result;

			return null;
		}

		#endregion

		#endregion


		private class SerializeObjInfo
		{
			public long id;

			public Hashtable classTable;
		}
	}
}

