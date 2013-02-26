define(function (require) {
    var
        config = require('config'),
        countries = require('vm.countries'),
        dataprimer = require('dataprimer'),
        bind = function () {
            ko.bindingHandlers.compose = {
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var activeItem = ko.utils.unwrapObservable(valueAccessor()) || {};

                }
            };

            ko.virtualElements.allowedBindings.compose = true;
            ko.applyBindings(dataprimer, $('#applicationHost').get(0));
            ko.applyBindings(countries, $(config.viewIds.countries).get(0));
        };

    return {
        bind: bind
    };
});