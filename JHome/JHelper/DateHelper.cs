using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHelper
{
    public static class DateHelper
    {
        public static string DateFormat(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
