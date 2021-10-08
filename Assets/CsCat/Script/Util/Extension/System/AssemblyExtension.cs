using System;
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// 获得NameSpace下的所有类,例如：Assembly.GetExecutingAssembly().GetClassesOfNameSpace("cat.io");
        /// </summary>
        public static Type[] GetTypesOfNameSpace(this Assembly assembly, string targetNamespace)
        {
            List<Type> typeList = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (targetNamespace.Equals(type.Namespace))
                    typeList.Add(type);
            }

            return typeList.ToArray();
        }


        public static MemberInfo[] GetCustomAttributeMemberInfos<T>(this Assembly assembly)
        {
            List<MemberInfo> result = new List<MemberInfo>();
            foreach (var type in assembly.GetTypes())
            {
                foreach (var memberInfo in type.GetMembers(BindingFlagsConst.All))
                {
                    if (memberInfo.GetCustomAttribute<T>() == null) continue;
                    result.AddUnique(memberInfo);
                }
            }

            return result.ToArray();
        }
    }
}