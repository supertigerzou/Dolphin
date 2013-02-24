define(function (require) {
    var
        config = require('config'),
        binder = require('binder'),
        dataprimer = require('dataprimer'),
        countries = require('vm.countries'),
        router = require('router'),
        run = function () {
            config.dataServiceInit();

            router.mapNav('vm.countries');

            $.when(router.activate('vm.countries'))
            .then(dataprimer.fetch())
            .done(function () {
                countries.activate();
            })
            .done(binder.bind());
        };
    return {
        run: run
    };
});