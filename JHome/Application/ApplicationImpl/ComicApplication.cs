using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;
using Application.IApplication;

namespace Application.ApplicationImpl
{
    public class ComicApplication : IComicApplication
    {
        public List<ComicDto> GetAll()
        {
            return ComicDto.GetAll();
        }

        public List<ComicVolumeDto> GetVolumeById(int id)
        {
            return ComicDto.GetVolumeById(id);
        }

        public ComicDto GetById(int id)
        {
            return ComicDto.GetById(id);
        }
    }
}
