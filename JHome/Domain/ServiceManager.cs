using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using Domain.IService;
using Factory;

namespace Domain
{
    public static class ServiceManager
    {
        public readonly static IGetImgService GetImgService = ServiceFactory.CreateInstance<IGetImgService>("GetImg");

    }
}
