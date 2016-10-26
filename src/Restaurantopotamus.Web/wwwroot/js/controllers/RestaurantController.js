angular.module('app')
    .controller('RestaurantController', RestaurantController);

RestaurantController.$inject = ['$scope'];

function RestaurantController($scope) {
    $scope.Restaurants = [
        { "Id": "0c58e752-ee11-4ee9-b30a-0509a21d55f8", "Name": "Restaurantasaurus", "CuisineType": "bananas" },
        { "Id": "1c58e752-ee11-4ee9-b30a-0509a21d55f8", "Name": "RestaurantasaurusRex", "CuisineType": "choco-bananas" }
    ];
}