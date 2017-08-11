var JSRE = {
    core: {},
    me:{}
}

if (this.Array) {
    //从数组最后倒数获得元素 
    //e.g. [1,2,3,4].last(1) = 3;
    this.Array.prototype.last = function (n) {
        n = n || 0;
        return (this.length > 0 ? this[this.length - 1 - n] : undefined);
    }

    //抽取数组对象成为新的数组 
    //e.g. [{ name:'A', num:0 },{ name:'B', num:1 },{ name:'C', num:2 }].extract('name') = ['A','B','C'];
    this.Array.prototype.extract = function () {
        var rv = [];
        for (var i = 0; i < this.length; i++) {
            var v = this[i];
            var n = 0;
            while (true) {
                if (n >= arguments.length || v == undefined)
                    break;
                v = v[arguments[n]];
                n++;
            }
            if (v != undefined) { rv.push(v); }
        }
        return rv;
    }
}

if ($ && $.ui) {
    //确保 Jquery UI 移动框可以移除Windows
    $.ui.dialog.prototype._makeDraggable = function () {
        this.uiDialog.draggable({
            containment: false
        });
    };
}