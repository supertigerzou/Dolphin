define(function (require) {

    "use strict";

    var $           = require('jquery'),
        _           = require('underscore'),
        Backbone = require('backbone'),
        WeiboListView = require('app/views/weiboList'),
        models = require('app/models/weibo'),
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
            $('.twit-reader', this.el).append(this.weiboPostsView.render().el);

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
        },
        
        events: {
            "click #loadPosts": "load"
        },
        
        load: function () {
            WB2.anyWhere($.proxy(function (W) {
                W.parseCMD("/statuses/user_timeline.json", $.proxy(function (sResult, bStatus) {
                    if (bStatus == true) {
                        this.weiboPosts.set(sResult.statuses);
                    }
                }, this), {
                    userid: 1418197612
                }, {
                    method: 'get'
                });
            }, this));
        },

    });

});