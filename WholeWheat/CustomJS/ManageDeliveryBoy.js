$(document).ready()
{
    DeliveryBoyDetail_PartialView(0);
}

function InsertUpdateDeliveryBoy() {
    var deliveryboyid = $('#txtDeliveryBoyId').val();
    var deliveryboyname = $('#txtdeliveryboyname').val();
    var phone = $('#txtdeliveryboyphone').val();
    var email = $('#txtdeliveryboyemail').val();
    var cnic = $('#txtdeliveryboycnic').val();
    var description = $('#txtdeliveryboydescription').val();
    var address = $("#txtdeliveryboyaddress").val();
    var status = $('#ddlstatus').val();
    if ($('#txtdeliveryboyname').val() == "") {
        $.notify('Enter DeliveryBoy Name', { position: "top center", className: "warn" });
    }
    else if ($('#txtdeliveryboyphone').val() == "") {
        $.notify('Enter Phone No', { position: "top center", className: "warn" });
    }
    else if ($('#txtdeliveryboyemail').val() == "") {
        $.notify('Enter Email', { position: "top center", className: "warn" });
    }
    else if ($('#txtdeliveryboycnic').val() == "") {
        $.notify('Enter Cnic', { position: "top center", className: "warn" });
    }
    else if ($('#txtdeliveryboydescription').val() == "") {
        $.notify('Enter Description', { position: "top center", className: "warn" });
    }
    else if ($('#txtdeliveryboyaddress').val() == "-1") {
        $.notify('Enter Address', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $.ajax({
            url: "/ManageDeliveryBoy/AddDeliveryBoy",
            data: JSON.stringify({
                DeliveryBoyID: deliveryboyid,
                DeliveryBoyName: deliveryboyname,
                Phone: phone,
                Email: email,
                Cnic: cnic,
                Description: description,
                Address: address,
                StatusId: status
            }),
            type: "POST",
            dataType: "json",
            async: true,
            cache: false,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            if (data.Success == true) {
                $("#AddDeliveryBoy").hide();
                $('.modal-backdrop').remove();
                $response = JSON.parse(data.Response);
                if ($response.pFlag == "1") {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "success" });
                }
                else {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "error" });
                }
            }
            DeliveryBoyDetail_PartialView();
            ClearFeilds();
        });
    }
}
function DeliveryBoyDetail_PartialView() {
    var Status = 0;
    $.ajax({
        url: "/ManageDeliveryBoy/DeliveryBoyDetail_PartialView",
        data: JSON.stringify({
            StatusID: Status
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#DeliveryBoyDetail_PartialView').html(data);
        });
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

$(document).on('click', '.EditDeliveryBoy', function (event) {
    ClearFeilds();
    $('#txtDeliveryBoyId').val($(this).attr('data-DeliveryBoyid'));
    $('#txtdeliveryboyname').val($(this).attr('data-DeliveryBoyName'));
    $('#txtdeliveryboyphone').val($(this).attr('data-Phone'));
    $('#txtdeliveryboyemail').val($(this).attr('data-Email'));
    $('#txtdeliveryboycnic').val($(this).attr('data-Cnic'));
    $('#txtdeliveryboydescription').val($(this).attr('data-Description'));
    $("#txtdeliveryboyaddress").val($(this).attr('data-Address'));
    $('#ddlstatus').val($(this).attr('data-StatusId'));
});

function ClearFeilds() {
    $('#txtDeliveryBoyId').val(0);
    $('#txtdeliveryboyname').val(null);
    $('#txtdeliveryboyphone').val(null);
    $('#txtdeliveryboyemail').val(null);
    $('#txtdeliveryboycnic').val(null);
    $('#txtdeliveryboydescription').val(null);
    $("#txtdeliveryboyaddress").val(null);
    $('#ddlstatus').val(-1);
}
