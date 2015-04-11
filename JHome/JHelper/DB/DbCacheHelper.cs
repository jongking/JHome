using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHelper.DB
{
    public static class DbCacheHelper
    {
        private static string _prefix = "DB_";

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool UpDateTableCache<T>()
        {
            var list = DbHelper.GetDataTableToList<T>(DbHelper.GetTable(DbHelper.GetTableFromClass<T>()));
            CacheHelper.SetCache(GetCacheKey<T>(), list);
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetCache<T>()
        {
            if (!CacheHelper.HasCache(GetCacheKey<T>()))
            {
                UpDateTableCache<T>();
            }
            return CacheHelper.GetCache<List<T>>(GetCacheKey<T>());
        }

        private static string GetCacheKey<T>()
        {
            return _prefix + DbHelper.GetTableFromClass<T>();
        }
    }
}
