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
    public void Reg()
    {
        var kv = new KeyValue
        {
            {"one", WebHelper.JsonSerialize(new Users())},
        };

        JsonResult.SetDateByKeyValue(kv);
    }
}