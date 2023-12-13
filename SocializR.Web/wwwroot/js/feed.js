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
        this.setAttribute('style', 'height:' + this.scrollHeight + 'px;overflow-y:hidden;');
    }).on('input', function () {
        this.style.height = 'auto';
        this.style.height = this.scrollHeight + 'px';
    });

    var page = 1;

    var profilePhoto = $('#current-user-photo').attr('src');

    //Puts user picture on new elements or at page refresh
    (function PutUserPictures() {
        $('.current-user-photo').attr('src', profilePhoto);
    })();

    function PutUserPictures(elems) {
        $(elems).find('.current-user-photo').attr('src', profilePhoto);
    }

    //Puts user likes on new elements or at page refresh
    (() => PutUserLikes($('.row')))();

    function PutUserLikes(elems) {
        $(elems).find('.like').each(function (index, elem) {
            if ($(elem.parentElement).find('.is-liked').val() === 'True') {
                $(elem).addClass('liked');
            }
        });
        LikeOrUnlike($(elems).find('.like'));
    }

    $('#posts-container').on('click', '.delete-post-btn', function deletePost(ev) {

        var result = confirm("Are you sure you want to delete this post?");

        if (result === true) {
            var postId = $(ev.target).closest('.post-container').find('#Id').val();

            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'POST',
                url: 'Home/DeletePost',
                data: { id: postId },
                success: function () {
                    $(ev.target).closest('.post-container').remove();
                },
                fail: function (ex) {
                    alert('something went wrong');
                }
            });
        }
    });

    function ChangeComments(elem, number) {
        var text = $(elem).text().split(" ");
        var nrOfComments = text[0];
        var newNrOfComments = parseInt(nrOfComments) + number;
        $(elem).text(newNrOfComments.toString() + ' Comments');
    }

    //Delete comment
    var deleteCommentHandler = function deleteComment(ev) {

        var result = confirm("Are you sure you want to delete this comment?");

        if (result === true) {
            commentId = $(ev.target).closest('.delete-comment-container').find('.comment-id').val();
            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'POST',
                url: 'Home/DeleteComment',
                data: { id: commentId },
                success: function () {
                    ChangeComments($($(ev.target).closest('.post-container')).find('.see-comments .nr-of-comments'), -1);
                    $(ev.target).closest('.comment-container').remove();
                },
                fail: function (ex) {
                    alert('something went wrong');
                }
            });
        } 
    };

    (() => $('.delete-comment-btn').on('click', deleteCommentHandler))();

    function PutDeleteCommentButtons(elems) {
        $(elems).find('.delete-comment-btn').on('click', deleteCommentHandler);
    }

    (function PutDeletePostButtons() {
        $('.delete-post-container').each(function (index, elem) {
            if ($(elem.parentElement).find('.is-current-user-post').val() === 'True') {
                var deleteButton = $('<button class="float-right delete-post-btn close" type="button" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
                $(elem).append(deleteButton);
            }
        });
    }());

    function PutDeletePostButtons(elems) {
        $(elems).find('.delete-post-container').each(function (index, elem) {
            if ($(elem.parentElement).find('.is-current-user-post').val() === 'True') {
                var deleteButton = $('<button class="float-right delete-post-btn close" type="button" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
                $(elem).append(deleteButton);
            }
        });
    }

    //Infinite Scroll
    $(window).on("scroll", function () {
        if ($(window).scrollTop() === $(document).height() - $(window).height()) {
            let userId = $('#Id').val();
            $.ajax({
                headers: {
                    'Accept': 'application/json'
                },
                type: 'GET',
                url: 'Home/NextPosts',
                data: { userId: userId, page: page , isProfileView: false},
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
                            PutUserPictures(newelement);
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

    //Handle File Selects in Post
    function handleFileSelect(evt) {
        var files = evt.target.files;

        for (var i = 0; i < files.length; i++) {
            var file = files[i];

            if (file.type.match('image.*')) {

                var reader = new FileReader();
                reader.onload = (function () {
                    return function (e) {
                        $('#new-post-video').parent()[0].hidden = true
                        $('#new-post-image').attr("src", e.target.result);
                        $('#new-post-image').show();
                    };
                })(file);
                reader.readAsDataURL(file);
            }
            else if (file.type.match('video.*')) {

                reader = new FileReader();
                reader.onload = (function () {
                    return function (e) {
                        $('#new-post-image').hide();
                        var $source = $('#new-post-video');
                        $source[0].src = URL.createObjectURL(file);
                        $source.parent()[0].load();
                        $source.parent()[0].hidden = false;
                    };
                })(file);
                reader.readAsDataURL(file);
            }
        }
    }

    $('#files').on('change', handleFileSelect);

    $('#images').on('click', '.delete-link', function (e) {

        var parent = e.target.parentElement.parentElement;
        var imageId = $(parent).find('.image-id').val();

        var divToDelete = e.target.parentElement.parentElement.parentElement;
        divToDelete.removeChild(parent);
    });

    //Handle New Post
    $('#share').on('click', function (e) {

        //var newPhotos = $('.image-id[value="-1"]').parent();
        //var imagesToInsert = [];
        //var title = $('#post-title').val();

        //newPhotos.each(function (index, element) {
        //    imagesToInsert.push({
        //        Content: $(element).find("img").attr("src"),
        //        Caption: $(element).find($('.caption')).val()
        //    });
        //});

        var form = $('#post-form');
        var body = $('#post-body').val();
        var title = $('#post-title').val();

        if (body === "") {
            alert('your post must have a body');
            return false;
        }

        formData.append('title', title);
        formData.append('body', body);

        var token = $('input[name="__RequestVerificationToken"]', form).val();

        $.ajax({
            type: 'POST',
            url: 'Home/AddPost',
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                location.reload();
            },
            error: function () {
                alert('something went wrong');
            }
        });
    });

    //Handle New Comment
    $('#posts-container').on('click', '.comment', function (e) {

        var parent = e.target.parentElement.parentElement;
        var comment = $(parent).find('.comment-body').val();

        parent = e.target.parentElement.parentElement.parentElement;
        var postId = $(parent).find('#Id').val();

        if (comment !== "") {
            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                dataType: "json",
                type: 'POST',
                url: 'Home/AddComment',
                data: JSON.stringify({ body: comment.trim(), postId: postId }),
                success: function (response) {

                    $.get('Home/GetCommentWidget', { body: comment.trim() }, function (htmlResponse) {
                        var newelement = $.parseHTML(htmlResponse);
                        var elem = $(newelement[1]).find('.search-for-link');
                        var text = $(elem).text().trim();
                        elem.text(text);
                        //$(newelement[1]).linkify();
                        $(e.target).closest('.comments-container').find('.fresh-comments-container').append(newelement[1]);
                        $(newelement).find('.comment-id').val(response);
                        PutDeleteCommentButtons(newelement);
                    });

                    ChangeComments($(e.target).closest('.post-container').find('.see-comments .nr-of-comments'), 1);
                    $(parent).find('.comment-body').val("");
                },
                fail: function (ex) {
                    alert('something went wrong');
                }
            });
        }
    });

    $('.more-comments').on('click', function (elem) {
        var container = $(elem.target).closest('.post-footer').find('.comments-container');
        var page = $(elem.target).closest('.post-footer').find('.comments-page').val();
        var postId = $(elem.target).closest('.post-footer').find('#Id').val();
        $.get('Home/NextComments', { page: page, postId: postId }), function (response) {
            var requests = Array();
            for (var i = 0; i < response.length; i++) {
                var comment = response[i];
                requests.push(
                    $.ajax({
                        dataType: "html",
                        type: 'GET',
                        url: 'Home/GetComment',
                        data: comment,
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
                    var elem = $(newelement[1]).find('.search-for-link');
                    var text = $(elem).text().trim();
                    elem.text(text);
                    $(newelement[1]).linkify();
                    $(container).prepend(newelement);
                    PutDeleteCommentButtons(newelement);
                });
                $(elem.target).closest('.post-footer').find('.comments-page').val(parseInt(page) + 1);
            });
        };
    });

    //Handle New Like
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