using System;
using System.Collections.Generic;

namespace KNQTT.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> SetValue<T>(this IEnumerable<T> items, Action<T>
         updateMethod)
        {
            foreach (T item in items)
            {
                updateMethod(item);
            }
            return items;
        }

        /// <summary>
        /// Lấy thông tin mở rộng
        /// </summary>
        /// <typeparam name="T">T is Value Data Type, String</typeparam>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static T GetValueEnsureNotNull<T>(this Dictionary<string, string> input, string key, T defaultVal)
        {
            try
            {
                if (input.TryGetValue(key, out string val))
                {
                    return (T)Convert.ChangeType(val, typeof(T));
                }
                return defaultVal;
            }
            catch
            {
                return defaultVal;
            }
        }
    }
}
