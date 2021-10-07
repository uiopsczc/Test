using System.Collections.Generic;
using System.Reflection.Emit;

namespace CsCat
{
  public partial class ModuleBuilderUtil
  {
    private static Dictionary<AssemblyBuilder, ModuleBuilder> _moduleBuilderDict =
      new Dictionary<AssemblyBuilder, ModuleBuilder>();

    public static ModuleBuilder GetModuleBuilder(AssemblyBuilder assemblyBuilder = null)
    {
      if (assemblyBuilder == null)
        assemblyBuilder = AssemblyBuilderUtil.GetAssemblyBuilder();
        if (!_moduleBuilderDict.ContainsKey(assemblyBuilder))
            _moduleBuilderDict[assemblyBuilder] = assemblyBuilder.DefineDynamicModule(assemblyBuilder.GetName().Name);

        return _moduleBuilderDict[assemblyBuilder];
    }


  }
}
