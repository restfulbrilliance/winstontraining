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
                var $textArea = $('#js-responseData');
                $textArea.text(JSON.stringify(response));
            },

            //FAILURE
            function () {
                console.log("error response code received");
            }
        );

        console.log("getAllDevelopers method complete.");
    }

    function createDeveloper(firstName, lastName) {

        console.log("createDeveloper start.");

        var developer = {
            firstName: firstName,
            lastName: lastName
        };

        $.ajax(
            {
                url: '/api/developers',
                contentType: 'application/json',
                method: 'POST',
                data: JSON.stringify(developer) 
            }
        ).then(

            //SUCCESS
            function (response, status, jxhr) {
                console.log("createDeveloper received response.");
                console.log("response = %o", response);
                console.log("status = " + status);
                console.log("jxhr = %o", jxhr);

                //select the element with id=responseData using jQuery
                var $textArea = $('#js-responseData');
                $textArea.text(JSON.stringify(response));
            },

            //FAILURE
            function () {
                console.log("error response code received");
            }
        );

        console.log("createDeveloper method complete.");
    }

    //DOM ready for the JavaScript class
    $(function () {
    });

    return {
        getAllDevelopers: getAllDevelopers,
        createDeveloper : createDeveloper
    };
})(jQuery);


