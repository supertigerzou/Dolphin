define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        tpl = require('text!tpl/CourseLessonListItem.html'),

        template = _.template(tpl);

    return Backbone.View.extend({
        tagName: "div",

        initialize: function () {
            this.$el.data("id", this.model.attributes.Id);
            this.$el.attr("class", "ets-ui-lesson-container ets-nth-" + this.model.attributes.No);
            this.model.on("change", this.render, this);
            this.model.on("destroy", this.close, this);
        },

        render: function () {
            this.$el.html(template(this.model.attributes));
            return this;
        }
    });

});