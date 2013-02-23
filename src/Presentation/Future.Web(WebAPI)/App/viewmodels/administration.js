define(function (require) {
    var app = require('durandal/app'),
        dataContext = require('../../Scripts/app/datacontext');
    
    return {
        //view location can be customized as below
        //viewUrl: '/Tmpl/administration.html',
        displayName: 'Administration',
        countryTemplate: 'countries.view',
        countries: ko.observableArray(),
        activate: function () {
            dataContext.countries.getData({results: this.countries});
        },
        select: function(item) {
            //the app model allows easy display of modal dialogs by passing a view model
            //views are usually located by convention, but you an specify it as well with viewUrl
            item.viewUrl = 'views/detail';
            app.showModal(item);
        },
        canDeactivate: function () {
            //the router's activator calls this function to see if it can leave the screen
            return app.showMessage('Are you sure you want to leave this page?', 'Navigate', ['Yes', 'No']);
        }
    };
});