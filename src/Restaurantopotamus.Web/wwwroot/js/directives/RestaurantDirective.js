angular.module('app')
    .controller('ResDirController', ['$scope', function ($scope) {
        res: '=';
    }])
    .directive('restaurant', restaurantDirective);

function restaurantDirective() {
    return {
        restrict: 'E',
        template: '<div class="restaurant">' +
                    '<h3>{{ res.name }}</h3>' +
                    '<h4>Featured Cuisine: {{ res.cuisineType }}</h4>' +
                    '<rating restaurantId="{{ res.id }}"></rating>' +
                   '</div>'
    };
}