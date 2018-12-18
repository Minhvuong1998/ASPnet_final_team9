//dialog
var PAGE = 1;
var URLLOGIN = '/portfolio/dang-nhap';
// Thống kê danh mục
function getCharCategory(url) {
    $.ajax({
        type: 'GET',
        data: {},
        url: url,
        success: function (data) {
            if (data != "") {
                var obj = JSON.parse(data);
                var cates = [];
                var i = 0;
                var i_view = 0;
                obj.forEach(function (value) {
                    i = i + 1;
                    i_view = i_view + value.Count_view;
                    var cate = {
                        label: value[0],
                        value: value[1]
                    }
                    cates.push(cate);
                });
                //DONUT CHART
                var donut = new Morris.Donut({
                    element: 'sales-chart',
                    resize: true,
                    colors: ["#3c8dbc", "#f56954", "#00a65a"],
                    data: cates,
                    hideHover: 'auto'
                    //    [
                    //  { label: "Download Sales", value: 12 },
                    //  { label: "In-Store Sales", value: 30 },
                    //  { label: "Mail-Order Sales", value: 20 }
                    //]
                });
            } else {

            }
        }
    });
}
//THống kê bài viết
function TkPost(startDate, endDate, url) {
    $.ajax({
        type: 'GET',
        data: { astartDate: startDate, aendDate: endDate },
        url: url,
        success: function (data) {
            if (data != null) {
                var obj = JSON.parse(data);
                var posts = [];
                var i = 0;
                var i_view = 0;
                obj.forEach(function (value) {
                    i = i + 1;
                    i_view = i_view + value.Count_view;
                    var post = {
                        0: i,
                        1: value.Title,
                        2: value.Count_view,
                        3: value.PostedOn.split('T')[0]
                    }
                    posts.push(post);
                });
                var dataPost = $('#example2').DataTable();
                dataPost.clear().draw();
                dataPost.rows.add(
                    posts
                ).draw();
                $('#total-post').html('Tổng bài viết: ' + i);
                $('#total-view').html('Tổng lượt truy cập: ' + i_view);
            } else {

            }
        }
    });
}
//Up downCate
function UpCate(id, i, urlUp, urlDowwn) {
    $.ajax({
        type: 'GET',
        data: { aid: id },
        url: urlUp,
        success: function (data) {
            if (data != '') {
                var obj = JSON.parse(data);
                if (obj.length == 2) {
                    var objChange = obj[1];
                    var objNotChange = obj[0];
                    if (obj[1].Id == id) {
                        objChange = obj[0];
                        objNotChange = obj[1];
                    }
                    $('#category-' + id).insertBefore('#category-' + objChange.Id);
                    $('#category-stt-' + id).html(Number(i) - Number(1));
                    $('#category-stt-' + objChange.Id).html(i);
                    $('#category-up-' + id).html(
                        '<a href="javascript: void(0)" onclick="UpCate(' + id + ', ' + (Number(i) - Number(1)) + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-up"></i>' +
                        '</a>&nbsp;' +
                        '<a href="javascript: void(0)" onclick="DownCate(' + id + ', ' + (Number(i) - Number(1)) + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-down"></i>' +
                        '</a>');
                    $('#category-up-' + objChange.Id).html(
                        '<a href="javascript: void(0)" onclick="UpCate(' + objChange.Id + ', ' + i + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-up"></i>' +
                        '</a>&nbsp;' +
                        '<a href="javascript: void(0)" onclick="DownCate(' + objChange.Id + ', ' + i + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-down"></i>' +
                        '</a>');
                    $('#category-' + id).css({ 'opacity': 0 })
                    $('#category-' + objChange.Id).css({ 'opacity': 0 })
                    changeStyle('#category-' + id);
                    changeStyle('#category-' + objChange.Id);
                }
            }
        }
    });
}
function DownCate(id, i, urlUp, urlDowwn) {
    $.ajax({
        type: 'GET',
        data: { aid: id },
        url: urlDowwn,
        success: function (data) {
            if (data != '') {
                var obj = JSON.parse(data);
                if (obj.length == 2) {
                    var objChange = obj[1];
                    var objNotChange = obj[0];
                    if (obj[1].Id == id) {
                        objChange = obj[0];
                        objNotChange = obj[1];
                    }
                    $('#category-' + objChange.Id).insertBefore('#category-' + id);
                    $('#category-stt-' + id).html(Number(i) + Number(1));
                    $('#category-stt-' + objChange.Id).html(i);
                    $('#category-up-' + id).html(
                        '<a href="javascript: void(0)" onclick="UpCate(' + id + ', ' + (Number(i) + Number(1)) + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-up"></i>' +
                        '</a>&nbsp;' +
                        '<a href="javascript: void(0)" onclick="DownCate(' + id + ', ' + (Number(i) + Number(1)) + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-down"></i>' +
                        '</a>');
                    $('#category-up-' + objChange.Id).html(
                        '<a href="javascript: void(0)" onclick="UpCate(' + objChange.Id + ', ' + i + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-up"></i>' +
                        '</a>&nbsp;' +
                        '<a href="javascript: void(0)" onclick="DownCate(' + objChange.Id + ', ' + i + ', \'' + urlUp + '\', \'' + urlDowwn + '\')" class="btn btn-warning btn-xs" >' +
                            '<i class="fa fa-angle-down"></i>' +
                        '</a>');
                    $('#category-' + id).css({ 'opacity': 0 })
                    $('#category-' + objChange.Id).css({ 'opacity': 0 })
                    changeStyle('#category-' + id);
                    changeStyle('#category-' + objChange.Id);
                }
            }
        }
    });
}

//Comment
//show dtu
function GetmoreComment(url, idPost, urlRed, urlPost, countComment, idParent) {
    $.ajax({
        type: 'GET',
        data: { aPAGE: PAGE + 1, aidPost: idPost },
        url: url,
        success: function (data) {
            if (data != null) {
                var obj = JSON.parse(data);
                var i = 0;
                if (obj.length > 0) {
                    obj.forEach(function (value) {
                        i = i + 1;
                        var urlPicture = "";
                        var type = value.Type_Comment;
                        switch (type) {
                            case "employer": {
                                urlPicture = "/Assets/Upload/Employer/" + value.EMPLOYER.EMPLOYER_LOGO;
                                break;
                            }
                            case "student": {
                                urlPicture = "/Assets/Upload/Student/" + value.STUDENT.CLOSEUP_PICTURE_1;
                                break;
                            }
                            case "admin": {
                                urlPicture = "/Assets/Upload/User/" + value.User.Picture;
                                break;
                            }
                            default: {
                                // do something
                            }
                        }
                        $('#block-comment-' + idParent).append('<div  style="margin: 10px" class="comment-main" id="comment-person-' + value.Id + '">' +
                                    '<img src="' + urlPicture + '" style="width: 50px; float: left" />' +
                                    '<p style="float: left; padding: 5px">' +
                                    value.Contents + '<br />' +
                                    '<a class="label label-info" onclick="Comment(' + value.Id + ', ' + value.Id_Post + ', \'' + urlPost + '\')" href="javascript: void(0)">Trả lơi</a>' +
                                    '<a id="show-comment-' + value.id + '" style="margin-left: 4px;" class="label label-primary" onclick="RedComment(' + value.Id + ', ' + value.Id_Post + ', \'' + urlRed + '\',\'' + urlPost + '\' )" href="javascript: void(0)">Xem bình luận (' + value.Count_comment + ')</a>' +
                                    '</p>' +
                                    '<div style="clear: both"></div>' +
                                    '<div style="margin-left: 10px" id="block-comment-' + value.Id + '"></div>' +
                                '</div>');
                        // Style
                        $('#block-comment-' + idParent).css({ 'opacity': 0 })
                        changeStyle('#block-comment-' + idParent);
                    });
                    if (obj.length == 10) {
                        var numberShow = Number(10 * PAGE) + Number(i);
                        $('#get-more-comment').html('Hiển thị thêm ' + '(' + numberShow + '/' + countComment + ')');
                    } else {
                        $('#get-more-comment').html('Không còn bình luận nào.');
                    }
                }
                PAGE = PAGE + 1;
            } else {
                //alert("Lỗi thử lại");
            }
        }
    });
}
function RedComment(idParent, idPost, url, urlPost) {
    if ($('#block-comment-' + idParent)[0].childElementCount == 0) {
        $.ajax({
            type: 'GET',
            data: { aidParent: idParent, aidPost: idPost },
            url: url,
            success: function (data) {
                if (data != null) {
                    $('#block-comment-' + idParent).html('');
                    var obj = JSON.parse(data);
                    obj.forEach(function (value) {
                        var urlPicture = "";
                        var type = value.Type_Comment;
                        switch (type) {
                            case "employer": {
                                urlPicture = "/Assets/Upload/Employer/" + value.EMPLOYER.EMPLOYER_LOGO;
                                break;
                            }
                            case "student": {
                                urlPicture = "/Assets/Upload/Student/" + value.STUDENT.CLOSEUP_PICTURE_1;
                                break;
                            }
                            case "admin": {
                                urlPicture = "/Assets/Upload/User/" + value.User.Picture;
                                break;
                            }
                            default: {
                                // do something
                            }
                        }
                        $('#block-comment-' + idParent).append('<div  style="margin: 10px;" class="comment-main" id="comment-person-' + value.Id + '">' +
                                    '<img src="' + urlPicture + '" style="width: 50px; float: left" />' +
                                    '<p style="float: left; padding: 5px">' +
                                    value.Comments + '<br />' +
                                    '<a class="label label-info" onclick="Comment(' + value.Id + ', ' + value.Id_Post + ', \'' + urlPost + '\')" href="javascript: void(0)">Trả lơi</a>' +
                                    '<a id="show-comment-' + value.Id + '" style="margin-left: 4px;" class="label label-primary" onclick="RedComment(' + value.Id + ', ' + value.Id_Post + ', \'' + url + '\',\'' + urlPost + '\' )" href="javascript: void(0)">Xem bình luận (' + value.Count_comment + ')</a>' +
                                    '</p>' +
                                    '<div style="clear: both"></div>' +
                                    '<div style="margin-left: 10px" id="block-comment-' + value.Id + '"></div>' +
                                '</div>');
                        // Style
                        $('#block-comment-' + idParent).css({'opacity': 0})
                        changeStyle('#block-comment-' + idParent);
                    });
                } else {
                    //alert("Lỗi thử lại");
                    dialog_login("Thông báo", "Vui lòng đăng nhập để bình luận", URLLOGIN);
                }
            }
        });
    } else {
        changeStyleDown('#block-comment-' + idParent);
        //$('#block-comment-' + idParent).html('');
    }
}
function Comment(id, idPost, url, urlRed) {
    $('.comment-post').remove();
    $("#block-comment-" + id).prepend(
        '<div class="row comment-post" id="comment-post'+id+'">' +
            '<div class="left_content">' +
                '<div class="contact_area">' +
                    '<form action="javascript: void(0)" class="contact_form">' +
                        '<textarea id="comment-' + id + '" name="comment" class="form-control comment" cols="30" rows="10" placeholder="Nhập nội dung"></textarea>' +
                        '<button class="btn btn-red" onclick="Postcoment(' + idPost + ', ' + id + ', \'' + url + '\', \'' + urlRed + '\')">Bình luận</button>' +
                    '</form>' +
                '</div>' +
            '</div>' +
        '</div>');
    CKEDITOR.replace('comment-'+id, {
        customConfig: '/Assets/ckeditor/config-comment.js'
    });
}
function Postcoment(idPost, idParent, url, urlRed) {
    //var comment = $('#comment-' + idParent).val();
    var comment = CKEDITOR.instances['comment-' + idParent].getData();
    //alert(comment);
    $.ajax({
        type: 'GET',
        data: { aidPost: idPost, aidParent: idParent, acomment: comment },
        url: url,
        success: function (data) {
            if (data != null && data != '') {
                var obj = JSON.parse(data);
                var type = obj[0].Type_Comment;
                var urlPicture = "";
                switch (type) {
                    case "employer": {
                        urlPicture = "/Assets/Upload/Employer/" + obj[0].EMPLOYER.EMPLOYER_LOGO;
                        break;
                    }
                    case "student": {
                        urlPicture = "/Assets/Upload/Student/" + obj[0].STUDENT.CLOSEUP_PICTURE_1;
                        break;
                    }
                    case "admin": {
                        urlPicture = "/Assets/Upload/User/" + obj[0].User.Picture;
                        break;
                    }
                    default: {
                        // do something
                    }
                }
                $('#comment-post' + idParent).remove();
                $('#block-comment-' + idParent).prepend('<div  style="margin: 10px;opacity:0" class="comment-main" id="comment-person-' + obj[0].Id + '">' +
                            '<img src="' + urlPicture + '" style="width: 50px; float: left" />' +
                            '<p style="float: left; padding: 5px">' +
                            comment + '<br />' +
                            '<a class="label label-info" onclick="Comment(' + obj[0].Id + ', ' + obj[0].Id_Post + ', \'' + url + '\', \'' + urlRed + '\')" href="javascript: void(0)">Trả lơi</a>' +
                            '<a id="show-comment-' + obj[0].Id + '" style="margin-left: 4px;" class="label label-primary" onclick="RedComment(' + obj[0].Id + ', ' + obj[0].Id_Post + ',\'' + urlRed + '\', \'' + url + '\')" href="javascript: void(0)">Xem bình luận (' + obj[0].Count_comment + ')</a>' +
                            '</p>' +
                            '<div style="clear: both"></div>' +
                            '<div style="margin-left: 10px" id="block-comment-' + obj[0].Id + '"></div>' +
                        '</div>');
                changeStyle('#comment-person-' + obj[0].Id);
                //$('#comment-' + idParent).val('');
                if (idParent == 0) {
                    CKEDITOR.instances['comment-' + idParent].setData('');
                }
                if (obj[1] != null) {
                    $('#show-comment-' + idParent).html('Xem bình luận (' + obj[1].Count_comment + ')');
                }
            } else {
                //alert("Lỗi thử lại");
                dialog_login("Thông báo", "Vui lòng đăng nhập để bình luận", URLLOGIN);
            }
        }
    });
}


//Orther
function changeStyle(id) {
    var opti = 0;
    var change = setInterval(function () {
        if (opti > 1) {
            clearInterval(change);
        }
        opti = Number(opti) + Number(0.1);
        $(id).css({ 'opacity': opti });
    }, 150);
}
function changeStyleDown(id) {
    var opti = 1;
    var change = setInterval(function () {
        if (opti < 0) {
            clearInterval(change);
            $(id).html('');
        }
        opti = Number(opti) - Number(0.1);
        $(id).css({ 'opacity': opti });
    }, 50);
}
function dialog_dtu(Item) {
    $.confirm({
        title: "DTU",
        content: "DTU",
        theme: 'material',
        type: 'orange',
        animation: 'scale',
        icon: 'fa fa-question',
        closeAnimation: 'scale',
        opacity: 0.5,
        buttons: {
            'cancel': {
                text: 'Đóng',
                btnClass: 'btn-blue',
                action: function () { }
            },
        }
    });
}
function dialog_infor(Content, Title) {
    $.confirm({
        title: Title,
        content: Content,
        theme: 'material',
        type: 'orange',
        animation: 'scale',
        icon: 'fa fa-question',
        closeAnimation: 'scale',
        opacity: 0.5,
        buttons: {
            'cancel': {
                text: 'Đóng',
                btnClass: 'btn-blue',
                action: function () { }
            },
        }
    });
}
//Submit form
function SubmitForm(idForm) {
    $('#' + idForm).submit();
}
//comfirm submit
function dialog_cf_sm(title, content, idForm) {
    $.confirm({
        title: title,
        content: content,
        theme: 'material',
        type: 'orange',
        animation: 'scale',
        icon: 'fa fa-question',
        closeAnimation: 'scale',
        opacity: 0.5,
        buttons: {
            'confirm': {
                text: 'Yes',
                btnClass: 'btn-red',
                action: function () {
                    SubmitForm(idForm);
                }
            },
            'cancel': {
                text: 'No',
                btnClass: 'btn-blue',
                action: function () { }
            },
        }
    });
}
//comfirm
function myconfirm(title, content) {
    $.confirm({
        title: title,
        content: content,
        theme: 'material',
        type: 'orange',
        animation: 'scale',
        icon: 'fa fa-question',
        closeAnimation: 'scale',
        opacity: 0.5,
        buttons: {
            'cancel': {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () { }
            },
        }
    });
}
function dialog_login(title, content, url) {
    $.confirm({
        title: title,
        content: content,
        theme: 'material',
        type: 'orange',
        animation: 'scale',
        icon: 'fa fa-question',
        closeAnimation: 'scale',
        opacity: 0.5,
        buttons: {
            'confirm': {
                text: 'Đăng nhập',
                btnClass: 'btn-red',
                action: function () {
                    window.location = url;
                }
            },
            'cancel': {
                text: 'Hủy',
                btnClass: 'btn-blue',
                action: function () { }
            },
        }
    });
}
function dialog_cf(title, content, url) {
    $.confirm({
        title: title,
        content: content,
        theme: 'material',
        type: 'orange',
        animation: 'scale',
        icon: 'fa fa-question',
        closeAnimation: 'scale',
        opacity: 0.5,
        buttons: {
            'confirm': {
                text: 'Yes',
                btnClass: 'btn-red',
                action: function () {
                    window.location = url;
                }
            },
            'cancel': {
                text: 'No',
                btnClass: 'btn-blue',
                action: function () { }
            },
        }
    });
}


// Admin
function ActivePost(id, url) {
    $.ajax({
        type: 'GET',
        data: { aid: id },
        url: url,
        success: function (data) {
            if (data != null) {
                if (data == "true") {
                    $("#Active-" + id).html(
                        '<i class="fa fa-fw fa-check-square-o"></i> Đã duyệt');
                    $("#Active-" + id).attr("class", "btn btn-success btn-sm");
                } else {
                    $("#Active-" + id).html(
                        '<i class="fa fa-fw fa-minus-circle"></i> Chưa duyệt');
                    $("#Active-" + id).attr("class", "btn btn-danger btn-sm");
                }
                $('#Tb-Table').attr("style", "");
                $('#Tb-Table').attr("class", "alert alert-success");
                $('#Tb-Content').html('Thay đổi thành công');
                setTimeout(function () {
                    $('#Tb-Table').attr("style", "display: none");
                }, 1000);
            } else {
                $('#Tb-Table').attr("style", "");
                $('#Tb-Table').attr("class", "alert alert-danger");
                $('#Tb-Content').html('Thay đổi thất bại');
                setTimeout(function () {
                    $('#Tb-Table').attr("style", "display: none");
                }, 1000);
            }
        }
    });
}
function ActiveRecruitment(id, url) {
    $('.Tb-Active').html('');
    $.ajax({
        type: 'GET',
        data: { aid: id },
        url: url,
        success: function (data) {
            if (data != null) {
                if (data == "true") {
                    $("#Active-" + id).html(
                        '<i class="fa fa-fw fa-check-square-o"></i> Đã duyệt');
                    $("#Active-" + id).attr("class", "btn btn-success btn-sm");
                } else {
                    $("#Active-" + id).html(
                        '<i class="fa fa-fw fa-minus-circle"></i> Chưa duyệt');
                    $("#Active-" + id).attr("class", "btn btn-danger btn-sm");
                }
                $('#Tb-Table').attr("style", "");
                $('#Tb-Table').attr("class", "alert alert-success");
                $('#Tb-Content').html('Thay đổi thành công');
                setTimeout(function () {
                    $('#Tb-Table').attr("style", "display: none");
                }, 1000);
            } else {
                $('#Tb-Table').attr("style", "");
                $('#Tb-Table').attr("class", "alert alert-danger");
                $('#Tb-Content').html('Thay đổi thành công');
                setTimeout(function () {
                    $('#Tb-Table').attr("style", "display: none");
                }, 1000);
            }
        }
    });
}

//chosse select -> view
$(document).ready(function () {
    function chaneHref(Item) {
        alert("fdff");
    }
});
// chosse
$(document).ready(function () {
    $('.checkbox').on('click', function () {
        if ($('.chosseall').is(":checked")) {
            $('.checkboxId').prop('checked', true);
        } else {
            $('.checkboxId').prop('checked', false);
        }
    });
});

function checkbox() {
    if ($('.checkboxId').is(":checked")) {
        $('.checkboxId').iCheck('uncheck');
        $('.checkbox-toggle').html('<i class="fa fa-square-o"></i>');
    } else {
        $('.checkboxId').iCheck('check');
        $('.checkbox-toggle').html('<i class="fa fa-check-square-o"></i>');
    }
}

// Active
function Active(id, url) {
    $.ajax({
        type: 'GET',
        data: { aid: id},
        url: url,
        success: function (data) {
            if (data != null) {
                if (data == "true") {
                    $(".Active" + id).html(
                        '<input type="checkbox" class="switch-input">' +
                        '<span data-on="On" data-off="Off" class="switch-label"></span>'+
                        '<span class="switch-handle"></span>');
                } else {
                    $(".Active" + id).html(
                        '<input checked type="checkbox" class="switch-input">' +
                        '<span data-on="On" data-off="Off" class="switch-label"></span>' +
                        '<span class="switch-handle"></span>');
                }
                $('.Tb-Active').html(
                    '<div class="alert alert-success alert-dismissible">'+
                        '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>'+
                        '<h4 style="text-align:center"><i class="icon fa fa-check"></i> Thay đổi thành công</h4>'+
                    '</div>')
            } else {
                $.confirm({
                    title: "Error",
                    content: "Error!!! Try again or Refresh browser, Please.",
                    theme: 'material',
                    type: 'orange',
                    animation: 'scale',
                    icon: 'fa fa-question',
                    closeAnimation: 'scale',
                    opacity: 0.5,
                    buttons: {
                        'Ok': {
                            text: 'No',
                            btnClass: 'btn-blue',
                            action: function () { }
                        },
                    }
                });
            }
        }
    });
}