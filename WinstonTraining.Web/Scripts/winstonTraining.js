var _winstonTraining = window.winstonTraining || {};

_winstonTraining = (function ($) {

    function getAllDevelopers() {

        console.log("getAllDevelopers start.");

        $.ajax(
            {
                url: '/api/developers'
            }
        ).then(
            function (response, status, jxhr) {
                console.log("getAllDevelopers received response.");
                console.log(response);
                console.log("status = " + status);
                console.log(jxhr);
            },
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
