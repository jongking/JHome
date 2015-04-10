//JHome中单独使用的函数
J.JHome = {
    LogOut: function () {
        document.cookie = "J_UserName=; path=/";
    },
    IsLog: function () {
        if (J.User != undefined) {
            return true;
        } else {
            return false;
        }
    },
    CheckPower: function (powerId) {
        if (!J.JHome.IsLog()) {
            return false;
        }
        var result = false;
        J.GetJSONSync(J.JHome.RootPath + "api/Power.aspx", "CheckPower", { PowerId: powerId }, function (msg) {
            result = msg;
        });
        return result;
    },
    RootPath: "../"
}