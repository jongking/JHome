using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AuthenticationModel 的摘要说明
/// </summary>
public class AuthenticationModel
{
    public string Key1;

    public string Key2;

    public AuthenticationModel(string key1, string key2)
    {
        Key1 = key1;
        Key2 = key2;
    }

    public string EnCode()
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Key1 + "11" + Key2, "MD5");
    }

    public bool AuthenCheck(string checkString)
    {
        if (checkString == EnCode())
        {
            return true;
        }
        return false;
    }
}