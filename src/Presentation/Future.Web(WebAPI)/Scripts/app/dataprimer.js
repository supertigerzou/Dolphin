define('dataprimer', ['jquery', 'ko', 'datacontext'], function ($, ko, datacontext) {
    var
        fetch = function () {
            return $.Deferred(function (def) {
                var data = {
                    countries: ko.observable()
                };

                $.when(datacontext.countries.getData({ results: data.countries }))
                    .done(function () {
                        def.resolve();
                    });
            }).promise();
        };

    return {
        fetch: fetch
    };
});