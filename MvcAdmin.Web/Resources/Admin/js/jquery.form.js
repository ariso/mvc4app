/**
 * form - jQuery EasyUI 
 * 
 * Copyright (c) 2009-2013 www.jeasyui.com. All rights reserved.
 *
 * Licensed under the GPL or commercial licenses
 * To use it on other terms please contact us: jeasyui@gmail.com
 * http://www.gnu.org/licenses/gpl.txt
 * http://www.jeasyui.com/license_commercial.php
 */
/*
$('#ff').form({
    url:...,
    onSubmit: function(){
        //进行表单验证
        //如果返回false阻止提交
    },
    success:function(data){
        alert(data)
    }
});
//提交表单
$('#ff').submit();

//调用表单插件的'submit'方法提交
$('#ff').form('submit', {
    url:...,
    onSubmit: function(){
        //进行表单验证
        //如果返回false阻止提交
    },
    success:function(data){
        alert(data)
    }
});

$('#ff').form('clear');

$('#ff').form('load','form_data.json');

function loaddata2(){
			$('#ff').form('load',{
				name:'name2',
				email:'mymail@gmail.com',
				subject:'subject2',
				message:'message2',
				language:5
			});
		}
*/
/*已经精简，去掉了easyui的特性*/
(function($){
	/**
	 * submit the form
	 */
	function ajaxSubmit(target, options){
		options = options || {};
		
		var param = {};
		if (options.onSubmit){
			if (options.onSubmit.call(target, param) == false) {
				return;
			}
		}
		
		var form = $(target);
		if (options.url){
			form.attr('action', options.url);
		}
		var frameId = 'easyui_frame_' + (new Date().getTime());
		var frame = $('<iframe id='+frameId+' name='+frameId+'></iframe>')
				.attr('src', window.ActiveXObject ? 'javascript:false' : 'about:blank')
				.css({
					position:'absolute',
					top:-1000,
					left:-1000
				});
		var t = form.attr('target'), a = form.attr('action');
		form.attr('target', frameId);
		
		var paramFields = $();
		try {
			frame.appendTo('body');
			frame.bind('load', cb);
			for(var n in param){
				var f = $('<input type="hidden" name="' + n + '">').val(param[n]).appendTo(form);
				paramFields = paramFields.add(f);
			}
			form[0].submit();
		} finally {
			form.attr('action', a);
			t ? form.attr('target', t) : form.removeAttr('target');
			paramFields.remove();
		}
		var checkCount = 10;
		function cb(){
			frame.unbind();
			var body = $('#'+frameId).contents().find('body');
			var data = body.html();
			if (data == ''){
				if (--checkCount){
					setTimeout(cb, 100);
					return;
				}
				return;
			}
			var ta = body.find('>textarea');
			if (ta.length){
				data = ta.val();
			} else {
				var pre = body.find('>pre');
				if (pre.length){
					data = pre.html();
				}
			}
			if (options.success){
				options.success(data);
			}
			setTimeout(function(){
				frame.unbind();
				frame.remove();
			}, 100);
		}
	}
	/**
	 * load form data
	 * if data is a URL string type load from remote site, 
	 * otherwise load from local data object. 
	 */
	function load(target, data){
		if (!$.data(target, 'form')){
			$.data(target, 'form', {
				options: $.extend({}, $.fn.form.defaults)
			});
		}
		var opts = $.data(target, 'form').options;
		
		if (typeof data == 'string'){
			var param = {};
			if (opts.onBeforeLoad.call(target, param) == false) return;
			
			$.ajax({
				url: data,
				data: param,
				dataType: 'json',
				success: function(data){
					_load(data);
				},
				error: function(){
					opts.onLoadError.apply(target, arguments);
				}
			});
		} else {
			_load(data);
		}
		
		function _load(data){
			var form = $(target);
			for(var name in data){
				var val = data[name];
				var rr = _checkField(name, val);
				if (!rr.length){
					//var f = form.find('input[numberboxName="'+name+'"]');
					//if (f.length){

					//} else {
					//	$('input[name="'+name+'"]', form).val(val);
					//	$('textarea[name="'+name+'"]', form).val(val);
					//	$('select[name="'+name+'"]', form).val(val);
					//}
					$('input[name="' + name + '"]', form).val(val);
					$('textarea[name="' + name + '"]', form).val(val);
					$('select[name="' + name + '"]', form).val(val);
				}
			}
			opts.onLoadSuccess.call(target, data);
		}
		
		/**
		 * check the checkbox and radio fields
		 */
		function _checkField(name, val){
			var form = $(target);
			var rr = $('input[name="'+name+'"][type=radio], input[name="'+name+'"][type=checkbox]', form);
			$.fn.prop ? rr.prop('checked',false) : rr.attr('checked',false);
			rr.each(function(){
				var f = $(this);
				if (f.val() == String(val)){
					$.fn.prop ? f.prop('checked',true) : f.attr('checked',true);
				}
			});
			return rr;
		}
	}
	
	/**
	 * clear the form fields
	 */
	function clear(target){
		$('input,select,textarea', target).each(function(){
			var t = this.type, tag = this.tagName.toLowerCase();
			if (t == 'text' || t == 'hidden' || t == 'password' || tag == 'textarea'){
				this.value = '';
			} else if (t == 'file'){
				var file = $(this);
				file.after(file.clone().val(''));
				file.remove();
			} else if (t == 'checkbox' || t == 'radio'){
				this.checked = false;
			} else if (tag == 'select'){
				this.selectedIndex = -1;
			}
		});
	}
	
	function reset(target){
		target.reset();
		var t = $(target);
	}
	
	/**
	 * set the form to make it can submit with ajax.
	 */
	function setForm(target){
		var options = $.data(target, 'form').options;
		var form = $(target);
		form.unbind('.form').bind('submit.form', function(){
			setTimeout(function(){
				ajaxSubmit(target, options);
			}, 0);
			return false;
		});
	}
	
	$.fn.form = function(options, param){
		if (typeof options == 'string'){
			return $.fn.form.methods[options](this, param);
		}
		
		options = options || {};
		return this.each(function(){
			if (!$.data(this, 'form')){
				$.data(this, 'form', {
					options: $.extend({}, $.fn.form.defaults, options)
				});
			}
			setForm(this);
		});
	};
	
	$.fn.form.methods = {
		submit: function(jq, options){
			return jq.each(function(){
				ajaxSubmit(this, $.extend({}, $.fn.form.defaults, options||{}));
			});
		},
		load: function(jq, data){
			return jq.each(function(){
				load(this, data);
			});
		},
		clear: function(jq){
			return jq.each(function(){
				clear(this);
			});
		},
		reset: function(jq){
			return jq.each(function(){
				reset(this);
			});
		}
	};
	
	$.fn.form.defaults = {
		url: null,
		onSubmit: function(param){},
		success: function(data){},
		onBeforeLoad: function(param){},
		onLoadSuccess: function(data){},
		onLoadError: function(){}
	};
})(jQuery);
