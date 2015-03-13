using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Exception
{
    public enum ExceptionType
    {
        普通,
        数据库,
        领域模型自检,
    }

    public class JException : System.Exception
    {
        public ExceptionType ExceptionType;

        public JException(string errMsg, ExceptionType exceptionType)
            : base(errMsg)
        {
            ExceptionType = exceptionType;
        }
    }
}
