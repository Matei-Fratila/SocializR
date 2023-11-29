﻿jQuery(function ($) {

    $('.albums-container').on('click', '.delete', function (e) {
        var container = $(e.target).closest('.album-container');
        var id = $(container).find('.album-id').val();

        $.post('Album/Delete', { id: id }, function () {
            alert("album deleted successfully!");
            container.remove();
        });
    });

    $('.albums-container').on('click', '.edit', function (e) {
        var container = $(e.target).closest('.album-container');
        var id = $(container).find('.album-id').val();
        var name = $(container).find('.album-name').val();

        $(location).attr('href', 'Media/Index');
    });

    $('.show-gallery').on('click', function (e) {
        var id = $(e.target).closest('.album-container').find('.album-id').val();
        var name = $(e.target).closest('.album-container').find('.album-name').val();

        $.get('Media/GetGallery', { id: id }, function (response) {
            $('#myModal').find('#modal-gallery').empty();
            $('#myModal').find('#modal-gallery').append(response);
            $('#myModal').find('.modal-title').text(name);
            $('#myModal').modal('show');
        }).then(function () {
            $('.search-for-link').each(function (index, elem) {
                var text = $(elem).text().trim();
                $(elem).text(text);
                $(elem).linkify();
            });
        });
    });
});