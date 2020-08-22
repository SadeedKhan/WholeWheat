$(document).ready()
{
    CustomerDetail_PartialView(0);
}

function InsertUpdateCustomer() {
    var customerid = $('#txtCustomerId').val();
    var customername = $('#txtcustomername').val();
    var phone = $('#txtcustomerphone').val();
    var email = $('#txtcustomeremail').val();
    var description = $('#txtcustomerdescription').val();
    var address = $("#txtcustomeraddress").val();
    var status = $('#ddlstatus').val();
    if ($('#txtcustomername').val() == "") {
        $.notify('Enter Customer Name', { position: "top center", className: "warn" });
    }
    else if ($('#txtcustomerphone').val() == "") {
        $.notify('Enter Phone No', { position: "top center", className: "warn" });
    }
    else if ($('#txtcustomeremail').val() == "") {
        $.notify('Enter Email', { position: "top center", className: "warn" });
    }
    else if ($('#txtcustomerdescription').val() == "") {
        $.notify('Enter Description', { position: "top center", className: "warn" });
    }
    else if ($('#txtcustomeraddress').val() == "") {
        $.notify('Enter Address', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $.ajax({
            url: "/ManageCustomer/AddCustomer",
            data: JSON.stringify({
                CustomerID: customerid,
                CustomerName: customername,
                Phone: phone,
                Email: email,
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
                $("#AddCustomer").hide();
                $('.modal-backdrop').remove();
                $response = JSON.parse(data.Response);
                if ($response.pFlag == "1") {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "success" });
                }
                else {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "error" });
                }
            }
            CustomerDetail_PartialView();
            ClearFeilds();
            });
    }
}
function CustomerDetail_PartialView() {
    var Status = 0;
    $.ajax({
        url: "/ManageCustomer/CustomerDetail_PartialView",
        data: JSON.stringify({
            StatusID: Status
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#CustomerDetail_PartialView').html(data);
        })
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

    $(document).on('click', '.EditCustomer', function (event) {
        ClearFeilds();
        $('#txtCustomerId').val($(this).attr('data-Customerid'));
        $('#txtcustomername').val($(this).attr('data-CustomerName'));
        $('#txtcustomerphone').val($(this).attr('data-Phone'));
        $('#txtcustomeremail').val($(this).attr('data-Email'));
        $('#txtcustomerdescription').val($(this).attr('data-Description'));
        $("#txtcustomeraddress").val($(this).attr('data-Address'));
        $('#ddlstatus').val($(this).attr('data-StatusId'));
    });

    function ClearFeilds() {
        $('#txtCustomerId').val(0);
        $('#txtcustomername').val(null);
        $('#txtcustomerphone').val(null);
        $('#txtcustomeremail').val(null);
        $('#txtcustomerdescription').val(null);
        $("#txtcustomeraddress").val(null);
        $('#ddlstatus').val(-1);
    }
