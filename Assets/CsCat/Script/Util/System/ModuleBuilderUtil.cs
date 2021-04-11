using System.Collections.Generic;
using System.Reflection.Emit;

namespace CsCat
{
  public partial class ModuleBuilderUtil
  {
    private static Dictionary<AssemblyBuilder, ModuleBuilder> moduleBuilder_dict =
      new Dictionary<AssemblyBuilder, ModuleBuilder>();

    public static ModuleBuilder GetModuleBuilder(AssemblyBuilder assemblyBuilder = null)
    {
      if (assemblyBuilder == null)
        assemblyBuilder = AssemblyBuilderUtil.GetAssemblyBuilder();
      if (!moduleBuilder_dict.ContainsKey(assemblyBuilder))
      {
        moduleBuilder_dict[assemblyBuilder] = assemblyBuilder.DefineDynamicModule(assemblyBuilder.GetName().Name);
      }

      return moduleBuilder_dict[assemblyBuilder];
    }


  }
}
