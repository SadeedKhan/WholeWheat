$(document).ready()
{
    ExpenseHeadDetail_PartialView(0);
}

function InsertUpdateExpenseHead() {
    var expenseheadid = $('#txtExpenseHeadId').val();
    var expenseheadname = $('#txtexpenseheadname').val();
    var status = $('#ddlstatus').val();
    if ($('#txtexpenseheadname').val() == "") {
        $.notify('Enter Expense Head', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $.ajax({
            url: "/ManageExpenseHead/AddExpenseHead",
            data: JSON.stringify({
                ExpenseHeadID: expenseheadid,
                ExpenseHeadName: expenseheadname,
                StatusId: status
            }),
            type: "POST",
            dataType: "json",
            async: true,
            cache: false,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            if (data.Success == true) {
                $("#AddExpenseHead").hide();
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
            ExpenseHeadDetail_PartialView();
        });
    }
}
function ExpenseHeadDetail_PartialView() {
    var Status = 0;
    $.ajax({
        url: "/ManageExpenseHead/ExpenseHeadDetail_PartialView",
        data: JSON.stringify({
            StatusID: Status
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#ExpenseHeadDetail_PartialView').html(data);
    });
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

$(document).on('click', '.EditExpenseHead', function (event) {
    console.log("clicked");
    ClearFeilds();
    $('#txtExpenseHeadId').val($(this).attr('data-ExpenseHeadid'));
    $('#txtexpenseheadname').val($(this).attr('data-ExpenseHeadName'));
    $('#ddlstatus').val($(this).attr('data-StatusId'));
});

function ClearFeilds() {
    $('#txtExpenseHeadId').val(0);
    $('#txtexpenseheadname').val(null);
    $('#ddlstatus').val(-1);
}
