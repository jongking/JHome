var J = {};
J.GetJSON = function (url, action, data, successFun, errorFun, async) {
    if (async == undefined) {
        async = true;
    }
    if (action == undefined || action == "") {
        alert("error:no action");
        return;
    }

    data.action = action;
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        dataType: "json",
        async: async,
        success: function(msg) {
            if (msg.Code == 0) {
                var result = $.parseJSON(msg.Date);
                successFun(result);
            } else {
                if (errorFun != undefined) {
                    errorFun(msg.Code, msg.ErrorReson);
                } else {
                    alert(msg.ErrorReson);
                }
            }
        }
    });
}