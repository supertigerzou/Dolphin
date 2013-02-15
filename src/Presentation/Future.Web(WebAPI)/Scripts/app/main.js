define('jquery', [], function () { return this.jQuery; });
define('underscore', [], function () { return this._; });
define('amplify', [], function () { return this.amplify; });
define('infuser', [], function () { return this.infuser; });
define('ko', [], function () { return this.ko; });

requirejs.config({
    baseUrl: "/Scripts/app",
    paths: {
        jquery: '../jquery-1.9.1'
    }
});

requirejs(['bootstrapper'], function (bootstrapper) {
    bootstrapper.run();
});