using System.Collections.Generic;
using System.Linq;

namespace KNQTT.Application.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Check empty list object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listObject"></param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this List<T> listObject)
        {
            return listObject?.Any() ?? false;
        }
    }
}
