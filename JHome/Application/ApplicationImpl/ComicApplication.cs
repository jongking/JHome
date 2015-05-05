using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Application.Dto;
using Application.IApplication;
using Domain;
using Domain.Model.Comic;
using JHelper.WebCrawler;

namespace Application.ApplicationImpl
{
    public class ComicApplication : IComicApplication
    {
        public bool AddComic(string name, string titlename, string type = "", string auth = "未知", string des = "", string orginCoverImg = "", string detailUrl = "", string otherMessage = "", int state = 0)
        {
            if (Comic.HasComic(name))
            {
                return false;
            }
            var coverImg = "cover" + orginCoverImg.Substring(orginCoverImg.LastIndexOf("."));
            var comic = new Comic()
            {
                ComicName = name,
                TitleName = titlename,
                ComicType = type,
                ComicAuthor = auth,
                Description = des,
                CoverImgPath = coverImg,
                OrginCoverImgPath = orginCoverImg,
                DetailUrl = detailUrl,
                OtherMessage = otherMessage,
                ComicState = state
            };
            return comic.Add();
        }

        public bool DownLoadOverImage(string comicname, string serImgpath, string currentPage = "", string host = "images.dmzj.com")
        {
            var comic = Comic.ComicRepository.GetByComicName(comicname);
            if (comic == null || comic.Id == 0)
            {
                return false;
            }

            if (!Directory.Exists(serImgpath))
            {
                Directory.CreateDirectory(serImgpath);
            }
            if (!Directory.Exists(serImgpath + comic.Id + "/"))
            {
                Directory.CreateDirectory(serImgpath + comic.Id + "/");
            }

            CrawlerHelper.CrawlImage(comic.OrginCoverImgPath, serImgpath + comic.Id + "/" + comic.CoverImgPath, currentPage, host);

            return true;
        }

        public int UpdateComic(int comicid, string titleName, string description)
        {
            var comic = new Comic()
            {
                Id = comicid,
                TitleName = titleName,
                Description = description
            };
            return Comic.ComicRepository.Update(comic, "TitleName", "Description");
        }

        public bool AddComicVolume(int comicid, string volumeName, string volDetailUrl, int sortno)
        {
            var comicVolume = new ComicVolume()
            {
                ComicId = comicid,
                VolumeName = volumeName,
                VolDetailUrl = volDetailUrl,
                SortNo = sortno,
            };
            return comicVolume.Add();
        }

        public bool AddComicPage(int comicid, int volumeId, string pageImgPath, int pageNumber)
        {
            var comicPage = new ComicPage()
            {
                ComicId = comicid,
                VolumeId = volumeId,
                PageImgPath = pageImgPath,
                PageNumber = pageNumber,
            };
            return comicPage.Add();
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

        public ComicDto GetByName(string comicName)
        {
            return ComicDto.GetByName(comicName);
        }
    }
}
