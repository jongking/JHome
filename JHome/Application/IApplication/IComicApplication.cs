using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;

namespace Application.IApplication
{
    public interface IComicApplication
    {
        List<ComicDto> GetAll();
    }
}
