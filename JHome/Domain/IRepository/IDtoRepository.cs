using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.IRepository
{
    public interface IDtoRepository
    {
        IList<T> GetList<T>(string msg);
        T GetModel<T>(string msg);
    }
}
