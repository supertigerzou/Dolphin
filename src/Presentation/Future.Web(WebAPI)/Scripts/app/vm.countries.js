define('vm.countries', ['underscore', 'ko', 'utils', 'datacontext'], function (_, ko, utils, datacontext) {
    var countryTemplate = "countries.view",
        countries = ko.observable(),
        items = {},
        activate = function() {
            datacontext.countries.getData()
            .done(function (data) {
                $('#busyindicator').text('loaded ' + data.length + ' objects.');
                items = _.reduce(data, function (memo, dto) {
                    memo[dto.Code] = dto;
                    return memo;
                }, {});
                var underlyingArray = utils.mapMemoToArray(items);

                countries(underlyingArray);
                console.log(countries());
            });
        };

    return {
        activate: activate,
        countryTemplate: countryTemplate,
        countries: countries
    };
});