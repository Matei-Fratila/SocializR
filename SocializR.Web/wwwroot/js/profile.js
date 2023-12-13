jQuery(function ($) {

    $('.select2').select2();

    var id = $('#Id').val();
    var page = 1;

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

    $(window).on("scroll", function () {
        if ($(window).scrollTop() === $(document).height() - $(window).height()) {
            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'GET',
                url: 'Home/NextPosts',
                data: { userId: id, page: page, isProfileView: true },
                success: function (response) {
                    if (response.length === 0) {
                        $(window).off(scroll);
                        $('#posts-container').append("<p>No more posts</p>");
                    }

                    var requests = Array();

                    for (var i = 0; i < response.length; i++) {
                        var post = response[i];
                        requests.push(
                            $.ajax({
                                dataType: "html",
                                type: 'GET',
                                url: 'Home/GetPostWidget',
                                data: post,
                                success: function (htmlResponse) {

                                },
                                error: function (ex) {
                                    alert('error');
                                }
                            })
                        );
                    }

                    var defer = $.when.apply($, requests);
                    defer.done(function () {

                        $.each(arguments, function (index, responseData) {
                            var newelement = $.parseHTML(responseData[0]);
                            $('#posts-container').append(newelement);
                            PutUserLikes(newelement);
                            PutDeletePostButtons(newelement);
                            PutDeleteCommentButtons(newelement);
                        });

                    });
                },
                fail: function (ex) {
                    alert('something went wrong');
                }
            });

            page++;
        }
    });

    (() => PutUserLikes($('*')))();
   // (() => PutDeletePostButtons($('*')));
    (() => PutDeleteCommentButtons($('*')));

    function PutUserLikes(elems) {
        $(elems).find('.like').each(function (index, elem) {
            if ($(elem.parentElement).find('.is-liked').val() === 'True') {
                $(elem).addClass('liked');
            }
        });
        LikeOrUnlike($(elems).find('.like'));
    }
    function PutDeleteCommentButtons(elems) {
        $(elems).find('.delete-comment-btn').on('click', deleteCommentHandler);
    }
    function PutDeletePostButtons(elems) {
        $(elems).find('.delete-post-container').each(function (index, elem) {
            if ($(elem.parentElement).find('.is-current-user-post').val() === 'True') {
                var deleteButton = $('<button class="float-right delete-post-btn close" type="button" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
                $(elem).append(deleteButton);
            }
        });
    }

    function LikeOrUnlike(likes) {

        for (i = 0; i < likes.length; i++) {
            (function () {
                var like = likes[i];
                var id = $(like.parentElement).find('#Id').val();

                like.addEventListener("click", function (e) {

                    if (e.target.tagName === "I") {
                        $(e.target).parent().toggleClass("liked");
                    }
                    if (e.target.tagName === "TEXT") {
                        $(e.target).parent().toggleClass("liked");
                    }
                    if (e.target.tagName === "BUTTON") {
                        $(e.target).toggleClass("liked");
                    }

                });

                like.addEventListener("click", function (e) {
                    DecideLike(id, like);
                });
            })();
        }
    }
    var DecideLike = function (id, like) {
        var elem = $(like.parentElement).find('.see-likes');

        //the other way around because toggle function on like buttons happens first
        if ($(like).hasClass('liked')) {
            AddLike(id, elem);
        }
        else {
            DeleteLike(id, elem);
        }

    };
    var AddLike = function (id, elem) {
        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'Home/Like',
            data: { id: id },
            success: function () {
                var text = $(elem).text().trim().split(" ");
                var nrOfLikes = text[0];
                var likesIncremented = parseInt(nrOfLikes) + 1;
                $(elem).text(likesIncremented.toString() + ' Hearts');
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });
    };
    var DeleteLike = function (id, elem) {
        $.ajax({
            headers: {
                'Accept': 'application/json'
            },
            type: 'POST',
            url: 'Home/DeleteLike',
            data: { id: id },
            success: function () {
                var text = $(elem).text().trim().split(" ");
                var nrOfLikes = text[0];
                var likesIncremented = parseInt(nrOfLikes) - 1;
                $(elem).text(likesIncremented.toString() + ' Hearts');
            },
            fail: function (ex) {
                alert('something went wrong');
            }
        });

    };
});