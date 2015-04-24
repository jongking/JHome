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
        List<ComicVolumeDto> GetVolumeById(int comicid);
        List<ComicPageDto> GetPagesByVolId(int volid);
        ComicDto GetById(int id);
    }
}
