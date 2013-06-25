directory.CourseView = Backbone.View.extend({

    initialize: function () {
        this.searchResults = new directory.CourseUnitCollection();
        this.filteredSearchResults = new directory.CourseUnitCollection();
        this.searchresultsView = new directory.CourseUnitListView({
            model: this.filteredSearchResults, className: 'search-results'
        });
        this.toPage = 1;
        this.pageSize = 9;
    },
    
    render:function () {
        this.$el.html(this.template());
        $('#searchResults', this.el).append(this.searchresultsView.render().el);
        return this;
    },
    
    events: {
        "click #searchUnit": "search",
        "keypress #searchTerm": "onkeypress",
        "click .k-link": "changePage"
    },
    
    search: function () {
        var key = $('#searchTerm').val();
        this.searchResults.fetch({
            reset: true, data: { SearchTerm: key },
            success: _.bind(function (model, resp, options) {
                this.filteredSearchResults.reset(model.slice(0, 9));
            }, this)
        });
        setTimeout(function () {
            $('.loader').addClass('active');
        });
    },
    
    onkeypress: function (event) {
        if (event.keyCode === 13) { // enter key pressed
            event.preventDefault();
            this.search();
        }
    },
    
    changePage: function (event) {
        var pageTo = event.target.dataset.page;
        this.filteredSearchResults.reset(this.searchResults.slice(9 * (pageTo - 1), 9 * pageTo));
    }

});