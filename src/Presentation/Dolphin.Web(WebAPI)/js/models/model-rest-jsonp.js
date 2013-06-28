directory.Employee = Backbone.Model.extend({

    urlRoot:"http://localhost:3000/employees",

    initialize:function () {
        this.reports = new directory.EmployeeCollection();
        this.reports.url = this.urlRoot + "/" + this.id + "/reports";
    },
    
    sync: function (method, model, options) {
        if (method === "read") {
            options.dataType = "jsonp";
        }
        return Backbone.sync.apply(Backbone, arguments);
    }

});

directory.EmployeeCollection = Backbone.Collection.extend({

    model: directory.Employee,

    url:"http://localhost:3000/employees",

    sync: function (method, model, options) {
        if (method === "read") {
            options.dataType = "jsonp";
        }
        return Backbone.sync.apply(Backbone, arguments);
    }

});

//var originalSync = Backbone.sync;
//Backbone.sync = function (method, model, options) {
//    if (method === "read") {
//        options.dataType = "jsonp";
//        return originalSync.apply(Backbone, arguments);
//    }
//};