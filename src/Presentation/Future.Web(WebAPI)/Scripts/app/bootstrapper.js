define('bootstrapper', ['jquery', 'infuser', 'ko', 'vm.countries'], function ($, infuser, ko, countries) {
    var run = function () {
        $('#busyindicator').text('start loading...');
        countries.activate();
        
        infuser.defaults.templatePrefix = "_";
        infuser.defaults.templateSuffix = ".tmpl.html";
        infuser.defaults.templateUrl = "/Tmpl";

        ko.applyBindings(countries, $('#countries-view').get(0));
    };
    return {
        run: run
    };
});