define(function (require) {

    "use strict";

    var $           = require('jquery'),
        _           = require('underscore'),
        Backbone    = require('backbone'),
        tpl         = require('text!tpl/Weibo.html'),
        WeiboListItemView = require('app/views/WeiboListItem'),
        template = _.template(tpl);

    return Backbone.View.extend({
        tagName: 'div',
        id: 'searchResults',
        className: 'k-widget k-listview',

        initialize: function (options) {
            var self = this;
            this.model.on("reset", this.render, this);
            this.model.on("add", function (weiboPost) {
                self.$el.append(new WeiboListItemView({ model: weiboPost }).render().el);
            });
        },

        render: function () {
            this.$el.empty();
            _.each(this.model.models, function (weiboPost) {
                this.$el.append(new WeiboListItemView({ model: weiboPost }).render().el);
            }, this);

            return this;
        }

    });

});