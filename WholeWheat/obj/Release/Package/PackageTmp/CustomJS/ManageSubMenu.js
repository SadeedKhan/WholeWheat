$(document).ready()
{
    SubMenuDetail_PartialView();
}

function InsertUpdateSubMenu() {
    var submenuid = $('#txtSubMenuId').val();
    var submenuname = $('#txtsubmenuname').val();
    var menuid = $('#ddlmenu').val();
    var code = $('#txtsubmenucode').val();
    var price = $('#txtprice').val();
    var file = $("#file").get(0).files;
    var filename = $('input[type=file]').val().replace(/C:\\fakepath\\/i, '');
    if (filename == "") {
        filename = $('#txtimagename').val();
    }
    var color = $('input[name=color]:checked').val();
    var description = $('#txtdescription').val();
    var status = $('#ddlstatus').val();
    if ($('#txtsubmenuname').val() == "") {
        $.notify('Enter Sub Menu Name', { position: "top center", className: "warn" });
    }
    else if ($('#ddlmenu').val() == "") {
        $.notify('Select Menu', { position: "top center", className: "warn" });
    }
    else if ($('#txtsubmenucode').val() == "") {
        $.notify('Enter Code', { position: "top center", className: "warn" });
    }
    else if ($('#txtprice').val() == "") {
        $.notify('Enter Price', { position: "top center", className: "warn" });
    }
    else if ($('input[name=color]:checked').length == "") {
        $.notify('Select Color', { position: "top center", className: "warn" });
    }
    else if ($('#txtdescription').val() == "-1") {
        $.notify('Enter Description', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $("#AddSubMenu").hide();
        $('.modal-backdrop').remove();
        var data = new FormData;
        data.append("SubMenuID", submenuid);
        data.append("SubMenuName", submenuname);
        data.append("MenuID", menuid);
        data.append("Code", code);
        data.append("Price", price);
        data.append("File", file[0]);
        data.append("FileName", filename);
        data.append("Color", color);
        data.append("Description", description);
        data.append("StatusID", status);
        $.ajax({
            url: "/ManageSubMenu/AddSubMenu",
            data: JSON.stringify({
            }),
            type: "POST",
            data: data,
            contentType: false,
            processData: false
        }).done(function (data) {
            if (data.Success == true) {

                $response = JSON.parse(data.Response);
                if ($response.pFlag == "1") {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "success" });
                }
                else {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "error" });
                }
            }
            SubMenuDetail_PartialView();
            ClearFeilds();
        });
    }
}

function SubMenuDetail_PartialView() {
    var Status = 0;
    $.ajax({
        url: "/ManageSubMenu/SubMenuDetail_PartialView",
        data: JSON.stringify({
            StatusID: Status
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#SubMenuDetail_PartialView').html(data);
        });
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

$(document).on('click', '.EditSubMenu', function (event) {
    console.log("clicked");
    ClearFeilds();
    $('#txtSubMenuId').val($(this).attr('data-SubMenuid'))
    $('#ddlmenu').val($(this).attr('data-Menuid'));
    $('#txtsubmenuname').val($(this).attr('data-SubMenuName'));
    $('#txtsubmenucode').val($(this).attr('data-Code'));
    $('#txtprice').val($(this).attr('data-Price'));
    $('#txtimagename').val($(this).attr('data-Image'));
    $('input[name=color][value=' + $(this).attr('data-Color') + ']').prop('checked', true);
    $('#txtdescription').val($(this).attr('data-description'));
    $('#ddlstatus').val($(this).attr('data-Status-Id'));
});
$(document).on('click', '.open-modalimage', function (event) {
    console.log("clicked");
    var DocumentPath = $(this).attr('data-image');
    $("#showimg").attr("src", DocumentPath);
});

function ClearFeilds() {
    $('#txtMenuId').val(0);
    $('#ddlstatus').val(-1);
    $('#ddlmenu').val(null);
    $('#txtsubmenucode').val(null);
    $('#txtprice').val(null);
    $('input:radio[name=color]').each(function () { $(this).prop('checked', false); });
}
