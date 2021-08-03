using System;
using System.Text;
using Newtonsoft.Json;

namespace Info.Initializations
{
    public static class JsonHelper
    {
        /// <summary>
        ///     Serializes a generic cache value to a string cache value
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="value">Cache Value</param>
        /// <returns>String cache value</returns>
        public static byte[] SerializeObject<T>(T value)
        {
            string cacheValue;
            if (value is string)
                cacheValue = value.ToString();
            else
                cacheValue = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(cacheValue);
        }

        /// <summary>
        ///     Deserializes a cache value to (T) type cache value
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="cacheValue">Byte Array Cache Value</param>
        /// <returns>(T) type cache value</returns>
        public static T DeserializeObject<T>(byte[] cacheValue)
        {
            if (cacheValue == null)
                return default;
            var str = Encoding.UTF8.GetString(cacheValue);
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(str, typeof(T));
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
