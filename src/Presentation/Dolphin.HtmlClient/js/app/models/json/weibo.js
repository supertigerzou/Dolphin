define(function (require) {

    "use strict";

    var $ = require('jquery'),
        Backbone = require('backbone'),
        WeiboPost = Backbone.Model.extend({
            //urlRoot: "https://api.weibo.com/2/statuses/show.json?id=3625255643417090&access_token=2.00CzbyXB_tJgIC80157f788eEibekC",
            urlRoot: "https://api.weibo.com/2/statuses/user_timeline.json?userid=1418197612&source=1960524153&_cache_time=0&method=get&access_token=2.00CzbyXB_tJgIC80157f788eEibekC&__rnd=1380178791657",
            initialize: function() {

            }
        }),
        
        WeiboPostCollection = Backbone.Collection.extend({
            model: WeiboPost,
            url: "https://api.weibo.com/2/statuses/user_timeline.json?userid=1418197612&source=1960524153&_cache_time=0&method=get&access_token=2.00CzbyXB_tJgIC80157f788eEibekC&__rnd=1380178791657"
        });

    return {
        WeiboPost: WeiboPost,
        WeiboPostCollection: WeiboPostCollection
    };

});