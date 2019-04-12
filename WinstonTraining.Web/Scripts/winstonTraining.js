var _winstonTraining = window.winstonTraining || {};

_winstonTraining = (function ($) {

    function publicMethod1() {
        alert("public method 1");
    }

    function publicMethod2() {
        alert("public method 2");
    }

    function privateMethod() {
        alert("private method");
    }

    //DOM ready for the JavaScript class
    $(function () {

        //var domElement = document.getElementById('homePageMainBody');

        //var $els = $('#testSpan').closest('[data-js-match="testMatch"]');

        //if ($els.length > 0)
            //$els.remove();
            //$els.css({ color: 'red' });
    });

    return {
        method1: publicMethod1,
        method2: publicMethod2
    };

})(jQuery);
