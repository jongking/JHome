﻿//初始化validate
$(document).ready(function () {
    var $form = $("form");
    $.each($form, function(i, v) {
        $(this).validate();
    });
//    $("form").validate();
});
//改变validdate的默认错误提示方法
$.validator.defaults.errorPlacement =
    function(place, $element) {
        //使用Bootstrap的提示框
        tooltipValidator(place, $element);
    };
$.validator.defaults.success =
    function (place, element) {
    };

function tooltipValidator(place, $element) {
    $element.tooltip('destroy');
    $element.data("tooltip", place[0].innerHTML);
    $element.tooltip({
        title: place[0].innerHTML,
        animation: false,
        placement: "bottom",
        trigger: "manual",
    });
    $element.tooltip('show');
}
function popoverValidator(place, $element) {
    $element = $element.popover('destroy');
    $element.data("popover", place[0].innerHTML);
    $element.popover({
        content: place[0].innerHTML,
        animation: false,
        placement: "bottom",
        trigger: "manual",
    });
    $element.popover('show');
}
/*
 * Translated default messages for the jQuery validation plugin.
 * Locale: ZH (Chinese, 中文 (Zhōngwén), 汉语, 漢語)
 */
$.extend($.validator.messages, {
    required: "这是必填字段",
    remote: "请修正此字段",
    email: "请输入有效的电子邮件地址",
    url: "请输入有效的网址",
    date: "请输入有效的日期",
    dateISO: "请输入有效的日期 (YYYY-MM-DD)",
    number: "请输入有效的数字",
    digits: "只能输入数字",
    creditcard: "请输入有效的信用卡号码",
    equalTo: "你的输入不相同",
    extension: "请输入有效的后缀",
    maxlength: $.validator.format("最多可以输入 {0} 个字符"),
    minlength: $.validator.format("最少要输入 {0} 个字符"),
    rangelength: $.validator.format("请输入长度在 {0} 到 {1} 之间的字符串"),
    range: $.validator.format("请输入范围在 {0} 到 {1} 之间的数值"),
    max: $.validator.format("请输入不大于 {0} 的数值"),
    min: $.validator.format("请输入不小于 {0} 的数值")
});

//设置alertModel
var alertModel = "<div id=\"AlertModal\" class=\"modal fade\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"mySmallModalLabel\" aria-hidden=\"true\"><div class=\"modal-dialog modal-sm\"><div id='AlertType' class=\"modal-content alert alert-danger\"><div class=\"modal-header\" data-dismiss=\"modal\" id='AlertMsg'></div><div id=\"AlertShow\" class=\"modal-body\"></div></div></div></div>";
$(document).ready(function() {
    $("body").append(alertModel);
});
J.error = function (obj) {
    $("#AlertMsg").html("<h4>Error</h4>");
    $("#AlertType").removeClass("alert-success");
    $("#AlertType").addClass("alert-danger");
    $("#AlertShow").html(obj);
    $("#AlertModal").modal({});
}
J.alert = function (obj) {
    $("#AlertMsg").html("<h4>Message</h4>");
    $("#AlertType").removeClass("alert-danger");
    $("#AlertType").addClass("alert-success");
    $("#AlertShow").html(obj);
    $("#AlertModal").modal({});
}

//获取登录信息
J.GetJSONSync(J.JHome.RootPath + "api/user.aspx", "Check", {}, function (msg) {
    if (msg.Login != undefined) {
        J.User = msg;
    }
});