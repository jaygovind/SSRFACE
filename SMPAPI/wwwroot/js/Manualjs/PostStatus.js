$(document).ready(function () {

    //Post New Status
    $('.PostStatus').on('click', function () {
        debugger;

        var NewstatusorPost = $('#NewStatusTextarea').val();

        //alert('Hello');
        var Postorstatus = {
            PostContent: NewstatusorPost
        };

        $.ajax({

            type: 'POST',
            url: "/Newpost/AddnewpostStatus",
            data: { Postorstatus},
            success: function (response) {
                debugger;
                //var allCommentsArea = $('<div>').addClass('panel panel-default post-content');
                //allCommentsArea.html(response);

                $("#Newpost").prepend(response);
            },
            error: function (response) {
                alert('Sorry: Something Wrong');
            }

        });

    });

    jQuery("time.timeago").timeago();

});