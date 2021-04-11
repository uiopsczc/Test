using System;
using System.Reflection;

namespace CsCat
{
  public class AssemblyUtil
  {
    public static Assembly GetAssembly(string assembly_name)
    {
      foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        if (assembly.GetName().Name.Equals(assembly_name))
          return assembly;
      return null;
    }
  }
}