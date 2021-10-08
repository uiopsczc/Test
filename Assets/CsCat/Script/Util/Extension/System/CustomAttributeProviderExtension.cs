using System.Reflection;

namespace CsCat
{
    public static class CustomAttributeProviderExtension
    {
        public static T GetCustomAttribute<T>(this ICustomAttributeProvider self, int index = 0,
            bool isContainInherit = false)
        {
            var customAttributes = self.GetCustomAttributes(typeof(T), isContainInherit);
            return customAttributes.Length > index ? (T) customAttributes[index] : default;
        }
    }
}