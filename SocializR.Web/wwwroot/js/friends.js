$(document).ready(function () {

    $('.accept-friend-request').on('click', function (e) {
        var id = $(e.target).closest('.user-container').find('.user-id').val();
        $.ajax({
            type: 'POST',
            url: Router.action('Friendship', 'AddFriend', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.reject-friend-request').on('click', function (e) {
        var id = $(e.target).closest('.user-container').find('.user-id').val();
        $.ajax({
            type: 'POST',
            url: Router.action('FriendRequest', 'DeleteFriendRequest', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

    //$('#friendrequests').on('click', '.approve-button', function (e) {

    //    var parent = e.target.parentElement.parentElement.parentElement;

    //    var id = $(parent).find('.user-id').val();

    //    $.ajax({
    //        type: 'POST',
    //        url: Router.action('Friendship', 'AddFriend', { id: id }),
    //        success: function (cities) {
    //            $(parent).find('button').remove();
    //            $(parent).find('.message').remove();
    //            $(parent).find('.time-created').html('friends since now');
    //            $('#friends').prepend(parent);
    //        }
    //    });
    //});

    //$('#friendrequests').on('click', '.reject-button', function (e) {
    //    var parent = e.target.parentElement.parentElement.parentElement;

    //    var id = $(parent).find('.user-id').val();

    //    $.ajax({
    //        type: 'POST',
    //        url: Router.action('Friendship', 'DeleteFriendRequest', {id:id}),
    //        success: function (cities) {
    //            $(parent).remove();
    //        }
    //    });
    //});
});