angular.module('app.Hippo')
    .component('rating', {
        bindings: { average: '@', total: '@', restaurantId: '@', rate: '&' },
        controller: RatingController,
        template: '<div class="rating">' +
                    '<star-rating max="5" rating-value="$ctrl.average" on-rating-selected="$ctrl.rate(rating)"></star-rating>' +
                    '<span>Out of {{ $ctrl.total }} total rating(s)</span>' +
                   '</div>'
    });