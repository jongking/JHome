using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Dto;
using Application.IApplication;
using Domain.Model.Comic;
using Factory;
using JHelper;

public partial class api_Comic : GloPage
{
    private readonly IComicApplication _comicApplication = ApplicationFactory.CreateInstance<IComicApplication>("Comic");

    public void GetAllComics()
    {
        var comicList = _comicApplication.GetAll();
        
        JsonResult.SetDateByClass(comicList);
    }

    public void GetComicDetail()
    {
        var id = WebHelper.Request("Id", Page);

        var comic = _comicApplication.GetById(Convert.ToInt32(id));
        if (comic.Id > 0)
        {
            var comicVol = _comicApplication.GetVolumeById(Convert.ToInt32(id));

            var comicWrapper = new ComicWrapper()
            {
                Comic = comic,
                ComicVolumeList = comicVol
            };
            JsonResult.SetDateByClass(comicWrapper);
        }
    }

    public void GetComicPages()
    {
        var comicid = WebHelper.Request("comicid", Page);
        var volumeid = WebHelper.Request("volumeid", Page);

        var comic = _comicApplication.GetById(Convert.ToInt32(comicid));
        if (comic.Id > 0)
        {
            var comicPages = _comicApplication.GetPagesByVolId(Convert.ToInt32(volumeid));

            var comicWrapper = new ComicWrapper()
            {
                Comic = comic,
                ComicPageList = comicPages
            };
            JsonResult.SetDateByClass(comicWrapper);
        }
    }

    public class ComicWrapper
    {
        public ComicDto Comic;
        public List<ComicVolumeDto> ComicVolumeList;
        public List<ComicPageDto> ComicPageList;
    }
}