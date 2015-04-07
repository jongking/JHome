using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.IRepository;
using Factory;

namespace Application.Dto
{
    public class BaseDto
    {
        internal readonly static IDtoRepository DtoRepository = RepositoryFactory.CreateInstance<IDtoRepository>("Dto");
    }
}
