define(function (require) {
    var isNavigating = ko.observable(false);

    return {
        isNavigating: isNavigating
    };
});