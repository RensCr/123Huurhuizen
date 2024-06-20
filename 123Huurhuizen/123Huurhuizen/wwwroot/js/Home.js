$(document).ready(function () {
    $('[data-fancybox="gallery"]').fancybox();

    // Dynamically set the height of columns to match the container
    var containerHeight = $('.container').outerHeight();
    $('.col-heigth').height(containerHeight);
});