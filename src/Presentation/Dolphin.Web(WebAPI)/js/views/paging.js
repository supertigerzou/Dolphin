directory.PagingView = Backbone.View.extend({

    tagName:'div',

    className: 'k-pager-wrap k-widget',

    initialize:function (options) {
        var self = this;
        this.model.on("change", this.render, this);
    },

    render: function () {
        this.$el.empty();
        this.$el.append(this.template(this.model.attributes));

        return this;
    }
});