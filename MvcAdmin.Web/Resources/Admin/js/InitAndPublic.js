//初始化分页插件
// <ul class="pagination"></ul>引用方式
function initP() {
    $.jqPaginator('ul.pagination', {
        wrapper: '',
        first: '<li class="first"><a href="javascript:;">第一页</a></li>',
        prev: '<li class="prev"><a href="javascript:;">上一页</a></li>',
        next: '<li class="next"><a href="javascript:;">下一页</a></li>',
        last: '<li class="last"><a href="javascript:;">最后一页</a></li>',
        page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
        totalPages: 1,/*总数 */
        visiblePages: 8,/*可见页面*/
        currentPage: 1
        //onPageChange: function (num, type) {
        //    //$('#p1').text(type + '：' + num);change：6
        //    //alert(num);
        //    func(num)
        //}
    });
}
//从一个界面切换到另一个界面
function formToForm(id1, id2) {
    $("#" + id1).hide(300);
    $("#" + id2).show(300);
}

//界面切换绑定
function bindFormToForm() {
    $("a.app-button").bind("click", function () {
        var id1 = $(this).attr("form-tag-h");
        var id2 = $(this).attr("form-tag-s");
        //$("#" + id1).hide(300);
        //$("#" + id2).show(300);
        formToForm(id1, id2);
    });
}

function loadContent(url) {
    var myLoading = $.sobox.loading();
    $("#content").load(url, {}, function () {
        //alert("加载成功");
        myLoading.close();
    });
}

//全选
function selectCheck() {
    var selectArr = $("input[type=checkbox][tag=chk]");
    for (var i = 0; i < selectArr.length; i++) {
        $(selectArr[i]).attr("checked", !($(selectArr[i]).is(':checked')))
    }
}