define('binder', ['ko', 'config', 'vm.countries'], function (ko, config, countries) {
    var bind = function () {
        ko.applyBindings(countries, $(config.viewIds.countries).get(0));
    };

    return {
        bind: bind
    };
});