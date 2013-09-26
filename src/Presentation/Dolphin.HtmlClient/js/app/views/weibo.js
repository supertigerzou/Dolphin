define(function (require) {

    "use strict";

    var $           = require('jquery'),
        _           = require('underscore'),
        Backbone = require('backbone'),
        WeiboListView = require('app/views/weiboList'),
        tpl         = require('text!tpl/Weibo.html'),
        template = _.template(tpl);

    return Backbone.View.extend({
        initialize: function () {
            this.weiboPosts = new models.WeiboPostCollection();

            this.weiboPostsView = new WeiboListView({
                model: this.weiboPosts
            });
        },

        render: function () {
            this.$el.html(template());

            WB2.anyWhere(function (W) {
                W.widget.connectButton({
                    id: "wb_connect_btn",
                    type: "3,2",
                    callback: {
                        login: function (o) {
                            console.log('logged in successfully');
                        },
                        logout: function () {
                        }
                    }
                });
            });
            
            return this;
        }

    });

});