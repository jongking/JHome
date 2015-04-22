using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.IApplication;
using Factory;

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
        var comicList = _comicApplication.GetAll();
        
        JsonResult.SetDateByClass(comicList);
    }
}