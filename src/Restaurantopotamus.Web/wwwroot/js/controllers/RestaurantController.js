angular.module('app')
    .controller('RestaurantController', RestaurantController);

RestaurantController.$inject = ['$scope', '$http'];

function RestaurantController($scope, $http) {
    $http.get('/api/restaurant').then(function successCallback(response) {
        $scope.Restaurants = response.data;
    }, function errorCallback(response) {
        debugger;
    });
}