var _winstonTraining = window.winstonTraining || {};

_winstonTraining = (function ($) {

    function getAllDevelopers() {

        console.log("getAllDevelopers start.");

        $.ajax(
            {
                url: '/api/developers'
            }
        ).then(
            //SUCCESS
            function (response, status, jxhr) {
                console.log("getAllDevelopers received response.");
                console.log("response = %o", response);
                console.log("status = " + status);
                console.log("jxhr = %o", jxhr);

                //select the element with id=responseData using jQuery
                var $textArea = $('#responseData');
                $textArea.text(JSON.stringify(response));
            },
            //FAILURE
            function () {
                console.log("error response code received");
            }
        );

        console.log("getAllDevelopers method complete.");
    }

    //DOM ready for the JavaScript class
    $(function () {
    });

    return {
        getAllDevelopers: getAllDevelopers
    };

})(jQuery);
