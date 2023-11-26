$(document).ready(function () {

    $('.search-for-link').each(function (index, elem) {
        var text = $(elem).text().trim();
        $(elem).text(text);
        $(elem).linkify();
    });

    function format(repo) {
        if (repo.loading) {
            return repo.text;
        }

        var markup = '<span><img src=' + Router.action('Profile', 'RenderProfilePicture', { id: repo.id }) + ' class="img-responsive img-circle img-thumbnail small-user-photo" /> ' + repo.text + '</span>';

        return markup;
    }

    $('#search-bar').select2({
        allowClear: true,
        ajax: {
            url: Router.action('Search', 'Search'),
            dataType: 'json',
            delay: 250,
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page || 1
                };
                return query;
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                return {
                    results: data.results,
                    pagination: {
                        more: (params.page * 5) < parseInt(data.total_Count.toString())
                    }
                };
            },
            cache: true

        },
        placeholder: 'Search for a user',
        escapeMarkup: function (markup) { return markup; },
        minimumInputLength: 1,
        templateResult: format

    }).on('change.select2', function (e) {

        var id = $("#search-bar option:selected").val();

        window.location = Router.action('Profile', 'Get', { id });

    });

    $("#County").change(function () {
        $("#City").empty();
        $.ajax({
            type: 'GET',
            url: Router.action('City', 'GetAllByCounty', { id: $("#County").val() }),
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

    $("#select2interests").select2({
        placeholder: "Select an interest"
    });

    $("#select2interests-view").select2({
        placeholder: "No interests"
    });

}); 
