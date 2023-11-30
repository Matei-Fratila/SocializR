jQuery(function ($) {
    $('.search-for-link').each(function (index, elem) {
        var text = $(elem).text().trim();
        $(elem).text(text);
        $(elem).linkify();
    });

    function format(repo) {
        if (repo.loading) {
            return repo.text;
        }

        var markup = '<span><img src=' + 'Profile/RenderProfilePicture?id=' + repo.id + ' class="rounded-circle img-thumbnail small-user-photo" /> ' + repo.text + '</span>';

        return markup;
    }

    //$('#search-bar').select2({
    //    allowClear: true,
    //    ajax: {
    //        url: 'Search/Search',
    //        dataType: 'json',
    //        delay: 250,
    //        data: function (params) {
    //            var query = {
    //                search: params.term,
    //                page: params.page || 1
    //            };
    //            return query;
    //        },
    //        processResults: function (data, params) {
    //            params.page = params.page || 1;

    //            return {
    //                results: data.results,
    //                pagination: {
    //                    more: (params.page * 5) < parseInt(data.total_Count.toString())
    //                }
    //            };
    //        },
    //        cache: true

    //    },
    //    placeholder: 'Search for a user',
    //    escapeMarkup: function (markup) { return markup; },
    //    minimumInputLength: 1,
    //    templateResult: format

    //}).on('change.select2', function (e) {

    //    var id = $("#search-bar option:selected").val();

    //    window.location = `Profile/Index/${id}`;

    //});

    $("#County").on("change", function () {
        var countyId = $("#County").val();
        $("#City").empty();
        $.ajax({
            type: 'GET',
            url: '/City/GetAllByCounty',
            data: { id: countyId },
            success: function (cities) {
                $.each(cities, function (i, city) {
                    $("#City").append('<option value="'
                        + city.value + '">'
                        + city.text + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed.' + ex);
            }
        });
        return false;
    });
}); 
