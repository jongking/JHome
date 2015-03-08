using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain.Model;
using JHelper;

public partial class api_User : GloPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Action)
        {
            case "Get":
                Get();
                break;
        }
    }

    private void Get()
    {
        var jsonResult = new JsonResult();
        var kv = new KeyValue {{"one", WebHelper.JsonSerialize(new Users())}};
        jsonResult.SetDateByKeyValue(kv);
        Result = jsonResult.ToString();
    }
}