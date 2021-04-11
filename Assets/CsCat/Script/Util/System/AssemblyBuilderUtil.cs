using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace CsCat
{
  public class AssemblyBuilderUtil
  {
    private static readonly Dictionary<ObjectInfos, AssemblyBuilder> assemblyBuilder_dict =
      new Dictionary<ObjectInfos, AssemblyBuilder>();

    public static AssemblyBuilder GetAssemblyBuilder(string assembly_name = null,
      AssemblyBuilderAccess assemblyBuilderAccess = AssemblyBuilderAccess.Run)
    {
      var infos = new ObjectInfos(assembly_name, assemblyBuilderAccess);
      if (!assemblyBuilder_dict.ContainsKey(infos))
      {
        string assembly_name_string;
        if (assembly_name == null)
          assembly_name_string = Guid.NewGuid().ToString().Replace("-", "");
        else
          assembly_name_string = assembly_name;
        var assemblyName = new AssemblyName(assembly_name_string);
        assemblyBuilder_dict[infos] =
          AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, assemblyBuilderAccess);
      }

      return assemblyBuilder_dict[infos];
    }
  }
}