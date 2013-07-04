define(function(require) {

    "use strict";

    var $ = require('jquery'),
        _ = require('underscore'),
        Backbone = require('backbone'),
        CourseUnitListView = require('app/views/courseUnitList'),
        CourseLessonListView = require('app/views/courseLessonList'),
        PagingView = require('app/views/paging'),
        models = require('app/models/course'),
        tpl = require('text!tpl/Course.html'),
        template = _.template(tpl);

    return Backbone.View.extend({
        initialize: function() {
            this.pagingModel = new Backbone.Model({ pageSize: 10, currentPage: 0, totalCount: 0 });
            this.searchResults = new models.CourseUnitCollection();
            this.filteredSearchResults = new models.CourseUnitCollection();
            this.lessonsUnderCurrentUnit = new Backbone.Collection();

            this.searchresultsView = new CourseUnitListView({
                model: this.filteredSearchResults
            });
            this.pagingView = new PagingView({ model: this.pagingModel });
            this.lessonsView = new CourseLessonListView({
                model: this.lessonsUnderCurrentUnit
            });
        },

        render: function() {
            this.$el.html(template());
            $('.result-section', this.el).append(this.searchresultsView.render().el);
            $('.result-section', this.el).append(this.pagingView.render().el);
            return this;
        },

        events: {
            "click #searchUnit": "search",
            "keypress #searchTerm": "onkeypress",
            "click .k-link": "changePage",
            "click .ets-ui-acc-btn-close": "closeActivityWindow",
            "click .ets-ui-lesson-img": "showActivityWindow",
            "click .courseUnit": "changeUnit"
        },

        search: function() {
            var key = $('#searchTerm').val();
            this.searchResults.fetch({
                reset: true,
                data: { SearchTerm: key },
                success: _.bind(function(model, resp, options) {
                    if (model.length === 0) $('.result-section').hide();
                    else {
                        $('.result-section').show();
                        this.gotoPage(model, 1);
                    }
                }, this)
            });
            setTimeout(function() {
                $('.loader').addClass('active');
            });
        },

        onkeypress: function(event) {
            if (event.keyCode === 13) { // enter key pressed
                event.preventDefault();
                this.search();
            }
        },

        changePage: function(event) {
            this.gotoPage(this.searchResults, event.target.dataset.page);
        },

        gotoPage: function(originalCollection, pageTo) {
            this.pagingModel.set({ totalCount: originalCollection.length, currentPage: pageTo });
            this.filteredSearchResults.reset(originalCollection.slice((
                this.pagingModel.get('currentPage') - 1) * this.pagingModel.get('pageSize'),
                this.pagingModel.get('currentPage') * this.pagingModel.get('pageSize')));
        },
        
        showActivityWindow: function () {
            $('.ets-ui-acc-bd').animate({ height: 540 });
            $('#activityWindow').show();
        },
        
        closeActivityWindow: function () {
            $('.ets-ui-acc-bd').animate({ height: 0 });
            $('#activityWindow').fadeOut();
        },
        
        changeUnit: function (event) {
            var unitId = $(event.currentTarget).data('id');
            this.lessonsUnderCurrentUnit.reset(
                new Backbone.Collection(this.searchResults.where({ Id: unitId }).attributes.Lessons));
        }
    });
});