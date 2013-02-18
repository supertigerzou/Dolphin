define('bootstrapper', ['jquery', 'infuser', 'ko', 'config', 'binder', 'dataprimer', 'vm.countries'],
    function ($, infuser, ko, config, binder, dataprimer, countries) {
        var run = function () {
            $('#busyindicator').text('start loading...');

            config.dataServiceInit();

            $.when(dataprimer.fetch())
            .done(function () {
                countries.activate();
            })
            .done(binder.bind());
        };
        return {
            run: run
        };
    });