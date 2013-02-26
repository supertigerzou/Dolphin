requirejs.config({
    paths: {
        jquery: '../jquery-1.9.1'
    }
});

define(function (require) {
    var
        config = require('config'),
        binder = require('binder'),
        dataprimer = require('dataprimer'),
        shell = require('shell'),
        router = require('router');

        config.dataServiceInit();

        router.mapNav('welcome');
        router.mapNav('vm.countries');

        $.when(dataprimer.fetch())
        .done(function () {
            shell.activate('shell');
        })
        .done(binder.bind());
});