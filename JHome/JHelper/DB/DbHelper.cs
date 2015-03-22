using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace JHelper.DB
{
    public static class DbHelper
    {
        public static SqlDatabase GetDatabase(string name = "con")
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            SqlDatabase sqlServerDb = factory.Create(name) as SqlDatabase;
            return sqlServerDb;
        }

        public static int ExecuteNonQuery(string sql, string name = "con")
        {
            return GetDatabase(name).ExecuteNonQuery(CommandType.Text, sql);
        }

        public static object ExecuteScalar(string sql, string name = "con")
        {
            return GetDatabase(name).ExecuteScalar(CommandType.Text, sql);
        }

        public static DataSet ExecuteDataSet(string sql, string name = "con")
        {
            return GetDatabase(name).ExecuteDataSet(CommandType.Text, sql);
        }

//        public static IDataReader ExecuteReader(string sql, string name = "con")
//        {
        //            return GetDatabase(name).ExecuteReader(CommandType.Text, sql);
//        }

        public static DataTable ExecuteDataTable(string sql, string name = "con")
        {
            return GetDatabase(name).ExecuteDataSet(CommandType.Text, sql).Tables[0];
        }

        public static bool TestConnection(string name = "con")
        {
            DbConnection connect = null;
            try
            {
                connect = GetDatabase(name).CreateConnection();
                connect.Open();
            }
            catch
            {
                return false;
            }
            finally
            {
                if (connect != null) connect.Close();
            }
            return true;
        }
        public static T GetModel<T>(string sql, string name = "con")
        {
            T model = (T)Activator.CreateInstance(typeof(T));
            DataTable dt = ExecuteDataTable(sql, name);
            if (dt.Rows.Count > 0)
            {
                model = GetDataRowToModel<T>(dt.Rows[0], typeof(T));
            }
            return model;
        }
        public static IList<T> GetList<T>(string sql, string name = "con")
        {
            IList<T> list = new List<T>();
            DataTable dt = ExecuteDataTable(sql, name);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Type tm = typeof (T);
                var model = GetDataRowToModel<T>(dt.Rows[i], tm);
                list.Add(model);
            }
            return list;
        }
        public static T GetDataRowToModel<T>(DataRow dr, Type tm)
        {
            T model = (T) Activator.CreateInstance(tm);
            PropertyInfo[] pis = tm.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (pi.CanWrite)
                {
                    string propertyName = pi.Name;
                    if (dr.RowState != DataRowState.Detached && !dr.Table.Columns.Contains(propertyName))
                    {
                        continue;
                    }
                    if (null != dr[propertyName] && !dr[propertyName].Equals(DBNull.Value))
                    {
                        if (pi.PropertyType.IsGenericType)
                        {
                            if (pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                pi.SetValue(model, dr[propertyName], null);
                            }
                            else
                            {
                                pi.SetValue(model, Convert.ChangeType(dr[propertyName].ToString(), pi.PropertyType),
                                    null);
                            }
                        }
                        else
                        {
                            pi.SetValue(model, Convert.ChangeType(dr[propertyName].ToString(), pi.PropertyType), null);
                        }
                    }
                }
            }
            return model;
        }

        public static object InsertModel<T>(T model, string tableName)
        {
            var ssc = SimpleSqlCreater.Insert(tableName);
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (var propertyInfo in pis)
            {
                if (propertyInfo.CanWrite)
                {
                    if(propertyInfo.Name == "Id") continue;

                    string propertyName = propertyInfo.Name;
                    ssc.AddParam(propertyName, "'" + propertyInfo.GetValue(model, null) + "'");
                }
            }
            return ExecuteScalar(ssc.ToString());
        }
        
        public static object InsertModel<T>(T model)
        {
            return InsertModel(model, GetTableFromClass<T>());
        }

        public static int UpdateModel<T>(T model, string tableName)
        {
            var ssc = SimpleSqlCreater.Update(tableName);
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (var propertyInfo in pis)
            {
                if (propertyInfo.CanWrite)
                {
                    if (propertyInfo.Name == "Id")
                    {
                        ssc.Eq("Id", propertyInfo.GetValue(model, null).ToString());
                        continue;
                    }

                    string propertyName = propertyInfo.Name;
                    ssc.AddParam(propertyName, "'" + propertyInfo.GetValue(model, null) + "'");
                }
            }
            return ExecuteNonQuery(ssc.ToString());
        }
        
        public static int UpdateModel<T>(T model)
        {
            return UpdateModel(model, GetTableFromClass<T>());
        }

        public static int UpdateModel<T>(T model, string tableName, string keyFieldName, string keyValue, string separator = "'")
        {
            var ssc = SimpleSqlCreater.Update(tableName);
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (var propertyInfo in pis)
            {
                if (propertyInfo.CanWrite)
                {
                    if (propertyInfo.Name == "Id") continue;

                    string propertyName = propertyInfo.Name;
                    ssc.AddParam(propertyName, "'" + propertyInfo.GetValue(model, null) + "'");
                }
            }
            ssc.Eq(keyFieldName, keyValue, separator);
            return ExecuteNonQuery(ssc.ToString());
        }

        public static int Remove<T>(T model, string tableName)
        {
            var ssc = SimpleSqlCreater.Update(tableName);
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (var propertyInfo in pis)
            {
                if (propertyInfo.CanWrite)
                {
                    ssc.Eq(propertyInfo.Name, propertyInfo.GetValue(model, null).ToString());
                }
            }
            return ExecuteNonQuery(ssc.ToString());
        }

        public static int Remove<T>(T model)
        {
            return Remove(model, GetTableFromClass<T>());
        }

        public static string GetTableFromClass<T>()
        {
            return typeof(T).Name + "T";
        }
    }
}