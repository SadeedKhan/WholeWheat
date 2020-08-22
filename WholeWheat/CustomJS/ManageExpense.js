$(document).ready()
{
    $('#txtexpensedate').datepicker({
        todayHighlight: true
    });
    $('#txtexpensedate').datepicker('setDate', new Date());
    ExpenseDetail_PartialView();
    $('#txtexpensename').select2({
        width: '100%'
    });
    $("#txtexpensename").change(function () {
        var expensenameid = $("#txtexpensename").val();
        $("#txtexpensereference").val(expensenameid);
    });
}

function InsertUpdateExpense() {
    var filename = "";
    var expenseid = $('#txtExpenseId').val();
    var expensenameid = $('#txtexpensename').val();
    var expensedate = $('#txtexpensedate').val();
    var referenceno = $('#txtexpensereference').val();
    var expenseheadid = $('#ddlexpensehead').val();
    var expenseamount = $('#txtexpenseamount').val();
    var file = $("#expensefile").get(0).files;
    filename = $('input[type=file]').val().replace(/C:\\fakepath\\/i, '');
    if (filename == "") {
        filename = $('#txtexpenseimagename').val();
    }
    var notes = $('#txtnote').val();
    var status = $('#ddlstatus').val();
    if ($('#txtexpensename').val() == "") {
        $.notify('Enter Expense Name', { position: "top center", className: "warn" });
    }
    else if ($('#txtexpensedate').val() == "") {
        $.notify('Select Date', { position: "top center", className: "warn" });
    }
    else if ($('#txtexpensereference').val() == "") {
        $.notify('Enter Reference No', { position: "top center", className: "warn" });
    }
    else if ($('#ddlexpensehead').val() == "") {
        $.notify('Select Expense Head', { position: "top center", className: "warn" });
    }
    else if ($('#txtexpenseamount').val() == "") {
        $.notify('Enter Amount', { position: "top center", className: "warn" });
    }
    else if ($('#ddlstatus').val() == "-1") {
        $.notify('Select Status', { position: "top center", className: "warn" });
    }
    else {
        $("#AddExpense").hide();
        $('.modal-backdrop').remove();
        var data = new FormData;
        data.append("ExpenseID", expenseid);
        data.append("ExpenseNameID", expensenameid);
        data.append("ExpenseDate", expensedate);
        data.append("ReferenceNo", referenceno);
        data.append("ExpenseHeadID", expenseheadid);
        data.append("ExpenseAmount", expenseamount);
        data.append("File", file[0]);
        data.append("FileName", filename);
        data.append("Notes", notes);
        data.append("StatusID", status);
        $.ajax({
            url: "/ManageExpense/AddExpense",
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
            ExpenseDetail_PartialView();
            ClearFeilds();
        });
    }
}

function ExpenseDetail_PartialView() {
    var Status = 0;
    $.ajax({
        url: "/ManageExpense/ExpenseDetail_PartialView",
        data: JSON.stringify({
            StatusID: Status
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#ExpenseDetail_PartialView').html(data);
    });
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

$(document).on('click', '.EditExpense', function (event) {
    console.log("clicked");
    ClearFeilds();
    $('#txtExpenseId').val($(this).attr('data-expenseid'))
    $('#txtexpensename').val($(this).attr('data-expensenameid')).trigger('change');
    $('#txtexpensedate').val($(this).attr('data-expensedate'));
    $('#txtexpensereference').val($(this).attr('data-referenceno'));
    $('#ddlexpensehead').val($(this).attr('data-expenseheadid'));
    $('#txtexpenseamount').val($(this).attr('data-amount'));
    $('#txtexpenseimagename').val($(this).attr('data-image'));
    var image = $(this).attr('data-image');
    $('#txtnote').val($(this).attr('data-notes'));
    $('#ddlstatus').val($(this).attr('data-statusid'));
});
$(document).on('click', '.open-modalimage', function (event) {
    console.log("clicked");
    var DocumentPath = $(this).attr('data-image');
    $("#showimg").attr("src", DocumentPath);
});

function ClearFeilds() {
    $('#txtExpenseId').val("0")
    $('#txtexpensename').val(null);
    $('#txtexpensedate').val(null);
    $('#txtexpensereference').val(null);
    $('#ddlexpensehead').val(null);
    $('input[type=file]').val(null);
    $('#txtexpenseamount').val(null);
    $('#txtexpenseimagename').val(null);
    $('#txtnote').val(null);
    $('#ddlstatus').val("-1");
}
