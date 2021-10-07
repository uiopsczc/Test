using System;
using System.Reflection;

namespace CsCat
{
    public partial class TypeUtil
    {
        public static Type GetType(string classPath, string dllName = null)
        {
            // return Type.GetType(string.Format("{0}, {1}, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", className, dllName));
            if (dllName == null && GetCacheDict().ContainsKey(classPath))
                return GetCacheDict()[classPath];

            string dllName1 = dllName;
            if (dllName == null)
                dllName1 = Assembly.GetExecutingAssembly().FullName;
            string typeString = StringUtilCat.LinkStringWithCommon(classPath, dllName1);
            if (GetCacheDict().ContainsKey(typeString))
                return GetCacheDict()[typeString];

            Type result = Type.GetType(typeString);
            if (dllName == null) //查找所有assembly中的类
            {
                Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in Assemblies)
                {
                    result = assembly.GetType(classPath);
                    if (result == null) continue;
                    GetCacheDict()[StringUtilCat.LinkStringWithCommon(classPath, assembly.FullName)] = result;
                    break;
                }
            }

            GetCacheDict()[StringUtilCat.LinkStringWithCommon(classPath, dllName ?? StringConst.String_Empty)] = result;
            return result;
        }
    }
}