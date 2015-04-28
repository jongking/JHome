var J = {};
J.GetJSON = function(url, action, data, successFun, errorFun, completeFun) {
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
        complete: completeFun
    });
}

J.GetJSONSync = function(url, action, data, successFun, errorFun, completeFun) {
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
        success: function(msg) {
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
        complete: completeFun
    });
    return result;
}

J.GetJSONByForm = function(url, action, selecter, successFun, errorFun, completeFun) {
    if (action == undefined || action == "") {
        J.error("error:no action");
        return undefined;
    }

    selecter.ajaxSubmit({
        type: "POST",
        url: url,
        data: { action: action },
        dataType: "json",
        success: function(msg) {
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
        complete: completeFun
    });
}

J.GetUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return null;
}

J.Table = {
    AddTable: function(id, objlist, showFields, fieldTypes) {
        var tablestr = '<table class="table table-responsive table-hover table-bordered table-striped">';
        var fieldarr = showFields.split(",");
        var typearr = fieldTypes.split(",");
        if (fieldarr.length != typearr.length) {
            alert("J.Table.AddTable 发生错误");
            return;
        }
        tablestr += '<tr>';
        $.each(fieldarr, function(i2, field) {
            tablestr += '<th>' + field + '</th>';
        });
        tablestr += '</tr>';
        $.each(objlist, function(i, obj) {
            tablestr += '<tr class="TableTr" data-trindex="' + i + '">';
            $.each(fieldarr, function(i2, field) {
                switch (typearr[i2]) {
                case 'Text':
                    tablestr += '<td>' + eval("obj." + field) + '</td>';
                    break;
                default:
                    tablestr += '<td>' + eval("obj." + field) + '</td>';
                }
            });
            tablestr += '</tr>';
        });
        tablestr += '</table>';
        $("#" + id).html(tablestr);
        $("#" + id).data("ObjList", objlist);
    },
    AddTableFromJson: function(id, showFields, fieldTypes, url, action, successFun) {
        J.GetJSON(url, action, {}, function(msg) {
            J.Table.AddTable(id, msg, showFields, fieldTypes);
            if (successFun != undefined) {
                successFun();
            }
        });
    }
}

J.TRCode = {
    htmlencode:
        function htmlencode(s) {
            var div = document.createElement('div');
            div.appendChild(document.createTextNode(s));
            return div.innerHTML;
        },
    htmldecode:
        function htmldecode(s) {
            var div = document.createElement('div');
            div.innerHTML = s;
            return div.innerText || div.textContent;
        }
}