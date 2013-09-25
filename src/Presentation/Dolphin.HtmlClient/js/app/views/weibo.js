define(function (require) {

    "use strict";

    var $           = require('jquery'),
        _           = require('underscore'),
        Backbone    = require('backbone'),
        tpl         = require('text!tpl/Weibo.html'),

        template = _.template(tpl);

    return Backbone.View.extend({

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