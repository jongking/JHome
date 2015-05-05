using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;

namespace Application.IApplication
{
    public interface IComicApplication
    {
        bool AddComic(string name, string titlename, string type = "", string auth = "未知", string des = "", string orginCoverImg = "", string detailUrl = "", string otherMessage = "", int state = 0);
        bool AddComicVolume(int comicid, string volumeName, string volDetailUrl, int sortno);
        bool AddComicPage(int comicid, int volumeId, string pageImgPath, int pageNumber);
        bool DownLoadOverImage(string comicname, string serImgpath, string currentPage = "", string host = "images.dmzj.com");
        int UpdateComic(int comicid, string titleName, string description);
        List<ComicDto> GetAll();
        List<ComicVolumeDto> GetVolumeById(int comicid);
        List<ComicPageDto> GetPagesByVolId(int volid);
        ComicDto GetById(int id);
        ComicDto GetByName(string comicName);
    }
}
