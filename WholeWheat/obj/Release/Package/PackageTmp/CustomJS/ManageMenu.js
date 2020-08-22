$(document).ready()
{
    MenuDetail_PartialView(0);
}

function InsertUpdateMenu() {
    var menuid = $('#txtMenuId').val();
    var menuname = $('#txtmenuname').val();
    var status = $('#ddlstatus').val();
    if ($('#txtmenuname').val() == "") {
        $.notify('Enter Menu Name', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $.ajax({
            url: "/ManageMenu/AddMenu",
            data: JSON.stringify({
                Menuid: menuid,
                Menuname: menuname,
                StatusId: status
            }),
            type: "POST",
            dataType: "json",
            async: true,
            cache: false,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            if (data.Success == true) {
                $("#AddMenu").hide();
                $('.modal-backdrop').remove();
                $response = JSON.parse(data.Response);
                if ($response.pFlag == "1") {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "success" });
                }
                else {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "error" });
                }
            }
            ClearFeilds();
            MenuDetail_PartialView();
            });
    }
}
function MenuDetail_PartialView() {
    var Status = 0;
    $.ajax({
        url: "/ManageMenu/MenuDetail_PartialView",
        data: JSON.stringify({
            StatusID: Status
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#MenuDetail_PartialView').html(data);
        });
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

    $(document).on('click', '.EditMenu', function (event) {
        console.log("clicked");
        ClearFeilds();
        $('#txtMenuId').val($(this).attr('data-Menuid'));
        $('#txtmenuname').val($(this).attr('data-MenuName'));
        $('#ddlstatus').val($(this).attr('data-Status-Id'));
    });

    function ClearFeilds() {
        $('#txtMenuId').val(0);
        $('#txtmenuname').val(null);
        $('#ddlstatus').val(-1);
    }
