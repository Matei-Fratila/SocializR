var formData = new FormData();

jQuery(function ($) {

    $('video').on('play', function () {
        var id = this.id;
        $('video').each(function (index) {
            if (id !== this.id) {
                this.pause();
            }
        });
    }); 

    $('textarea').each(function () {
        this.setAttribute('style', 'height:' + (this.scrollHeight) + 'px;overflow-y:hidden;');
    }).on('input', function () {
        this.style.height = 'auto';
        this.style.height = (this.scrollHeight) + 'px';
    });

    function handleFileSelect(evt) {
        var files = evt.target.files;
        var id = $('#album-id').val();

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            if (file.type.match('image.*') || file.type.match('video.*')) {
                formData.append('media', file, file.name);
            }
        }

        if (formData.getAll('media').length !== 0) {
            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'POST',
                url: 'Media/Upload?id=' + id,
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    location.reload();
                },
                error: function (ex) {
                    alert('error');
                }
            });
        }
    }

    $('#files').change(handleFileSelect);

    $('#images').on('click', '.delete', function (e) {

        var container = $(e.target).closest('figure');
        var id = $(container).find('.image-id').val();

        if (id === -1) {
            container.remove();
        }
        else {
            $.post('Media/Delete', { id: id }), function () {
                alert("Image was deleted successfuly!");
                container.remove();
            });
        }

    });

    $('#save-changes').click(function (e) {

        var name = $('#album-name').val();
        var id = $("#album-id").val();

        var allPhotos = $('.image-id').closest('figure');
        var imagesToUpdate = [];

        allPhotos.each(function (index, element) {
            imagesToUpdate.push({
                Id: $(element).find($('.image-id')).val(),
                Caption: $(element).find($('.caption')).val()
            });
        });

        model = {
            id: id,
            media: imagesToUpdate
        };

        //if (allPhotos.length !== 0) {
        //    $.ajax({
        //        type: 'POST',
        //        url: Router.action('Media', 'Edit'),
        //        data: model
        //    });
        //}

        //var form = $('#edit-album');
        //if ($(form).validate().valid()) {
        //    $(form).submit();
        //}

        if (allPhotos.length !== 0) {
            $.ajax({
                type: 'POST',
                url: 'Media/Edit',
                data: model,
                success: function () {
                    var form = $('#edit-album');
                    if ($(form).valid()) {
                        $(form).submit();
                    }
                },
                error: function (error) {
                    alert('error');
                }
            });
        }
        else {
            var form = $('#edit-album');
            if ($(form).valid()) {
                $(form).submit();
            }
        }

        //var mediaEditPromise = new Promise(function (resolve, reject) {
        //    if (allPhotos.length !== 0) {
        //        $.ajax({
        //            type: 'POST',
        //            url: Router.action('Media', 'Edit'),
        //            data: model,
        //            success: function () {
        //                resolve('Done - Media Edit');
        //            },
        //            error: function (error) {
        //                reject(error);
        //            }
        //        });
        //    }
        //    else {
        //        resolve();
        //    }
        //});

        //var albumEditPromise = new Promise(function (resolve, reject) {
        //    var form = $('#edit-album');
        //    if ($(form).valid()) {
        //        $(form).submit();
        //    }
        //    else {
        //        reject('Error - Album Edit');
        //    }
        //});

        //Promise.all([albumEditPromise, mediaEditPromise]).then(
        //    function (values) {
        //        //location.reload();
        //    },

        //    function (values) {
        //        alert(values);
        //    }
        //);

    });
});