using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JHelper;

/// <summary>
/// JsonResult 的摘要说明
/// </summary>
public class JsonResult
{
    public enum ResultCode
    {
        正常 = 0,
        错误
    }

    public ResultCode Code = ResultCode.正常;

    public string ErrorReson = "";

    public string Date = "";

    public void SetDateByClass(object obj)
    {
        Date = WebHelper.JsonSerialize(obj);
    }

    public void SetDateByJsonString(string str)
    {
        Date = str;
    }

    public void SetDateByKeyValue(KeyValue kv)
    {
        Date = WebHelper.JsonSerialize(kv);
    }

    public override string ToString()
    {
        return WebHelper.JsonSerialize(this);
    }
}

public class KeyValue : Dictionary<string,string>
{
    
}