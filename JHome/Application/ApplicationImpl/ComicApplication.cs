using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;
using Application.IApplication;
using Domain.Model.Comic;

namespace Application.ApplicationImpl
{
    public class ComicApplication : IComicApplication
    {
        public bool AddComic(string name, string titlename, string type = "", string auth = "未知", string des = "", string coverImg = "", string orginCoverImg = "", int state = 0)
        {
            if (Comic.HasComic(name))
            {
                return false;
            }
            var comic = new Comic()
            {
                ComicName = name,
                TitleName = titlename,
                ComicType = type,
                ComicAuthor = auth,
                Description = des,
                CoverImgPath = coverImg,
                OrginCoverImgPath = orginCoverImg,
                ComicState = state
            };
            return comic.Add();
        }

        public bool DownLoadOverImage(string comicname, string currentPage = "", string host = "images.dmzj.com")
        {
            return true;
        }

        public List<ComicDto> GetAll()
        {
            return ComicDto.GetAll();
        }

        public List<ComicVolumeDto> GetVolumeById(int id)
        {
            return ComicDto.GetVolumeById(id);
        }

        public List<ComicPageDto> GetPagesByVolId(int volid)
        {
            return ComicDto.GetPagesByVolId(volid);
        }

        public ComicDto GetById(int id)
        {
            return ComicDto.GetById(id);
        }
    }
}
