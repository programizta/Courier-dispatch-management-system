// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setText(id) {
    $('#opt').text($(id).text());
}

$(document).ready(function () {
    $('#1').click(function () {
        setText('#1');
    });

    $('#2').click(function () {
        setText('#2');
    });

    $('#3').click(function () {
        setText('#3');
    });

    $('#4').click(function () {
        setText('#4');
    });

    $('#5').click(function () {
        setText('#5');
    });
});