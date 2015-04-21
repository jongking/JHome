using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain.Model.Comic;
using JHelper.DB;

public partial class api_CreateTable : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DbHelper.ExecuteScalar(SimpleSqlCreater.CreateTable<Comic>().ToString());
        DbHelper.ExecuteScalar(SimpleSqlCreater.CreateTable<ComicVolume>().ToString());
        DbHelper.ExecuteScalar(SimpleSqlCreater.CreateTable<ComicPage>().ToString());
        Response.Write("OK");
    }
}