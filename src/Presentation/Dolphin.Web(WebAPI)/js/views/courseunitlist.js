directory.CourseUnitListView = Backbone.View.extend({

    tagName:'ul',

    className: 'nav nav-list',
    
    events: {
        "click .k-link": "changePage"
    },

    initialize:function () {
        var self = this;
        this.model.on("reset", this.render, this);
        this.model.on("add", function (courseUnit) {
            self.$el.append(new directory.CourseUnitListItemView({model:courseUnit}).render().el);
        });
        this.toPage = 1;
        this.pageSize = 9;
    },

    render: function () {
        this.$el.empty();
        _.each(_.first(_.rest(this.model.models, (this.toPage - 1) * this.pageSize), this.pageSize), function (courseUnit) {
            this.$el.append(new directory.CourseUnitListItemView({model:courseUnit}).render().el);
        }, this);
        this.$el.append(this.template({}));

        return this;
    },
    
    changePage: function (event) {
        this.toPage = event.target.dataset.page;
        this.render();
    }
});

directory.CourseUnitListItemView = Backbone.View.extend({

    tagName:"li",

    initialize:function () {
        this.model.on("change", this.render, this);
        this.model.on("destroy", this.close, this);
    },

    render:function () {
        // The clone hack here is to support parse.com which doesn't add the id to model.attributes. For all other persistence
        // layers, you can directly pass model.attributes to the template function
        var data = _.clone(this.model.attributes);
        data.id = this.model.id;
        this.$el.html(this.template(data));
        return this;
    }

});