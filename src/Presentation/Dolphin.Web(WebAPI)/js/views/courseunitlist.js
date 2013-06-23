directory.CourseUnitListView = Backbone.View.extend({

    tagName:'ul',

    className:'nav nav-list',

    initialize:function () {
        var self = this;
        this.model.on("reset", this.render, this);
        this.model.on("add", function (courseUnit) {
            self.$el.append(new directory.CourseUnitListItemView({model:courseUnit}).render().el);
        });
    },

    render: function () {
        var courseUnits = [];
        this.$el.empty();
        _.each(this.model.models, function (courseUnit) {
            //this.$el.append(new directory.CourseUnitListItemView({model:courseUnit}).render().el);
            courseUnits.push(_.clone(courseUnit.attributes));
        }, this);
        
        var dataSource = new kendo.data.DataSource({
            data: courseUnits,
            pageSize: 9
        });
        
        $("#pager").kendoPager({
            dataSource: dataSource
        });
        
        $("#searchResults").kendoListView({
            dataSource: dataSource,
            selectable: "multiple",
            template: kendo.template(this.template({}))
        });

        return this;
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