define(function (require) {
    var
        config = require('/Scripts/app/config.js'),
        binder = require('/Scripts/app/binder.js'),
        dataprimer = require('/Scripts/app/dataprimer.js'),
        countries = require('/Scripts/app/vm.countries.js'),
        run = function () {
            config.dataServiceInit();

            $.when(dataprimer.fetch())
            .done(function () {
                countries.activate();
            })
            .done(binder.bind());
        };
    return {
        run: run
    };
});