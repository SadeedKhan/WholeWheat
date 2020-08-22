$(document).ready()
{
    ExpenseNameDetail_PartialView();
}

function InsertUpdateExpenseName() {
    var expensenameid = $('#txtExpenseNameId').val();
    var expensename = $('#txtexpensename').val();
    var status = $('#ddlstatus').val();
    if ($('#txtexpensename').val() == "") {
        $.notify('Enter Expense Name', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $.ajax({
            url: "/ManageExpenseName/AddExpenseName",
            data: JSON.stringify({
                ExpenseNameID: expensenameid,
                ExpenseName: expensename,
                StatusId: status
            }),
            type: "POST",
            dataType: "json",
            async: true,
            cache: false,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            if (data.Success == true) {
                $("#AddExpenseName").hide();
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
            ExpenseNameDetail_PartialView();
        });
    }
}
function ExpenseNameDetail_PartialView() {
    $.ajax({
        url: "/ManageExpenseName/ExpenseNameDetail_PartialView",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#ExpenseNameDetail_PartialView').html(data);
    });
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

$(document).on('click', '.EditExpenseName', function (event) {
    console.log("clicked");
    ClearFeilds();
    $('#txtExpenseNameId').val($(this).attr('data-ExpenseNameid'));
    $('#txtexpensename').val($(this).attr('data-ExpenseName'));
    $('#ddlstatus').val($(this).attr('data-StatusId'));
});

function ClearFeilds() {
    $('#txtExpenseNameId').val(0);
    $('#txtexpensename').val(null);
}
