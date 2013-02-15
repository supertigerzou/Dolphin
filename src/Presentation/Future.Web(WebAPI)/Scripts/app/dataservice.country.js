define('dataservice.country', ['amplify'], function (amplify) {
    var init = function() {
        amplify.request.define('countries', 'ajax', {
            url: '/api/CountryRegion',
            dataType: 'json',
            type: 'GET'
        });
    },
    getCountries = function(callbacks) {
        return amplify.request({
            resourceId: 'countries',
            success: callbacks.success,
            error: callbacks.error
        });
    };

    init();

    return {
        getCountries: getCountries
    };
});