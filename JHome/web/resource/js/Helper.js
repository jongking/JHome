var J = {};
J.GetJSON = function (url, action, data, successFun, errorFun, completeFun) {
    if (action == undefined || action == "") {
        J.error("error:no action");
        return;
    }

    data.action = action;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: "json",
        async: true,
        success: function(msg) {
            if (msg.Code == 0) {
                var result;
                if (msg.Date == "") {
                    result = "";
                } else {
                    result = $.parseJSON(msg.Date);
                }
                successFun(result);
            } else {
                if (errorFun != undefined) {
                    errorFun(msg.Code, msg.ErrorReson);
                } else {
                    J.error(msg.ErrorReson);
                }
            }
        },
        complete:completeFun
    });
}

J.GetJSONSync = function (url, action, data, successFun, errorFun, completeFun) {
    if (action == undefined || action == "") {
        J.error("error:no action");
        return undefined;
    }
    var result;

    data.action = action;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: "json",
        async: false,
        success: function (msg) {
            if (msg.Code == 0) {
                var result;
                if (msg.Date == "") {
                    result = "";
                } else {
                    result = $.parseJSON(msg.Date);
                }
                if (successFun != undefined) {
                    successFun(result);
                }
            } else {
                if (errorFun != undefined) {
                    errorFun(msg.Code, msg.ErrorReson);
                } else {
                    J.error(msg.ErrorReson);
                }
                result = undefined;
            }
        },
        complete:completeFun
    });
    return result;
}

J.GetJSONByForm = function (url, action, selecter, successFun, errorFun, completeFun) {
    if (action == undefined || action == "") {
        J.error("error:no action");
        return undefined;
    }

    selecter.ajaxSubmit({
        type: "POST",
        url: url,
        data: { action: action },
        dataType: "json",
        success: function (msg) {
            if (msg.Code == 0) {
                var result;
                if (msg.Date == "") {
                    result = "";
                } else {
                    result = $.parseJSON(msg.Date);
                }
                if (successFun != undefined) {
                    successFun(result);
                }
            } else {
                if (errorFun != undefined) {
                    errorFun(msg.Code, msg.ErrorReson);
                } else {
                    J.error(msg.ErrorReson);
                }
            }
        },
        complete:completeFun
    });
}