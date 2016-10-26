angular.module('app')
    .controller('ResDirController', ['$scope', function ($scope) {
        res: '=';
    }])
    .directive('restaurant', restaurantDirective);

function restaurantDirective() {
    return {
        restrict: 'E',
        template: '<div class="restaurant">' +
                    '<h3>{{ res.Name }}</h3>' +
                    '<h4>{{ res.CuisineType }}</h4>' +
                    '<rating></rating>' +
                   '</div>'
    };
}