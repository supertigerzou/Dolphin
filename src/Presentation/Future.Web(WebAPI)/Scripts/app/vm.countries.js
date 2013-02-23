define(function (require) {
    var
        dataContext = require('/Scripts/app/datacontext.js'),
        countryTemplate = "countries.view",
        countries = ko.observableArray(),
        activate = function() {
            dataContext.countries.getData({ results: countries });
        };

    return {
        activate: activate,
        countryTemplate: countryTemplate,
        countries: countries
    };
});