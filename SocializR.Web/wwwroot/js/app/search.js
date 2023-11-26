//$(document).ready(function () {

//    $('.search-bar').select2({
//        ajax: {
//            url: 'https://api.github.com/search/repositories',
//            dataType: 'json',
//            delay: 250,
//            data: function (params) {
//                var query = {
//                    search: params.term,
//                    page: params.page || 1
//                };
//                return query;
//            },
//            processResults: function (data) {
//                return {
//                    results: data.items
//                };
//            }
//        }
//    });
//});