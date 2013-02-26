define(function () {
    var
        viewIds = {
            countries: '#countries-view',
            welcome: '#welcome-view'
        },
        dataServiceInit = function () { },
        configExternalTemplate = function () {
            infuser.defaults.templatePrefix = "_";
            infuser.defaults.templateSuffix = ".tmpl.html";
            infuser.defaults.templateUrl = "/Tmpl";
        },
        init = function () {
            configExternalTemplate();
        };

    init();

    return {
        dataServiceInit: dataServiceInit,
        viewIds: viewIds
    };
});