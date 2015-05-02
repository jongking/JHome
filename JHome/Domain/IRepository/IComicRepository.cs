using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model.Comic;

namespace Domain.IRepository
{
    public interface IComicRepository
    {
        void Add(Comic comic);
        Comic GetByComicName(string comicName);
    }
}
