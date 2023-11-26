$(document).ready(function () {

    $('.action').each(function (index, elem) {
        if ($(elem).closest('.user-container').find('.is-deleted').val() === 'False') {
            $(elem).addClass('delete-user');
            $(elem).append('<i class="glyphicon glyphicon-ok"></i>');
        }
        else {
            $(elem).addClass('activate-user');
            $(elem).append('<i class="glyphicon glyphicon-remove"></i>');
        }
    });

    $('#users-container').on('click', '.delete-user', function (e) {
        var userId = $(e.target).closest('.user-container').find('.user-id').val();
        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: Router.action('User', 'Delete', { id: userId }),
            success: function (response) {
                var link = $(e.target).closest('.user-container').find('.action');
                $(link).removeClass('delete-user');
                $(link).addClass('activate-user');
                $(link).find('i').remove();
                $(link).append('<i class="glyphicon glyphicon-remove"></i>');
                
            },
            error: function (ex) {
                alert('Something went wrong');
            }
        });
    });

    $('#users-container').on('click', '.activate-user', function (e) {
        var userId = $(e.target).closest('.user-container').find('.user-id').val();
        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: Router.action('User', 'Activate', { id: userId }),
            success: function (response) {
                var link = $(e.target).closest('.user-container').find('.action');
                $(link).removeClass('activate-user');
                $(link).addClass('delete-user');
                $(link).find('i').remove();
                $(link).append('<i class="glyphicon glyphicon-ok"></i>');
                
            },
            error: function (ex) {
                alert('Something went wrong');
            }
        });
    });

});
