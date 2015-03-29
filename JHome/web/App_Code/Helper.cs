using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Application.Dto;
using Application.IApplication;
using Factory;

/// <summary>
/// Helper 的摘要说明
/// </summary>
public static class Helper
{
    

    /// <summary>
    /// 设置登录信息
    /// </summary>
    public static void SetAuthen(string userName, Page page)
    {
        var auth = new AuthenticationModel(userName, DateTime.Now.ToString("yy-MM-dd"));
        page.Response.Cookies["J_UserName"].Value = userName;
        page.Response.Cookies["J_Key"].Value = auth.EnCode();
    }
    /// <summary>
    /// 检测是否已登录
    /// </summary>
    public static bool CheckAuthen(Page page)
    {
        if (page.Request.Cookies["J_UserName"] == null || page.Request.Cookies["J_Key"] == null)
        {
            return false;
        }

        var auth = new AuthenticationModel(page.Request.Cookies["J_UserName"].Value, DateTime.Now.ToString("yy-MM-dd"));
        return auth.AuthenCheck(page.Request.Cookies["J_Key"].Value);
    }
    
    /// <summary>
    /// 从cookies中获取用户信息
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static UserDto GetLoginUser(Page page)
    {
        if (page.Request.Cookies["J_UserName"] == null || page.Request.Cookies["J_Key"] == null)
        {
            return null;
        }

        string userName = page.Request.Cookies["J_UserName"].Value;

        IUserApplication iUserApplication = ApplicationFactory.CreateInstance<IUserApplication>("User");

        var user = iUserApplication.Get(userName);

        return user.Id == 0 ? null : user;
    }
}