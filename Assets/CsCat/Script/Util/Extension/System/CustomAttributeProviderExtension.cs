using System.Reflection;

namespace CsCat
{
  public static class CustomAttributeProviderExtension
  {

    public static T GetCustomAttribute<T>(this ICustomAttributeProvider self, int index = 0,
      bool is_contain_inherit = false)
    {
      object[] customAttributes = self.GetCustomAttributes(typeof(T), is_contain_inherit);
      if (customAttributes != null && customAttributes.Length > index)
        return (T) customAttributes[index];
      return default(T);
    }

  }
}