$(document).ready()
{
    Sales_PartialView();
}

//  **********************Partial Views************************************/

function Sales_PartialView() {
    $.ajax({
        url: "/Sales/Sales_PartialView",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "html",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $('#Sales_PartialView').html(data);
    })
    $(document).ajaxStop(function () {
        $('#loading-image').hide();
    });
}

$(document).on('click', '.PrintSale', function (event) {
    ClearPrint();
    var SaleID = $(this).attr('data-saleid');
    TransactionNo = $(this).attr('data-reciptno')
    var SaleType = $(this).attr('data-saletype');
    var datetime = $(this).attr('data-saledate');
    var DiscountAmount = $(this).attr('data-discountamount');
    var DeliveryCharges = $(this).attr('data-deliverycharges');
    var Subtotal = $(this).attr('data-subtotalamount');
    var Total = $(this).attr('data-totalamount');
    var Paid = $(this).attr('data-paidamount');
    var Change = $(this).attr('data-balance');
    var TotalItems = $(this).attr('data-totalitems');
    if (SaleType == "Delivery") {
        var phone = $(this).attr('data-customerphone');
        var address = $(this).attr('data-customeraddress');
        $('#CustomerPhoneNoPrint').text("Phone #: " + phone);
        $('#CustomerAddressPrint').text("Address: " + address);
    }
    if ($('#ddlsaletype').val() == "Take-Away") {
        var TakeAwayCustomerName = $(this).attr('data-takeawaycustomername');
        $('#CustomerNamePrint').text("Customer Name: " + TakeAwayCustomerName);
    }
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
    $.ajax({
        url: "/Sales/SaleDetail_PartialView",
        data: JSON.stringify({
            SaleID: SaleID
        }),
        type: "POST",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        for (var i = 0; i < $response.length; i++) {
            var Quantity = $response[i].quantity;
            var Price = $response[i].price;
            var NetTotal = $response[i].netTotal;
            var ItemName = $response[i].saleItem;
            $('#tableprint tbody').append('<tr><td style="text-align:left;">' + ItemName + '</td><td style="text-align:center;">' + Price + '</td><td style="text-align:center;">' + Quantity + '</td><td style="text-align:center;">' + NetTotal + '</td></tr>');
        }
    })
    $('#ticket').modal('show');
});

function PrintTicket() {
    $('.modal-body').removeAttr('id');
    window.print();
    $('.modal-body').attr('id', 'modal-body');
}

function delete_record(SaleID) {
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
                url: "/Sales/DeleteRecord",
                data: JSON.stringify({
                    SaleID: SaleID
                }),
                type: "POST",
                dataType: "json",
                contentType: 'application/json; charset=utf-8'
            }).done(function (data) {
                if (data.Success == true) {
                    Sales_PartialView();
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

