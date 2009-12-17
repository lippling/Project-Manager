using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ProjectManager
{
    public static class ICustomAttributeProviderExtension
    {
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static T GetAttribute<T>(this ICustomAttributeProvider provider)
        {
            return provider.GetAttribute<T>(false);
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static T GetAttribute<T>(this ICustomAttributeProvider provider, bool includeInheritedAttribute)
        {
            var attributes = provider.GetCustomAttributes(typeof(T), includeInheritedAttribute);
            if (attributes.Length > 0)
                return ((T)attributes[0]);
            return default(T);
        }
    }
}
