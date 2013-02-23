define(function (require) {
    var
        config = require('/Scripts/app/config.js'),
        countries = require('/Scripts/app/vm.countries.js'),
        dataprimer = require('/Scripts/app/dataprimer.js'),
        bind = function () {
            ko.applyBindings(dataprimer, $('#applicationHost').get(0));
            ko.applyBindings(countries, $(config.viewIds.countries).get(0));
        };

    return {
        bind: bind
    };
});