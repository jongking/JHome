"use strict";
var header = '<nav class="navbar navbar-inverse navbar-fixed-top"><div class="container-fluid"><div class="navbar-header">' +
    '<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"><span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>' +
    '<a id="LinkManager" class="navbar-brand" href="' + J.JHome.RootPath + 'www/manager" style="display:none;"><span class="glyphicon glyphicon-home"></span></a>' +
    '<a class="navbar-brand" href="' + J.JHome.RootPath + 'www">JHome</a>' +
    '<a class="navbar-brand" href="' + J.JHome.RootPath + 'www/chatRoom.html"></a>' +
    '</div><div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1"><ul class="nav navbar-nav">' +
    '<li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">爬虫<span class="caret"></span></a>' +
    '<ul class="dropdown-menu" role="menu">' +
    '<li><a href="' + J.JHome.RootPath + 'www/manager/comicCrawler.html">漫画爬虫</a></li>' +
    '</ul></li>' +
    '<li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">用户管理<span class="caret"></span></a>' +
    '<ul class="dropdown-menu" role="menu">' +
    '<li><a href="' + J.JHome.RootPath + 'www/manager/user.html">用户</a></li>' +
    '<li><a href="' + J.JHome.RootPath + 'www/manager/role.html">角色</a></li>' +
    '</ul></li>'+
    '<li><a href="#">待定</a></li></ul>' +
    '<ul class="nav navbar-nav navbar-right" id="LoginShowMenu"><li><a href="#" class="UserName"></a></li><li><a href="" id="LogOutA">LogOut</a></li></ul>' +
    '<form class="navbar-form navbar-right" role="search" id="RegForm"><div class="form-group"><input type="text" class="form-control" placeholder="UserName" name="UserName" validate="{required:true,minlength:6}">' +
    '<input type="password" class="form-control" placeholder="PassWord" name="PassWord" validate="{required:true,minlength:8}">' +
    '</div><div class="btn-group" role="group"><button type="submit" class="btn btn-default" id="RegButton">Reg</button><button type="submit" class="btn btn-default" id="LoginButton">Login</button></div></form></div></div></nav>';
document.write(header);

$(document).ready(function () {
    if (!J.JHome.IsLog()) {
        $("#LoginShowMenu").hide();
    } else {
        $("#RegForm").hide();
        $(".UserName").text(J.User.UserName);
        $("#LogOutA").bind("click", function () {
            J.JHome.LogOut();
        });
    }
    if (J.JHome.CheckPower(1)) {
        $("#LinkManager").show();
    }
    $("#RegButton").bind("click", function (e) {
        e.preventDefault();
        //检查是否验证通过
        if ($("#RegForm").valid()) {
            J.GetJSONByForm(J.JHome.RootPath + "api/user.aspx", "Reg", $("#RegForm"), function (msg) {
                location.reload();
            });
        }
    });
    $("#LoginButton").bind("click", function (e) {
        e.preventDefault();
        //检查是否验证通过
        if ($("#RegForm").valid()) {
            J.GetJSONByForm(J.JHome.RootPath + "api/user.aspx", "Login", $("#RegForm"), function (msg) {
                location.reload();
            });
        }
    });
});