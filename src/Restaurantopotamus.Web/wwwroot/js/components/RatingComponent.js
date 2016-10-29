angular.module('app.Hippo')
    .component('rating', {
        bindings: { average: '@', total: '@' },
        controller: RatingController,
        template: '<div class="rating">' +
                    '<star-rating max="5" rating-value="$ctrl.average" on-rating-selected="$ctrl.rate()"></star-rating>' +
                    '<span>Out of {{ $ctrl.total }} total ratings</span>' +
                   '</div>'
    });