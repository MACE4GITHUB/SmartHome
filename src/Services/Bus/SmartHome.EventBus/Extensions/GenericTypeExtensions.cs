using System;
using System.Linq;

namespace SmartHome.EventBus.Extensions
{
    public static class GenericTypeExtensions
    {
        public static string GetNormalizeTypeName(this Type type) =>
            type.IsGenericType
                ? $"{type.Name.Remove(type.Name.IndexOf('`'))}<{string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray())}>"
                : type.Name;

        public static string GetNormalizeTypeName(this object @object) =>
            @object.GetType().GetNormalizeTypeName();
    }
}
