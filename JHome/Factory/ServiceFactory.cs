using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Factory
{
    public class ServiceFactory
    {
        private const string AssemblyPath = "Infrastructure.Service";

        #region CreateObject

        //不使用缓存
        private static object CreateObject(string classNamespace, string assemblyName = "Infrastructure")
        {
            object objType = JHelper.CacheHelper.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(assemblyName).CreateInstance(classNamespace);
                    JHelper.CacheHelper.SetCache(classNamespace, objType);// 写入缓存
                }
                catch
                {
                    throw;
                }
            }
            return objType;
        }
        #endregion

        public static T CreateInstance<T>(string str)
        {
            string classNamespace = AssemblyPath + "." + str + "Service";
            object objType = CreateObject(classNamespace);
            return (T)objType;
        }
    }
}
