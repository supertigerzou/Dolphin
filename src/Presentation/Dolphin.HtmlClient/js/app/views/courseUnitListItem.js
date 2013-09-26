define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        tpl = require('text!tpl/CourseUnitListItem.html'),

        template = _.template(tpl);

    return Backbone.View.extend({
        tagName: "div",

        className: "courseUnit",

        initialize: function () {
            this.$el.data({ id: this.model.attributes.Id });
            this.model.on("change", this.render, this);
            this.model.on("destroy", this.close, this);
        },

        render: function() {
            this.$el.html(template(this.model.attributes));
            return this;
        }
    });

});