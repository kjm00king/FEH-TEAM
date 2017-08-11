JSRE.core.event = function () {
    var me = this;
    me.Listeners = {};
    
    me.bind = function (type, fun, name, data) {
        if (me.Listeners[type] == undefined)
            me.Listeners[type] = {};
        if (name == undefined)
            name = "default";
        me.Listeners[type][name] = [fun, data];
        return me;
    }

    me.unbind = function (type, name) {
        if (name == undefined)
            name = "default";
        if (me.Listeners[type] == undefined)
            return;
        if (me.Listeners[type][name] == undefined)
            return;
        delete me.Listeners[type][name];
        return me;
    }

    me.exec = function (type) {

        var rv;

        if (me.Listeners[type] == undefined)
            return;

        var arg = [];

        if (arguments.length > 1) {
            arg = [];
            for (var i = 1; i < arguments.length; i++)
                arg.push(arguments[i]);
        }

        var num = 0;
        for (var kk in me.Listeners[type]) { num++; }
        if (num > 1) { rv = []; }

        for (var k in me.Listeners[type]) {
            var ev = me.Listeners[type][k][0];
            var data = me.Listeners[type][k][1];
            if (data != undefined)
                arg.splice(0, 0, data);
            if (num > 1)
                rv.push(ev.apply(me, arg))
            else
                rv = ev.apply(me, arg);
        }
        return rv;
    }
}