
using System;
using System.Reflection;

namespace CsCat
{
  public partial class TypeUtil
  {
    public static Type GetType(string class_path, string dll_name = null)
    {
      // return Type.GetType(string.Format("{0}, {1}, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", className, dllName));
      if (dll_name == null && cache_dict.ContainsKey(class_path))
        return cache_dict[class_path];

      string _dll_name = dll_name;
      if (dll_name == null)
        _dll_name = Assembly.GetExecutingAssembly().FullName;
      string type_string = string.Format("{0},{1}", class_path, _dll_name);
      if (cache_dict.ContainsKey(type_string))
        return cache_dict[type_string];

      Type result = Type.GetType(type_string);
      if (dll_name == null)//查找所有assembly中的类
      {
        Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in Assemblies)
        {
          result = assembly.GetType(class_path);
          if (result != null)
          {
            cache_dict[string.Format("{0},{1}", class_path, assembly.FullName)] = result;
            break;
          }
        }
      }
      cache_dict[string.Format("{0},{1}", class_path, dll_name ?? "")] = result;
      return result;
    }

  }
}