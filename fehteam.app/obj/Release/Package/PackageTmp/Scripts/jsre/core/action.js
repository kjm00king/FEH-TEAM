JSRE.core.action = function () {
    var me = this;
    JSRE.core.event.apply(me, arguments);
    
    me.RequestUrl = arguments[0] || "";
    me.RequestType = arguments[1] || "GET";

    me.bind("begin", function () { });
    me.bind("success", function () { });
    me.bind("fail", function () { });
    me.bind("complete", function () { });
    me.bind("alert", function (msg) { alert(r.msg); });

    me.get = function (_data) {
        me.RequestType = "GET";
        me.action(_data);
    }

    me.post = function (_data) {
        me.RequestType = "POST";
        me.action(_data);
    }

    me.action = function (_data) {
        _data = _data || {};
        me.exec("begin");
        $.ajax({
            type: me.RequestType,
            url: me.RequestUrl,
            data: { "param": JSON.stringify(_data) },
            success: function (r) {
                me.exec("process", r);
                if (r.result)
                    me.exec("success", r.data);
                else
                    me.exec("alert", r.msg);
                me.exec("fail");
            },
            complete: function () { me.exec("complete"); },
            dataType: "json",
        });
    }
}