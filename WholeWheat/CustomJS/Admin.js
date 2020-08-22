$(document).ajaxStart(function () {
    $('#loading-image').show();
}).ajaxStop(function () {
    $('#loading-image').hide();
});
function Logout()
{
    $.ajax({
        url: "/Login/Logout",
        data: JSON.stringify({
        }),
        type: "POST",
        dataType: "json",
        async: true,
        cache: false,
        contentType: 'application/json; charset=utf-8'
    }).done(function (data) {
        })
    .always(function () {
        window.location.href = '/Login'
    });
}