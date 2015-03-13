using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Factory
{
    public sealed class RepositoryFactory
    {
        private const string AssemblyPath = "Infrastructure.Repository";

        #region CreateObject 

		//不使用缓存
        private static object CreateObjectNoCache(string classNamespace)
		{		
			try
			{
                var objType = Activator.CreateInstance(Type.GetType(classNamespace));
                return objType;
			}
			catch
			{
				return null;
			}			
        }
        #endregion

        public static T CreateInstance<T>(string str)
        {
            string classNamespace = AssemblyPath + "." + str + "Repository";
            //Make.BLL.Ebo.ProductSortEbo
            object objType = CreateObjectNoCache(classNamespace);
            return (T)objType;
        }
    }
}
