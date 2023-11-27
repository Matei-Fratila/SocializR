$(document).ready(function () {

    $('.album-container a').remove();

    var id = $('#Id').val();

    $('.unfriend').on('click', function () {
        $.ajax({
            type: 'POST',
            url: Router.action('Friendship', 'Unfriend', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.send-friend-request').on('click', function () {
        $.ajax({
            type: 'POST',
            url: Router.action('FriendRequest', 'SendFriendRequest', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.accept-friend-request').on('click', function () {
        $.ajax({
            type: 'POST',
            url: Router.action('Friendship', 'AddFriend', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.reject-friend-request').on('click', function () {
        $.ajax({
            type: 'POST',
            url: Router.action('FriendRequest', 'DeleteFriendRequest', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.delete-friendrequest').on('click', function () {
        $.ajax({
            type: 'POST',
            url: Router.action('FriendRequest', 'DeleteFriendRequest', { id: id }),
            success: function (cities) {
                location.reload();
            }
        });
    });

});