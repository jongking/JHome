using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace JHelper.DB
{
    public class SimpleDbCommandCreater
    {
        private readonly string _table;
        private string _where;
        private readonly CmOperatorType _operatorType;
        private string _linker;
        private Dictionary<string, object> _kyDictionary = new Dictionary<string, object>();

        
        private SimpleDbCommandCreater(string table, CmOperatorType operatorType)
        {
            _table = table;
            _operatorType = operatorType;
            _where = "";
            _linker = " AND ";
        }

        public static SimpleDbCommandCreater Select(string table)
        {
            return new SimpleDbCommandCreater(table, CmOperatorType.Select);
        }
        public static SimpleDbCommandCreater Insert(string table)
        {
            return new SimpleDbCommandCreater(table, CmOperatorType.Insert);
        }
        public static SimpleDbCommandCreater Update(string table)
        {
            return new SimpleDbCommandCreater(table, CmOperatorType.Update);
        }
        public static SimpleDbCommandCreater Delete(string table)
        {
            return new SimpleDbCommandCreater(table, CmOperatorType.Delete);
        }
        public static SimpleDbCommandCreater Select<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();
            return new SimpleDbCommandCreater(table, CmOperatorType.Select);
        }
        public static SimpleDbCommandCreater Insert<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();
            return new SimpleDbCommandCreater(table, CmOperatorType.Insert);
        }
        public static SimpleDbCommandCreater Update<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();

            return new SimpleDbCommandCreater(table, CmOperatorType.Update);
        }
        public static SimpleDbCommandCreater Delete<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();
            return new SimpleDbCommandCreater(table, CmOperatorType.Delete);
        }
        public SimpleDbCommandCreater Eq(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} = @{1} ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater BigEq(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} >= @{1} ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater SmlEq(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} <= @{1} ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater Big(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} > @{1} ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater Sml(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} < @{1} ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater Like(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} LIKE ('%' + @{1} + '%') ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater NotLike(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} NOT LIKE ('%' + @{1} + '%') ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater LLike(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} LIKE ('%' + @{1}) ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater RLike(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} LIKE (@{1} + '%') ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater In(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} IN (@{1}) ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater NotIn(string filedName, object param)
        {
            _where += _linker + string.Format(" {0} NOT IN (@{1}) ", filedName, filedName);
            AddParam(filedName, param);
            return this;
        }
        public SimpleDbCommandCreater And()
        {
            _linker = " AND ";
            return this;
        }
        public SimpleDbCommandCreater Or()
        {
            _linker = " OR ";
            return this;
        }
        public SimpleDbCommandCreater AddParam(string filedName, object param)
        {
            _kyDictionary.Add(filedName, param);
            return this;
        }
        public DbCommand ToDbCommand()
        {
            var db = DbHelper.GetDatabase();
            if (_operatorType == CmOperatorType.Select)
            {
                var sql = string.Format("SELECT * FROM {0} WHERE 1=1 {1}", _table, _where);
                var cmd = db.GetSqlStringCommand(sql);
                foreach (var v in _kyDictionary)
                {
                    db.AddInParameter(cmd, "@" + v.Key, DbType.String, v.Value);
                }
                return cmd;
            }
            if (_operatorType == CmOperatorType.Insert)
            {
                string fileNames = "";
                string pars = "";
                foreach (var v in _kyDictionary)
                {
                    fileNames += v.Key + ",";
                    pars += "@" + v.Key + ",";
                }
                if (fileNames.Length > 0) fileNames = fileNames.Substring(0, fileNames.Length - 1);
                if (pars.Length > 0) pars = pars.Substring(0, pars.Length - 1);
                var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", _table, fileNames, pars);
                var cmd = db.GetSqlStringCommand(sql);
                foreach (var v in _kyDictionary)
                {
                    db.AddInParameter(cmd, "@" + v.Key, DbType.String, v.Value);
                }
                return cmd;
            }
            if (_operatorType == CmOperatorType.Update)
            {
                string fileNames = "";
                foreach (var v in _kyDictionary)
                {
                    fileNames += v.Key + " = @" + v.Key + " ,";
                }
                if (fileNames.Length > 0) fileNames = fileNames.Substring(0, fileNames.Length - 1);
                var sql = string.Format("UPDATE {0} SET {2} WHERE 1=1 {1}", _table, _where, fileNames);
                var cmd = db.GetSqlStringCommand(sql);
                foreach (var v in _kyDictionary)
                {
                    db.AddInParameter(cmd, "@" + v.Key, DbType.String, v.Value);
                }
                return cmd;
            }
            if (_operatorType == CmOperatorType.Delete)
            {
                var sql = string.Format("DELETE FROM {0} WHERE 1=1 {1}", _table, _where);
                var cmd = db.GetSqlStringCommand(sql);
                foreach (var v in _kyDictionary)
                {
                    db.AddInParameter(cmd, "@" + v.Key, DbType.String, v.Value);
                }
                return cmd;
            }
            return null;
        }
        public enum CmOperatorType
        {
            Select,
            Insert,
            Update,
            Delete
        }
    }
}
