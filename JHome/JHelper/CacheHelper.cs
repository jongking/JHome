using System.Collections;
using System.Collections.Generic;

namespace JHelper
{
    public static class CacheHelper
    {
        public static readonly Hashtable HashCache = new Hashtable();
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            var objCache = HashCache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static bool HasCache(string cacheKey)
        {
            var objCache = HashCache;
            return objCache[cacheKey] != null;
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            var objCache = HashCache;
            objCache.Add(cacheKey, objObject);
        }
    }
}
