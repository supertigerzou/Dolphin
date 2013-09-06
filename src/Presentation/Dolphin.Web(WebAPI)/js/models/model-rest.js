directory.CourseUnit = Backbone.Model.extend({
    urlRoot: "http://localhost/Dolphin.Web.WebAPI/Api/Course/Units",
    initialize: function () {

    }
});

directory.CourseUnitCollection = Backbone.Collection.extend({
    model: directory.CourseUnit,
    url: "http://dolphin.azurewebsites.net/Api/Course/Search"
});