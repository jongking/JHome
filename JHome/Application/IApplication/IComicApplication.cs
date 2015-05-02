using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;

namespace Application.IApplication
{
    public interface IComicApplication
    {
        bool AddComic(string name, string titlename, string type = "", string auth = "未知", string des = "", string coverImg = "", string orginCoverImg = "", int state = 0);
        bool DownLoadOverImage(string comicname, string currentPage = "", string host = "images.dmzj.com");
        List<ComicDto> GetAll();
        List<ComicVolumeDto> GetVolumeById(int comicid);
        List<ComicPageDto> GetPagesByVolId(int volid);
        ComicDto GetById(int id);
    }
}
