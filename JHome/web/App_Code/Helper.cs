using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

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
}