requirejs.config({
    paths: {
        jquery: '../jquery-1.9.1'
    }
});

define(function (require) {
    var bootstrapper = require('bootstrapper');
    
    bootstrapper.run();
});