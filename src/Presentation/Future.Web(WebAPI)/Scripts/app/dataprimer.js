define(function (require) {
    var
        dataContext = require('/Scripts/app/datacontext.js'),
        router = require('/Scripts/app/router.js'),
        fetch = function () {
            return $.Deferred(function (def) {
                var data = {
                    countries: ko.observable()
                };

                router.isNavigating(true);
                $.when(dataContext.countries.getData({ results: data.countries }))
                    .done(function () {
                        def.resolve();
                    }).always(function () {
                        router.isNavigating(false);
                    });
            }).promise();
        };

    return {
        router: router,
        fetch: fetch
    };
});