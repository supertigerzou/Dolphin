define('vm.countries', ['underscore', 'ko', 'datacontext'], function (_, ko, datacontext) {
    var countryTemplate = "countries.view",
        countries = ko.observableArray(),
        activate = function() {
            datacontext.countries.getData({results: countries});
        };

    return {
        activate: activate,
        countryTemplate: countryTemplate,
        countries: countries
    };
});