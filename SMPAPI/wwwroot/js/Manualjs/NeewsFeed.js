$(document).ready(function () {


    //Click Comment
    $('.Comment').on('click', function () {
        debugger;
        var id = $(this).attr("data-id");
        var allCommentsArea = $('<div>').addClass('allComments_' + id);
        var Url = $(this).data('request-url');
        //function that allow us to get all comments related to post id
        $.ajax({

            type: 'GET',
            url: Url,
            data: { postId: id },
            success: function (response) {

                if ($('div').hasClass('allComments_' + id + '')) {
                    $('div[class=allComments_' + id + ']').remove();
                }
                //console.log(response);


                allCommentsArea.html(response);
                allCommentsArea.prependTo('#commentsBlock_' + id);


            },
            error: function (response) {
                alert('Sorry: Comments cannot be loaded !');
            }


        })

    });

    //Add New Comment
    $('.addComment').on('click', function () {
        debugger;
        var Url = $(this).data('request-url');
        var postId = $(this).attr('data-id');
        var commentMsg = $('#comment_' + postId).val();
        var dateTimeNow = new Date();

        //alert('Hello');
        var comment = {
            CommentMsg: commentMsg,
            CommentedDate: dateTimeNow.toLocaleString()
        };

        $.ajax({

            type: 'POST',
            url: Url,
            data: { comment, postId },
            success: function (response) {

                $('div[class=allComments_' + postId + ']').remove();

                var allCommentsArea = $('<div>').addClass('allComments_' + postId);
                allCommentsArea.html(response);

                allCommentsArea.prependTo('#commentsBlock_' + postId);

            },
            error: function (response) {
                alert('Sorry: Something Wrong');
            }

        });

    });

    jQuery("time.timeago").timeago();

});