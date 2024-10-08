using Infrastructure.Core.CoreOptions;
using Infrastructure.Core.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Infrastructure.Core.Utilities
{
    /// <summary>
    /// Read json file
    /// </summary>
    public static partial class CoreUtility
    {
        private static readonly string _pathFile = ProjectConfig.Options.MasterFilePath;

        /// <summary>
        /// Đọc data từ file json theo key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>T or default(T) if exception</returns>
        public static T ReadDataByKey<T>(string key)
        {
            try
            {
                using (StreamReader streamReader = File.OpenText(Path.Combine(Environment.CurrentDirectory, _pathFile)))
                using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                {
                    return ((JObject)JToken.ReadFrom(jsonReader)).SelectToken(key).ToObject<T>();
                }
            }
            catch (Exception ex)
            {
                throw new CoreException(ex.ToString());
            }
        }

        /// <summary>
        /// Đọc data từ file json theo key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <returns>T or default(T) if exception</returns>
        public static T ReadDataByKey<T>(string filePath, string key)
        {
            try
            {
                using (StreamReader streamReader = File.OpenText(Path.Combine(Environment.CurrentDirectory, filePath)))
                using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                {
                    return ((JObject)JToken.ReadFrom(jsonReader)).SelectToken(key).ToObject<T>();
                }
            }
            catch (Exception ex)
            {
                throw new CoreException(ex.ToString());
            }
        }
    }
}
