define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        CourseUnitListItemView = require('app/views/courseUnitListItem');

    return Backbone.View.extend({
        tagName: 'div',
        id: 'searchResults',
        className: 'k-widget k-listview k-selectable',

        initialize: function(options) {
            var self = this;
            this.model.on("reset", this.render, this);
            this.model.on("add", function(courseUnit) {
                self.$el.append(new CourseUnitListItemView({ model: courseUnit }).render().el);
            });
        },

        render: function() {
            this.$el.empty();
            _.each(this.model.models, function(courseUnit) {
                this.$el.append(new CourseUnitListItemView({ model: courseUnit }).render().el);
            }, this);

            return this;
        }
    });

});