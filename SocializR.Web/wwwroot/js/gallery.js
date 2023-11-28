jQuery(function ($) {

    $('.item').first().addClass('active');
    $('li').first().addClass('active');

    $("#myCarousel").carousel({
        interval: false
    });

    //$("#myCarousel").carousel();
});