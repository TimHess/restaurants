angular.module('app')
    .controller('RestaurantController', RestaurantController);

RestaurantController.$inject = ['$scope', '$http'];

function RestaurantController($scope, $http) {
    $scope.Restaurants = $http.get('/api/restaurant');
}