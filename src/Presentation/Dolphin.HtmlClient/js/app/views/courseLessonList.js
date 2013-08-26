define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        CourseLessonListItemView = require('app/views/courseLessonListItem');

    return Backbone.View.extend({
        tagName: 'div',
        id: 'unitLessons',

        initialize: function(options) {
            var self = this;
            this.$el.css({ width: '800px' });
            this.model.on("reset", this.render, this);
            this.model.on("add", function (courseLesson) {
                self.$el.append(new CourseLessonListItemView({ model: courseLesson }).render().el);
            });
        },

        render: function() {
            this.$el.empty();
            _.each(this.model.models, function(courseLesson) {
                this.$el.append(new CourseLessonListItemView({ model: courseLesson }).render().el);
            }, this);

            return this;
        }
    });

});