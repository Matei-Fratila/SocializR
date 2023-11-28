jQuery(function ($) {

    $('.select2').select2();

    $('.album-container a').remove();

    var id = $('#Id').val();

    $('.unfriend').on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'Friendship/Unfriend',
            data: { id: id },
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.send-friend-request').on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'FriendRequest/SendFriendRequest',
            data: { id: id },
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.accept-friend-request').on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'Friendship/AddFriend', 
            data: { id: id },
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.reject-friend-request').on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'FriendRequest/DeleteFriendRequest', 
            data: { id: id },
            success: function (cities) {
                location.reload();
            }
        });
    });

    $('.delete-friendrequest').on('click', function () {
        $.ajax({
            type: 'POST',
            url: 'FriendRequest/DeleteFriendRequest', 
            data: { id: id },
            success: function (cities) {
                location.reload();
            }
        });
    });
});