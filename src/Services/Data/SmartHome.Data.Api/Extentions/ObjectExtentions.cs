using System;
using System.Linq;

namespace SmartHome.Data.Api.Extentions
{
    /// <summary>
    /// Determines Object Extentions.
    /// </summary>
    public static class ObjectExtentions
    {
        /// <summary>
        /// Adds items to objects and return new object array.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static object[] ObjectAdd(this object[] objects, params object[] items)
        {
            if (objects.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(objects));
            }

            if (items.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(items));
            }

            var list = objects.ToList();
            list.AddRange(items);

            return list.ToArray();
        }

        /// <summary>
        /// Returns true when items is Null or Empty.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object[] items) =>
            items == null || items.Length == 0;
    }
}
