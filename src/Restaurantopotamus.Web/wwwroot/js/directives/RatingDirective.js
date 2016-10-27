angular.module('app')
    .controller('RateDirController', ['$scope', function ($scope) {
    }])
    .directive('rating', ratingDirective);

function ratingDirective() {
    return {
        restrict: 'E',
        template: '<div class="rating">' +
                    '<star-rating max="5" rating-value="2"></star-rating>' +
                   '</div>'
    };
}