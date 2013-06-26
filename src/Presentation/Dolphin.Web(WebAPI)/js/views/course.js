directory.CourseView = Backbone.View.extend({

    initialize: function () {
        this.pagingModel = new Backbone.Model({ pageSize: 8, currentPage: 0, totalCount: 0 });
        this.searchResults = new directory.CourseUnitCollection();
        this.filteredSearchResults = new directory.CourseUnitCollection();
        this.searchresultsView = new directory.CourseUnitListView({
            model: this.filteredSearchResults, className: 'search-results'
        });
        this.pagingView = new directory.PagingView({ model: this.pagingModel });
    },
    
    render:function () {
        this.$el.html(this.template());
        $('#searchResults', this.el).append(this.searchresultsView.render().el);
        $('.result-section', this.el).append(this.pagingView.render().el);
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
                if (model.length === 0) $('.result-section').hide();
                else {
                    $('.result-section').show();
                    this.gotoPage(model, 1);
                }
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
        this.gotoPage(this.searchResults, event.target.dataset.page);
    },

    gotoPage: function (originalCollection, pageTo) {
        this.pagingModel.set({totalCount: originalCollection.length, currentPage: pageTo});
        this.filteredSearchResults.reset(originalCollection.slice((
            this.pagingModel.get('currentPage') - 1) * this.pagingModel.get('pageSize'),
            this.pagingModel.get('currentPage') * this.pagingModel.get('pageSize')));
    }
});