define(function (require) {
    var
        config = require('config'),
        countries = require('vm.countries'),
        dataprimer = require('dataprimer'),
        bind = function () {
            ko.applyBindings(dataprimer, $('#applicationHost').get(0));
            ko.applyBindings(countries, $(config.viewIds.countries).get(0));
        };

    return {
        bind: bind
    };
});