/*******************************************************************************
* 异步上传文件（无法兼容IE8以下，file拒绝访问，IE9以上未测试，火狐和谷歌可用）
* 实现单个多次上传不刷新
* @author 柳伟伟 <702295399@qq.com>
* @version 1.1 (2015-4-7)
*******************************************************************************/
/*
//地址
url: 'index.aspx',
// 文件域名字
 fileName: 'filedata',
// 其他表单数据
params: { name: 'pxblog' },
// 上传完成后, 返回json, text
dataType: 'json',
// 上传之前回调,return true表示可继续上传
onSend
//选择后回调传文件值
onUpload
// 上传之后回调
onComplate
*/
(function ($) {
    var frameCount = 0;
    var formName = "";
    var fileObj = null;
    var iframeObj = null;
    var state = {};
    //清空值
    function clean(target) {

        var file = $(fileObj);
        var col = file.clone(true).val("");
        file.after(col);
        file.remove();
        fileObj = col;
        //关键说明
        //先得到当前的对象和参数，接着进行克隆（同时克隆事件）
        //将克隆好的副本放在原先的之后，按照顺序逐个删除，最后初始化克隆的副本
        var options = state.options;
        options.onUpload($(target).val());
    };
    function ajaxSubmit(target) {
        if (fileObj == null) {
            alert("请选择文件");
            return;
        }
        if ($(fileObj).val() == '' || $(fileObj).val() == null) {
            alert("请选择文件");
            return;
        }
        var options = state.options;
        var form = $("form[name=" + formName + "]");


        iframeObj.bind("load", function () {
            var contents = $(this).contents().get(0);
            var data = $(contents).find('body').text();
            if ('json' == options.dataType) {
                data = window.eval('(' + data + ')');
            }

            options.onComplate(data);

            iframeObj.remove();
            form.remove();
            iframeObj = null;
            fileObj = null;
            //setTimeout(function () {
            //    iframe.remove();
            //    form.remove();
            //}, 3000);

        });

        form[0].submit();
    };
    //构造
    $.fn.upload = function (options) {
        if (typeof options == "string") {
            return $.fn.upload.methods[options](this);
        }
        options = options || {};
        state = $.data(this, "upload");
        if (state)
            $.extend(state.options, options);
        else {
            state = $.data(this, "upload", {
                options: $.extend({}, $.fn.upload.defaults, options)
            });
        }

        //return this.each(function () {
        var opts = state.options;

        if (opts.url == '') {
            return;
        }

        var canSend = opts.onSend();
        if (!canSend) {
            return;
        }
        if (iframeObj == null) {

            var frameName = 'upload_frame_' + (frameCount++);
            var iframe = $('<iframe style="position:absolute;top:-9999px" ></iframe>').attr('name', frameName);
            formName = 'form_' + frameName;
            var form = $('<form method="post" style="display:none;" enctype="multipart/form-data" />').attr('name', formName);
            form.attr("target", frameName).attr('action', opts.url);
            // form中增加数据域
            var formHtml = '<input type="file" name="' + opts.fileName + '" >';
            for (key in opts.params) {
                formHtml += '<input type="hidden" name="' + key + '" value="' + opts.params[key] + '">';
            }
            form.append(formHtml);


            iframe.appendTo("body");

            form.appendTo("body");


            var fileInput = $('input[type=file][name=' + opts.fileName + ']', form);
            fileInput.click();
            fileObj = fileInput;
            iframeObj = iframe;
        } else {
            fileObj.click();
        }
        fileObj.unbind("change")
        fileObj.bind("change", function () {
            //$(this).val()
            opts.onUpload($(this).val());
        })
    };
    //方法
    $.fn.upload.methods = {
        clean: function (jq) {
            return jq.each(function () {
                clean(this);
            });
        },
        ajaxSubmit: function (jq) {
            return jq.each(function () {
                ajaxSubmit(jq);
            });
        }
    };
    //默认项
    $.fn.upload.defaults = $.extend({}, {
        url: '',
        fileName: 'filedata',
        dataType: 'json',
        params: {},
        onSend: function () { return true; },
        onUpload: function (str) { },
        onComplate: function () { return true; }
    });
})(jQuery);