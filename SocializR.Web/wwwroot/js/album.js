jQuery(function ($) {

    $('.delete-album').on('click', function (e) {
        var container = $(e.target).closest('.album-container');
        var id = $(container).find('#Id').val();

        $.post('Album/Delete', { id: id }, function () {
            alert("Album deleted successfully!");
            location.reload();
        });
    });

    $('.delete-media').on('click', function (e) {
        var container = $(e.target).closest('.card');
        var id = $(container).find('#Media_media_Id').val();

        $.post('../Media/Delete', { id: id }, function () {
            alert("Media deleted successfully!");
            location.reload();
        });
    });

    $('#upload-media').on('change', function (e) {
        var files = e.target.files;
        let formData = new FormData();
        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            if (file.type.match('image.*')) {
                formData.append('media', file, file.name);
            }
        }

        var container = $(e.target).closest('form');
        var albumId = $(container).find('#Id').val();
        formData.append("AlbumId", albumId);

        $.ajax({
            type: 'POST',
            url: '../Media/Upload',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                location.reload();
            },
            error: function (err) {
                alert(err.statusText);
            }
        });

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