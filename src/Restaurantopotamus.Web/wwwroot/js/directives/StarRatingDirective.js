// http://angulartutorial.blogspot.com/2014/03/rating-stars-in-angular-js-using.html
(function () {
    'use strict';

    angular
      .module('app')
      .controller('StarController', StarController)
      .directive('starRating', starRating);

    function StarController() {
        this.rateFunction = function (rating) {
            console.log('Rating selected: ' + rating);
        };
    }

    function starRating() {
        return {
            restrict: 'E',
            template:
              '<ul class="star-rating">' +
              '  <li ng-repeat="star in stars" class="star" ng-class="{filled: star.filled}" ng-click="toggle($index)">' +
              '    <i class="fa fa-star"></i>' +
              '  </li>' +
              '</ul>',
            scope: {
                ratingValue: '=?',
                max: '=?',
                onRatingSelected: '&'
            },
            link: function (scope, elem, attrs) {
                var updateStars = function () {
                    scope.stars = [];
                    for (var i = 0; i < scope.max; i++) {
                        scope.stars.push({
                            filled: i < scope.ratingValue
                        });
                    }
                };

                scope.toggle = function (index) {
                    scope.ratingValue = index + 1;
                    scope.onRatingSelected({
                        rating: index + 1
                    });
                };

                scope.$watch('ratingValue', function (oldVal, newVal) { if (newVal) { updateStars(); } });
            }
        };
    }
})();