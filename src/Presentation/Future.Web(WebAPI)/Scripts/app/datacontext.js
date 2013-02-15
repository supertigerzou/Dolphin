/// <reference path="../jquery-1.9.1.intellisense.js" />

define('datacontext', ['jquery', 'dataservice.country'], function ($, country) {
    var countries = {
        getData: function () {
            return $.Deferred(function (def) {
                $('#busyindicator').text('loading...');
                country.getCountries({
                    success: function (dto) {
                        def.resolve(dto);
                    },
                    error: function (response) {
                        def.reject(response);
                    }
                });
            }).promise();
        }
    };

    return {
        countries: countries
    };
});