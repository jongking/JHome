using Application.IApplication;
using Factory;
using JHelper;

public partial class api_User : GloPage
{
    private readonly IUserApplication _iUserApplication = ApplicationFactory.CreateInstance<IUserApplication>("User");

    public void Reg()
    {
        var userName = WebHelper.Request("UserName", Page);
        var passWord = WebHelper.Request("PassWord", Page);

        if (_iUserApplication.Reg(userName, passWord))
        {
            Helper.SetAuthen(userName, Page);
        }
    }

    public void Login()
    {
        var userName = WebHelper.Request("UserName", Page);
        var passWord = WebHelper.Request("PassWord", Page);

        if (_iUserApplication.Login(userName, passWord))
        {
            Helper.SetAuthen(userName, Page);
        }
        else
        {
            JsonResult.Error("密码或用户名错误");
        }
    }

    public void Check()
    {
        if (Helper.CheckAuthen(Page))
        {
            JsonResult.SetDateByKeyValue(new KeyValue
            {
                {"Login", "true"},
                {"UserName", Request.Cookies["J_UserName"].Value},
            });
        }
    }
}