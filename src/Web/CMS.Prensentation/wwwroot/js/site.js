// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Oluşturmuş olduğum Notification Partial'ın iki sn sonra kalkması için ;

$(function () {

    setTimeout(() => {
        $("div.alert.notification").fadeOut();
    }, 2000);

})

