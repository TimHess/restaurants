angular.module('app')
    .component('rating', {
        bindings: { restaurantid: '@', ratingSummary: '=' },
        controller: RatingController,
        template: '<div class="rating">' +
                    '<star-rating max="5" rating-value="' + $ctrl.ratingSummary.averageRating + '"></star-rating>' +
                    '<span>Out of ' + RatingController.$scope.ratingSummary + ' total ratings</span>' +
                   '</div>'
    });