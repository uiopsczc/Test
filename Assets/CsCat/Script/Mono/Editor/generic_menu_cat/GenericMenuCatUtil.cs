
using System.Collections.Generic;
using System.Reflection;

namespace CsCat
{
  public class GenericMenuCatUtil
  {
    /// <summary>
    /// 所有GenericMenuCat菜单放在dict中
    /// </summary>
    public static Dictionary<string, GenericMenuCat> dict = new Dictionary<string, GenericMenuCat>();

    /// <summary>
    /// 加载所有GenericMenuCat菜单到dict中
    /// </summary>
    public static void Load()
    {
      dict.Clear();
      Assembly assembly = Assembly.GetExecutingAssembly();
      foreach (MemberInfo memberInfo in assembly.GetCustomAttributeMemberInfos<GenericMenuItemAttribute>())
      {
        GenericMenuItemAttribute genericMenuItemAttribute = memberInfo.GetCustomAttribute<GenericMenuItemAttribute>();
        GenericMenuCat genericMenuCat =
          dict.GetOrAddDefault(genericMenuItemAttribute.rootName, () => new GenericMenuCat());
        genericMenuCat.InitOrUpdateRoot(genericMenuItemAttribute, (MethodInfo) memberInfo);
      }
    }

  }
}