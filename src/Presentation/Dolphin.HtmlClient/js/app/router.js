define(function (require) {

    "use strict";

    var $           = require('jquery'),
        Backbone    = require('backbone'),
        ShellView   = require('app/views/Shell'),
        HomeView    = require('app/views/Home'),

        $body = $('body'),
        shellView = new ShellView({el: $body}).render(),
        $content = $("#content", shellView.el),
        homeView = new HomeView({el: $content});

    // Close the search dropdown on click anywhere in the UI
    $body.click(function () {
        $('.dropdown').removeClass("open");
    });

    $("body").on("click", "#showMeBtn", function (event) {
        event.preventDefault();
        shellView.search();
    });

    return Backbone.Router.extend({

        routes: {
            "": "home",
            "contact": "contact",
            "course": "course",
            "weibo": "weibo",
            "employees/:id": "employeeDetails"
        },

        home: function () {
            homeView.delegateEvents(); // delegate events when the view is recycled
            homeView.render();
            shellView.selectMenuItem('home-menu');
        },

        contact: function () {
            require(["app/views/contact"], function (ContactView) {
                var view = new ContactView({el: $content});
                view.render();
                shellView.selectMenuItem('contact-menu');
            });
        },
        
        course: function () {
            require(["app/views/course"], function (CourseView) {
                var view = new CourseView({ el: $content });
                view.render();
                shellView.selectMenuItem('course-menu');
            });
        },
        
        weibo: function () {
            require(["app/views/weibo"], function (WeiboView) {
                var view = new WeiboView({ el: $content });
                view.render();
                shellView.selectMenuItem('weibo-menu');
            });
        },

        employeeDetails: function (id) {
            require(["app/views/Employee", "app/models/employee"], function (EmployeeView, models) {
                var employee = new models.Employee({id: id});
                employee.fetch({
                    success: function (data) {
                        // Note that we could also 'recycle' the same instance of EmployeeFullView
                        // instead of creating new instances
                        var view = new EmployeeView({model: data, el: $content});
                        view.render();
                    }
                });
                shellView.selectMenuItem();
            });
        }

    });

});