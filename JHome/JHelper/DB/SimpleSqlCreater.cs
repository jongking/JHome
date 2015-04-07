using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace JHelper.DB
{
    public class SimpleSqlCreater
    {
        private readonly string _table;
        private string _where;
        private readonly OperatorType _operatorType;
        private string _linker;
        private string _orderby;
        private int _limit;
        private Dictionary<string, string> _kyDictionary = new Dictionary<string, string>(); 

        private SimpleSqlCreater(string table, OperatorType operatorType)
        {
            _table = table;
            _operatorType = operatorType;
            _where = "";
            _limit = 0;
            _orderby = "";
            _linker = " AND ";
        }

        public static SimpleSqlCreater Select(string table)
        {
            return new SimpleSqlCreater(table, OperatorType.Select);
        }
        public static SimpleSqlCreater Insert(string table)
        {
            return new SimpleSqlCreater(table, OperatorType.Insert);
        }
        public static SimpleSqlCreater Update(string table)
        {
            return new SimpleSqlCreater(table, OperatorType.Update);
        }
        public static SimpleSqlCreater Delete(string table)
        {
            return new SimpleSqlCreater(table, OperatorType.Delete);
        }
        public static SimpleSqlCreater Select<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();
            return new SimpleSqlCreater(table, OperatorType.Select);
        }
        public static SimpleSqlCreater Insert<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();
            return new SimpleSqlCreater(table, OperatorType.Insert);
        }
        public static SimpleSqlCreater Update<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();

            return new SimpleSqlCreater(table, OperatorType.Update);
        }
        public static SimpleSqlCreater Delete<T>()
        {
            var table = DbHelper.GetTableFromClass<T>();
            return new SimpleSqlCreater(table, OperatorType.Delete);
        }
        public SimpleSqlCreater Eq(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} = {2}{1}{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater BigEq(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} >= {2}{1}{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater SmlEq(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} <= {2}{1}{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater Big(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} > {2}{1}{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater Sml(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} < {2}{1}{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater Like(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} LIKE {2}%{1}%{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater NotLike(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} NOT LIKE {2}%{1}%{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater LLike(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} LIKE {2}%{1}{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater RLike(string filedName, string param, string separator = "'")
        {
            _where += _linker + string.Format(" {0} LIKE {2}{1}%{2} ", filedName, param, separator);
            return this;
        }
        public SimpleSqlCreater In(string filedName, string param)
        {
            _where += _linker + string.Format(" {0} IN ({1}) ", filedName, param);
            return this;
        }
        public SimpleSqlCreater NotIn(string filedName, string param)
        {
            _where += _linker + string.Format(" {0} NOT IN ({1}) ", filedName, param);
            return this;
        }
        public SimpleSqlCreater And()
        {
            _linker = " AND ";
            return this;
        }
        public SimpleSqlCreater Or()
        {
            _linker = " OR ";
            return this;
        }

        public SimpleSqlCreater Limit(int num)
        {
            _limit = num;
            return this;
        }
        public SimpleSqlCreater OrderBy(string fildName, OrderByType orderBy)
        {
            _orderby = string.Format(orderBy == OrderByType.Asc ? " {0} ASC " : " {0} DESC ", fildName);
            return this;
        }

        public SimpleSqlCreater AddParam(string filedName, string param)
        {
            _kyDictionary.Add(filedName, param);
            return this;
        }
        public override string ToString()
        {
            if (_operatorType == OperatorType.Select)
            {
                string limit = "";
                string orderby = "";
                if (_limit != 0)
                {
                    limit = string.Format(" TOP {0}", _limit);
                }
                if (_orderby != "")
                {
                    orderby = string.Format(" ORDER BY {0} ", _orderby);
                }
                return string.Format("SELECT {2} * FROM {0} WHERE 1=1 {1} {3}", _table, _where, limit, orderby);
            }
            if (_operatorType == OperatorType.Insert)
            {
                string fileNames = "";
                string pars = "";
                foreach (var v in _kyDictionary)
                {
                    fileNames += v.Key + ",";
                    pars += v.Value + ",";
                }
                if (fileNames.Length > 0) fileNames = fileNames.Substring(0, fileNames.Length - 1);
                if (pars.Length > 0) pars = pars.Substring(0, pars.Length - 1);

                return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", _table, fileNames, pars);
            }
            if (_operatorType == OperatorType.Update)
            {
                string fileNames = "";
                foreach (var v in _kyDictionary)
                {
                    fileNames += v.Key + " = " + v.Value + " ,";
                }
                if (fileNames.Length > 0) fileNames = fileNames.Substring(0, fileNames.Length - 1);

                return string.Format("UPDATE {0} SET {2} WHERE 1=1 {1}", _table, _where, fileNames);
            }
            if (_operatorType == OperatorType.Delete)
            {
                return string.Format("DELETE FROM {0} WHERE 1=1 {1}", _table, _where);
            }
            return "ERROR";
        }

        public enum OperatorType
        {
            Select,
            Insert,
            Update,
            Delete
        }

        public enum OrderByType
        {
            Desc,
            Asc
        }
    }
}
