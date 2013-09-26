define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        tpl = require('text!tpl/WeiboListItem.html'),

        template = _.template(tpl);

    return Backbone.View.extend({
        tagName: "div",

        className: "weiboPost",

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