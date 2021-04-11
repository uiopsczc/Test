using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CsCat
{
  public static class AssemblyExtension
  {
    /// <summary>
    /// 获得NameSpace下的所有类,例如：Assembly.GetExecutingAssembly().GetClassesOfNameSpace("cat.io");
    /// </summary>
    public static Type[] GetTypesOfNameSpace(this Assembly assembly, string target_namespace)
    {
      Type[] types = assembly.GetTypes().Where(t => t.Namespace == target_namespace).ToArray();
      return types;
    }





    public static MemberInfo[] GetCustomAttributeMemberInfos<T>(this Assembly assembly)
    {
      List<MemberInfo> result = new List<MemberInfo>();
      foreach (var type in assembly.GetTypes())
      {
        foreach (MemberInfo memberInfo in type.GetMembers(BindingFlagsConst.All))
        {
          if (memberInfo.GetCustomAttribute<T>() != null)
            result.AddUnique(memberInfo);
        }
      }

      return result.ToArray();
    }


  }
}