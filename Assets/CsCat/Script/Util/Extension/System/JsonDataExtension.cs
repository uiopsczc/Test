using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using NPOI.SS.Formula.Functions;
using UnityEngine;

namespace CsCat
{
    public static class JsonDataExtension
    {
        public static T To<T>(this JsonData self)
        {
            var type = typeof(T);
            if (type.IsArray)
                return self.InvokeExtensionGenericMethod<T>(StringConst.String_ToArray, new[] {type.GetElementType()});
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                Type[] dictTypes = type.GetGenericArguments();
                Type keyType = dictTypes[0];
                Type valueType = dictTypes[1];
                return self.InvokeExtensionGenericMethod<T>(StringConst.String_ToDict, new[] {keyType, valueType});
            }

            var t = self.ToString();
            return t.To<T>();
        }

        public static Dictionary<K, V> ToDict<K, V>(this JsonData self)
        {
            if (self == null)
                return null;
            Dictionary<K, V> result = new Dictionary<K, V>();
            foreach (var key in self.Keys)
                result[key.To<K>()] = self[key].To<V>();
            return result;
        }

        public static List<T> ToList<T>(this JsonData self)
        {
            if (self == null)
                return null;
            var result = new List<T>();
            foreach (var value in self)
                result.Add(value.To<T>());
            return result;
        }


        public static T[] ToArray<T>(this JsonData self)
        {
            if (self == null)
                return null;
            T[] result = new T[self.Count];
            for (int i = 0; i < self.Count; i++)
                result[i] = self[i].To<T>();
            return result;
        }

        public static string ToJsonWithUTF8(this JsonData self, bool isPrettyPrint = true)
        {
            JsonWriter jsonWriter = new JsonWriter {PrettyPrint = isPrettyPrint};
            self.ToJson(jsonWriter);
            string result = jsonWriter.TextWriter.ToString();
            result = Regex.Unescape(result);
            return result;
        }
    }
}