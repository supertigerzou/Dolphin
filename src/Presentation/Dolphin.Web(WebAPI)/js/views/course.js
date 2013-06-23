directory.CourseView = Backbone.View.extend({

    initialize: function () {
        this.searchResults = new directory.CourseUnitCollection();
        this.searchresultsView = new directory.CourseUnitListView({ model: this.searchResults, className: 'search-results' });
    },
    
    render:function () {
        this.$el.html(this.template());
        $('#searchResults', this.el).append(this.searchresultsView.render().el);
        return this;
    },
    
    events: {
        "click #searchUnit": "search",
        "keypress #searchTerm": "onkeypress"
    },
    
    search: function () {
        var key = $('#searchTerm').val();
        this.searchResults.fetch({ reset: true, data: { SearchTerm: key } });
        setTimeout(function () {
            $('.loader').addClass('active');
        });
    },
    
    onkeypress: function (event) {
        if (event.keyCode === 13) { // enter key pressed
            event.preventDefault();
            this.search(event);
        }
    }

});