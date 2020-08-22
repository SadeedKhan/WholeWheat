$(document).ready(function () {
    MenuList_PartialView();
    $('.ReturnChange').show();
    SaleDetial_PartialView(0);
    CalculateTotal();
})
var TransactionNo = "";

//  **********************Partial Views************************************/

function SaleDetial_PartialView(SaleID) {
    var SaleID = SaleID;
    $.ajax({
        url: "/POS/SaleDetail_PartialView",
        data: JSON.stringify({
            SaleID: SaleID
        }),
        type: "POST",
        dataType: 'html',
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#SaleDetail_PartialView').html(data);
        CalculateTotal();
    })
}

function MenuList_PartialView() {
    var StatusID = 0;
    $.ajax({
        url: "/POS/MenuList_PartialView",
        data: JSON.stringify({
            StatusID: StatusID
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#MenuList_PartialView').html(data);
        SubMenuList_PartialView(0)
    });
}

function SubMenuList_PartialView(MenuID) {
    if (MenuID == null || MenuID == "") {
        MenuID = 0;
    }
    $.ajax({
        url: "/POS/SubMenuList_PartialView",
        data: JSON.stringify({
            MenuID: MenuID
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#SubMenuList_PartialView').html(data);
    });
}

function TodayPendingSaleDetail_PartialView() {
    $.ajax({
        url: "/POS/TodayPendingSaleDetail_PartialView",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#PendingSaleDetail_PartialView').html(data);
    });
}

function PendingSaleDetail_PartialView() {
    $.ajax({
        url: "/POS/PendingSaleDetail_PartialView",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#PendingSaleDetail_PartialView').html(data);
    });
}

function PendingDineInSaleDetail_PartialView() {
    $.ajax({
        url: "/POS/PendingDineInSaleDetail_PartialView",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#PendingDineInSaleDetail_PartialView').html(data);
    });
}

//  **********************Delete Record************************************/

function delete_record(SaleID) {
    $('#pendingsaledetail').modal('hide');
    $('#pendingdineinsaledetail').modal('hide');
    swal({
        title: "Enter Password!",
        type: "input",
        inputType: "password",
        showCancelButton: true,
        closeOnConfirm: false,
        inputPlaceholder: "Enter Password"
    }, function (inputValue) {
        if (inputValue === false)
            return false;
        if (inputValue === "") {
            swal.showInputError("You need to write password!");
            return false;
        }
        if (inputValue === "I88") {
            $.ajax({
                url: "/Sales/DeleteRecord",
                data: JSON.stringify({
                    SaleID: SaleID
                }),
                type: "POST",
                dataType: "json",
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                if (data.Success == true) {
                    if ($('#viewtodaysaletab').hasClass('Hold p1') == true) {
                        TodayPendingSaleDetail_PartialView();
                    }
                    else {
                        PendingSaleDetail_PartialView();
                    }
                    PendingDineInSaleDetail_PartialView();
                    $response = JSON.parse(data.Response);
                    if ($response.pFlag == "1") {
                        swal('Deleted!', $response.pFlag_Desc, "success");
                    }
                    else {
                        swal('Failed!', $response.pFlag_Desc, "error");
                    }
                }
            });
        }
        else
            swal('Failed!', 'Incorrect Password', "error");
    })
};

//  **********************Sale Type Pop Up************************************/

function SaleTypeModel() {
    var saletype = $('#ddlsaletype').val();
    if (saletype === '3') {
        $('#AddCustomer').modal({ show: true });
    }
    if (saletype === '2') {
        $('#AddCustomerName').modal({ show: true });
    }
    if (saletype === '1') {
        $('#AddTableNo').modal({ show: true });
    }
}

function CustomerNameModel() {
    $('#AddCustomerName').modal('hide');
}

function TableNoModel() {
    $('#AddTableNo').modal('hide');
}
//  **********************Add To Cart************************************/
$(document).on('click', '.add_to_cart', function (event) {
    $('.removerow').remove();
    $('#txtSubMenuId').val($(this).attr('data-submenuid'));
    $('#txtSubMenuPrice').val($(this).attr('data-sale_price'));
    $('#txtSubMenuTypeId').val($(this).attr('data-sub_menu_type_id'));
    $('#txtInStock').val($(this).attr('data-InStock'));
    $('#txtSubMenuName').val($(this).attr('data-sub_menu_name'));
    var submenuname = $(this).attr('data-sub_menu_name');
    var SubMenuId = $(this).attr('data-submenuid');
    var Quantity = 1;
    var Price = parseFloat($(this).attr('data-price'));
    var NetTotal = Quantity * Price;
    var SaleItem = $(this).attr('data-sub_menu_name');
    var RowExist = 0;
    var txtSaleDetailQuantity = null;
    var CurrentRow = null;
    //Check if Row Already Exist
    $("#tbl_cart_list tbody tr").each(function (index, element) {
        var detail = $(element).attr('data-submenuid');
        if ($(element).attr('data-submenuid') == SubMenuId) {
            txtSaleDetailQuantity = $(element).find(".SaleDetailQuantity");
            RowExist = 1;
            CurrentRow = element;
        }
    });
    if (RowExist === 1) {
        txtSaleDetailQuantity.val(parseInt(txtSaleDetailQuantity.val()) + 1);
        NetTotal = parseInt($(CurrentRow).attr('data-Price')) * $(CurrentRow).find('.SaleDetailQuantity').val();
        $(CurrentRow).find('.NetTotal').text(NetTotal);
        $(txtSaleDetailQuantity).trigger("change");
        $(CurrentRow).closest('tr').removeClass('bg-info');
        $(CurrentRow).closest('tr').addClass('SaleDetailRow');
    }
    else {
        $('#tbl_cart_list tbody').append('<tr  class="SaleDetailRow"  data-sub_menu_name="' + submenuname + '" data-submenuid="' + SubMenuId + '" data-Price="' + Price + '"><td><a  href="javascript:void(0)"><span class="fa-stack fa-sm RemoveSaleDetailRow" ><i class="fa fa-circle fa-stack-2x delete-product"></i><i class="fa fa-times fa-stack-1x fa-fw fa-inverse"></i></span></a></td><td><span>' + submenuname + '</span></td><td><span>' + Price + '</span></td><td><div class="nopadding"><a href="javascript:void(0)"><span  class="fa-stack fa-sm decbutton"><i class="fa fa-square fa-stack-2x light-grey"></i><i class="fa fa-minus fa-stack-1x fa-inverse white"></i></span></a><input type="text" id="txtSaleDetailQuantity"  class="form-control SaleDetailQuantity" value="' + Quantity + '" placeholder="0" maxlength="2"><a href="javascript:void(0)" ><span data-quantity="' + Quantity + '" class="fa-stack fa-sm incbutton"><i class="fa fa-square fa-stack-2x light-grey"></i><i class="fa fa-plus fa-stack-1x fa-inverse white"></i></span></a></div></td><td><span class="subtotal NetTotal">' + NetTotal + '</span></td></tr>');
    }
    if ($('#ItemsNum span,#ItemsNum2 span').text() == "") {
        $('#ItemsNum span,#ItemsNum2 span').text(1);
    }
    else {
        var items = parseInt($('#ItemsNum span').text());
        $('#ItemsNum span,#ItemsNum2 span').text("0");
        var inc = items + 1;
        $('#ItemsNum span,#ItemsNum2 span').text(parseInt(inc));
    }
    CalculateTotal();
});

//*********************************Increase And Decrement Quantity**********************

$(document).on("click", ".incbutton", function () {
    var $button = $(this);
    var oldValue = $button.parent().parent().find("input").val();
    var newVal = parseFloat(oldValue) + 1;
    $button.parent().parent().find("input").val(newVal);
    row = $(this).parents("tr");
    var NetTotal = parseInt(row.attr('data-Price')) * row.find('.SaleDetailQuantity').val();
    row.find('.NetTotal').text(NetTotal);
    CalculateTotal();
    var items = parseInt($('#ItemsNum span').text());
    $('#ItemsNum span,#ItemsNum2 span').text("");
    var inc = items + 1;
    $('#ItemsNum span,#ItemsNum2 span').text(parseInt(inc));
    $(this).closest('tr').removeClass('bg-info');
    $(this).closest('tr').addClass('SaleDetailRow');
});

$(document).on("click", ".decbutton", function () {
    var $button = $(this);
    var oldValue = $button.parent().parent().find("input").val();
    if (oldValue > 1) {
        var newVal = parseFloat(oldValue) - 1;
    }
    else { newVal = 1; } $button.parent().parent().find("input").val(newVal);
    row = $(this).parents("tr");
    var NetTotal = parseInt(row.attr('data-Price')) * row.find('.SaleDetailQuantity').val();
    row.find('.NetTotal').text(NetTotal);
    CalculateTotal();
    var items = parseInt($('#ItemsNum span').text());
    $('#ItemsNum span,#ItemsNum2 span').text("");
    if (oldValue > 1) {
        var inc = items - 1;
    }
    else
        var inc = items;
    $('#ItemsNum span,#ItemsNum2 span').text(parseInt(inc));
    var CurrentRow = $(this).parents('tr');
    if (CurrentRow.find('.SaleDetailQuantity').val() > 1) {
        $(this).closest('tr').removeClass('bg-info');
        $(this).closest('tr').addClass('SaleDetailRow');
    }
});

//************************************search sub menu*********************************
$("#searchProd").keyup(function () {
    var filter = $(this).val();
    $("#productList2 #menuname").each(function () {
        if ($(this).text().search(new RegExp(filter, "i")) < 0) {
            $(this).parent().parent().parent().hide();
        }
        else {
            $(this).parent().parent().parent().show();
        }
    });
});

//  **********************select menu************************************/

$(document).on("click", ".categories", function () {
    $(this).parent().children().removeClass('selectedGat');
    $(this).addClass('selectedGat');
    var MenuID = $(this).attr('data-id');
    SubMenuList_PartialView(MenuID);
});

//  **********************Calculate menu************************************/
function CalculateTotal() {
    var Total = 0.0;
    $('.NetTotal').each(function (index, element) {
        var NetTotal = $('.NetTotal').val();
        Total = parseFloat($(this).text()) + Total;
    });
    $('#Subtot').text(Total);
    total_change();
}

//********************************* function to calculate a percentage from a number************************************/
function percentage(tot, n) {
    var perc;
    perc = ((parseFloat(tot) * (parseFloat(n ? n : 0) * 0.01)));
    return perc;
}

// ********************************* change calculations************************************/
$('#Paid').on('keyup', function () {
    var change = -(parseFloat($('#total').text()) - parseFloat($(this).val()));
    if (change < 0) {
        $('#ReturnChange span').text(change.toFixed(3));
        $('#ReturnChange span').addClass("red");
        $('#ReturnChange span').removeClass("light-blue");
    } else {
        $('#ReturnChange span').text(change.toFixed(3));
        $('#ReturnChange span').removeClass("red");
        $('#ReturnChange span').addClass("light-blue");
    }
});
//********************************* function to calculate the total number************************************/

function total_change() {
    var tot;
    if (($('.TAX').val().indexOf('%') == -1) && ($('.Remise').val().indexOf('%') == -1)) {
        tot = parseFloat($('#Subtot').text().replace(/ /g, '')) + parseFloat($('.TAX').val() ? $('.TAX').val() : 0);
        $('#taxValue').text('Rs');
        $('#RemiseValue').text('Rs');
        $('#DeliveryValue').text('Rs');
        tot = tot - parseFloat($('.Remise').val() ? $('.Remise').val() : 0);
        tot = tot + $('.Delivery').val();
    } else if (($('.TAX').val().indexOf('%') != -1) && ($('.Remise').val().indexOf('%') == -1)) {
        tot = parseFloat($('#Subtot').text()) + percentage($('#Subtot').text(), $('.TAX').val());
        $('#taxValue').text(percentage($('#Subtot').text(), $('.TAX').val()).toFixed(3) + ' Rs');
        $('#RemiseValue').text('Rs');
        $('#DeliveryValue').text('Rs');
        tot = tot - parseFloat($('.Remise').val() ? $('.Remise').val() : 0);
    } else if (($('.TAX').val().indexOf('%') != -1) && ($('.Remise').val().indexOf('%') != -1)) {
        tot = parseFloat($('#Subtot').text()) + percentage($('#Subtot').text(), $('.TAX').val());
        $('#taxValue').text(percentage($('#Subtot').text(), $('.TAX').val()).toFixed(3) + ' Rs');
        tot = tot - percentage($('#Subtot').text(), $('.Remise').val());
        $('#RemiseValue').text(percentage($('#Subtot').text(), $('.Remise').val()).toFixed(3) + ' Rs');
    } else if (($('.TAX').val().indexOf('%') == -1) && ($('.Remise').val().indexOf('%') != -1)) {
        tot = parseFloat($('#Subtot').text()) + parseFloat($('.TAX').val() ? $('.TAX').val() : 0);
        tot = tot - percentage($('#Subtot').text(), $('.Remise').val());
        $('#taxValue').text('Rs');
        $('#RemiseValue').text(percentage($('#Subtot').text(), $('.Remise').val()).toFixed(3) + ' Rs');
    }
    tot = tot + parseFloat($('.Delivery').val());
    $('#total').text(tot.toFixed(3));
    $('#TotalModal').text('Total ' + tot.toFixed(3) + ' Rs');
}

//*************************Sale Payment Model*****************************//

function SalePaymentModel() {
    if ($('.SaleDetailRow').length > 0) {
        $.notify("Save the sale first!", { position: "top center", className: 'error' });
    }
    else if ($(".removerow").length != 0) {
        $.notify("Select the sale first!", { position: "top center", className: 'error' });
    }
    else {
        var SaleType = $('#ddlsaletype :selected').text();
        $('#SaleType span').text(SaleType);
        $('#AddSale').modal('show');
    }
}

//************************* Edit Sale *****************************//

$(document).on('click', '.EditSale', function (event) {
    Clear();
    ClearPrint();
    ClearKitchenPrint();
    $('#pendingsaledetail').modal('hide');
    var SaleID = $(this).attr('data-saleid');
    $('#txtSaleId').val($(this).attr('data-saleid'));
    TransactionNo = $(this).attr('data-reciptno');
    $('#ddlsaletype').val($(this).attr('data-saletypeid'));
    $('#ddldeliveryboy').val($(this).attr('data-deliveryboyid'));
    $('#txtTableNo').val($(this).attr('data-tableno'));
    $('#txtCustomerId').val($(this).attr('data-customerid'));
    $('#txtCustomerName').val($(this).attr('data-takeawaycustomername'));
    $('#txtcustomername').val($(this).attr('data-customername'));
    $('#txtcustomerphone').val($(this).attr('data-customerphone'));
    $('#txtcustomeremail').val($(this).attr('data-customeremail'));
    $('#txtcustomerdescription').val($(this).attr('data-customerdescription'));
    $('#txtcustomeraddress').val($(this).attr('data-customeraddress'));
    $('.TAX').val($(this).attr('data-tax'));
    $('#taxValue').text($(this).attr('data-taxamount'));
    $('.Remise').val($(this).attr('data-discountamount'));
    $('.Delivery').val($(this).attr('data-deliverycharges'));
    $('#Subtot').text($(this).attr('data-subtotalamount'));
    var totalamount = $(this).attr('data-totalamount');
    $('#TotalModal').text('Total ' + totalamount + ' Rs');
    $('#total').text(totalamount);
    $('#Paid').val($(this).attr('data-paidamount'));
    $('#ReturnChange span').text($(this).attr('data-balance'));
    $('#ItemsNum span,#ItemsNum2 span').text($(this).attr('data-totalitems'));
    SaleDetial_PartialView(SaleID);
});

$(document).on('click', '.EditDineInSale', function (event) {
    Clear();
    ClearPrint();
    ClearKitchenPrint();
    $('#pendingdineinsaledetail').modal('hide');
    var SaleID = $(this).attr('data-saleid');
    $('#txtSaleId').val($(this).attr('data-saleid'));
    TransactionNo = $(this).attr('data-reciptno')
    $('#ddlsaletype').val($(this).attr('data-saletypeid'));
    $('#ddldeliveryboy').val($(this).attr('data-deliveryboyid'));
    $('#txtTableNo').val($(this).attr('data-tableno'));
    $('.TAX').val($(this).attr('data-tax'));
    $('#taxValue').text($(this).attr('data-taxamount'));
    $('.Remise').val($(this).attr('data-discountamount'));
    $('#Subtot').text($(this).attr('data-subtotalamount'));
    var totalamount = $(this).attr('data-totalamount');
    $('#TotalModal').text('Total ' + totalamount + ' Rs');
    $('#total').text(totalamount);
    $('#Paid').val($(this).attr('data-paidamount'));
    $('#ReturnChange span').text($(this).attr('data-balance'));
    $('#ItemsNum span,#ItemsNum2 span').text($(this).attr('data-totalitems'));
    SaleDetial_PartialView(SaleID);
});
//*************************Add Customer*****************************//

function CheckUserExist() {
    var PhoneNo = $('#txtcustomerphone').val();
    $.ajax({
        url: "/POS/CheckUserExist",
        data: JSON.stringify({
            Phone: PhoneNo
        }),
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        if (data.Success == true) {
            $response = JSON.parse(data.Response);
            if ($response != null) {
                $.notify("Record Found", { position: "top center", className: 'success' });
                $('#txtCustomerId').val($response.customerID);
                $('#txtcustomername').val($response.customerName);
                $('#txtCustomerName').val($response.customerName);
                $('#txtcustomerphone').val($response.phone);
                $('#txtcustomeremail').val($response.email);
                $('#txtcustomerdescription').val($response.description);
                $("#txtcustomeraddress").val($response.address);
            }
            else {
                $.notify("No Record Found", { position: "top center", className: 'error' });
            }
        }
    })
}

function InsertUpdateCustomer() {
    var customerid = $('#txtCustomerId').val();
    var customername = $('#txtcustomername').val();
    $('#txtCustomerName').val(customername);
    var phone = $('#txtcustomerphone').val();
    var email = $('#txtcustomeremail').val();
    var description = $('#txtcustomerdescription').val();
    var address = $("#txtcustomeraddress").val();
    var status = 0;
    if ($('#txtcustomerphone').val() == "") {
        $.notify('Enter Phone No', { position: "top center", className: "warn" });
    }
    else if ($('#txtcustomername').val() == "") {
        $.notify('Enter Customer Name', { position: "top center", className: "warn" });
    }
    else if ($('#txtcustomeraddress').val() == "") {
        $.notify('Enter Address', { position: "top center", className: "warn" });
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
                $("#AddUnit").hide();
                $('.modal-backdrop').remove();
                $response = JSON.parse(data.Response);
                if ($response.pFlag == "1") {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "success" });
                    $('#txtCustomerId').val($response.customerID);
                    $('#AddCustomer').modal('hide');
                }
                else {
                    $.notify($response.pFlag_Desc, { position: "top center", className: "error" });
                }
            }
        });
    }
}

//*************************Sale Payment*****************************//

function AddSalePayment() {
    var SaleID = $('#txtSaleId').val();
    var Paid = $('#Paid').val();
    var PaidMethodID = $('#paymentMethod').val();
    var Change = $('#ReturnChange span').text();
    var SaleStatusID = 2;
    var SaleType = $('#ddlsaletype :selected').text();
    var currentdate = new Date();
    var TakeAwayCustomerName = $('#txtCustomerName').val();
    var datetime = "" + currentdate.getDate() + "/"
        + (currentdate.getMonth() + 1) + "/"
        + currentdate.getFullYear() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    var DiscountAmount = $('.Remise').val();
    var DeliveryCharges = $('.Delivery').val();
    var Subtotal = $('#Subtot').text();
    var Total = $('#total').text();
    var TotalItems = $('#ItemsNum span').text();
    if ($('.SaleDetailRow').length > 0) {
        $.notify("Save the sale first!", { position: "top center", className: 'error' });
    }
    else if ($('#Paid').val() === "") {
        $('#Paid').css('border-color', 'red');
        $.notify('Please Enter Payed Amount', { position: "top center", className: 'error' });
    }
    else {
        $('#AddSale').modal('hide');
        swal({
            title: "Enter Password!",
            type: "input",
            inputType: "password",
            showCancelButton: true,
            closeOnConfirm: false,
            inputPlaceholder: "Enter Password"
        }, function (inputValue) {
            if (inputValue === false)
                return false;
            if (inputValue === "") {
                swal.showInputError("You need to write password!");
                return false
            }
            if (inputValue === "I88") {
                $.ajax({
                    url: "/POS/AddSaleWithPayment",
                    data: JSON.stringify({
                        SaleID: SaleID,
                        PaidAmount: Paid,
                        Change: Change,
                        SaleStatusID: SaleStatusID,
                        PaymentModeID: PaidMethodID,
                    }),
                    type: "POST",
                    dataType: "json",
                    async: true,
                    cache: false,
                    contentType: 'application/json; charset=utf-8'
                }).done(function (data) {
                    if (data.Success == true) {
                        $response = JSON.parse(data.Response);
                        if ($response.pFlag == "1") {
                            $('#PrintSaleNo').text("ReciptNo: " + TransactionNo);
                            $('#SaleTypePrint').text("Sale Type: " + SaleType);
                            $('#SaleDateTime').text("Date: " + datetime);
                            $('#TotalItemPrint').text(TotalItems);
                            $('#DiscountPrint').text(DiscountAmount);
                            $('#DeliveryPrint').text(DeliveryCharges);
                            $('#GrandtotalPrint').text(Total);
                            $('#TotalAmountPrint').text(Subtotal);
                            $('#PaidAmountPrint').text(Paid);
                            $('#ChangePrint').text(Change);
                            $('#CustomerNamePrint').text("Customer Name: " + TakeAwayCustomerName);
                            $('#tbl_cart_list tbody tr').each(function (index, value) {
                                var Quantity = $(this).find('#txtSaleDetailQuantity').val();
                                var Price = $(this).attr('data-Price');
                                var NetTotal = $(this).find(".NetTotal").text();
                                var ItemName = $(this).attr('data-sub_menu_name');
                                $('#tableprint tbody').append('<tr><td style="text-align:left;">' + ItemName + '</td><td style="text-align:center;">' + Price + '</td><td style="text-align:center;">' + Quantity + '</td><td style="text-align:center;">' + NetTotal + '</td></tr>');
                                $('#tablekitchenprint tbody').append('<tr><td style="text-align:left;">' + ItemName + '</td><td style="text-align:center;">' + Quantity + '</td></tr></br>');
                            });
                            $('#ticket').modal('show');
                            swal($response.pFlag_Desc, "success");
                        } else {
                            swal('Failed!', $response.pFlag_Desc, "error");
                        }
                    }
                })
            }
            else
                swal('Failed!', 'Incorrect Password', "error");
        })
    }
}

//*************************Sale*****************************//

function saleBtn() {
    if ($(".removerow").length != 0) {
        $.notify("Select the sale first!", { position: "top center", className: 'error' });
    }
    else if ($('#ddlsaletype').val() == "0") {
        $.notify("Select the sale type first!", { position: "top center", className: 'error' });
    }
    else if ($('#ddlsaletype').val() == "3" && $('#Subtot').text() < 500 && $('.Delivery').val() == 0) {
        $.notify("Enter Delivery Charges first!", { position: "top center", className: 'error' });
    }
    else {
        $('#AddSale').modal('hide');
        var myobjlist = [];
        var SaleID = $('#txtSaleId').val();
        var SaleType = $('#ddlsaletype :selected').text();
        var SaleTypeID = $('#ddlsaletype').val();
        if ($('#ddldeliveryboy').val() == "" || $('#ddldeliveryboy').val() == null) {
            var DeliveryBoyID = 0;
        }
        else
            var DeliveryBoyID = $('#ddldeliveryboy').val();
        if ($('#txtTableNo').val() == null || $('#txtTableNo').val() == "") {
            var TableNo = "";
        }
        else
            var TableNo = $('#txtTableNo').val();
        if ($('#txtCustomerName').val() == null || $('#txtCustomerName').val() == "") {
            var TakeAwayCustomerName = "";
        }
        else
            var TakeAwayCustomerName = $('#txtCustomerName').val();
        if ($('#txtCustomerId').val() == 0)
            var CustomerID = "0";
        else
            CustomerID = $('#txtCustomerId').val();
        var Tax = $('.TAX').val();
        var TaxAmount = $('.TAX').val().indexOf('%') != -1 ? parseFloat($('#taxValue').text()) : $('.TAX').val();
        var currentdate = new Date();
        var datetime = "" + currentdate.getDate() + "/"
            + (currentdate.getMonth() + 1) + "/"
            + currentdate.getFullYear() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
        var DiscountAmount = $('.Remise').val();
        var DeliveryCharges = $('.Delivery').val();
        var Subtotal = $('#Subtot').text();
        var Total = $('#total').text();
        if ($('#Paid').val() == "") {
            var Paid = "0";
        }
        else
            Paid = $('#Paid').val();
        var PaidMethod = 3;
        var Change = $('#ReturnChange span').text();
        var SaleStatusID = 7;
        var OrderStatusID = 4;
        var TotalItems = $('#ItemsNum span').text();
        $('#tbl_cart_list tbody tr').each(function (index, value) {
            var ManageSaleDetailList = new Object();
            ManageSaleDetailList.SaleDetailId = $(this).attr('data-saledetailid');
            ManageSaleDetailList.SubMenuId = $(this).attr('data-submenuid');
            ManageSaleDetailList.Quantity = $(this).find('#txtSaleDetailQuantity').val();
            ManageSaleDetailList.Price = $(this).attr('data-Price');
            ManageSaleDetailList.NetTotal = $(this).find(".NetTotal").text();
            ManageSaleDetailList.ItemName = $(this).attr('data-sub_menu_name');
            myobjlist.push(ManageSaleDetailList);
        });
        $.ajax({
            url: "/POS/AddSale",
            data: JSON.stringify({
                SaleID: SaleID,
                TotalItems: TotalItems,
                SubTotal: Subtotal,
                Total: Total,
                DiscountAmount: DiscountAmount,
                DeliveryCharges: DeliveryCharges,
                PaidAmount: Paid,
                Change: Change,
                CustomerID: CustomerID,
                DeliveryBoyID: DeliveryBoyID,
                TableNo: TableNo,
                TakeAwayCustomerName: TakeAwayCustomerName,
                SaleTypeID: SaleTypeID,
                Tax: Tax,
                TaxAmount: TaxAmount,
                OrderStatusID: OrderStatusID,
                SaleStatusID: SaleStatusID,
                PaymentModeID: PaidMethod,
                SaleDetailList: myobjlist
            }),
            type: "POST",
            dataType: "json",
            async: true,
            cache: false,
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            if (data.Success == true) {
                $response = JSON.parse(data.Response);
                if ($response[0].pFlag == "1") {
                    $('#txtSaleId').val($response[0].transactionId);
                    TransactionNo = $response[0].transactionNo;
                    $('#KitchenPrintSaleNo').text("ReciptNo: " + TransactionNo);
                    $('#KitchenSaleTypePrint').text("Sale Type: " + SaleType);
                    $('#KitchenSaleDateTime').text("Date: " + datetime);
                    $('#KitchenTotalItemPrint').text(TotalItems);
                    if ($('#ddlsaletype').val() != "1") {
                        $('#KitchenCustomerNamePrint').text("Customer Name: " + TakeAwayCustomerName);
                    }
                    $('#tbl_cart_list tbody tr').each(function (index, value) {
                        var Quantity = $(this).find('#txtSaleDetailQuantity').val();
                        var ItemName = $(this).attr('data-sub_menu_name');
                        $('#kitchentableprint tbody').append('<tr><td style="text-align:left; font-weight:bold;"><h3>' + ItemName + '</h3></td><td style="text-align:center; font-weight:bold;"><h3>' + Quantity + '</h3></td></tr>');
                    });
                    SaleDetial_PartialView($response[0].transactionId)
                    $('#tbl_cart_list tbody tr').each(function (index, value) {
                        $(this).removeClass('SaleDetailRow');
                        $(this).addClass('bg-info');
                    });
                    $('#kitchenticket').modal('show');
                    $.notify($response[0].pFlag_Desc, { position: "top center", className: "success" });
                }
                else {
                    $.notify($response[0].pFlag_Desc, { position: "top center", className: "error" });
                }
            }
        });
    }
}

//*************************Delivered*****************************//

$(document).on('click', '.OrderDelivered', function (event) {
    var SaleID = $(this).attr('data-saleid');
    var CurrentRow = $(this).parents('tr');
    swal({
        title: 'Are you sure ?',
        text: "You won't be able to revert this!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: 'Yes, Delivered!',
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: "/POS/OrderDeliveredStatus",
                data: JSON.stringify({
                    SaleID: SaleID,
                }),
                type: "POST",
                dataType: "json",
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                if (data.Success) {
                    $response = JSON.parse(data.Response);
                    if ($response.pFlag === "1") {
                        swal($response.pFlag_Desc, 'the data has been clear.', "success");
                        if ($('#viewtodaysaletab').hasClass('Hold p1') == true) {
                            TodayPendingSaleDetail_PartialView();
                        }
                        else {
                            PendingSaleDetail_PartialView();
                        }
                    }
                    else {
                        swal($response.pFlag_Desc, 'the data has been clear.', "danger");
                    }
                }
            });
        })
});

//*********************************RemoveSaleDetailRow************************************/

$(document).on('click', '.RemoveSaleDetailRow', function (event) {
    var CurrentRow = $(this).parents('tr');
    var SaleDetailID = $(this).attr('data-saledetailid');
    var quantity = CurrentRow.find('.SaleDetailQuantity').val();
    var items = parseInt($('#ItemsNum span').text());
    if (CurrentRow.hasClass('SaleDetailRow') == true) {
        $(CurrentRow).remove();
        if (items > 0) {
            var inc = items - quantity;
        }
        else
            $('#ItemsNum span,#ItemsNum2 span').text("0");
        $('#ItemsNum span,#ItemsNum2 span').text(parseInt(inc));
        var rowCount = $('#tbl_cart_list tbody tr').length;
        if (rowCount == 0) {
            $('#tbl_cart_list tbody').append('<tr class="removerow"><td colspan = "5"><div class="messageVide">Empty List <span>(Select Items)</span></div></td></tr>');
        }
        CalculateTotal();
    }
    else {
        swal({
            title: 'Are you sure ?',
            text: 'You will not be able to recover this Data later!',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        },
            function () {
                $.ajax({
                    url: "/POS/CancelSaleDetailRow",
                    data: JSON.stringify({
                        SaleDetailID: SaleDetailID,
                    }),
                    type: "POST",
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8'
                }).done(function (data) {
                    if (data.Success) {
                        $response = JSON.parse(data.Response);
                        if ($response.pFlag === "1") {
                            var SaleID = $('#txtSaleId').val();
                            SaleDetial_PartialView(SaleID);
                            if (items > 0) {
                                var inc = items - quantity;
                            }
                            else
                                $('#ItemsNum span,#ItemsNum2 span').text("0");
                            $('#ItemsNum span,#ItemsNum2 span').text(parseInt(inc));
                            swal($response.pFlag_Desc, 'the data has been clear.', "success");
                        }
                        else {
                            swal('ERROR!', 'An Internal Error', "danger");
                        }
                    }
                });
            });
    }
})

//*************************Others*****************************//

$("#ddlsaletype").change(function () {
    var SaleType = $(this).find('option:selected').text();
});

function ViewTodaySaleDetail() {
    TodayPendingSaleDetail_PartialView();
    $('#pendingsaledetail').modal('show');
    $('#viewtodaysaletab').addClass(' p1');
}

function ViewSaleDetail() {
    PendingSaleDetail_PartialView();
    $('#pendingsaledetail').modal('show');
    $('#viewtodaysaletab').removeClass(' p1');
}

function DineInSaleDetail() {
    PendingDineInSaleDetail_PartialView();
    $('#pendingdineinsaledetail').modal('show');
}

//*************************Print*****************************//

function PrintTicket() {
    $('.modal-body').removeAttr('id');
    window.print();
    $('.modal-body').attr('id', 'modal-body');
    Clear();
    ClearPrint();
    ClearKitchenPrint();
}

function PrintBill() {
    if ($('.SaleDetailRow').length > 0) {
        $.notify("Save the sale first!", { position: "top center", className: 'error' });
    }
    else if ($(".removerow").length != 0) {
        $.notify("Select the sale first!", { position: "top center", className: 'error' });
    }
    else {
        var SaleType = $('#ddlsaletype :selected').text();
        var Tax = $('.TAX').val();
        var currentdate = new Date();
        var datetime = "" + currentdate.getDate() + "/"
            + (currentdate.getMonth() + 1) + "/"
            + currentdate.getFullYear() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
        var DiscountAmount = $('.Remise').val();
        var DeliveryCharges = $('.Delivery').val();
        var Subtotal = $('#Subtot').text();
        var Total = $('#total').text();
        var Change = $('#ReturnChange span').text();
        var TotalItems = $('#ItemsNum span').text();
        var Paid = $('#Paid').val();
        var TakeAwayCustomerName = $('#txtCustomerName').val();
        var PaidMethod = $('#paymentMethod').find('option:selected').val();
        var TaxAmount = $('.TAX').val().indexOf('%') != -1 ? parseFloat($('#taxValue').text()) : $('.TAX').val();
        $('#PrintSaleNo').text("ReciptNo: " + TransactionNo);
        $('#SaleTypePrint').text("Sale Type: " + SaleType);
        $('#SaleDateTime').text("Date: " + datetime);
        $('#TotalItemPrint').text(TotalItems);
        $('#DiscountPrint').text(DiscountAmount);
        $('#DeliveryPrint').text(DeliveryCharges);
        $('#GrandtotalPrint').text(Total);
        $('#TotalAmountPrint').text(Subtotal);
        $('#PaidAmountPrint').text(Paid);
        $('#ChangePrint').text(Change);
        if ($('#ddlsaletype').val() == "3") {
            var phone = $('#txtcustomerphone').val();
            var address = $("#txtcustomeraddress").val();
            $('#CustomerPhoneNoPrint').text("Phone #: " + phone);
            $('#CustomerAddressPrint').text("Address: " + address);
        }
        if ($('#ddlsaletype').val() == "2") {
            $('#CustomerNamePrint').text("Customer Name: " + TakeAwayCustomerName);
        }
        $('#tbl_cart_list tbody tr').each(function (index, value) {
            var Quantity = $(this).find('#txtSaleDetailQuantity').val();
            var Price = $(this).attr('data-Price');
            var NetTotal = $(this).find(".NetTotal").text();
            var ItemName = $(this).attr('data-sub_menu_name');
            $('#tableprint tbody').append('<tr><td style="text-align:left;">' + ItemName + '</td><td style="text-align:center;">' + Price + '</td><td style="text-align:center;">' + Quantity + '</td><td style="text-align:center;">' + NetTotal + '</td></tr>');
        });
        $('#ticket').modal('show');
    }
}

//**************************************Clear************************************/

function Clear() {
    $('#tbl_cart_list tbody tr').remove();
    $('#txtSaleId').val("0");
    $('#ddldeliveryboy').val(null);
    $('#txtTableNo').val(null);
    $('#txtCustomerId').val('0');
    $('#txtCustomerName').val(null);
    $('#txtcustomername').val(null);
    $('#txtcustomerphone').val(null);
    $('#txtcustomeremail').val(null);
    $('#txtcustomerdescription').val(null);
    $("#txtcustomeraddress").val(null);
    $('#Paid').val('0');
    $('#ReturnChange span').text('0');
    $('#ddlsaletype').val('0');
    $('#ddlcustomerSelect').val('1');
    $('.Remise').val('0');
    $('.Delivery').val('0');
    $('.TAX').val('0%');
    $('#taxValue').text('0');
    $('#total').text('0');
    $('#TotalModal').text("");
    $('#Subtot').text('0.00');
    $('#ItemsNum span, #ItemsNum2 span').text("0");
    $('#tbl_cart_list tbody').append('<tr class="removerow"><td colspan = "5"><div class="messageVide">Empty List <span>(Select Items)</span></div></td></tr>');
}
function ClearPrint() {
    $('#PrintSaleNo').text(null);
    $('#SaleTypePrint').text(null);
    $('#SaleDateTime').text(null);
    $('#CustomerNamePrint').text(null);
    $('#CustomerPhoneNo').text(null);
    $('#CustomerAddress').text(null);
    $('#CustomerPhoneNoPrint').text(null);
    $('#CustomerAddressPrint').text(null);
    $('#TotalItemPrint').text(null);
    $('#DiscountPrint').text(null);
    $('#DeliveryPrint').text(null);
    $('#GrandtotalPrint').text(null);
    $('#TotalAmountPrint').text(null);
    $('#PaidAmountPrint').text(null);
    $('#ChangePrint').text(null);
    $('#tableprint tbody tr').remove();
}
function ClearKitchenPrint() {
    $('#kitchentableprint tbody tr').remove();
    $('#KitchenPrintSaleNo').text(null);
    $('#KitchenSaleTypePrint').text(null);
    $('#KitchenSaleDateTime').text(null);
    $('#KitchenTotalItemPrint').text(null);
    $('#KitchenCustomerNamePrint').text(null);
}

//********************************************Other********************************//

