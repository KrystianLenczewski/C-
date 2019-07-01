// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $(document).one('click', '.like-review', function (e) {
        $(this).html('<i class="fa fa-heart" aria-hidden="true"></i> You liked this');
        $(this).children('.fa-heart').addClass('animate-like');
    });
});
