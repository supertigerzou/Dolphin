define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        tpl = require('text!tpl/Paging.html'),
        template = _.template(tpl);

    return Backbone.View.extend({
        tagName: 'div',

        className: 'k-pager-wrap k-widget',

        initialize: function(options) {
            var self = this;
            this.model.on("change", this.render, this);
        },

        render: function() {
            this.$el.empty();
            this.$el.append(template(this.model.attributes));

            return this;
        }
    });

});