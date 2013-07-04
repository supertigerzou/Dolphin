define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        CourseLessonListItemView = require('app/views/courseLessonListItem');

    return Backbone.View.extend({
        tagName: 'div',
        id: 'unitLessons',
        className: 'k-widget k-listview k-selectable',

        initialize: function(options) {
            var self = this;
            this.$el.css({ width: '800px' });
            this.model.on("reset", this.render, this);
            this.model.on("add", function(courseUnit) {
                self.$el.append(new CourseLessonListItemView({ model: courseUnit }).render().el);
            });
        },

        render: function() {
            this.$el.empty();
            _.each(this.model.models, function(courseUnit) {
                this.$el.append(new CourseLessonListItemView({ model: courseUnit }).render().el);
            }, this);

            return this;
        }
    });

});