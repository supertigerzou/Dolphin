define(function (require) {

    "use strict";

    var $ = require('jquery'),
        Backbone = require('backbone'),
        CourseUnit = Backbone.Model.extend({
            urlRoot: "http://localhost/Dolphin.Web.WebAPI/Api/Course/Units",
            initialize: function() {

            }
        }),
        
        CourseUnitCollection = Backbone.Collection.extend({
            model: CourseUnit,
            url: "http://localhost/Dolphin.Web.WebAPI/Api/Course/Search"
        });

    return {
        CourseUnit: CourseUnit,
        CourseUnitCollection: CourseUnitCollection
    };

});