"use strict";
var header = '<nav class="navbar navbar-inverse"><div class="container-fluid"><div class="navbar-header">' +
    '<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"><span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>' +
    '<a class="navbar-brand" href="#">JHome</a>' +
    '<a class="navbar-brand" href="./chatRoom.html">黑屋子</a>' +
    '</div><div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1"><ul class="nav navbar-nav">' +
    '<li><a href="#">待定</a></li><li><a href="#">待定</a></li></ul>' +
    '<ul class="nav navbar-nav navbar-right" id="LoginShowMenu"><li><a href="#" class="UserName"></a></li><li><a href="" id="LogOutA">LogOut</a></li></ul>' +
    '<form class="navbar-form navbar-right" role="search" id="RegForm"><div class="form-group"><input type="text" class="form-control" placeholder="UserName" name="UserName" validate="{required:true,minlength:6}">' +
    '<input type="password" class="form-control" placeholder="PassWord" name="PassWord" validate="{required:true,minlength:8}">' +
    '</div><div class="btn-group" role="group"><button type="submit" class="btn btn-default" id="RegButton">Reg</button><button type="submit" class="btn btn-default" id="LoginButton">Login</button></div></form></div></div></nav>';
document.write(header);

$(document).ready(function () {
    if (J.User == undefined) {
        $("#LoginShowMenu").hide();
    } else {
        $("#RegForm").hide();
        $(".UserName").text(J.User.UserName);
        $("#LogOutA").bind("click", function () {
            J.JHome.LogOut();
        });
    }

    $("#RegButton").bind("click", function (e) {
        e.preventDefault();
        //检查是否验证通过
        if ($("#RegForm").valid()) {
            J.GetJSONByForm("../api/user.aspx", "Reg", $("#RegForm"), function (msg) {
                location.reload();
            });
        }
    });
    $("#LoginButton").bind("click", function (e) {
        e.preventDefault();
        //检查是否验证通过
        if ($("#RegForm").valid()) {
            J.GetJSONByForm("../api/user.aspx", "Login", $("#RegForm"), function (msg) {
                location.reload();
            });
        }
    });
});