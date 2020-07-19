$(document).ready(function () {

    //Get All ReplyComment
    $('.Reply').on('click', function (e)
    {
        debugger;
        var ComID = $(this).attr('data-id');
        e.stopPropagation();
        e.preventDefault();
        $.ajax({
            type: 'GET',
            //url: '@Url.Action("GetSubComments", "Comments")',
            url: 'Comments/GetSubComments',
            contentType: "application/html; charset=utf-8",
            cache: false,
            datatype: "html",
            data: { ComID },
            success: function (response)
            {
                debugger;
                //Put code here like so

                if ($('div').hasClass('zoneReply_' + ComID + '')) {
                    $('div [class=zoneReply_' + ComID + ']').remove();
                }

                var selReply = $("<div>").addClass('zoneReply_' + ComID);

                selReply.append(response);
                selReply.prependTo($('.ReplayComments_' + ComID));

                $('.ReplayComments_' + ComID).show();

                $("#inputReplay_" + ComID).focus();

            },
            error: function (response) {
                alert('something Wrong');
            }
        });

    });

    //Add Reply Comment
    $('.ReplyAddComment').on('click', function () {
        debugger;
        var ComID = $(this).attr('data-id');
        var CommentMsg = $('#inputReplay_' + ComID).val();
        var dateTimeNow = new Date();

        var subComment = {
            CommentMsg: CommentMsg,
            CommentedDate: dateTimeNow.toLocaleString()
        };


        $.ajax({

            type: 'POST',
            //url: '@Url.Action("AddSubComment", "Comments")',
            url:'/Comments/AddSubComment',
            data: { subComment, ComID },
            dataType: 'html',
            success: function (response) {

                if ($('div').hasClass('zoneReply_' + ComID + '')) {
                    $('div [class=zoneReply_' + ComID + ']').remove();
                }

                var selReply = $("<div>").addClass('zoneReply_' + ComID);

                selReply.append(response);
                selReply.prependTo($('.ReplayComments_' + ComID));

                $('.ReplayComments_' + ComID).show();

            },
            error: function (response) {
                alert('something Wrong');
            }
        });

    });

    jQuery("time.timeago").timeago();


})