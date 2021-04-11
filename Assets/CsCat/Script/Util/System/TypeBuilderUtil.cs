using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CsCat
{
  public class TypeBuilderUtil
  {
    //TypeBuilder.CreateType即可生成type
    public static TypeBuilder GetTypeBuilder(string type_name = null,
      TypeAttributes typeAttributes = TypeAttributes.Public | TypeAttributes.Class, Type parent_type = null,
      Type[] interface_types = null, ModuleBuilder moduleBuilder = null)
    {
      if (moduleBuilder == null)
        moduleBuilder = ModuleBuilderUtil.GetModuleBuilder();
      if (type_name == null)
        type_name = Guid.NewGuid().ToString().Replace("-", "");
      return moduleBuilder.DefineType(type_name, typeAttributes, parent_type, interface_types);
    }
  }
}