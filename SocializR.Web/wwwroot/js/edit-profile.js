jQuery(function ($) {

    function handleFileSelect(evt) {
        var files = evt.target.files;
        var formData = new FormData();

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            if (file.type.match('image.*')) {
                formData.append('images', file, file.name);
            }
        }

        if (formData.getAll('images').length !== 0) {
            var f = files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                    $('.header-photo').attr("src", e.target.result);
            };
            reader.readAsDataURL(f);
        }
    }

    $('#ProfilePhoto').change(handleFileSelect);
});