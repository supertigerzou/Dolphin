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

define(function (require) {
    var bootstrapper = require('/Scripts/app/bootstrapper.js'),
        router = require('/Scripts/app/router.js');
    
    bootstrapper.run();
});