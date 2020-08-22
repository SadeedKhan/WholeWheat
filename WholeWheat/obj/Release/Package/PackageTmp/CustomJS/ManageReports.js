$(document).ready(function () {
    MonthlyGraphDetail();
});

/************************ Chart Data *************************/
var randomScalingFactor = function () { return Math.round(Math.random() * 100) };
var MonthlyExpenses = [];
var MonthlyRevenue = [];
var lineChartData = null;
function MonthlyGraphDetail() {
    $.ajax({
        url: "/Reports/MonthlyGraphDetail",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        $response = JSON.parse(data.Response);
        for (var i = 0; i < $response.length; i++) {
            MonthlyExpenses.push(parseInt($response[i].monthlyExpenses));
            MonthlyRevenue.push(parseInt($response[i].monthlyRevenue))
        }
        lineChartData = {
            labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            datasets: [
                {
                    label: "Expences",
                    backgroundColor: "rgba(255,99,132,0.2)",
                    borderColor: "rgba(255,99,132,1)",
                    pointBackgroundColor: "rgba(255,99,132,1)",
                    pointBorderColor: "#fff",
                    pointHoverBackgroundColor: "#fff",
                    pointHoverBorderColor: "rgba(255,99,132,1)",
                    data: MonthlyExpenses
                },
                {
                    label: "Revenue",
                    backgroundColor: "#34495e",
                    borderColor: "#2c3e50",
                    pointBackgroundColor: "#34495e",
                    pointBorderColor: "#fff",
                    pointHoverBackgroundColor: "#fff",
                    pointHoverBorderColor: "#2c3e50",
                    data: MonthlyRevenue
                }
            ]
        }
        Hi();
        function Hi() {
            var SubMenuName = [];
            var Total = [];
            // Chart.defaults.global.gridLines.display = false;
            PieChartDetail();
            var ctx = document.getElementById("canvas").getContext("2d");
            window.myLine = new Chart(ctx, {
                type: 'line',
                data: lineChartData,
                options: {
                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false
                            }
                        }],
                        yAxes: [{
                            gridLines: {
                                display: false
                            }
                        }]
                    },
                    scaleFontSize: 9,
                    tooltipFillColor: "rgba(0, 0, 0, 0.71)",
                    tooltipFontSize: 10,
                    responsive: true
                }
            });

            /********************* pie **********************/

            function PieChartDetail() {
                $.ajax({
                    url: "/Reports/PieChartDetail",
                    data: JSON.stringify({
                    }),
                    type: "POST",
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8'
                }).done(function (data) {
                    $response = JSON.parse(data.Response);
                    for (var i = 0; i < $response.length; i++) {
                        SubMenuName.push($response[i].subMenuName);
                        Total.push(parseInt($response[i].total))
                    }
                    var pieData = {
                        labels: SubMenuName,
                        datasets: [
                            {
                                data: Total,
                                backgroundColor: [
                                    "#34495E",
                                    "#7f8c8d",
                                    "#ECF0F1",
                                    "#3498DB",
                                    "#1ABC9C"
                                ],
                                hoverBackgroundColor: [
                                    "#3e5367",
                                    "#95a5a6",
                                    "#f5fbfc",
                                    "#459eda",
                                    "#2dc6a8"
                                ]
                            }
                        ]
                    };
                    Chart.defaults.global.legend.display = false;
                    var ctx2 = document.getElementById("chart-area2").getContext("2d");
                    window.myPie = new Chart(ctx2, {
                        type: 'pie',
                        data: pieData
                    });
                })
            }
        }
    })
}

/******* Range date picker *******/
$(function () {
    $('input[name="daterange"]').daterangepicker();
    $('input[name="daterangeP"]').daterangepicker();
    $('input[name="daterangeR"]').daterangepicker();
    var d = new Date().getFullYear();
    $('#ProductRange').val('01/01/' + d + ' - 12/31/' + d);
    $('#CustomerRange').val('01/01/' + d + ' - 12/31/' + d);
    $('#RegisterRange').val('01/01/' + d + ' - 12/31/' + d);

});

/********************************** Get repports functions ************************************/

function getSaleTypeReport() {
    if ($('#ddlsaletypeselect').find('option:selected').val() == "0") {
        $.notify('Select Sale Type', { position: "top center", className: "warn" });
    }
    else {
        var SaleType = $('#ddlsaletypeselect').find('option:selected').val();
        var Range = $('#CustomerRange').val();
        var start = Range.slice(6, 10) + '-' + Range.slice(0, 2) + '-' + Range.slice(3, 5);
        var end = Range.slice(19, 23) + '-' + Range.slice(13, 15) + '-' + Range.slice(16, 18);
        // ajax delete data to database
        $.ajax({
            url: "/Reports/SaleTypeDetail_PartialView",
            data: JSON.stringify({
                SaleTypeID: SaleType,
                FromDate: start,
                EndDate: end
            }),
            type: "POST",
            dataType: 'html',
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            $('#SaleTypeDetail_PartialView').html(data);
            $('#SaleTypeDetail').modal('show');
        })
    }
}

function getProductReport() {
    if ($('#ddlsubmenu').find('option:selected').val() == "") {
        $.notify('Select Product', { position: "top center", className: "warn" });
    }
    else {
        var product_id = $('#ddlsubmenu').find('option:selected').val();
        var Range = $('#ProductRange').val();
        var start = Range.slice(6, 10) + '-' + Range.slice(0, 2) + '-' + Range.slice(3, 5);
        var end = Range.slice(19, 23) + '-' + Range.slice(13, 15) + '-' + Range.slice(16, 18);
        // ajax set data to database
        $.ajax({
            url: "/Reports/ProductDetail_PartialView",
            data: JSON.stringify({
                ProductID: product_id,
                FromDate: start,
                EndDate: end
            }),
            type: "POST",
            dataType: 'html',
            contentType: 'application/json; charset=utf-8'
        }).done(function (data) {
            $('#ProductDetail_PartialView').html(data);
            $('#ProductDetail').modal('show');
        })
    }
}

function getyearstats(direction) {
    var currentyear = parseInt($('.statYear').text());
    var year = direction === 'next' ? currentyear - 1 : currentyear + 1;

    $.ajax({
        url: "http://www.dar-elweb.com/demos/zarpos/reports/getyearstats/" + year,
        type: "POST",
        success: function (data) {
            $('#statyears').html(data);
            $('.statYear').text(year);
            $('[data-toggle="tooltip"]').tooltip();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("error");
        }
    });
}
