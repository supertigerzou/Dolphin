/// <reference path="../jquery-1.9.1.intellisense.js" />

define(function (require) {
    var
        utils = require('/Scripts/app/utils.js'),
        country = require('/Scripts/app/dataservice.country.js'),
        RecordSet = function () {
            var items = {},
            getData = function (options) {
                var results = options && options.results;
                return $.Deferred(function (def) {
                    $('#busyindicator').text('loading...');
                    country.getCountries({
                        success: function (data) {
                            items = _.reduce(data, function (memo, dto) {
                                memo[dto.Code] = dto;
                                return memo;
                            }, {});
                            var underlyingArray = utils.mapMemoToArray(items);

                            results(underlyingArray);
                            def.resolve(results);
                        },
                        error: function (response) {
                            def.reject(response);
                        }
                    });
                }).promise();
            };

            return {
                getData: getData
            };
        },
        countries = new RecordSet();

    return {
        countries: countries
    };
});